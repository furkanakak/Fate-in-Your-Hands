from __future__ import annotations

import hashlib
import json
import re
import struct
from collections import Counter, deque
from pathlib import Path
from typing import Any

import yaml


REPO_ROOT = Path(__file__).resolve().parents[3]
STORY_SOURCE = REPO_ROOT / "generated_stories" / "saray_mutfagindan_muhre"
UNITY_ROOT = REPO_ROOT / "Fate-in-Your-Hands"
UNITY_STORY_DIR = UNITY_ROOT / "Assets" / "Stories" / "saray_mutfagindan_muhre"
UNITY_RESOURCES_DIR = UNITY_ROOT / "Assets" / "Resources" / "Stories" / "saray_mutfagindan_muhre"
STORY_ID = "saray_mutfagindan_muhre"
INITIAL_CARD_ID = "smm_card_001"
ZERO_HEALTH_ENDING_ID = "ending_isimsiz_mezar"
MAX_CHOICE_WORDS = 8
MAX_RESULT_WORDS = 18
CHOICE_FALLBACK = "Sakin kalıp ilerle."
FORBIDDEN_META_PATTERNS = (
    "şu yolu seçersin",
    "bu yolu seçersin",
    "yolunu seçersin",
    "bu seçimi yaparsan",
    "seçimin sonucunda",
    "bu hamle",
    "karar, hikâyeyi",
)
SENTENCE_ENDINGS = (".", "!", "?")

SMOKE_ROUTE_TARGETS = [
    ("mutfak_asci_rotasi", "Mutfak/aşçı rotası", "ending_en_guvenilen_saray_ascisi"),
    ("zehir_tadicisi_rotasi", "Zehir tadıcısı rotası", "ending_zehir_tadicisi_reformcu"),
    ("casus_rotasi", "Casus rotası", "ending_casus_orgutu_basi"),
    ("halk_isyani_rotasi", "Halk isyanı rotası", "ending_halk_devriminin_lideri"),
    ("safira_varis_rotasi", "Safira varis rotası", "ending_safiranin_muhur_naziri"),
    ("arslan_varis_rotasi", "Arslan varis rotası", "ending_arslanin_golgesi"),
    ("kemal_varis_rotasi", "Kemal varis rotası", "ending_kemalin_ekmek_kanunu"),
    ("corba_kehaneti_absurt_rotasi", "Çorba kehaneti absürt rotası", "ending_kehanet_corbasinin_velisi"),
    ("saray_kazi_absurt_rotasi", "Saray kazı absürt rotası", "ending_saray_kazinin_yorumcusu"),
    ("yanlis_tatli_diplomasi_rotasi", "Yanlış tatlı diplomasi rotası", "ending_tatli_diplomatik_evlilik"),
]


def read_yaml(name: str) -> Any:
    return yaml.safe_load((STORY_SOURCE / name).read_text(encoding="utf-8"))


def read_png_size(path: Path) -> tuple[int, int] | None:
    if not path.exists() or path.suffix.lower() != ".png":
        return None
    with path.open("rb") as handle:
        header = handle.read(24)
    if len(header) < 24 or header[:8] != b"\x89PNG\r\n\x1a\n" or header[12:16] != b"IHDR":
        return None
    return struct.unpack(">II", header[16:24])


def unity_path_exists(target_path: str) -> bool:
    return (UNITY_ROOT / target_path).exists()


def convert_counter_map(counter_map: dict[str, int] | None) -> list[dict[str, Any]]:
    return [
        {"counterId": key, "delta": value}
        for key, value in sorted((counter_map or {}).items())
    ]


def convert_counter_conditions(items: list[dict[str, Any]] | None) -> list[dict[str, Any]]:
    converted = []
    for item in items or []:
        converted.append(
            {
                "counterId": item.get("counterId", ""),
                "min": item.get("min", -2147483648),
                "max": item.get("max", 2147483647),
            }
        )
    return converted


def normalize_whitespace(value: str | None) -> str:
    return re.sub(r"\s+", " ", (value or "").strip())


def word_count(value: str | None) -> int:
    text = normalize_whitespace(value)
    return len(text.split()) if text else 0


def sentence_count(value: str | None) -> int:
    return sum(1 for character in normalize_whitespace(value) if character in ".!?")


def has_sentence_end(value: str | None) -> bool:
    text = normalize_whitespace(value)
    return bool(text) and text.endswith(SENTENCE_ENDINGS)


