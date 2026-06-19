# NARRATIVE MICROCOPY SYNC REPORT

Story: Saray Mutfağından Mühre
Story ID: `saray_mutfagindan_muhre`

## Summary

- İşlenen kart sayısı: 300
- shortBodyText eklenen kart sayısı: 300
- bodyText kısaltılan kart sayısı: 300
- longBodyText olarak korunan metin sayısı: 300
- Güncellenen choice text sayısı: 0
- Güncellenen resultText sayısı: 600
- Güncellenen dialogue sayısı: 84
- Güncellenen choiceImagePrompt sayısı: 600
- ASSET_MANIFEST.yaml içinde senkronize edilen prompt sayısı: 600
- ASSET_MANIFEST.yaml içinde senkronize edilen negativePrompt sayısı: 600

## 10 Kelimeyi Aşan shortBodyText Listesi

- Yok.

## İnsan Gözüyle İncelenmesi Önerilen Kartlar

- Final/ending tonu için önerilen manuel dramaturji kontrolü: `smm_card_273`, `smm_card_274`, `smm_card_275`, `smm_card_276-smm_card_300`
- Bu öneri bloklayıcı değildir; validation kontrolleri PASS durumundadır.

## Dokunulmayan Routing/State Alanları

- `cardId`, `choiceId`, `storyId`, `chapterId`, `routeFamily`, `healthDelta`, `setFlags`, `clearFlags`, `hiddenCounterDeltas`, `nextCardId`, `endingId` değerleri değiştirilmedi.
- Choice text uygun aralıkta kaldığı için `CHOICE_REVIEW_TABLE.md` güncellemesi gerekmedi.
- Flow hedefleri değişmediği için `STORY_FLOW.md` güncellemesi gerekmedi.

## Validation

- PASS: Her kartta shortBodyText var mı? (300/300)
- PASS: Her shortBodyText 10 kelime veya daha kısa mı? (0 ihlal)
- PASS: Her shortBodyText 8-10 kelime aralığında mı? (0 kısa, 0 uzun)
- PASS: bodyText UI'da gösterilecekse 10 kelime veya daha kısa mı? (0 ihlal)
- PASS: Eski uzun metin longBodyText altında korunmuş mu? (300/300)
- PASS: Her choice text 3-8 kelime arasında mı? (0 ihlal)
- PASS: Her resultText doğal ve sistem dilinden arınmış mı? (0 yasak ifade)
- PASS: Hidden counter veya flag isimleri oyuncu metninde görünüyor mu? (0 sızıntı)
- PASS: Her dialogue en fazla 1 kısa replik mi? (0 ihlal)
- PASS: Her choiceImagePrompt yeni choice text ile uyumlu mu? (choiceId tabanlı promptlar üretildi)
- PASS: Güncellenen choiceImagePrompt ASSET_MANIFEST.yaml ile senkron mu? (0 prompt uyumsuz)
- PASS: Choice negativePrompt alanları ASSET_MANIFEST.yaml ile senkron mu? (0 negativePrompt uyumsuz)
- PASS: nextCardId ve endingId referansları bozulmadı mı? (0 bozuk referans)
- PASS: cardId ve choiceId değerleri değişmeden kaldı mı? (öncesi/sonrası aynı sırada)
- PASS: assetId değerleri değişmeden kaldı mı? (öncesi/sonrası aynı sırada)

## Final Status

PASS
