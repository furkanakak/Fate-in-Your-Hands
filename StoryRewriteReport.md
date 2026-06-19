# Story Rewrite Report

Tarih: 2026-06-19

## Degisen dosyalar

- `Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/story_data.json`
- `Fate-in-Your-Hands/Assets/Resources/Stories/saray_mutfagindan_muhre/story_data.json`
- `Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/smoke_test_routes.json`
- `StoryRewriteReport.md`

## Yeniden yazilan kapsam

- 300 kartin tamami narrative rewrite pass kapsaminda yeniden duzenlendi.
- 28 ending metninin tamami yeniden yazildi.
- Her kartta tam 2 aktif secim korundu.
- Secim metinleri kisa, aksiyon odakli ve runtime limitlerine uygun tutuldu.
- Result textler 1-2 kisa cumlelik sahne sonucu olarak duzenlendi.
- `Bu yolu sectin`, `tercih ettin`, `devam etmek icin` gibi yapay/meta patternler icin kontrol yapildi.

## Baglanti ve erisilebilirlik kontrolleri

- Eksik `nextCardId`: 0
- Eksik `endingId`: 0
- Ulasilabilir kart: 300 / 300
- Unreachable kart: 0
- Referanslanan ending: 28 / 28
- Smoke route: 28 / 28 basarili
- `Assets/Stories` ve `Assets/Resources` altindaki `story_data.json` kopyalari byte-byte ayni tutuldu.

## Branch ve flag mantigi

- Yeni gorunur stat eklenmedi.
- Onceki karar etkileri story flag ve branch node mantigiyla kuruldu.
- 85 story memory flag kullanildi.
- Ana route flagleriyle pazar/muhur, saray mutfagi, lonca, kehanet corbasi, zehir tadimi, sifahane/cadi, casus agi, ordu/turnuva, hirsizlar, ayaklanma, varisler ve final muhur hatlari ayrildi.
- Item/iliski/olay flagleriyle `royal seal`, Safira, Bayram Muhafiz, Ak Gaga, Kara Nene borcu, Yusuf borcu, Meryem ayaklanmasi, panzehir ve sahte muhur izleri final endinglerine tasindi.

## Can sistemi kontrolleri

- Baslangic cani: 3
- Maksimum can: 3
- Minimum can: 0
- `zeroHealthEndingId`: `ending_isimsiz_mezar`
- Hidden counter mekanigi health-only pass icin devre disi birakildi: `hiddenCounters` bosaltildi, choice `hiddenCounterDeltas` temizlendi, ending hidden counter kosullari kaldirildi.
- Health delta dagilimi:
  - 559 secim: 0
  - 29 secim: -1
  - 12 secim: +1
- Can bitmedigi surece ana akista ilerleme korunur; uzun A/continue rotasi final bolumlerine kadar gider.

## Iyilestirilen endingler

- Tum endingler tek cumlelik kazan/kaybet metinleri olmaktan cikarilip onceki route izlerini odeyen kisa final sahnelerine donusturuldu.
- Basarisizlik/olum endingleri daha sahneli hale getirildi.
- Komik endingler dunya icinden mizahla yazildi: Ak Gaga, kehanet corbasi, yanlis tatli ve sahte muhur hatlari.
- Siyasi endingler varis, halk, mutfak, casus agi ve muhur kararlarini daha belirgin odedi.

## Image alanlari teyidi

- Image prompt dosyalarina dokunulmadi.
- `backgroundId`, `focusImageId`, `choiceImageId`, `endingImageId` alanlari korunarak yazildi.
- `assets` array'i degistirilmedi.
- UI dosyalarina dokunulmadi.

## Son dogrulama

- Choice word limit: temiz
- Result word limit: temiz
- Meta pattern kontrolu: temiz
- Dead-end/missing link kontrolu: temiz
- Smoke route kontrolu: temiz