def ensure_sentence_end(value: str) -> str:
    text = normalize_whitespace(value)
    return text if has_sentence_end(text) else f"{text}."


def has_forbidden_meta(value: str | None) -> bool:
    text = normalize_whitespace(value).casefold()
    return any(pattern.casefold() in text for pattern in FORBIDDEN_META_PATTERNS)


def has_manual_ellipsis(value: str | None) -> bool:
    text = normalize_whitespace(value)
    return "..." in text or "…" in text


def normalize_for_comparison(value: str | None) -> str:
    return re.sub(r"[^\wğüşöçıİĞÜŞÖÇ]+", " ", normalize_whitespace(value).casefold()).strip()


def repeats_choice(result_text: str | None, choice_text: str | None) -> bool:
    result = normalize_for_comparison(result_text)
    choice = normalize_for_comparison(choice_text)
    return len(choice) > 10 and choice in result


def fallback_result_text(health_delta: int) -> str:
    if health_delta > 0:
        return "Mutfaktaki bakışlar yumuşadı ve önün açıldı."
    if health_delta < 0:
        return "Sarayın bakışları keskinleşti ve adın deftere düştü."
    return "Koridordaki hava değişti ve yeni bir yol açıldı."


def alternate_fallback_result_text(health_delta: int) -> str:
    if health_delta > 0:
        return "Sessiz destek büyüdü ve kapılar sana aralandı."
    if health_delta < 0:
        return "Koridordaki fısıltı sertleşti ve izlerin görünür oldu."
    return "Odada sessizlik çözüldü ve yolun değişti."


COUNTER_RESULT_TEXTS = {
    "palace_suspicion": (
        "Koridordaki kuşku büyüdü ve adın daha sık anıldı.",
        "Sarayın kuşkusu yatıştı ve bakışlar senden uzaklaştı.",
    ),
    "black_market_debt": (
        "Pazar borcu büyüdü ve gölgeler peşine düştü.",
        "Pazar borcu hafifledi ve gölgeler geride kaldı.",
    ),
    "kitchen_favor": ("Mutfaktaki güven büyüdü ve kapılar sana aralandı.", None),
    "absurd_soup_omen_score": ("Çorbadaki işaret yayıldı; kaşıklar havada kaldı.", None),
    "absurd_goose_omen_score": ("Ak Gaga bağırdı; avludaki söylenti büyüdü.", None),
    "absurd_dessert_diplomacy_score": ("Tatlı tabağı el değiştirdi; salondaki denge bozuldu.", None),
    "poison_knowledge": ("Zehir kokusu netleşti ve tehlikeyi daha iyi okudun.", None),
    "healer_trust": ("Şifahanedeki güven güçlendi ve kapılar aralandı.", None),
    "spy_network": ("Fısıltı ağı genişledi ve haberler sana aktı.", None),
    "logistics_mastery": ("Defterler düzene girdi ve yolun hesabı güçlendi.", None),
    "rebellion_sympathy": ("Kalabalık adını yumuşak andı ve kapı açıldı.", None),
    "seal_access": ("Mühür odasına giden iz biraz daha açıldı.", None),
    "heir_arslan_support": ("Arslan'ın bakışı yumuşadı ve askerler geri çekildi.", None),
    "heir_kemal_support": ("Kemal notunu sakladı ve halk kapısı aralandı.", None),
    "heir_safira_support": ("Safira sessizce gülümsedi ve elçiler yol verdi.", None),
}
RISK_COUNTERS = {"palace_suspicion", "black_market_debt"}


def create_distinct_result_text(choice: dict[str, Any]) -> str:
    if choice.get("healthDelta", 0) < 0:
        return fallback_result_text(choice.get("healthDelta", 0))

    deltas = [item for item in choice.get("hiddenCounterDeltas", []) if item.get("delta", 0) != 0]
    deltas.sort(
        key=lambda item: (
            100 if item.get("counterId", "") in RISK_COUNTERS else 0,
            abs(item.get("delta", 0)),
        ),
        reverse=True,
    )
    for delta in deltas:
        variants = COUNTER_RESULT_TEXTS.get(delta.get("counterId", ""))
        if not variants:
            continue
        text = variants[0] if delta.get("delta", 0) > 0 else variants[1]
        if text:
            return text

    return fallback_result_text(choice.get("healthDelta", 0))


