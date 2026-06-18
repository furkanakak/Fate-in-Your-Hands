# Saray Mutfağından Mühre - Story Content Package

## Paket Özeti

Bu paket, `saray_mutfagindan_muhre` hikayesi için üretime hazır anlatı içeriğini içerir. Paket Unity kodu, prefab, scene veya görsel dosyası üretmez; kart verisi, seçim sonuçları, state bağlantıları, NPC bible, ending havuzu ve görsel üretim promptlarını teslim eder.

## Dosya Listesi

- `README_STORY_PACKAGE.md`
- `STORY_OVERVIEW.md`
- `STORY_BIBLE.md`
- `NPC_BIBLE.yaml`
- `STATE_MODEL.yaml`
- `VISUAL_STYLE_GUIDE.md`
- `BACKGROUND_LIBRARY.yaml`
- `PROP_CATALOG.yaml`
- `ASSET_MANIFEST.yaml`
- `ENDINGS.yaml`
- `CARDS_PART_01_INTRO.yaml`
- `CARDS_PART_02_KITCHEN_RISE.yaml`
- `CARDS_PART_03_PANTRY_AND_PASTRY.yaml`
- `CARDS_PART_04_SOUP_OMEN_AND_GOOSE.yaml`
- `CARDS_PART_05_POISON_TASTER.yaml`
- `CARDS_PART_06_HEALER_PATH.yaml`
- `CARDS_PART_07_SPY_NETWORK.yaml`
- `CARDS_PART_08_ARMY_SUPPLY.yaml`
- `CARDS_PART_09_BLACK_MARKET.yaml`
- `CARDS_PART_10_REBELLION.yaml`
- `CARDS_PART_11_HEIRS_AND_FACTIONS.yaml`
- `CARDS_PART_12_SEAL_AND_FINALS.yaml`
- `STORY_FLOW.md`
- `CHOICE_REVIEW_TABLE.md`
- `VALIDATION_REPORT.md`

## Sayılar

- Kart sayısı: 300
- Seçim sayısı: 600
- Ending sayısı: 28
- Major route family sayısı: 12
- Major scenario branch sayısı: 36
- Reusable background sayısı: 50
- Focus image prompt sayısı: 300
- Important NPC sayısı: 20

## Ana Rotalar

intro, kitchen_rise, pantry_pastry, soup_goose, poison_taster, healer_path, spy_network, army_supply, black_market, rebellion, heirs_and_factions, seal_and_finals

## Absürt Zincirler

- Kehanet çorbası: kurulum, gelişim, varis manipülasyonu ve final payoff içerir.
- Saray kazı Ak Gaga: yemleme, yorumlama, divan krizi ve final koruması içerir.
- Yanlış tatlı diplomasisi: helvahane hatası, elçi yorumu, dış destek ve evlilik/barış payoffu içerir.

## Validation Durumu

`VALIDATION_REPORT.md` sonucu: PASS

## Sonraki Aşamada Kullanım

Codex sonraki entegrasyon aşamasında bu paketi veri kaynağı olarak kullanmalı: önce `STATE_MODEL.yaml`, `ENDINGS.yaml` ve kart partlarını okuyup graph doğrulamasını tekrarlamalı; sonra `ASSET_MANIFEST.yaml` içindeki promptları görsel üretim pipelineına bağlamalı; en son story runtime içinde `nextCardId` ve `endingId` hedeflerini import etmelidir.
