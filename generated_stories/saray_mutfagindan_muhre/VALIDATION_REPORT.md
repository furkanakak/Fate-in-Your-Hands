# VALIDATION REPORT

Story: Saray Mutfağından Mühre
Story ID: `saray_mutfagindan_muhre`

## Summary

- Cards: 300
- Choices: 600
- Endings: 28
- Microcopy sync status: PASS

## Narrative Microcopy Checks

| Kontrol | Durum | Detay |
|---|---|---|
| Her kartta shortBodyText var mı? | PASS | 300/300 |
| Her shortBodyText 10 kelime veya daha kısa mı? | PASS | 0 ihlal |
| Her shortBodyText 8-10 kelime aralığında mı? | PASS | 0 kısa, 0 uzun |
| bodyText UI'da gösterilecekse 10 kelime veya daha kısa mı? | PASS | 0 ihlal |
| Eski uzun metin longBodyText altında korunmuş mu? | PASS | 300/300 |
| Her choice text 3-8 kelime arasında mı? | PASS | 0 ihlal |
| Her resultText doğal ve sistem dilinden arınmış mı? | PASS | 0 yasak ifade |
| Hidden counter veya flag isimleri oyuncu metninde görünüyor mu? | PASS | 0 sızıntı |
| Her dialogue en fazla 1 kısa replik mi? | PASS | 0 ihlal |
| Her choiceImagePrompt yeni choice text ile uyumlu mu? | PASS | choiceId tabanlı promptlar üretildi |
| Güncellenen choiceImagePrompt ASSET_MANIFEST.yaml ile senkron mu? | PASS | 0 prompt uyumsuz |
| Choice negativePrompt alanları ASSET_MANIFEST.yaml ile senkron mu? | PASS | 0 negativePrompt uyumsuz |
| nextCardId ve endingId referansları bozulmadı mı? | PASS | 0 bozuk referans |
| cardId ve choiceId değerleri değişmeden kaldı mı? | PASS | öncesi/sonrası aynı sırada |
| assetId değerleri değişmeden kaldı mı? | PASS | öncesi/sonrası aynı sırada |

## Structural Checks

| Kontrol | Durum | Detay |
|---|---|---|
| Toplam kart sayısı 250+ mı? | PASS | 300 |
| Toplam seçim sayısı 500+ mı? | PASS | 600 |
| Toplam ending sayısı 24+ mı? | PASS | 28 |
| Her kartta tam 2 seçim var mı? | PASS | 300 kart tarandı |
| Her seçimde text var mı? | PASS | 600 seçim tarandı |
| Her seçimde resultText var mı? | PASS | 600 seçim tarandı |
| Her seçimde healthDelta var mı? | PASS | 600 seçim tarandı |
| Her seçimde setFlags ve clearFlags var mı? | PASS | 600 seçim tarandı |
| Her seçimde hiddenCounterDeltas var mı? | PASS | tüm sayaçlar mevcut |
| Her seçimde yalnızca nextCardId veya endingId var mı? | PASS | hedef alanları kontrol edildi |
| Bozuk nextCardId var mı? | PASS | yok |
| Bozuk endingId var mı? | PASS | yok |
| Placeholder, TODO, later, devam edecek var mı? | PASS | yok |

## Final Status

PASS