def ensure_distinct_choice_results(choices: list[dict[str, Any]]) -> None:
    if len(choices) != 2 or choices[0]["resultText"] != choices[1]["resultText"]:
        return

    choices[1]["resultText"] = normalize_result_text(
        create_distinct_result_text(choices[1]),
        choices[1]["text"],
        choices[1]["healthDelta"],
    )
    if choices[0]["resultText"] == choices[1]["resultText"]:
        choices[1]["resultText"] = alternate_fallback_result_text(choices[1]["healthDelta"])


def normalize_choice_text(raw_text: str | None) -> str:
    text = normalize_whitespace(raw_text)
    if (
        not text
        or has_forbidden_meta(text)
        or has_manual_ellipsis(text)
        or word_count(text) > MAX_CHOICE_WORDS
    ):
        return CHOICE_FALLBACK
    return ensure_sentence_end(text)


def normalize_result_text(raw_text: str | None, choice_text: str | None, health_delta: int) -> str:
    text = normalize_whitespace(raw_text)
    if (
        not text
        or has_forbidden_meta(text)
        or has_manual_ellipsis(text)
        or word_count(text) > MAX_RESULT_WORDS
        or sentence_count(text) > 2
        or repeats_choice(text, choice_text)
    ):
        return fallback_result_text(health_delta)
    return ensure_sentence_end(text)


def run_text_normalizer_self_tests() -> dict[str, Any]:
    samples = [
        normalize_choice_text("Kapıyı sessizce aç") == "Kapıyı sessizce aç.",
        normalize_choice_text("Adama tekme...") == CHOICE_FALLBACK,
        normalize_result_text("", "Kapıyı sessizce aç.", 0) == "Koridordaki hava değişti ve yeni bir yol açıldı.",
        not has_forbidden_meta(normalize_result_text("Şu yolu seçersin: kapıyı açmak.", "Kapıyı sessizce aç.", -1)),
        normalize_result_text(
            "Kapıyı sessizce aç. Sonra bu kararın bütün ihtimallerini uzun uzun düşünürsün. Herkes seni dakikalarca izler.",
            "Kapıyı sessizce aç.",
            1,
        )
        == "Mutfaktaki bakışlar yumuşadı ve önün açıldı.",
        normalize_result_text("Kapıyı fark edilmeden açtın ve karanlık koridora girdin", "Kapıyı sessizce aç.", 0)
        == "Kapıyı fark edilmeden açtın ve karanlık koridora girdin.",
    ]
    return {
        "overallStatus": "PASS" if all(samples) else "FAIL",
        "checks": [
            {"check": f"text_normalizer_self_test_{index + 1}", "status": "PASS" if result else "FAIL"}
            for index, result in enumerate(samples)
        ],
    }


def convert_cards(cards: list[dict[str, Any]], source_by_card: dict[str, str]) -> list[dict[str, Any]]:
    converted = []
    for card in cards:
        converted_choices = []
        for index, choice in enumerate(card.get("choices", [])):
            choice_text = normalize_choice_text(choice.get("text", ""))
            result_text = normalize_result_text(choice.get("resultText", ""), choice_text, choice.get("healthDelta", 0))
            converted_choices.append(
                {
                    "choiceId": choice["choiceId"],
                    "slot": "A" if index == 0 else "B",
                    "choiceImageId": choice.get("choiceImageId", ""),
                    "text": choice_text,
                    "resultText": result_text,
                    "healthDelta": choice.get("healthDelta", 0),
                    "hiddenCounterDeltas": convert_counter_map(choice.get("hiddenCounterDeltas")),
                    "setFlags": choice.get("setFlags", []) or [],
                    "clearFlags": choice.get("clearFlags", []) or [],
                    "nextCardId": choice.get("nextCardId", "") or "",
                    "endingId": choice.get("endingId", "") or "",
                }
            )

        ensure_distinct_choice_results(converted_choices)
        converted.append(
            {
                "cardId": card["cardId"],
                "storyId": card.get("storyId", STORY_ID),
                "chapterId": card.get("chapterId", ""),
                "stage": card.get("stage", ""),
                "routeFamily": card.get("routeFamily", ""),
                "title": card.get("title", ""),
                "bodyText": card.get("bodyText", ""),
                "backgroundId": card.get("backgroundId", ""),
                "focusImageId": card.get("focusImageId", ""),
                "presentCharacters": card.get("presentCharacters", []) or [],
                "dialogue": [
                    {
                        "speakerId": item.get("speakerId", ""),
                        "emotion": item.get("emotion", ""),
                        "text": item.get("text", ""),
                    }
                    for item in card.get("dialogue", []) or []
                ],
                "choices": converted_choices,
                "sourceFile": source_by_card.get(card["cardId"], ""),
            }
        )
    return converted


