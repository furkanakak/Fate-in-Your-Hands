# Saray Mutfağından Mühre - Integration Validation Report

Overall status: **FAIL**

## Checks

- all_card_ids_unique: **PASS**
- all_choice_ids_unique: **PASS**
- all_ending_ids_unique: **PASS**
- each_card_has_exactly_two_choices: **PASS**
- all_next_card_ids_exist: **PASS**
- all_choice_ending_ids_exist: **PASS**
- all_choices_have_next_or_ending: **PASS**
- choice_texts_are_short_complete_actions: **PASS**
- result_texts_are_short_complete_events: **PASS**
- success_and_failure_results_are_distinct_per_card: **PASS**
- text_normalizer_self_tests_pass: **PASS**
- all_background_ids_have_real_images: **PASS**
- all_choice_image_ids_have_real_images: **PASS**
- all_ending_image_ids_have_real_images: **FAIL**
- cover_image_exists: **FAIL**
- no_unreachable_cards: **PASS**
- no_unreachable_endings: **PASS**
- no_duplicate_target_paths: **PASS**
- all_existing_images_have_expected_aspect_ratio: **PASS**
- all_smoke_routes_reach_target: **PASS**

## Counts

- cards: 300
- choices: 600
- endings: 28
- backgroundAssetsReferenced: 48
- choiceImageAssetsReferenced: 600
- endingImageAssetsReferenced: 28
- manifestAssets: 979
- manifestAssetsPresent: 650
- manifestAssetsMissing: 329

## Blocking Failures

- all_ending_image_ids_have_real_images
- cover_image_exists

## Missing Assets By Type

- cover: 1
- ending: 28
- focus: 300

## Missing Required Runtime Assets

- Backgrounds: 0
- Choice images: 0
- Ending images: 28
- Cover images: 1

## Text Quality

- Choice text failures: 0
- Result text failures: 0
- Identical pair results: 0
- Normalizer self tests: PASS

## Smoke Routes

- mutfak_asci_rotasi: PASS -> ending_en_guvenilen_saray_ascisi
- zehir_tadicisi_rotasi: PASS -> ending_zehir_tadicisi_reformcu
- casus_rotasi: PASS -> ending_casus_orgutu_basi
- halk_isyani_rotasi: PASS -> ending_halk_devriminin_lideri
- safira_varis_rotasi: PASS -> ending_safiranin_muhur_naziri
- arslan_varis_rotasi: PASS -> ending_arslanin_golgesi
- kemal_varis_rotasi: PASS -> ending_kemalin_ekmek_kanunu
- corba_kehaneti_absurt_rotasi: PASS -> ending_kehanet_corbasinin_velisi
- saray_kazi_absurt_rotasi: PASS -> ending_saray_kazinin_yorumcusu
- yanlis_tatli_diplomasi_rotasi: PASS -> ending_tatli_diplomatik_evlilik
