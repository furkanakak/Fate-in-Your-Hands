# VALIDATION REPORT

Story: Saray Mutfağından Mühre
Story ID: `saray_mutfagindan_muhre`

## Summary

- Cards: 300
- Choices: 600
- Endings: 28
- Çorba zinciri kartları: 44
- Saray kazı zinciri kartları: 25
- Yanlış tatlı zinciri kartları: 25

## Checks

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
| Ulaşılamayan kart var mı? | PASS | 0 |
| Ulaşılamayan ending var mı? | PASS | 0 |
| Her kartta backgroundId var mı? | PASS | tamam |
| Her kartta focusImageId var mı? | PASS | tamam |
| Her focusImageId asset manifestte var mı? | PASS | tamam |
| Her backgroundId background library'de var mı? | PASS | tamam |
| Her endingImageId asset manifestte var mı? | PASS | tamam |
| NPC_BIBLE firstAppearanceCardId değerleri gerçek kartlara gidiyor mu? | PASS | tamam |
| Prop usedByCardIds değerleri gerçek kartlara gidiyor mu? | PASS | tamam |
| Çorba kehaneti zinciri en az 20 kartta görünüyor mu? | PASS | 44 |
| Saray kazı zinciri en az 20 kartta görünüyor mu? | PASS | 25 |
| Yanlış tatlı diplomasi zinciri en az 20 kartta görünüyor mu? | PASS | 25 |
| Absürt olaylar finalde payoff alıyor mu? | PASS | çorba, kaz ve tatlı endingleri mevcut |
| Varis krizi tüm ana rotalara bağlanıyor mu? | PASS | 12 route family |
| Placeholder, TODO, later, devam edecek var mı? | PASS | yok |

## Final Status

PASS