def convert_endings(endings: list[dict[str, Any]]) -> list[dict[str, Any]]:
    return [
        {
            "endingId": ending["endingId"],
            "title": ending.get("title", ""),
            "endingType": ending.get("endingType", ""),
            "fullEndingText": ending.get("fullEndingText", ""),
            "endingImageId": ending.get("endingImageId", ""),
            "requiredFlags": ending.get("requiredFlags", []) or [],
            "forbiddenFlags": ending.get("forbiddenFlags", []) or [],
            "requiredHiddenCounters": convert_counter_conditions(ending.get("requiredHiddenCounters")),
            "forbiddenHiddenCounters": convert_counter_conditions(ending.get("forbiddenHiddenCounters")),
            "typicalRoute": ending.get("typicalRoute", ""),
            "importantPastChoices": ending.get("importantPastChoices", []) or [],
            "whyThisEndingHappens": ending.get("whyThisEndingHappens", ""),
            "payoffExplanation": ending.get("payoffExplanation", ""),
        }
        for ending in endings
    ]


def convert_assets(manifest_assets: list[dict[str, Any]]) -> list[dict[str, Any]]:
    converted = []
    for asset in manifest_assets:
        target_path = asset.get("targetPath", "")
        file_path = UNITY_ROOT / target_path
        size = read_png_size(file_path)
        converted.append(
            {
                "assetId": asset["assetId"],
                "storyId": asset.get("storyId", STORY_ID),
                "type": asset.get("type", ""),
                "targetPath": target_path,
                "format": asset.get("format", ""),
                "declaredSize": asset.get("size", ""),
                "width": size[0] if size else 0,
                "height": size[1] if size else 0,
                "exists": file_path.exists(),
            }
        )
    return converted


def build_smoke_routes(cards: list[dict[str, Any]]) -> list[dict[str, Any]]:
    card_by_id = {card["cardId"]: card for card in cards}
    routes = []
    for route_id, label, target_ending_id in SMOKE_ROUTE_TARGETS:
        queue = deque([(INITIAL_CARD_ID, [])])
        seen = {INITIAL_CARD_ID}
        found_steps = None

        while queue:
            card_id, path = queue.popleft()
            card = card_by_id.get(card_id)
            if not card:
                continue
            for choice in card["choices"]:
                step = {
                    "cardId": card_id,
                    "choiceId": choice["choiceId"],
                    "slot": choice["slot"],
                    "choiceText": choice["text"],
                }
                if choice.get("endingId") == target_ending_id:
                    found_steps = path + [step]
                    queue.clear()
                    break
                next_card_id = choice.get("nextCardId", "")
                if next_card_id and next_card_id not in seen:
                    seen.add(next_card_id)
                    queue.append((next_card_id, path + [step]))

        routes.append(
            {
                "routeId": route_id,
                "label": label,
                "targetEndingId": target_ending_id,
                "startCardId": INITIAL_CARD_ID,
                "reachesTarget": found_steps is not None,
                "stepCount": len(found_steps or []),
                "steps": found_steps or [],
            }
        )
    return routes


def collect_sources() -> list[dict[str, Any]]:
    sources = []
    for path in sorted(list(STORY_SOURCE.glob("*.yaml")) + list(STORY_SOURCE.glob("*.md"))):
        raw = path.read_bytes()
        sources.append(
            {
                "fileName": path.name,
                "byteCount": len(raw),
                "sha256": hashlib.sha256(raw).hexdigest(),
            }
        )
    return sources


def validate(
    cards: list[dict[str, Any]],
    endings: list[dict[str, Any]],
    assets: list[dict[str, Any]],
    smoke_routes: list[dict[str, Any]],
) -> dict[str, Any]:
    card_ids = [card["cardId"] for card in cards]
    choice_ids = [choice["choiceId"] for card in cards for choice in card["choices"]]
    ending_ids = [ending["endingId"] for ending in endings]
    card_id_set = set(card_ids)
    ending_id_set = set(ending_ids)
    asset_by_id = {asset["assetId"]: asset for asset in assets}
    target_paths = [asset["targetPath"] for asset in assets if asset.get("targetPath")]

    background_ids = [card["backgroundId"] for card in cards]
    choice_image_ids = [choice["choiceImageId"] for card in cards for choice in card["choices"]]
    ending_image_ids = [ending["endingImageId"] for ending in endings]
    cover_asset = next((asset for asset in assets if asset["type"] == "cover"), None)

    bad_next = [
        {"cardId": card["cardId"], "choiceId": choice["choiceId"], "nextCardId": choice["nextCardId"]}
        for card in cards
        for choice in card["choices"]
        if choice.get("nextCardId") and choice["nextCardId"] not in card_id_set
    ]
    bad_ending = [
        {"cardId": card["cardId"], "choiceId": choice["choiceId"], "endingId": choice["endingId"]}
        for card in cards
        for choice in card["choices"]
        if choice.get("endingId") and choice["endingId"] not in ending_id_set
    ]
    no_terminal = [
        {"cardId": card["cardId"], "choiceId": choice["choiceId"]}
        for card in cards
        for choice in card["choices"]
        if not choice.get("nextCardId") and not choice.get("endingId")
    ]

    reachable_cards = {INITIAL_CARD_ID}
    reachable_endings = set()
    queue = deque([INITIAL_CARD_ID])
    card_by_id = {card["cardId"]: card for card in cards}
    while queue:
        card_id = queue.popleft()
        for choice in card_by_id[card_id]["choices"]:
            if choice.get("endingId"):
                reachable_endings.add(choice["endingId"])
            next_card_id = choice.get("nextCardId", "")
            if next_card_id and next_card_id not in reachable_cards:
                reachable_cards.add(next_card_id)
                queue.append(next_card_id)

    missing_backgrounds = [
        asset_id for asset_id in sorted(set(background_ids))
        if asset_id not in asset_by_id or not asset_by_id[asset_id]["exists"]
    ]
    missing_choice_images = [
        asset_id for asset_id in sorted(set(choice_image_ids))
        if asset_id not in asset_by_id or not asset_by_id[asset_id]["exists"]
    ]
    missing_ending_images = [
        asset_id for asset_id in sorted(set(ending_image_ids))
        if asset_id not in asset_by_id or not asset_by_id[asset_id]["exists"]
    ]
    missing_cover_images = []
    if not cover_asset or not cover_asset["exists"]:
        missing_cover_images.append(cover_asset["assetId"] if cover_asset else "cover asset missing from manifest")

    manifest_missing_assets = [
        {"type": asset["type"], "assetId": asset["assetId"], "targetPath": asset["targetPath"]}
        for asset in assets
        if not asset["exists"]
    ]

    wrong_aspect_assets = []
    for asset in assets:
        if not asset["exists"]:
            continue
        expected = {
            "background": (16, 9),
            "cover": (16, 9),
            "ending": (16, 9),
            "choice": (4, 5),
            "focus": (4, 5),
        }.get(asset["type"])
        if not expected:
            continue
        width, height = asset["width"], asset["height"]
        if width <= 0 or height <= 0 or width * expected[1] != height * expected[0]:
            wrong_aspect_assets.append(
                {
                    "type": asset["type"],
                    "assetId": asset["assetId"],
                    "targetPath": asset["targetPath"],
                    "width": width,
                    "height": height,
                    "expectedAspectRatio": f"{expected[0]}:{expected[1]}",
                }
            )

    duplicate_targets = [
        {"targetPath": target, "count": count}
        for target, count in Counter(target_paths).items()
        if count > 1
    ]
    text_normalizer = run_text_normalizer_self_tests()
    choice_text_failures = [
        {"cardId": card["cardId"], "choiceId": choice["choiceId"], "text": choice["text"]}
        for card in cards
        for choice in card["choices"]
        if (
            has_forbidden_meta(choice["text"])
            or has_manual_ellipsis(choice["text"])
            or word_count(choice["text"]) > MAX_CHOICE_WORDS
            or not has_sentence_end(choice["text"])
        )
    ]
    result_text_failures = [
        {"cardId": card["cardId"], "choiceId": choice["choiceId"], "resultText": choice["resultText"]}
        for card in cards
        for choice in card["choices"]
        if (
            has_forbidden_meta(choice["resultText"])
            or has_manual_ellipsis(choice["resultText"])
            or word_count(choice["resultText"]) > MAX_RESULT_WORDS
            or sentence_count(choice["resultText"]) > 2
            or not has_sentence_end(choice["resultText"])
            or repeats_choice(choice["resultText"], choice["text"])
        )
    ]
    identical_pair_results = [
        {"cardId": card["cardId"], "resultText": card["choices"][0]["resultText"]}
        for card in cards
        if len(card["choices"]) == 2 and card["choices"][0]["resultText"] == card["choices"][1]["resultText"]
    ]

    checks = [
        ("all_card_ids_unique", len(card_ids) == len(set(card_ids))),
        ("all_choice_ids_unique", len(choice_ids) == len(set(choice_ids))),
        ("all_ending_ids_unique", len(ending_ids) == len(set(ending_ids))),
        ("each_card_has_exactly_two_choices", all(len(card["choices"]) == 2 for card in cards)),
        ("all_next_card_ids_exist", not bad_next),
        ("all_choice_ending_ids_exist", not bad_ending),
        ("all_choices_have_next_or_ending", not no_terminal),
        ("choice_texts_are_short_complete_actions", not choice_text_failures),
        ("result_texts_are_short_complete_events", not result_text_failures),
        ("success_and_failure_results_are_distinct_per_card", not identical_pair_results),
        ("text_normalizer_self_tests_pass", text_normalizer["overallStatus"] == "PASS"),
        ("all_background_ids_have_real_images", not missing_backgrounds),
        ("all_choice_image_ids_have_real_images", not missing_choice_images),
        ("all_ending_image_ids_have_real_images", not missing_ending_images),
        ("cover_image_exists", not missing_cover_images),
        ("no_unreachable_cards", set(card_ids) == reachable_cards),
        ("no_unreachable_endings", set(ending_ids) == reachable_endings),
        ("no_duplicate_target_paths", not duplicate_targets),
        ("all_existing_images_have_expected_aspect_ratio", not wrong_aspect_assets),
        ("all_smoke_routes_reach_target", all(route["reachesTarget"] for route in smoke_routes)),
    ]

    return {
        "overallStatus": "PASS" if all(result for _, result in checks) else "FAIL",
        "checks": [{"check": name, "status": "PASS" if result else "FAIL"} for name, result in checks],
        "counts": {
            "cards": len(cards),
            "choices": len(choice_ids),
            "endings": len(endings),
            "backgroundAssetsReferenced": len(set(background_ids)),
            "choiceImageAssetsReferenced": len(set(choice_image_ids)),
            "endingImageAssetsReferenced": len(set(ending_image_ids)),
            "manifestAssets": len(assets),
            "manifestAssetsPresent": sum(1 for asset in assets if asset["exists"]),
            "manifestAssetsMissing": len(manifest_missing_assets),
        },
        "missingAssets": manifest_missing_assets,
        "missingBackgrounds": missing_backgrounds,
        "missingChoiceImages": missing_choice_images,
        "missingEndingImages": missing_ending_images,
        "missingCoverImages": missing_cover_images,
        "badNextCardRefs": bad_next,
        "badEndingRefs": bad_ending,
        "choicesWithoutNextOrEnding": no_terminal,
        "choiceTextFailures": choice_text_failures,
        "resultTextFailures": result_text_failures,
        "identicalPairResults": identical_pair_results,
        "textNormalizer": text_normalizer,
        "unreachableCards": sorted(set(card_ids) - reachable_cards),
        "unreachableEndings": sorted(set(ending_ids) - reachable_endings),
        "duplicateTargetPaths": duplicate_targets,
        "wrongAspectAssets": wrong_aspect_assets,
        "smokeRoutes": [
            {
                "routeId": route["routeId"],
                "targetEndingId": route["targetEndingId"],
                "reachesTarget": route["reachesTarget"],
                "stepCount": route["stepCount"],
            }
            for route in smoke_routes
        ],
    }


def write_json(path: Path, data: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


def write_reports(story_package: dict[str, Any], validation: dict[str, Any], sources: list[dict[str, Any]]) -> None:
    counts = validation["counts"]
    missing_counter = Counter(item["type"] for item in validation["missingAssets"])
    changed_files = [
        "Fate-in-Your-Hands/Assets/Resources/Stories/saray_mutfagindan_muhre/story_data.json",
        "Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/story_data.json",
        "Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/smoke_test_routes.json",
        "Fate-in-Your-Hands/Assets/Scripts/Story/*.cs",
        "generated_stories/saray_mutfagindan_muhre/INTEGRATION_REPORT.md",
        "generated_stories/saray_mutfagindan_muhre/INTEGRATION_VALIDATION_REPORT.md",
        "generated_stories/saray_mutfagindan_muhre/tools/build_unity_integration.py",
    ]

    report_lines = [
        "# Saray Mutfağından Mühre - Integration Report",
        "",
        "## Status",
        "",
        f"- Overall validation: **{validation['overallStatus']}**",
        "- Integration is not considered complete while validation is FAIL.",
        "",
        "## Added/Changed Files",
        "",
        *[f"- `{item}`" for item in changed_files],
        "",
        "## Imported Counts",
        "",
        f"- Cards: {counts['cards']}",
        f"- Choices: {counts['choices']}",
        f"- Endings: {counts['endings']}",
        f"- Referenced backgrounds: {counts['backgroundAssetsReferenced']}",
        f"- Referenced choice images: {counts['choiceImageAssetsReferenced']}",
        f"- Referenced ending images: {counts['endingImageAssetsReferenced']}",
        f"- Manifest assets present: {counts['manifestAssetsPresent']} / {counts['manifestAssets']}",
        f"- Missing assets: {counts['manifestAssetsMissing']}",
        "",
        "## Missing Asset Summary",
        "",
        *[f"- {asset_type}: {missing_counter[asset_type]}" for asset_type in sorted(missing_counter)],
        "",
        "## Missing Asset List",
        "",
    ]
    if validation["missingAssets"]:
        report_lines.extend(
            f"- `{item['assetId']}` ({item['type']}) -> `{item['targetPath']}`"
            for item in validation["missingAssets"]
        )
    else:
        report_lines.append("- None")

    report_lines.extend(
        [
            "",
            "## Broken Reference List",
            "",
            f"- Bad nextCardId refs: {len(validation['badNextCardRefs'])}",
            f"- Bad endingId refs: {len(validation['badEndingRefs'])}",
            f"- Missing background refs: {len(validation['missingBackgrounds'])}",
            f"- Missing choice image refs: {len(validation['missingChoiceImages'])}",
            f"- Missing ending image refs: {len(validation['missingEndingImages'])}",
            f"- Missing cover image refs: {len(validation['missingCoverImages'])}",
            f"- Duplicate target paths: {len(validation['duplicateTargetPaths'])}",
            f"- Wrong aspect assets: {len(validation['wrongAspectAssets'])}",
            f"- Choice text failures: {len(validation['choiceTextFailures'])}",
            f"- Result text failures: {len(validation['resultTextFailures'])}",
            f"- Identical pair results: {len(validation['identicalPairResults'])}",
            "",
            "## Tests Run",
            "",
            "- Parsed all YAML files with PyYAML.",
            "- Read all Markdown files and recorded SHA-256 hashes.",
            "- Validated card, choice, ending uniqueness.",
            "- Validated graph references and reachability.",
            "- Validated choice/result copy quality, forbidden meta phrases, punctuation, and fallback normalization.",
            "- Validated manifest targetPath existence.",
            "- Validated PNG aspect ratios from PNG IHDR headers.",
            "- Generated and checked 10 smoke test routes.",
            "",
            "## Smoke Test Result",
            "",
        ]
    )
    report_lines.extend(
        f"- {route['routeId']} -> {route['targetEndingId']}: "
        f"{'PASS' if route['reachesTarget'] else 'FAIL'} ({route['stepCount']} choices)"
        for route in validation["smokeRoutes"]
    )
    report_lines.extend(
        [
            "",
            "## Remaining Risks",
            "",
            "- Cover image is missing, so the story catalog/start cover falls back to a plain UI state.",
            "- Ending images are missing, so ending screens fall back to text-only rendering until assets are supplied.",
            "- Focus images are missing in the manifest; current runtime uses background + choice images only.",
            "- Unity Editor import was not run by this script, so .meta files may be generated by Unity on first open.",
            "",
            "## Source Files Read",
            "",
            *[f"- `{source['fileName']}` ({source['byteCount']} bytes, sha256 `{source['sha256'][:12]}`)" for source in sources],
            "",
        ]
    )
    (STORY_SOURCE / "INTEGRATION_REPORT.md").write_text("\n".join(report_lines), encoding="utf-8")

    validation_lines = [
        "# Saray Mutfağından Mühre - Integration Validation Report",
        "",
        f"Overall status: **{validation['overallStatus']}**",
        "",
        "## Checks",
        "",
        *[f"- {item['check']}: **{item['status']}**" for item in validation["checks"]],
        "",
        "## Counts",
        "",
        *[f"- {key}: {value}" for key, value in validation["counts"].items()],
        "",
        "## Blocking Failures",
        "",
    ]
    failures = [item for item in validation["checks"] if item["status"] != "PASS"]
    if failures:
        validation_lines.extend(f"- {item['check']}" for item in failures)
    else:
        validation_lines.append("- None")
    validation_lines.extend(
        [
            "",
            "## Missing Assets By Type",
            "",
        ]
    )
    if missing_counter:
        validation_lines.extend(f"- {asset_type}: {missing_counter[asset_type]}" for asset_type in sorted(missing_counter))
    else:
        validation_lines.append("- None")
    validation_lines.extend(
        [
            "",
            "## Missing Required Runtime Assets",
            "",
            f"- Backgrounds: {len(validation['missingBackgrounds'])}",
            f"- Choice images: {len(validation['missingChoiceImages'])}",
            f"- Ending images: {len(validation['missingEndingImages'])}",
            f"- Cover images: {len(validation['missingCoverImages'])}",
            "",
            "## Text Quality",
            "",
            f"- Choice text failures: {len(validation['choiceTextFailures'])}",
            f"- Result text failures: {len(validation['resultTextFailures'])}",
            f"- Identical pair results: {len(validation['identicalPairResults'])}",
            f"- Normalizer self tests: {validation['textNormalizer']['overallStatus']}",
            "",
            "## Smoke Routes",
            "",
        ]
    )
    validation_lines.extend(
        f"- {route['routeId']}: {'PASS' if route['reachesTarget'] else 'FAIL'} -> {route['targetEndingId']}"
        for route in validation["smokeRoutes"]
    )
    validation_lines.append("")
    (STORY_SOURCE / "INTEGRATION_VALIDATION_REPORT.md").write_text(
        "\n".join(validation_lines), encoding="utf-8"
    )


def main() -> None:
    card_files = sorted(STORY_SOURCE.glob("CARDS_PART_*.yaml"))
    cards_raw = []
    source_by_card: dict[str, str] = {}
    for path in card_files:
        part_cards = yaml.safe_load(path.read_text(encoding="utf-8"))["cards"]
        cards_raw.extend(part_cards)
        for card in part_cards:
            source_by_card[card["cardId"]] = path.name

    endings_raw = read_yaml("ENDINGS.yaml")["endings"]
    state_model = read_yaml("STATE_MODEL.yaml")
    manifest_assets = read_yaml("ASSET_MANIFEST.yaml")["assets"]
    part_status = read_yaml("PART_STATUS.yaml")

    assets = convert_assets(manifest_assets)
    cards = convert_cards(cards_raw, source_by_card)
    endings = convert_endings(endings_raw)
    smoke_routes = build_smoke_routes(cards)
    sources = collect_sources()

    hidden_counters = [
        {
            "counterId": item.get("counterId", counter_id),
            "displayName": item.get("counterId", counter_id),
            "description": item.get("description", ""),
            "min": item.get("min", 0),
            "max": item.get("max", 20),
            "defaultValue": item.get("default", 0),
        }
        for counter_id, item in state_model["hiddenCounters"].items()
    ]
    flags = [
        {
            "flagId": item.get("flagId", ""),
            "description": item.get("description", ""),
        }
        for item in state_model.get("flags", [])
    ]
    health = state_model["visibleStats"]["health"]
    cover_asset = next((asset for asset in assets if asset["type"] == "cover"), None)

    story_package = {
        "storyId": STORY_ID,
        "title": part_status.get("title", "Saray Mutfağından Mühre"),
        "language": "tr-TR",
        "initialCardId": INITIAL_CARD_ID,
        "zeroHealthEndingId": ZERO_HEALTH_ENDING_ID,
        "coverImageId": cover_asset["assetId"] if cover_asset else "",
        "health": {
            "statId": "health",
            "displayName": health.get("displayName", "Can"),
            "min": health.get("min", 0),
            "max": health.get("max", 10),
            "defaultValue": health.get("default", 5),
        },
        "hiddenCounters": hidden_counters,
        "flags": flags,
        "cards": cards,
        "endings": endings,
        "assets": assets,
        "smokeRoutes": smoke_routes,
        "sourceFiles": sources,
    }

    validation = validate(cards, endings, assets, smoke_routes)

    write_json(UNITY_RESOURCES_DIR / "story_data.json", story_package)
    write_json(UNITY_STORY_DIR / "story_data.json", story_package)
    write_json(UNITY_STORY_DIR / "smoke_test_routes.json", {"storyId": STORY_ID, "routes": smoke_routes})
    write_json(STORY_SOURCE / "INTEGRATION_VALIDATION.json", validation)
    write_reports(story_package, validation, sources)

    print(json.dumps({"overallStatus": validation["overallStatus"], **validation["counts"]}, ensure_ascii=False))


if __name__ == "__main__":
    main()
