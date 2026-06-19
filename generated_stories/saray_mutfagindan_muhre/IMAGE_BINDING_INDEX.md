# Image Binding Index

Naming is intentionally ID-based so Unity binding is unambiguous.

- Background files: `Assets/Stories/saray_mutfagindan_muhre/Backgrounds/<backgroundId>.png`
- Choice files: `Assets/Stories/saray_mutfagindan_muhre/Choices/choice_<cardId>_<a|b>.png`
- Machine-readable copy for Unity: `Assets/Stories/saray_mutfagindan_muhre/image_bindings.json`

| Card ID | Title | Background | Choice | Choice ID | Image file | Choice text | Next |
|---|---|---|---|---|---|---|---|
| `smm_card_001` | Yetimlikten Bulaşıkhaneye: İlk Eşik | `bg_saray_bulasikhanesi_sabah.png` | A | `smm_card_001_a` | `choice_smm_card_001_a.png` | Lekeyi üstlenip gerçeği söyle | `smm_card_002` |
| `smm_card_001` | Yetimlikten Bulaşıkhaneye: İlk Eşik | `bg_saray_bulasikhanesi_sabah.png` | B | `smm_card_001_b` | `choice_smm_card_001_b.png` | Suçu kıdemsiz çırağa yönelt | `smm_card_003` |
| `smm_card_002` | Yetimlikten Bulaşıkhaneye: Sıcak Bakır | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_002_a` | `choice_smm_card_002_a.png` | Tabakları sessizce düzene sok | `smm_card_003` |
| `smm_card_002` | Yetimlikten Bulaşıkhaneye: Sıcak Bakır | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_002_b` | `choice_smm_card_002_b.png` | Fısıltıyı soytarıya sat | `smm_card_004` |
| `smm_card_003` | Yetimlikten Bulaşıkhaneye: Sessiz Bakış | `bg_hizmetkar_koridoru.png` | A | `smm_card_003_a` | `choice_smm_card_003_a.png` | Aç çocuğa payını bırak | `smm_card_004` |
| `smm_card_003` | Yetimlikten Bulaşıkhaneye: Sessiz Bakış | `bg_hizmetkar_koridoru.png` | B | `smm_card_003_b` | `choice_smm_card_003_b.png` | Kiler kapısını gizlice dene | `smm_card_005` |
| `smm_card_004` | Yetimlikten Bulaşıkhaneye: İnce Hesap | `bg_kiler_serin_tas.png` | A | `smm_card_004_a` | `choice_smm_card_004_a.png` | Ustanın ölçüsünü harfiyen uygula | `smm_card_005` |
| `smm_card_004` | Yetimlikten Bulaşıkhaneye: İnce Hesap | `bg_kiler_serin_tas.png` | B | `smm_card_004_b` | `choice_smm_card_004_b.png` | Kazın yürüyüşünü abartarak anlat | `smm_card_006` |
| `smm_card_005` | Yetimlikten Bulaşıkhaneye: Gizli Lekeler | `bg_saray_avlusu.png` | A | `smm_card_005_a` | `choice_smm_card_005_a.png` | Kapı nöbetçisine saygılı yaklaş | `smm_card_006` |
| `smm_card_005` | Yetimlikten Bulaşıkhaneye: Gizli Lekeler | `bg_saray_avlusu.png` | B | `smm_card_005_b` | `choice_smm_card_005_b.png` | Artan ekmeği pazara kaçır | `smm_card_008` |
| `smm_card_006` | Yetimlikten Bulaşıkhaneye: Kör Alev | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_006_a` | `choice_smm_card_006_a.png` | Lekeyi üstlenip gerçeği söyle | `smm_card_007` |
| `smm_card_006` | Yetimlikten Bulaşıkhaneye: Kör Alev | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_006_b` | `choice_smm_card_006_b.png` | Suçu kıdemsiz çırağa yönelt | `smm_card_008` |
| `smm_card_007` | Yetimlikten Bulaşıkhaneye: Üç Nohut | `bg_kaz_avlusu.png` | A | `smm_card_007_a` | `choice_smm_card_007_a.png` | Tabakları sessizce düzene sok | `smm_card_008` |
| `smm_card_007` | Yetimlikten Bulaşıkhaneye: Üç Nohut | `bg_kaz_avlusu.png` | B | `smm_card_007_b` | `choice_smm_card_007_b.png` | Fısıltıyı soytarıya sat | `smm_card_009` |
| `smm_card_008` | Yetimlikten Bulaşıkhaneye: Kapıdaki Fısıltı | `bg_saray_bulasikhanesi_sabah.png` | A | `smm_card_008_a` | `choice_smm_card_008_a.png` | Aç çocuğa payını bırak | `smm_card_009` |
| `smm_card_008` | Yetimlikten Bulaşıkhaneye: Kapıdaki Fısıltı | `bg_saray_bulasikhanesi_sabah.png` | B | `smm_card_008_b` | `choice_smm_card_008_b.png` | Kiler kapısını gizlice dene | `smm_card_010` |
| `smm_card_009` | Yetimlikten Bulaşıkhaneye: Aç Karnın Sözü | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_009_a` | `choice_smm_card_009_a.png` | Ustanın ölçüsünü harfiyen uygula | `smm_card_010` |
| `smm_card_009` | Yetimlikten Bulaşıkhaneye: Aç Karnın Sözü | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_009_b` | `choice_smm_card_009_b.png` | Kazın yürüyüşünü abartarak anlat | `smm_card_011` |
| `smm_card_010` | Yetimlikten Bulaşıkhaneye: Ustanın Sınavı | `bg_hizmetkar_koridoru.png` | A | `smm_card_010_a` | `choice_smm_card_010_a.png` | Kapı nöbetçisine saygılı yaklaş | `smm_card_011` |
| `smm_card_010` | Yetimlikten Bulaşıkhaneye: Ustanın Sınavı | `bg_hizmetkar_koridoru.png` | B | `smm_card_010_b` | `choice_smm_card_010_b.png` | Artan ekmeği pazara kaçır | `smm_card_013` |
| `smm_card_011` | Yetimlikten Bulaşıkhaneye: Kazanın Gölgesi | `bg_kiler_serin_tas.png` | A | `smm_card_011_a` | `choice_smm_card_011_a.png` | Lekeyi üstlenip gerçeği söyle | `smm_card_012` |
| `smm_card_011` | Yetimlikten Bulaşıkhaneye: Kazanın Gölgesi | `bg_kiler_serin_tas.png` | B | `smm_card_011_b` | `choice_smm_card_011_b.png` | Suçu kıdemsiz çırağa yönelt | `smm_card_013` |
| `smm_card_012` | Yetimlikten Bulaşıkhaneye: Tuz ve Yemin | `bg_saray_avlusu.png` | A | `smm_card_012_a` | `choice_smm_card_012_a.png` | Tabakları sessizce düzene sok | `smm_card_013` |
| `smm_card_012` | Yetimlikten Bulaşıkhaneye: Tuz ve Yemin | `bg_saray_avlusu.png` | B | `smm_card_012_b` | `choice_smm_card_012_b.png` | Fısıltıyı soytarıya sat | `smm_card_014` |
| `smm_card_013` | Yetimlikten Bulaşıkhaneye: Kırık Tabak | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_013_a` | `choice_smm_card_013_a.png` | Aç çocuğa payını bırak | `smm_card_014` |
| `smm_card_013` | Yetimlikten Bulaşıkhaneye: Kırık Tabak | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_013_b` | `choice_smm_card_013_b.png` | Kiler kapısını gizlice dene | `smm_card_015` |
| `smm_card_014` | Yetimlikten Bulaşıkhaneye: Mum Işığı | `bg_kaz_avlusu.png` | A | `smm_card_014_a` | `choice_smm_card_014_a.png` | Ustanın ölçüsünü harfiyen uygula | `smm_card_015` |
| `smm_card_014` | Yetimlikten Bulaşıkhaneye: Mum Işığı | `bg_kaz_avlusu.png` | B | `smm_card_014_b` | `choice_smm_card_014_b.png` | Kazın yürüyüşünü abartarak anlat | `smm_card_016` |
| `smm_card_015` | Yetimlikten Bulaşıkhaneye: Gece Nöbeti | `bg_saray_bulasikhanesi_sabah.png` | A | `smm_card_015_a` | `choice_smm_card_015_a.png` | Kapı nöbetçisine saygılı yaklaş | `smm_card_016` |
| `smm_card_015` | Yetimlikten Bulaşıkhaneye: Gece Nöbeti | `bg_saray_bulasikhanesi_sabah.png` | B | `smm_card_015_b` | `choice_smm_card_015_b.png` | Artan ekmeği pazara kaçır | `smm_card_018` |
| `smm_card_016` | Yetimlikten Bulaşıkhaneye: Kilitli Raf | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_016_a` | `choice_smm_card_016_a.png` | Lekeyi üstlenip gerçeği söyle | `smm_card_017` |
| `smm_card_016` | Yetimlikten Bulaşıkhaneye: Kilitli Raf | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_016_b` | `choice_smm_card_016_b.png` | Suçu kıdemsiz çırağa yönelt | `smm_card_018` |
| `smm_card_017` | Yetimlikten Bulaşıkhaneye: Sarayın Kulağı | `bg_hizmetkar_koridoru.png` | A | `smm_card_017_a` | `choice_smm_card_017_a.png` | Tabakları sessizce düzene sok | `smm_card_018` |
| `smm_card_017` | Yetimlikten Bulaşıkhaneye: Sarayın Kulağı | `bg_hizmetkar_koridoru.png` | B | `smm_card_017_b` | `choice_smm_card_017_b.png` | Fısıltıyı soytarıya sat | `smm_card_019` |
| `smm_card_018` | Yetimlikten Bulaşıkhaneye: Yağ Halkası | `bg_kiler_serin_tas.png` | A | `smm_card_018_a` | `choice_smm_card_018_a.png` | Aç çocuğa payını bırak | `smm_card_019` |
| `smm_card_018` | Yetimlikten Bulaşıkhaneye: Yağ Halkası | `bg_kiler_serin_tas.png` | B | `smm_card_018_b` | `choice_smm_card_018_b.png` | Kiler kapısını gizlice dene | `smm_card_020` |
| `smm_card_019` | Yetimlikten Bulaşıkhaneye: Ak Gaga'nın Yolu | `bg_saray_avlusu.png` | A | `smm_card_019_a` | `choice_smm_card_019_a.png` | Ustanın ölçüsünü harfiyen uygula | `smm_card_020` |
| `smm_card_019` | Yetimlikten Bulaşıkhaneye: Ak Gaga'nın Yolu | `bg_saray_avlusu.png` | B | `smm_card_019_b` | `choice_smm_card_019_b.png` | Kazın yürüyüşünü abartarak anlat | `smm_card_021` |
| `smm_card_020` | Yetimlikten Bulaşıkhaneye: Gümüş Kaşık | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_020_a` | `choice_smm_card_020_a.png` | Kapı nöbetçisine saygılı yaklaş | `smm_card_021` |
| `smm_card_020` | Yetimlikten Bulaşıkhaneye: Gümüş Kaşık | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_020_b` | `choice_smm_card_020_b.png` | Artan ekmeği pazara kaçır | `smm_card_023` |
| `smm_card_021` | Yetimlikten Bulaşıkhaneye: Kayıp Ölçü | `bg_kaz_avlusu.png` | A | `smm_card_021_a` | `choice_smm_card_021_a.png` | Lekeyi üstlenip gerçeği söyle | `smm_card_022` |
| `smm_card_021` | Yetimlikten Bulaşıkhaneye: Kayıp Ölçü | `bg_kaz_avlusu.png` | B | `smm_card_021_b` | `choice_smm_card_021_b.png` | Suçu kıdemsiz çırağa yönelt | `smm_card_023` |
| `smm_card_022` | Yetimlikten Bulaşıkhaneye: Kuyrukta Ekmek | `bg_saray_bulasikhanesi_sabah.png` | A | `smm_card_022_a` | `choice_smm_card_022_a.png` | Tabakları sessizce düzene sok | `smm_card_023` |
| `smm_card_022` | Yetimlikten Bulaşıkhaneye: Kuyrukta Ekmek | `bg_saray_bulasikhanesi_sabah.png` | B | `smm_card_022_b` | `choice_smm_card_022_b.png` | Fısıltıyı soytarıya sat | `smm_card_024` |
| `smm_card_023` | Yetimlikten Bulaşıkhaneye: Keskin Koku | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_023_a` | `choice_smm_card_023_a.png` | Aç çocuğa payını bırak | `smm_card_024` |
| `smm_card_023` | Yetimlikten Bulaşıkhaneye: Keskin Koku | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_023_b` | `choice_smm_card_023_b.png` | Kiler kapısını gizlice dene | `smm_card_025` |
| `smm_card_024` | Yetimlikten Bulaşıkhaneye: Mühre Giden İz | `bg_hizmetkar_koridoru.png` | A | `smm_card_024_a` | `choice_smm_card_024_a.png` | Ustanın ölçüsünü harfiyen uygula | `smm_card_025` |
| `smm_card_024` | Yetimlikten Bulaşıkhaneye: Mühre Giden İz | `bg_hizmetkar_koridoru.png` | B | `smm_card_024_b` | `choice_smm_card_024_b.png` | Kazın yürüyüşünü abartarak anlat | `smm_card_026` |
| `smm_card_025` | Yetimlikten Bulaşıkhaneye: Dar Boğaz | `bg_kiler_serin_tas.png` | A | `smm_card_025_a` | `choice_smm_card_025_a.png` | Kapı nöbetçisine saygılı yaklaş | `smm_card_026` |
| `smm_card_025` | Yetimlikten Bulaşıkhaneye: Dar Boğaz | `bg_kiler_serin_tas.png` | B | `smm_card_025_b` | `choice_smm_card_025_b.png` | Artan ekmeği pazara kaçır | `smm_card_028` |
| `smm_card_026` | Büyük Mutfakta Yükseliş: İlk Eşik | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_026_a` | `choice_smm_card_026_a.png` | Kazanı ustanın yöntemiyle kurtar | `smm_card_027` |
| `smm_card_026` | Büyük Mutfakta Yükseliş: İlk Eşik | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_026_b` | `choice_smm_card_026_b.png` | Lezzeti gizli baharatla yükselt | `smm_card_028` |
| `smm_card_027` | Büyük Mutfakta Yükseliş: Sıcak Bakır | `bg_buyuk_mutfak_sakin.png` | A | `smm_card_027_a` | `choice_smm_card_027_a.png` | Baharatı ölçerek sabırla ekle | `smm_card_028` |
| `smm_card_027` | Büyük Mutfakta Yükseliş: Sıcak Bakır | `bg_buyuk_mutfak_sakin.png` | B | `smm_card_027_b` | `choice_smm_card_027_b.png` | Ustanın tarifini kendi adına geçir | `smm_card_029` |
| `smm_card_028` | Büyük Mutfakta Yükseliş: Sessiz Bakış | `bg_baharat_odasi_safran.png` | A | `smm_card_028_a` | `choice_smm_card_028_a.png` | Hükümdarın tabağını temiz tut | `smm_card_029` |
| `smm_card_028` | Büyük Mutfakta Yükseliş: Sessiz Bakış | `bg_baharat_odasi_safran.png` | B | `smm_card_028_b` | `choice_smm_card_028_b.png` | Hükümdarın tabağına işaret bırak | `smm_card_030` |
| `smm_card_029` | Büyük Mutfakta Yükseliş: İnce Hesap | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_029_a` | `choice_smm_card_029_a.png` | Yamakları adil biçimde sırala | `smm_card_030` |
| `smm_card_029` | Büyük Mutfakta Yükseliş: İnce Hesap | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_029_b` | `choice_smm_card_029_b.png` | Yamakları birbirine düşür | `smm_card_031` |
| `smm_card_030` | Büyük Mutfakta Yükseliş: Gizli Lekeler | `bg_taht_salonu.png` | A | `smm_card_030_a` | `choice_smm_card_030_a.png` | Eksik eti deftere açık yaz | `smm_card_031` |
| `smm_card_030` | Büyük Mutfakta Yükseliş: Gizli Lekeler | `bg_taht_salonu.png` | B | `smm_card_030_b` | `choice_smm_card_030_b.png` | Eksik eti fısıltıyla kapat | `smm_card_033` |
| `smm_card_031` | Büyük Mutfakta Yükseliş: Kör Alev | `bg_mutfak_cati_arasi.png` | A | `smm_card_031_a` | `choice_smm_card_031_a.png` | Kazanı ustanın yöntemiyle kurtar | `smm_card_032` |
| `smm_card_031` | Büyük Mutfakta Yükseliş: Kör Alev | `bg_mutfak_cati_arasi.png` | B | `smm_card_031_b` | `choice_smm_card_031_b.png` | Lezzeti gizli baharatla yükselt | `smm_card_033` |
| `smm_card_032` | Büyük Mutfakta Yükseliş: Üç Nohut | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_032_a` | `choice_smm_card_032_a.png` | Baharatı ölçerek sabırla ekle | `smm_card_033` |
| `smm_card_032` | Büyük Mutfakta Yükseliş: Üç Nohut | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_032_b` | `choice_smm_card_032_b.png` | Ustanın tarifini kendi adına geçir | `smm_card_034` |
| `smm_card_033` | Büyük Mutfakta Yükseliş: Kapıdaki Fısıltı | `bg_buyuk_mutfak_sakin.png` | A | `smm_card_033_a` | `choice_smm_card_033_a.png` | Hükümdarın tabağını temiz tut | `smm_card_034` |
| `smm_card_033` | Büyük Mutfakta Yükseliş: Kapıdaki Fısıltı | `bg_buyuk_mutfak_sakin.png` | B | `smm_card_033_b` | `choice_smm_card_033_b.png` | Hükümdarın tabağına işaret bırak | `smm_card_035` |
| `smm_card_034` | Büyük Mutfakta Yükseliş: Aç Karnın Sözü | `bg_baharat_odasi_safran.png` | A | `smm_card_034_a` | `choice_smm_card_034_a.png` | Yamakları adil biçimde sırala | `smm_card_035` |
| `smm_card_034` | Büyük Mutfakta Yükseliş: Aç Karnın Sözü | `bg_baharat_odasi_safran.png` | B | `smm_card_034_b` | `choice_smm_card_034_b.png` | Yamakları birbirine düşür | `smm_card_036` |
| `smm_card_035` | Büyük Mutfakta Yükseliş: Ustanın Sınavı | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_035_a` | `choice_smm_card_035_a.png` | Eksik eti deftere açık yaz | `smm_card_036` |
| `smm_card_035` | Büyük Mutfakta Yükseliş: Ustanın Sınavı | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_035_b` | `choice_smm_card_035_b.png` | Eksik eti fısıltıyla kapat | `smm_card_038` |
| `smm_card_036` | Büyük Mutfakta Yükseliş: Kazanın Gölgesi | `bg_taht_salonu.png` | A | `smm_card_036_a` | `choice_smm_card_036_a.png` | Kazanı ustanın yöntemiyle kurtar | `smm_card_037` |
| `smm_card_036` | Büyük Mutfakta Yükseliş: Kazanın Gölgesi | `bg_taht_salonu.png` | B | `smm_card_036_b` | `choice_smm_card_036_b.png` | Lezzeti gizli baharatla yükselt | `smm_card_038` |
| `smm_card_037` | Büyük Mutfakta Yükseliş: Tuz ve Yemin | `bg_mutfak_cati_arasi.png` | A | `smm_card_037_a` | `choice_smm_card_037_a.png` | Baharatı ölçerek sabırla ekle | `smm_card_038` |
| `smm_card_037` | Büyük Mutfakta Yükseliş: Tuz ve Yemin | `bg_mutfak_cati_arasi.png` | B | `smm_card_037_b` | `choice_smm_card_037_b.png` | Ustanın tarifini kendi adına geçir | `smm_card_039` |
| `smm_card_038` | Büyük Mutfakta Yükseliş: Kırık Tabak | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_038_a` | `choice_smm_card_038_a.png` | Hükümdarın tabağını temiz tut | `smm_card_039` |
| `smm_card_038` | Büyük Mutfakta Yükseliş: Kırık Tabak | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_038_b` | `choice_smm_card_038_b.png` | Hükümdarın tabağına işaret bırak | `smm_card_040` |
| `smm_card_039` | Büyük Mutfakta Yükseliş: Mum Işığı | `bg_buyuk_mutfak_sakin.png` | A | `smm_card_039_a` | `choice_smm_card_039_a.png` | Yamakları adil biçimde sırala | `smm_card_040` |
| `smm_card_039` | Büyük Mutfakta Yükseliş: Mum Işığı | `bg_buyuk_mutfak_sakin.png` | B | `smm_card_039_b` | `choice_smm_card_039_b.png` | Yamakları birbirine düşür | `smm_card_041` |
| `smm_card_040` | Büyük Mutfakta Yükseliş: Gece Nöbeti | `bg_baharat_odasi_safran.png` | A | `smm_card_040_a` | `choice_smm_card_040_a.png` | Eksik eti deftere açık yaz | `smm_card_041` |
| `smm_card_040` | Büyük Mutfakta Yükseliş: Gece Nöbeti | `bg_baharat_odasi_safran.png` | B | `smm_card_040_b` | `choice_smm_card_040_b.png` | Eksik eti fısıltıyla kapat | `smm_card_043` |
| `smm_card_041` | Büyük Mutfakta Yükseliş: Kilitli Raf | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_041_a` | `choice_smm_card_041_a.png` | Kazanı ustanın yöntemiyle kurtar | `smm_card_042` |
| `smm_card_041` | Büyük Mutfakta Yükseliş: Kilitli Raf | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_041_b` | `choice_smm_card_041_b.png` | Lezzeti gizli baharatla yükselt | `smm_card_043` |
| `smm_card_042` | Büyük Mutfakta Yükseliş: Sarayın Kulağı | `bg_taht_salonu.png` | A | `smm_card_042_a` | `choice_smm_card_042_a.png` | Baharatı ölçerek sabırla ekle | `smm_card_043` |
| `smm_card_042` | Büyük Mutfakta Yükseliş: Sarayın Kulağı | `bg_taht_salonu.png` | B | `smm_card_042_b` | `choice_smm_card_042_b.png` | Ustanın tarifini kendi adına geçir | `smm_card_044` |
| `smm_card_043` | Büyük Mutfakta Yükseliş: Yağ Halkası | `bg_mutfak_cati_arasi.png` | A | `smm_card_043_a` | `choice_smm_card_043_a.png` | Hükümdarın tabağını temiz tut | `smm_card_044` |
| `smm_card_043` | Büyük Mutfakta Yükseliş: Yağ Halkası | `bg_mutfak_cati_arasi.png` | B | `smm_card_043_b` | `choice_smm_card_043_b.png` | Hükümdarın tabağına işaret bırak | `smm_card_045` |
| `smm_card_044` | Büyük Mutfakta Yükseliş: Ak Gaga'nın Yolu | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_044_a` | `choice_smm_card_044_a.png` | Yamakları adil biçimde sırala | `smm_card_045` |
| `smm_card_044` | Büyük Mutfakta Yükseliş: Ak Gaga'nın Yolu | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_044_b` | `choice_smm_card_044_b.png` | Yamakları birbirine düşür | `smm_card_046` |
| `smm_card_045` | Büyük Mutfakta Yükseliş: Gümüş Kaşık | `bg_buyuk_mutfak_sakin.png` | A | `smm_card_045_a` | `choice_smm_card_045_a.png` | Eksik eti deftere açık yaz | `smm_card_046` |
| `smm_card_045` | Büyük Mutfakta Yükseliş: Gümüş Kaşık | `bg_buyuk_mutfak_sakin.png` | B | `smm_card_045_b` | `choice_smm_card_045_b.png` | Eksik eti fısıltıyla kapat | `smm_card_048` |
| `smm_card_046` | Büyük Mutfakta Yükseliş: Kayıp Ölçü | `bg_baharat_odasi_safran.png` | A | `smm_card_046_a` | `choice_smm_card_046_a.png` | Kazanı ustanın yöntemiyle kurtar | `smm_card_047` |
| `smm_card_046` | Büyük Mutfakta Yükseliş: Kayıp Ölçü | `bg_baharat_odasi_safran.png` | B | `smm_card_046_b` | `choice_smm_card_046_b.png` | Lezzeti gizli baharatla yükselt | `smm_card_048` |
| `smm_card_047` | Büyük Mutfakta Yükseliş: Kuyrukta Ekmek | `bg_saray_bulasikhanesi_gece.png` | A | `smm_card_047_a` | `choice_smm_card_047_a.png` | Baharatı ölçerek sabırla ekle | `smm_card_048` |
| `smm_card_047` | Büyük Mutfakta Yükseliş: Kuyrukta Ekmek | `bg_saray_bulasikhanesi_gece.png` | B | `smm_card_047_b` | `choice_smm_card_047_b.png` | Ustanın tarifini kendi adına geçir | `smm_card_049` |
| `smm_card_048` | Büyük Mutfakta Yükseliş: Keskin Koku | `bg_taht_salonu.png` | A | `smm_card_048_a` | `choice_smm_card_048_a.png` | Hükümdarın tabağını temiz tut | `smm_card_049` |
| `smm_card_048` | Büyük Mutfakta Yükseliş: Keskin Koku | `bg_taht_salonu.png` | B | `smm_card_048_b` | `choice_smm_card_048_b.png` | Hükümdarın tabağına işaret bırak | `smm_card_050` |
| `smm_card_049` | Büyük Mutfakta Yükseliş: Mühre Giden İz | `bg_mutfak_cati_arasi.png` | A | `smm_card_049_a` | `choice_smm_card_049_a.png` | Yamakları adil biçimde sırala | `smm_card_050` |
| `smm_card_049` | Büyük Mutfakta Yükseliş: Mühre Giden İz | `bg_mutfak_cati_arasi.png` | B | `smm_card_049_b` | `choice_smm_card_049_b.png` | Yamakları birbirine düşür | `smm_card_051` |
| `smm_card_050` | Büyük Mutfakta Yükseliş: Dar Boğaz | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_050_a` | `choice_smm_card_050_a.png` | Eksik eti deftere açık yaz | `smm_card_051` |
| `smm_card_050` | Büyük Mutfakta Yükseliş: Dar Boğaz | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_050_b` | `choice_smm_card_050_b.png` | Eksik eti fısıltıyla kapat | `smm_card_053` |
| `smm_card_051` | Kiler ve Helvahane: İlk Eşik | `bg_kiler_serin_tas.png` | A | `smm_card_051_a` | `choice_smm_card_051_a.png` | Yanlış tepsiyi hemen geri çağır | `smm_card_052` |
| `smm_card_051` | Kiler ve Helvahane: İlk Eşik | `bg_kiler_serin_tas.png` | B | `smm_card_051_b` | `choice_smm_card_051_b.png` | Tatlı hatasını fırsata çevir | `smm_card_053` |
| `smm_card_052` | Kiler ve Helvahane: Sıcak Bakır | `bg_kiler_gizli_raflar.png` | A | `smm_card_052_a` | `choice_smm_card_052_a.png` | Kiler anahtarını Nimet'e teslim et | `smm_card_053` |
| `smm_card_052` | Kiler ve Helvahane: Sıcak Bakır | `bg_kiler_gizli_raflar.png` | B | `smm_card_052_b` | `choice_smm_card_052_b.png` | Anahtarın kopyasını gizlice al | `smm_card_054` |
| `smm_card_053` | Kiler ve Helvahane: Sessiz Bakış | `bg_helvahane_gul_kokusu.png` | A | `smm_card_053_a` | `choice_smm_card_053_a.png` | Gül şerbetini usulüne göre sun | `smm_card_054` |
| `smm_card_053` | Kiler ve Helvahane: Sessiz Bakış | `bg_helvahane_gul_kokusu.png` | B | `smm_card_053_b` | `choice_smm_card_053_b.png` | Şerbeti varis adına yolla | `smm_card_055` |
| `smm_card_054` | Kiler ve Helvahane: İnce Hesap | `bg_helvahane_gece.png` | A | `smm_card_054_a` | `choice_smm_card_054_a.png` | Lale Kalfa'yı açıkça savun | `smm_card_055` |
| `smm_card_054` | Kiler ve Helvahane: İnce Hesap | `bg_helvahane_gece.png` | B | `smm_card_054_b` | `choice_smm_card_054_b.png` | Suçu suskun çırağa yükle | `smm_card_056` |
| `smm_card_055` | Kiler ve Helvahane: Gizli Lekeler | `bg_elcilik_salonu.png` | A | `smm_card_055_a` | `choice_smm_card_055_a.png` | Elçiye hatayı incelikle anlat | `smm_card_056` |
| `smm_card_055` | Kiler ve Helvahane: Gizli Lekeler | `bg_elcilik_salonu.png` | B | `smm_card_055_b` | `choice_smm_card_055_b.png` | Elçinin yüzüğünü kabul et | `smm_card_058` |
| `smm_card_056` | Kiler ve Helvahane: Kör Alev | `bg_buzlu_kiler_odasi.png` | A | `smm_card_056_a` | `choice_smm_card_056_a.png` | Yanlış tepsiyi hemen geri çağır | `smm_card_057` |
| `smm_card_056` | Kiler ve Helvahane: Kör Alev | `bg_buzlu_kiler_odasi.png` | B | `smm_card_056_b` | `choice_smm_card_056_b.png` | Tatlı hatasını fırsata çevir | `smm_card_058` |
| `smm_card_057` | Kiler ve Helvahane: Üç Nohut | `bg_yabanci_elci_cadiri.png` | A | `smm_card_057_a` | `choice_smm_card_057_a.png` | Kiler anahtarını Nimet'e teslim et | `smm_card_058` |
| `smm_card_057` | Kiler ve Helvahane: Üç Nohut | `bg_yabanci_elci_cadiri.png` | B | `smm_card_057_b` | `choice_smm_card_057_b.png` | Anahtarın kopyasını gizlice al | `smm_card_059` |
| `smm_card_058` | Kiler ve Helvahane: Kapıdaki Fısıltı | `bg_kiler_serin_tas.png` | A | `smm_card_058_a` | `choice_smm_card_058_a.png` | Gül şerbetini usulüne göre sun | `smm_card_059` |
| `smm_card_058` | Kiler ve Helvahane: Kapıdaki Fısıltı | `bg_kiler_serin_tas.png` | B | `smm_card_058_b` | `choice_smm_card_058_b.png` | Şerbeti varis adına yolla | `smm_card_060` |
| `smm_card_059` | Kiler ve Helvahane: Aç Karnın Sözü | `bg_kiler_gizli_raflar.png` | A | `smm_card_059_a` | `choice_smm_card_059_a.png` | Lale Kalfa'yı açıkça savun | `smm_card_060` |
| `smm_card_059` | Kiler ve Helvahane: Aç Karnın Sözü | `bg_kiler_gizli_raflar.png` | B | `smm_card_059_b` | `choice_smm_card_059_b.png` | Suçu suskun çırağa yükle | `smm_card_061` |
| `smm_card_060` | Kiler ve Helvahane: Ustanın Sınavı | `bg_helvahane_gul_kokusu.png` | A | `smm_card_060_a` | `choice_smm_card_060_a.png` | Elçiye hatayı incelikle anlat | `smm_card_061` |
| `smm_card_060` | Kiler ve Helvahane: Ustanın Sınavı | `bg_helvahane_gul_kokusu.png` | B | `smm_card_060_b` | `choice_smm_card_060_b.png` | Elçinin yüzüğünü kabul et | `smm_card_063` |
| `smm_card_061` | Kiler ve Helvahane: Kazanın Gölgesi | `bg_helvahane_gece.png` | A | `smm_card_061_a` | `choice_smm_card_061_a.png` | Yanlış tepsiyi hemen geri çağır | `smm_card_062` |
| `smm_card_061` | Kiler ve Helvahane: Kazanın Gölgesi | `bg_helvahane_gece.png` | B | `smm_card_061_b` | `choice_smm_card_061_b.png` | Tatlı hatasını fırsata çevir | `smm_card_063` |
| `smm_card_062` | Kiler ve Helvahane: Tuz ve Yemin | `bg_elcilik_salonu.png` | A | `smm_card_062_a` | `choice_smm_card_062_a.png` | Kiler anahtarını Nimet'e teslim et | `smm_card_063` |
| `smm_card_062` | Kiler ve Helvahane: Tuz ve Yemin | `bg_elcilik_salonu.png` | B | `smm_card_062_b` | `choice_smm_card_062_b.png` | Anahtarın kopyasını gizlice al | `smm_card_064` |
| `smm_card_063` | Kiler ve Helvahane: Kırık Tabak | `bg_buzlu_kiler_odasi.png` | A | `smm_card_063_a` | `choice_smm_card_063_a.png` | Gül şerbetini usulüne göre sun | `smm_card_064` |
| `smm_card_063` | Kiler ve Helvahane: Kırık Tabak | `bg_buzlu_kiler_odasi.png` | B | `smm_card_063_b` | `choice_smm_card_063_b.png` | Şerbeti varis adına yolla | `smm_card_065` |
| `smm_card_064` | Kiler ve Helvahane: Mum Işığı | `bg_yabanci_elci_cadiri.png` | A | `smm_card_064_a` | `choice_smm_card_064_a.png` | Lale Kalfa'yı açıkça savun | `smm_card_065` |
| `smm_card_064` | Kiler ve Helvahane: Mum Işığı | `bg_yabanci_elci_cadiri.png` | B | `smm_card_064_b` | `choice_smm_card_064_b.png` | Suçu suskun çırağa yükle | `smm_card_066` |
| `smm_card_065` | Kiler ve Helvahane: Gece Nöbeti | `bg_kiler_serin_tas.png` | A | `smm_card_065_a` | `choice_smm_card_065_a.png` | Elçiye hatayı incelikle anlat | `smm_card_066` |
| `smm_card_065` | Kiler ve Helvahane: Gece Nöbeti | `bg_kiler_serin_tas.png` | B | `smm_card_065_b` | `choice_smm_card_065_b.png` | Elçinin yüzüğünü kabul et | `smm_card_068` |
| `smm_card_066` | Kiler ve Helvahane: Kilitli Raf | `bg_kiler_gizli_raflar.png` | A | `smm_card_066_a` | `choice_smm_card_066_a.png` | Yanlış tepsiyi hemen geri çağır | `smm_card_067` |
| `smm_card_066` | Kiler ve Helvahane: Kilitli Raf | `bg_kiler_gizli_raflar.png` | B | `smm_card_066_b` | `choice_smm_card_066_b.png` | Tatlı hatasını fırsata çevir | `smm_card_068` |
| `smm_card_067` | Kiler ve Helvahane: Sarayın Kulağı | `bg_helvahane_gul_kokusu.png` | A | `smm_card_067_a` | `choice_smm_card_067_a.png` | Kiler anahtarını Nimet'e teslim et | `smm_card_068` |
| `smm_card_067` | Kiler ve Helvahane: Sarayın Kulağı | `bg_helvahane_gul_kokusu.png` | B | `smm_card_067_b` | `choice_smm_card_067_b.png` | Anahtarın kopyasını gizlice al | `smm_card_069` |
| `smm_card_068` | Kiler ve Helvahane: Yağ Halkası | `bg_helvahane_gece.png` | A | `smm_card_068_a` | `choice_smm_card_068_a.png` | Gül şerbetini usulüne göre sun | `smm_card_069` |
| `smm_card_068` | Kiler ve Helvahane: Yağ Halkası | `bg_helvahane_gece.png` | B | `smm_card_068_b` | `choice_smm_card_068_b.png` | Şerbeti varis adına yolla | `smm_card_070` |
| `smm_card_069` | Kiler ve Helvahane: Ak Gaga'nın Yolu | `bg_elcilik_salonu.png` | A | `smm_card_069_a` | `choice_smm_card_069_a.png` | Lale Kalfa'yı açıkça savun | `smm_card_070` |
| `smm_card_069` | Kiler ve Helvahane: Ak Gaga'nın Yolu | `bg_elcilik_salonu.png` | B | `smm_card_069_b` | `choice_smm_card_069_b.png` | Suçu suskun çırağa yükle | `smm_card_071` |
| `smm_card_070` | Kiler ve Helvahane: Gümüş Kaşık | `bg_buzlu_kiler_odasi.png` | A | `smm_card_070_a` | `choice_smm_card_070_a.png` | Elçiye hatayı incelikle anlat | `smm_card_071` |
| `smm_card_070` | Kiler ve Helvahane: Gümüş Kaşık | `bg_buzlu_kiler_odasi.png` | B | `smm_card_070_b` | `choice_smm_card_070_b.png` | Elçinin yüzüğünü kabul et | `smm_card_073` |
| `smm_card_071` | Kiler ve Helvahane: Kayıp Ölçü | `bg_yabanci_elci_cadiri.png` | A | `smm_card_071_a` | `choice_smm_card_071_a.png` | Yanlış tepsiyi hemen geri çağır | `smm_card_072` |
| `smm_card_071` | Kiler ve Helvahane: Kayıp Ölçü | `bg_yabanci_elci_cadiri.png` | B | `smm_card_071_b` | `choice_smm_card_071_b.png` | Tatlı hatasını fırsata çevir | `smm_card_073` |
| `smm_card_072` | Kiler ve Helvahane: Kuyrukta Ekmek | `bg_kiler_serin_tas.png` | A | `smm_card_072_a` | `choice_smm_card_072_a.png` | Kiler anahtarını Nimet'e teslim et | `smm_card_073` |
| `smm_card_072` | Kiler ve Helvahane: Kuyrukta Ekmek | `bg_kiler_serin_tas.png` | B | `smm_card_072_b` | `choice_smm_card_072_b.png` | Anahtarın kopyasını gizlice al | `smm_card_074` |
| `smm_card_073` | Kiler ve Helvahane: Keskin Koku | `bg_kiler_gizli_raflar.png` | A | `smm_card_073_a` | `choice_smm_card_073_a.png` | Gül şerbetini usulüne göre sun | `smm_card_074` |
| `smm_card_073` | Kiler ve Helvahane: Keskin Koku | `bg_kiler_gizli_raflar.png` | B | `smm_card_073_b` | `choice_smm_card_073_b.png` | Şerbeti varis adına yolla | `smm_card_075` |
| `smm_card_074` | Kiler ve Helvahane: Mühre Giden İz | `bg_helvahane_gul_kokusu.png` | A | `smm_card_074_a` | `choice_smm_card_074_a.png` | Lale Kalfa'yı açıkça savun | `smm_card_075` |
| `smm_card_074` | Kiler ve Helvahane: Mühre Giden İz | `bg_helvahane_gul_kokusu.png` | B | `smm_card_074_b` | `choice_smm_card_074_b.png` | Suçu suskun çırağa yükle | `smm_card_076` |
| `smm_card_075` | Kiler ve Helvahane: Dar Boğaz | `bg_helvahane_gece.png` | A | `smm_card_075_a` | `choice_smm_card_075_a.png` | Elçiye hatayı incelikle anlat | `smm_card_076` |
| `smm_card_075` | Kiler ve Helvahane: Dar Boğaz | `bg_helvahane_gece.png` | B | `smm_card_075_b` | `choice_smm_card_075_b.png` | Elçinin yüzüğünü kabul et | `smm_card_078` |
| `smm_card_076` | Çorba Alameti ve Ak Gaga: İlk Eşik | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_076_a` | `choice_smm_card_076_a.png` | Çorbanın alametini dürüstçe açıkla | `smm_card_077` |
| `smm_card_076` | Çorba Alameti ve Ak Gaga: İlk Eşik | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_076_b` | `choice_smm_card_076_b.png` | Yeni alameti bilerek pişir | `smm_card_078` |
| `smm_card_077` | Çorba Alameti ve Ak Gaga: Sıcak Bakır | `bg_kaz_avlusu.png` | A | `smm_card_077_a` | `choice_smm_card_077_a.png` | Ak Gaga'yı sakince besle | `smm_card_078` |
| `smm_card_077` | Çorba Alameti ve Ak Gaga: Sıcak Bakır | `bg_kaz_avlusu.png` | B | `smm_card_077_b` | `choice_smm_card_077_b.png` | Kaz yemine gizli koku sür | `smm_card_079` |
| `smm_card_078` | Çorba Alameti ve Ak Gaga: Sessiz Bakış | `bg_kaz_kumesi.png` | A | `smm_card_078_a` | `choice_smm_card_078_a.png` | Nohut işaretini bozma | `smm_card_079` |
| `smm_card_078` | Çorba Alameti ve Ak Gaga: Sessiz Bakış | `bg_kaz_kumesi.png` | B | `smm_card_078_b` | `choice_smm_card_078_b.png` | Nohutları varis adına diz | `smm_card_080` |
| `smm_card_079` | Çorba Alameti ve Ak Gaga: İnce Hesap | `bg_muneccim_kulesi.png` | A | `smm_card_079_a` | `choice_smm_card_079_a.png` | Müneccime ölçüyü göster | `smm_card_080` |
| `smm_card_079` | Çorba Alameti ve Ak Gaga: İnce Hesap | `bg_muneccim_kulesi.png` | B | `smm_card_079_b` | `choice_smm_card_079_b.png` | Müneccimi fısıltıyla yönlendir | `smm_card_081` |
| `smm_card_080` | Çorba Alameti ve Ak Gaga: Gizli Lekeler | `bg_divan_salonu.png` | A | `smm_card_080_a` | `choice_smm_card_080_a.png` | Kazın yolunu temiz bırak | `smm_card_081` |
| `smm_card_080` | Çorba Alameti ve Ak Gaga: Gizli Lekeler | `bg_divan_salonu.png` | B | `smm_card_080_b` | `choice_smm_card_080_b.png` | Kazın yolunu perdeyle çevir | `smm_card_083` |
| `smm_card_081` | Çorba Alameti ve Ak Gaga: Kör Alev | `bg_taht_salonu.png` | A | `smm_card_081_a` | `choice_smm_card_081_a.png` | Çorbanın alametini dürüstçe açıkla | `smm_card_082` |
| `smm_card_081` | Çorba Alameti ve Ak Gaga: Kör Alev | `bg_taht_salonu.png` | B | `smm_card_081_b` | `choice_smm_card_081_b.png` | Yeni alameti bilerek pişir | `smm_card_083` |
| `smm_card_082` | Çorba Alameti ve Ak Gaga: Üç Nohut | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_082_a` | `choice_smm_card_082_a.png` | Ak Gaga'yı sakince besle | `smm_card_083` |
| `smm_card_082` | Çorba Alameti ve Ak Gaga: Üç Nohut | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_082_b` | `choice_smm_card_082_b.png` | Kaz yemine gizli koku sür | `smm_card_084` |
| `smm_card_083` | Çorba Alameti ve Ak Gaga: Kapıdaki Fısıltı | `bg_kaz_avlusu.png` | A | `smm_card_083_a` | `choice_smm_card_083_a.png` | Nohut işaretini bozma | `smm_card_084` |
| `smm_card_083` | Çorba Alameti ve Ak Gaga: Kapıdaki Fısıltı | `bg_kaz_avlusu.png` | B | `smm_card_083_b` | `choice_smm_card_083_b.png` | Nohutları varis adına diz | `smm_card_085` |
| `smm_card_084` | Çorba Alameti ve Ak Gaga: Aç Karnın Sözü | `bg_kaz_kumesi.png` | A | `smm_card_084_a` | `choice_smm_card_084_a.png` | Müneccime ölçüyü göster | `smm_card_085` |
| `smm_card_084` | Çorba Alameti ve Ak Gaga: Aç Karnın Sözü | `bg_kaz_kumesi.png` | B | `smm_card_084_b` | `choice_smm_card_084_b.png` | Müneccimi fısıltıyla yönlendir | `smm_card_086` |
| `smm_card_085` | Çorba Alameti ve Ak Gaga: Ustanın Sınavı | `bg_muneccim_kulesi.png` | A | `smm_card_085_a` | `choice_smm_card_085_a.png` | Kazın yolunu temiz bırak | `smm_card_086` |
| `smm_card_085` | Çorba Alameti ve Ak Gaga: Ustanın Sınavı | `bg_muneccim_kulesi.png` | B | `smm_card_085_b` | `choice_smm_card_085_b.png` | Kazın yolunu perdeyle çevir | `smm_card_088` |
| `smm_card_086` | Çorba Alameti ve Ak Gaga: Kazanın Gölgesi | `bg_divan_salonu.png` | A | `smm_card_086_a` | `choice_smm_card_086_a.png` | Çorbanın alametini dürüstçe açıkla | `smm_card_087` |
| `smm_card_086` | Çorba Alameti ve Ak Gaga: Kazanın Gölgesi | `bg_divan_salonu.png` | B | `smm_card_086_b` | `choice_smm_card_086_b.png` | Yeni alameti bilerek pişir | `smm_card_088` |
| `smm_card_087` | Çorba Alameti ve Ak Gaga: Tuz ve Yemin | `bg_taht_salonu.png` | A | `smm_card_087_a` | `choice_smm_card_087_a.png` | Ak Gaga'yı sakince besle | `smm_card_088` |
| `smm_card_087` | Çorba Alameti ve Ak Gaga: Tuz ve Yemin | `bg_taht_salonu.png` | B | `smm_card_087_b` | `choice_smm_card_087_b.png` | Kaz yemine gizli koku sür | `smm_card_089` |
| `smm_card_088` | Çorba Alameti ve Ak Gaga: Kırık Tabak | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_088_a` | `choice_smm_card_088_a.png` | Nohut işaretini bozma | `smm_card_089` |
| `smm_card_088` | Çorba Alameti ve Ak Gaga: Kırık Tabak | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_088_b` | `choice_smm_card_088_b.png` | Nohutları varis adına diz | `smm_card_090` |
| `smm_card_089` | Çorba Alameti ve Ak Gaga: Mum Işığı | `bg_kaz_avlusu.png` | A | `smm_card_089_a` | `choice_smm_card_089_a.png` | Müneccime ölçüyü göster | `smm_card_090` |
| `smm_card_089` | Çorba Alameti ve Ak Gaga: Mum Işığı | `bg_kaz_avlusu.png` | B | `smm_card_089_b` | `choice_smm_card_089_b.png` | Müneccimi fısıltıyla yönlendir | `smm_card_091` |
| `smm_card_090` | Çorba Alameti ve Ak Gaga: Gece Nöbeti | `bg_kaz_kumesi.png` | A | `smm_card_090_a` | `choice_smm_card_090_a.png` | Kazın yolunu temiz bırak | `smm_card_091` |
| `smm_card_090` | Çorba Alameti ve Ak Gaga: Gece Nöbeti | `bg_kaz_kumesi.png` | B | `smm_card_090_b` | `choice_smm_card_090_b.png` | Kazın yolunu perdeyle çevir | `smm_card_093` |
| `smm_card_091` | Çorba Alameti ve Ak Gaga: Kilitli Raf | `bg_muneccim_kulesi.png` | A | `smm_card_091_a` | `choice_smm_card_091_a.png` | Çorbanın alametini dürüstçe açıkla | `smm_card_092` |
| `smm_card_091` | Çorba Alameti ve Ak Gaga: Kilitli Raf | `bg_muneccim_kulesi.png` | B | `smm_card_091_b` | `choice_smm_card_091_b.png` | Yeni alameti bilerek pişir | `smm_card_093` |
| `smm_card_092` | Çorba Alameti ve Ak Gaga: Sarayın Kulağı | `bg_divan_salonu.png` | A | `smm_card_092_a` | `choice_smm_card_092_a.png` | Ak Gaga'yı sakince besle | `smm_card_093` |
| `smm_card_092` | Çorba Alameti ve Ak Gaga: Sarayın Kulağı | `bg_divan_salonu.png` | B | `smm_card_092_b` | `choice_smm_card_092_b.png` | Kaz yemine gizli koku sür | `smm_card_094` |
| `smm_card_093` | Çorba Alameti ve Ak Gaga: Yağ Halkası | `bg_taht_salonu.png` | A | `smm_card_093_a` | `choice_smm_card_093_a.png` | Nohut işaretini bozma | `smm_card_094` |
| `smm_card_093` | Çorba Alameti ve Ak Gaga: Yağ Halkası | `bg_taht_salonu.png` | B | `smm_card_093_b` | `choice_smm_card_093_b.png` | Nohutları varis adına diz | `smm_card_095` |
| `smm_card_094` | Çorba Alameti ve Ak Gaga: Ak Gaga'nın Yolu | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_094_a` | `choice_smm_card_094_a.png` | Müneccime ölçüyü göster | `smm_card_095` |
| `smm_card_094` | Çorba Alameti ve Ak Gaga: Ak Gaga'nın Yolu | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_094_b` | `choice_smm_card_094_b.png` | Müneccimi fısıltıyla yönlendir | `smm_card_096` |
| `smm_card_095` | Çorba Alameti ve Ak Gaga: Gümüş Kaşık | `bg_kaz_avlusu.png` | A | `smm_card_095_a` | `choice_smm_card_095_a.png` | Kazın yolunu temiz bırak | `smm_card_096` |
| `smm_card_095` | Çorba Alameti ve Ak Gaga: Gümüş Kaşık | `bg_kaz_avlusu.png` | B | `smm_card_095_b` | `choice_smm_card_095_b.png` | Kazın yolunu perdeyle çevir | `smm_card_098` |
| `smm_card_096` | Çorba Alameti ve Ak Gaga: Kayıp Ölçü | `bg_kaz_kumesi.png` | A | `smm_card_096_a` | `choice_smm_card_096_a.png` | Çorbanın alametini dürüstçe açıkla | `smm_card_097` |
| `smm_card_096` | Çorba Alameti ve Ak Gaga: Kayıp Ölçü | `bg_kaz_kumesi.png` | B | `smm_card_096_b` | `choice_smm_card_096_b.png` | Yeni alameti bilerek pişir | `smm_card_098` |
| `smm_card_097` | Çorba Alameti ve Ak Gaga: Kuyrukta Ekmek | `bg_muneccim_kulesi.png` | A | `smm_card_097_a` | `choice_smm_card_097_a.png` | Ak Gaga'yı sakince besle | `smm_card_098` |
| `smm_card_097` | Çorba Alameti ve Ak Gaga: Kuyrukta Ekmek | `bg_muneccim_kulesi.png` | B | `smm_card_097_b` | `choice_smm_card_097_b.png` | Kaz yemine gizli koku sür | `smm_card_099` |
| `smm_card_098` | Çorba Alameti ve Ak Gaga: Keskin Koku | `bg_divan_salonu.png` | A | `smm_card_098_a` | `choice_smm_card_098_a.png` | Nohut işaretini bozma | `smm_card_099` |
| `smm_card_098` | Çorba Alameti ve Ak Gaga: Keskin Koku | `bg_divan_salonu.png` | B | `smm_card_098_b` | `choice_smm_card_098_b.png` | Nohutları varis adına diz | `smm_card_100` |
| `smm_card_099` | Çorba Alameti ve Ak Gaga: Mühre Giden İz | `bg_taht_salonu.png` | A | `smm_card_099_a` | `choice_smm_card_099_a.png` | Müneccime ölçüyü göster | `smm_card_100` |
| `smm_card_099` | Çorba Alameti ve Ak Gaga: Mühre Giden İz | `bg_taht_salonu.png` | B | `smm_card_099_b` | `choice_smm_card_099_b.png` | Müneccimi fısıltıyla yönlendir | `smm_card_101` |
| `smm_card_100` | Çorba Alameti ve Ak Gaga: Dar Boğaz | `bg_buyuk_mutfak_atesli.png` | A | `smm_card_100_a` | `choice_smm_card_100_a.png` | Kazın yolunu temiz bırak | `smm_card_101` |
| `smm_card_100` | Çorba Alameti ve Ak Gaga: Dar Boğaz | `bg_buyuk_mutfak_atesli.png` | B | `smm_card_100_b` | `choice_smm_card_100_b.png` | Kazın yolunu perdeyle çevir | `smm_card_103` |
| `smm_card_101` | Zehir Tadım Odası: İlk Eşik | `bg_zehir_tadim_odasi.png` | A | `smm_card_101_a` | `choice_smm_card_101_a.png` | Tortuyu Simin'e açıkça göster | `smm_card_102` |
| `smm_card_101` | Zehir Tadım Odası: İlk Eşik | `bg_zehir_tadim_odasi.png` | B | `smm_card_101_b` | `choice_smm_card_101_b.png` | Tortuyu gizli koza çevir | `smm_card_103` |
| `smm_card_102` | Zehir Tadım Odası: Sıcak Bakır | `bg_zehir_tadim_odasi_gece.png` | A | `smm_card_102_a` | `choice_smm_card_102_a.png` | Panzehiri doğru kişiye ulaştır | `smm_card_103` |
| `smm_card_102` | Zehir Tadım Odası: Sıcak Bakır | `bg_zehir_tadim_odasi_gece.png` | B | `smm_card_102_b` | `choice_smm_card_102_b.png` | Panzehir kavanozunu sakla | `smm_card_104` |
| `smm_card_103` | Zehir Tadım Odası: Sessiz Bakış | `bg_baharat_odasi_safran.png` | A | `smm_card_103_a` | `choice_smm_card_103_a.png` | Kadehi usulünce önce kendin tat | `smm_card_104` |
| `smm_card_103` | Zehir Tadım Odası: Sessiz Bakış | `bg_baharat_odasi_safran.png` | B | `smm_card_103_b` | `choice_smm_card_103_b.png` | Kadehi rakibe önce uzat | `smm_card_105` |
| `smm_card_104` | Zehir Tadım Odası: İnce Hesap | `bg_taht_salonu.png` | A | `smm_card_104_a` | `choice_smm_card_104_a.png` | Zehir defterini eksiksiz tut | `smm_card_105` |
| `smm_card_104` | Zehir Tadım Odası: İnce Hesap | `bg_taht_salonu.png` | B | `smm_card_104_b` | `choice_smm_card_104_b.png` | Defterdeki satırı değiştir | `smm_card_106` |
| `smm_card_105` | Zehir Tadım Odası: Gizli Lekeler | `bg_sifahane_ic_odasi.png` | A | `smm_card_105_a` | `choice_smm_card_105_a.png` | Şüpheli baharatı mühürlet | `smm_card_106` |
| `smm_card_105` | Zehir Tadım Odası: Gizli Lekeler | `bg_sifahane_ic_odasi.png` | B | `smm_card_105_b` | `choice_smm_card_105_b.png` | Baharatı karaborsaya sızdır | `smm_card_108` |
| `smm_card_106` | Zehir Tadım Odası: Kör Alev | `bg_zehir_tadim_odasi.png` | A | `smm_card_106_a` | `choice_smm_card_106_a.png` | Tortuyu Simin'e açıkça göster | `smm_card_107` |
| `smm_card_106` | Zehir Tadım Odası: Kör Alev | `bg_zehir_tadim_odasi.png` | B | `smm_card_106_b` | `choice_smm_card_106_b.png` | Tortuyu gizli koza çevir | `smm_card_108` |
| `smm_card_107` | Zehir Tadım Odası: Üç Nohut | `bg_zehir_tadim_odasi_gece.png` | A | `smm_card_107_a` | `choice_smm_card_107_a.png` | Panzehiri doğru kişiye ulaştır | `smm_card_108` |
| `smm_card_107` | Zehir Tadım Odası: Üç Nohut | `bg_zehir_tadim_odasi_gece.png` | B | `smm_card_107_b` | `choice_smm_card_107_b.png` | Panzehir kavanozunu sakla | `smm_card_109` |
| `smm_card_108` | Zehir Tadım Odası: Kapıdaki Fısıltı | `bg_baharat_odasi_safran.png` | A | `smm_card_108_a` | `choice_smm_card_108_a.png` | Kadehi usulünce önce kendin tat | `smm_card_109` |
| `smm_card_108` | Zehir Tadım Odası: Kapıdaki Fısıltı | `bg_baharat_odasi_safran.png` | B | `smm_card_108_b` | `choice_smm_card_108_b.png` | Kadehi rakibe önce uzat | `smm_card_110` |
| `smm_card_109` | Zehir Tadım Odası: Aç Karnın Sözü | `bg_taht_salonu.png` | A | `smm_card_109_a` | `choice_smm_card_109_a.png` | Zehir defterini eksiksiz tut | `smm_card_110` |
| `smm_card_109` | Zehir Tadım Odası: Aç Karnın Sözü | `bg_taht_salonu.png` | B | `smm_card_109_b` | `choice_smm_card_109_b.png` | Defterdeki satırı değiştir | `smm_card_111` |
| `smm_card_110` | Zehir Tadım Odası: Ustanın Sınavı | `bg_sifahane_ic_odasi.png` | A | `smm_card_110_a` | `choice_smm_card_110_a.png` | Şüpheli baharatı mühürlet | `smm_card_111` |
| `smm_card_110` | Zehir Tadım Odası: Ustanın Sınavı | `bg_sifahane_ic_odasi.png` | B | `smm_card_110_b` | `choice_smm_card_110_b.png` | Baharatı karaborsaya sızdır | `smm_card_113` |
| `smm_card_111` | Zehir Tadım Odası: Kazanın Gölgesi | `bg_zehir_tadim_odasi.png` | A | `smm_card_111_a` | `choice_smm_card_111_a.png` | Tortuyu Simin'e açıkça göster | `smm_card_112` |
| `smm_card_111` | Zehir Tadım Odası: Kazanın Gölgesi | `bg_zehir_tadim_odasi.png` | B | `smm_card_111_b` | `choice_smm_card_111_b.png` | Tortuyu gizli koza çevir | `smm_card_113` |
| `smm_card_112` | Zehir Tadım Odası: Tuz ve Yemin | `bg_zehir_tadim_odasi_gece.png` | A | `smm_card_112_a` | `choice_smm_card_112_a.png` | Panzehiri doğru kişiye ulaştır | `smm_card_113` |
| `smm_card_112` | Zehir Tadım Odası: Tuz ve Yemin | `bg_zehir_tadim_odasi_gece.png` | B | `smm_card_112_b` | `choice_smm_card_112_b.png` | Panzehir kavanozunu sakla | `smm_card_114` |
| `smm_card_113` | Zehir Tadım Odası: Kırık Tabak | `bg_baharat_odasi_safran.png` | A | `smm_card_113_a` | `choice_smm_card_113_a.png` | Kadehi usulünce önce kendin tat | `smm_card_114` |
| `smm_card_113` | Zehir Tadım Odası: Kırık Tabak | `bg_baharat_odasi_safran.png` | B | `smm_card_113_b` | `choice_smm_card_113_b.png` | Kadehi rakibe önce uzat | `smm_card_115` |
| `smm_card_114` | Zehir Tadım Odası: Mum Işığı | `bg_taht_salonu.png` | A | `smm_card_114_a` | `choice_smm_card_114_a.png` | Zehir defterini eksiksiz tut | `smm_card_115` |
| `smm_card_114` | Zehir Tadım Odası: Mum Işığı | `bg_taht_salonu.png` | B | `smm_card_114_b` | `choice_smm_card_114_b.png` | Defterdeki satırı değiştir | `smm_card_116` |
| `smm_card_115` | Zehir Tadım Odası: Gece Nöbeti | `bg_sifahane_ic_odasi.png` | A | `smm_card_115_a` | `choice_smm_card_115_a.png` | Şüpheli baharatı mühürlet | `smm_card_116` |
| `smm_card_115` | Zehir Tadım Odası: Gece Nöbeti | `bg_sifahane_ic_odasi.png` | B | `smm_card_115_b` | `choice_smm_card_115_b.png` | Baharatı karaborsaya sızdır | `smm_card_118` |
| `smm_card_116` | Zehir Tadım Odası: Kilitli Raf | `bg_zehir_tadim_odasi.png` | A | `smm_card_116_a` | `choice_smm_card_116_a.png` | Tortuyu Simin'e açıkça göster | `smm_card_117` |
| `smm_card_116` | Zehir Tadım Odası: Kilitli Raf | `bg_zehir_tadim_odasi.png` | B | `smm_card_116_b` | `choice_smm_card_116_b.png` | Tortuyu gizli koza çevir | `smm_card_118` |
| `smm_card_117` | Zehir Tadım Odası: Sarayın Kulağı | `bg_zehir_tadim_odasi_gece.png` | A | `smm_card_117_a` | `choice_smm_card_117_a.png` | Panzehiri doğru kişiye ulaştır | `smm_card_118` |
| `smm_card_117` | Zehir Tadım Odası: Sarayın Kulağı | `bg_zehir_tadim_odasi_gece.png` | B | `smm_card_117_b` | `choice_smm_card_117_b.png` | Panzehir kavanozunu sakla | `smm_card_119` |
| `smm_card_118` | Zehir Tadım Odası: Yağ Halkası | `bg_baharat_odasi_safran.png` | A | `smm_card_118_a` | `choice_smm_card_118_a.png` | Kadehi usulünce önce kendin tat | `smm_card_119` |
| `smm_card_118` | Zehir Tadım Odası: Yağ Halkası | `bg_baharat_odasi_safran.png` | B | `smm_card_118_b` | `choice_smm_card_118_b.png` | Kadehi rakibe önce uzat | `smm_card_120` |
| `smm_card_119` | Zehir Tadım Odası: Ak Gaga'nın Yolu | `bg_taht_salonu.png` | A | `smm_card_119_a` | `choice_smm_card_119_a.png` | Zehir defterini eksiksiz tut | `smm_card_120` |
| `smm_card_119` | Zehir Tadım Odası: Ak Gaga'nın Yolu | `bg_taht_salonu.png` | B | `smm_card_119_b` | `choice_smm_card_119_b.png` | Defterdeki satırı değiştir | `smm_card_121` |
| `smm_card_120` | Zehir Tadım Odası: Gümüş Kaşık | `bg_sifahane_ic_odasi.png` | A | `smm_card_120_a` | `choice_smm_card_120_a.png` | Şüpheli baharatı mühürlet | `smm_card_121` |
| `smm_card_120` | Zehir Tadım Odası: Gümüş Kaşık | `bg_sifahane_ic_odasi.png` | B | `smm_card_120_b` | `choice_smm_card_120_b.png` | Baharatı karaborsaya sızdır | `smm_card_123` |
| `smm_card_121` | Zehir Tadım Odası: Kayıp Ölçü | `bg_zehir_tadim_odasi.png` | A | `smm_card_121_a` | `choice_smm_card_121_a.png` | Tortuyu Simin'e açıkça göster | `smm_card_122` |
| `smm_card_121` | Zehir Tadım Odası: Kayıp Ölçü | `bg_zehir_tadim_odasi.png` | B | `smm_card_121_b` | `choice_smm_card_121_b.png` | Tortuyu gizli koza çevir | `smm_card_123` |
| `smm_card_122` | Zehir Tadım Odası: Kuyrukta Ekmek | `bg_zehir_tadim_odasi_gece.png` | A | `smm_card_122_a` | `choice_smm_card_122_a.png` | Panzehiri doğru kişiye ulaştır | `smm_card_123` |
| `smm_card_122` | Zehir Tadım Odası: Kuyrukta Ekmek | `bg_zehir_tadim_odasi_gece.png` | B | `smm_card_122_b` | `choice_smm_card_122_b.png` | Panzehir kavanozunu sakla | `smm_card_124` |
| `smm_card_123` | Zehir Tadım Odası: Keskin Koku | `bg_baharat_odasi_safran.png` | A | `smm_card_123_a` | `choice_smm_card_123_a.png` | Kadehi usulünce önce kendin tat | `smm_card_124` |
| `smm_card_123` | Zehir Tadım Odası: Keskin Koku | `bg_baharat_odasi_safran.png` | B | `smm_card_123_b` | `choice_smm_card_123_b.png` | Kadehi rakibe önce uzat | `smm_card_125` |
| `smm_card_124` | Zehir Tadım Odası: Mühre Giden İz | `bg_taht_salonu.png` | A | `smm_card_124_a` | `choice_smm_card_124_a.png` | Zehir defterini eksiksiz tut | `smm_card_125` |
| `smm_card_124` | Zehir Tadım Odası: Mühre Giden İz | `bg_taht_salonu.png` | B | `smm_card_124_b` | `choice_smm_card_124_b.png` | Defterdeki satırı değiştir | `smm_card_126` |
| `smm_card_125` | Zehir Tadım Odası: Dar Boğaz | `bg_sifahane_ic_odasi.png` | A | `smm_card_125_a` | `choice_smm_card_125_a.png` | Şüpheli baharatı mühürlet | `smm_card_126` |
| `smm_card_125` | Zehir Tadım Odası: Dar Boğaz | `bg_sifahane_ic_odasi.png` | B | `smm_card_125_b` | `choice_smm_card_125_b.png` | Baharatı karaborsaya sızdır | `smm_card_128` |
| `smm_card_126` | Şifahane Yolu: İlk Eşik | `bg_sifahane_avlusu.png` | A | `smm_card_126_a` | `choice_smm_card_126_a.png` | Yaralıyı kayda geçirerek tedavi et | `smm_card_127` |
| `smm_card_126` | Şifahane Yolu: İlk Eşik | `bg_sifahane_avlusu.png` | B | `smm_card_126_b` | `choice_smm_card_126_b.png` | Yarayı siyasi sır yap | `smm_card_128` |
| `smm_card_127` | Şifahane Yolu: Sıcak Bakır | `bg_sifahane_ic_odasi.png` | A | `smm_card_127_a` | `choice_smm_card_127_a.png` | Panzehiri halka açık hazırla | `smm_card_128` |
| `smm_card_127` | Şifahane Yolu: Sıcak Bakır | `bg_sifahane_ic_odasi.png` | B | `smm_card_127_b` | `choice_smm_card_127_b.png` | Panzehiri yalnız varise ayır | `smm_card_129` |
| `smm_card_128` | Şifahane Yolu: Sessiz Bakış | `bg_yarali_asker_cadiri.png` | A | `smm_card_128_a` | `choice_smm_card_128_a.png` | Bera'nın yeminini koru | `smm_card_129` |
| `smm_card_128` | Şifahane Yolu: Sessiz Bakış | `bg_yarali_asker_cadiri.png` | B | `smm_card_128_b` | `choice_smm_card_128_b.png` | Bera'nın defterini kopyala | `smm_card_130` |
| `smm_card_129` | Şifahane Yolu: İnce Hesap | `bg_yeralti_sarnici.png` | A | `smm_card_129_a` | `choice_smm_card_129_a.png` | Otları doğru dozda karıştır | `smm_card_130` |
| `smm_card_129` | Şifahane Yolu: İnce Hesap | `bg_yeralti_sarnici.png` | B | `smm_card_129_b` | `choice_smm_card_129_b.png` | Dozu cesurca artır | `smm_card_131` |
| `smm_card_130` | Şifahane Yolu: Gizli Lekeler | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_130_a` | `choice_smm_card_130_a.png` | Hastayı saraydan sakince çıkar | `smm_card_131` |
| `smm_card_130` | Şifahane Yolu: Gizli Lekeler | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_130_b` | `choice_smm_card_130_b.png` | Hastayı isyancılara yönlendir | `smm_card_133` |
| `smm_card_131` | Şifahane Yolu: Kör Alev | `bg_sifahane_avlusu.png` | A | `smm_card_131_a` | `choice_smm_card_131_a.png` | Yaralıyı kayda geçirerek tedavi et | `smm_card_132` |
| `smm_card_131` | Şifahane Yolu: Kör Alev | `bg_sifahane_avlusu.png` | B | `smm_card_131_b` | `choice_smm_card_131_b.png` | Yarayı siyasi sır yap | `smm_card_133` |
| `smm_card_132` | Şifahane Yolu: Üç Nohut | `bg_sifahane_ic_odasi.png` | A | `smm_card_132_a` | `choice_smm_card_132_a.png` | Panzehiri halka açık hazırla | `smm_card_133` |
| `smm_card_132` | Şifahane Yolu: Üç Nohut | `bg_sifahane_ic_odasi.png` | B | `smm_card_132_b` | `choice_smm_card_132_b.png` | Panzehiri yalnız varise ayır | `smm_card_134` |
| `smm_card_133` | Şifahane Yolu: Kapıdaki Fısıltı | `bg_yarali_asker_cadiri.png` | A | `smm_card_133_a` | `choice_smm_card_133_a.png` | Bera'nın yeminini koru | `smm_card_134` |
| `smm_card_133` | Şifahane Yolu: Kapıdaki Fısıltı | `bg_yarali_asker_cadiri.png` | B | `smm_card_133_b` | `choice_smm_card_133_b.png` | Bera'nın defterini kopyala | `smm_card_135` |
| `smm_card_134` | Şifahane Yolu: Aç Karnın Sözü | `bg_yeralti_sarnici.png` | A | `smm_card_134_a` | `choice_smm_card_134_a.png` | Otları doğru dozda karıştır | `smm_card_135` |
| `smm_card_134` | Şifahane Yolu: Aç Karnın Sözü | `bg_yeralti_sarnici.png` | B | `smm_card_134_b` | `choice_smm_card_134_b.png` | Dozu cesurca artır | `smm_card_136` |
| `smm_card_135` | Şifahane Yolu: Ustanın Sınavı | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_135_a` | `choice_smm_card_135_a.png` | Hastayı saraydan sakince çıkar | `smm_card_136` |
| `smm_card_135` | Şifahane Yolu: Ustanın Sınavı | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_135_b` | `choice_smm_card_135_b.png` | Hastayı isyancılara yönlendir | `smm_card_138` |
| `smm_card_136` | Şifahane Yolu: Kazanın Gölgesi | `bg_sifahane_avlusu.png` | A | `smm_card_136_a` | `choice_smm_card_136_a.png` | Yaralıyı kayda geçirerek tedavi et | `smm_card_137` |
| `smm_card_136` | Şifahane Yolu: Kazanın Gölgesi | `bg_sifahane_avlusu.png` | B | `smm_card_136_b` | `choice_smm_card_136_b.png` | Yarayı siyasi sır yap | `smm_card_138` |
| `smm_card_137` | Şifahane Yolu: Tuz ve Yemin | `bg_sifahane_ic_odasi.png` | A | `smm_card_137_a` | `choice_smm_card_137_a.png` | Panzehiri halka açık hazırla | `smm_card_138` |
| `smm_card_137` | Şifahane Yolu: Tuz ve Yemin | `bg_sifahane_ic_odasi.png` | B | `smm_card_137_b` | `choice_smm_card_137_b.png` | Panzehiri yalnız varise ayır | `smm_card_139` |
| `smm_card_138` | Şifahane Yolu: Kırık Tabak | `bg_yarali_asker_cadiri.png` | A | `smm_card_138_a` | `choice_smm_card_138_a.png` | Bera'nın yeminini koru | `smm_card_139` |
| `smm_card_138` | Şifahane Yolu: Kırık Tabak | `bg_yarali_asker_cadiri.png` | B | `smm_card_138_b` | `choice_smm_card_138_b.png` | Bera'nın defterini kopyala | `smm_card_140` |
| `smm_card_139` | Şifahane Yolu: Mum Işığı | `bg_yeralti_sarnici.png` | A | `smm_card_139_a` | `choice_smm_card_139_a.png` | Otları doğru dozda karıştır | `smm_card_140` |
| `smm_card_139` | Şifahane Yolu: Mum Işığı | `bg_yeralti_sarnici.png` | B | `smm_card_139_b` | `choice_smm_card_139_b.png` | Dozu cesurca artır | `smm_card_141` |
| `smm_card_140` | Şifahane Yolu: Gece Nöbeti | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_140_a` | `choice_smm_card_140_a.png` | Hastayı saraydan sakince çıkar | `smm_card_141` |
| `smm_card_140` | Şifahane Yolu: Gece Nöbeti | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_140_b` | `choice_smm_card_140_b.png` | Hastayı isyancılara yönlendir | `smm_card_143` |
| `smm_card_141` | Şifahane Yolu: Kilitli Raf | `bg_sifahane_avlusu.png` | A | `smm_card_141_a` | `choice_smm_card_141_a.png` | Yaralıyı kayda geçirerek tedavi et | `smm_card_142` |
| `smm_card_141` | Şifahane Yolu: Kilitli Raf | `bg_sifahane_avlusu.png` | B | `smm_card_141_b` | `choice_smm_card_141_b.png` | Yarayı siyasi sır yap | `smm_card_143` |
| `smm_card_142` | Şifahane Yolu: Sarayın Kulağı | `bg_sifahane_ic_odasi.png` | A | `smm_card_142_a` | `choice_smm_card_142_a.png` | Panzehiri halka açık hazırla | `smm_card_143` |
| `smm_card_142` | Şifahane Yolu: Sarayın Kulağı | `bg_sifahane_ic_odasi.png` | B | `smm_card_142_b` | `choice_smm_card_142_b.png` | Panzehiri yalnız varise ayır | `smm_card_144` |
| `smm_card_143` | Şifahane Yolu: Yağ Halkası | `bg_yarali_asker_cadiri.png` | A | `smm_card_143_a` | `choice_smm_card_143_a.png` | Bera'nın yeminini koru | `smm_card_144` |
| `smm_card_143` | Şifahane Yolu: Yağ Halkası | `bg_yarali_asker_cadiri.png` | B | `smm_card_143_b` | `choice_smm_card_143_b.png` | Bera'nın defterini kopyala | `smm_card_145` |
| `smm_card_144` | Şifahane Yolu: Ak Gaga'nın Yolu | `bg_yeralti_sarnici.png` | A | `smm_card_144_a` | `choice_smm_card_144_a.png` | Otları doğru dozda karıştır | `smm_card_145` |
| `smm_card_144` | Şifahane Yolu: Ak Gaga'nın Yolu | `bg_yeralti_sarnici.png` | B | `smm_card_144_b` | `choice_smm_card_144_b.png` | Dozu cesurca artır | `smm_card_146` |
| `smm_card_145` | Şifahane Yolu: Gümüş Kaşık | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_145_a` | `choice_smm_card_145_a.png` | Hastayı saraydan sakince çıkar | `smm_card_146` |
| `smm_card_145` | Şifahane Yolu: Gümüş Kaşık | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_145_b` | `choice_smm_card_145_b.png` | Hastayı isyancılara yönlendir | `smm_card_148` |
| `smm_card_146` | Şifahane Yolu: Kayıp Ölçü | `bg_sifahane_avlusu.png` | A | `smm_card_146_a` | `choice_smm_card_146_a.png` | Yaralıyı kayda geçirerek tedavi et | `smm_card_147` |
| `smm_card_146` | Şifahane Yolu: Kayıp Ölçü | `bg_sifahane_avlusu.png` | B | `smm_card_146_b` | `choice_smm_card_146_b.png` | Yarayı siyasi sır yap | `smm_card_148` |
| `smm_card_147` | Şifahane Yolu: Kuyrukta Ekmek | `bg_sifahane_ic_odasi.png` | A | `smm_card_147_a` | `choice_smm_card_147_a.png` | Panzehiri halka açık hazırla | `smm_card_148` |
| `smm_card_147` | Şifahane Yolu: Kuyrukta Ekmek | `bg_sifahane_ic_odasi.png` | B | `smm_card_147_b` | `choice_smm_card_147_b.png` | Panzehiri yalnız varise ayır | `smm_card_149` |
| `smm_card_148` | Şifahane Yolu: Keskin Koku | `bg_yarali_asker_cadiri.png` | A | `smm_card_148_a` | `choice_smm_card_148_a.png` | Bera'nın yeminini koru | `smm_card_149` |
| `smm_card_148` | Şifahane Yolu: Keskin Koku | `bg_yarali_asker_cadiri.png` | B | `smm_card_148_b` | `choice_smm_card_148_b.png` | Bera'nın defterini kopyala | `smm_card_150` |
| `smm_card_149` | Şifahane Yolu: Mühre Giden İz | `bg_yeralti_sarnici.png` | A | `smm_card_149_a` | `choice_smm_card_149_a.png` | Otları doğru dozda karıştır | `smm_card_150` |
| `smm_card_149` | Şifahane Yolu: Mühre Giden İz | `bg_yeralti_sarnici.png` | B | `smm_card_149_b` | `choice_smm_card_149_b.png` | Dozu cesurca artır | `smm_card_151` |
| `smm_card_150` | Şifahane Yolu: Dar Boğaz | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_150_a` | `choice_smm_card_150_a.png` | Hastayı saraydan sakince çıkar | `smm_card_151` |
| `smm_card_150` | Şifahane Yolu: Dar Boğaz | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_150_b` | `choice_smm_card_150_b.png` | Hastayı isyancılara yönlendir | `smm_card_153` |
| `smm_card_151` | Saray Casusları: İlk Eşik | `bg_gizli_gecit_mumlu.png` | A | `smm_card_151_a` | `choice_smm_card_151_a.png` | Sırrı Rasih'e eksiksiz götür | `smm_card_152` |
| `smm_card_151` | Saray Casusları: İlk Eşik | `bg_gizli_gecit_mumlu.png` | B | `smm_card_151_b` | `choice_smm_card_151_b.png` | Sırrı iki tarafa sat | `smm_card_153` |
| `smm_card_152` | Saray Casusları: Sıcak Bakır | `bg_eski_arsiv_odasi.png` | A | `smm_card_152_a` | `choice_smm_card_152_a.png` | Arşiv notunu mühürle koru | `smm_card_153` |
| `smm_card_152` | Saray Casusları: Sıcak Bakır | `bg_eski_arsiv_odasi.png` | B | `smm_card_152_b` | `choice_smm_card_152_b.png` | Arşiv notunu değiştir | `smm_card_154` |
| `smm_card_153` | Saray Casusları: Sessiz Bakış | `bg_hizmetkar_koridoru.png` | A | `smm_card_153_a` | `choice_smm_card_153_a.png` | Soytarının sözünü dikkatle tart | `smm_card_154` |
| `smm_card_153` | Saray Casusları: Sessiz Bakış | `bg_hizmetkar_koridoru.png` | B | `smm_card_153_b` | `choice_smm_card_153_b.png` | Soytarıyı sahte habere sür | `smm_card_155` |
| `smm_card_154` | Saray Casusları: İnce Hesap | `bg_mutfak_cati_arasi.png` | A | `smm_card_154_a` | `choice_smm_card_154_a.png` | Geçidi kimseye belli etme | `smm_card_155` |
| `smm_card_154` | Saray Casusları: İnce Hesap | `bg_mutfak_cati_arasi.png` | B | `smm_card_154_b` | `choice_smm_card_154_b.png` | Geçide işaret kazı | `smm_card_156` |
| `smm_card_155` | Saray Casusları: Gizli Lekeler | `bg_sessiz_kutuphane.png` | A | `smm_card_155_a` | `choice_smm_card_155_a.png` | Başvezire ölçülü bilgi ver | `smm_card_156` |
| `smm_card_155` | Saray Casusları: Gizli Lekeler | `bg_sessiz_kutuphane.png` | B | `smm_card_155_b` | `choice_smm_card_155_b.png` | Başveziri korkuyla zorla | `smm_card_158` |
| `smm_card_156` | Saray Casusları: Kör Alev | `bg_saray_camasirhanesi.png` | A | `smm_card_156_a` | `choice_smm_card_156_a.png` | Sırrı Rasih'e eksiksiz götür | `smm_card_157` |
| `smm_card_156` | Saray Casusları: Kör Alev | `bg_saray_camasirhanesi.png` | B | `smm_card_156_b` | `choice_smm_card_156_b.png` | Sırrı iki tarafa sat | `smm_card_158` |
| `smm_card_157` | Saray Casusları: Üç Nohut | `bg_gizli_gecit_mumlu.png` | A | `smm_card_157_a` | `choice_smm_card_157_a.png` | Arşiv notunu mühürle koru | `smm_card_158` |
| `smm_card_157` | Saray Casusları: Üç Nohut | `bg_gizli_gecit_mumlu.png` | B | `smm_card_157_b` | `choice_smm_card_157_b.png` | Arşiv notunu değiştir | `smm_card_159` |
| `smm_card_158` | Saray Casusları: Kapıdaki Fısıltı | `bg_eski_arsiv_odasi.png` | A | `smm_card_158_a` | `choice_smm_card_158_a.png` | Soytarının sözünü dikkatle tart | `smm_card_159` |
| `smm_card_158` | Saray Casusları: Kapıdaki Fısıltı | `bg_eski_arsiv_odasi.png` | B | `smm_card_158_b` | `choice_smm_card_158_b.png` | Soytarıyı sahte habere sür | `smm_card_160` |
| `smm_card_159` | Saray Casusları: Aç Karnın Sözü | `bg_hizmetkar_koridoru.png` | A | `smm_card_159_a` | `choice_smm_card_159_a.png` | Geçidi kimseye belli etme | `smm_card_160` |
| `smm_card_159` | Saray Casusları: Aç Karnın Sözü | `bg_hizmetkar_koridoru.png` | B | `smm_card_159_b` | `choice_smm_card_159_b.png` | Geçide işaret kazı | `smm_card_161` |
| `smm_card_160` | Saray Casusları: Ustanın Sınavı | `bg_mutfak_cati_arasi.png` | A | `smm_card_160_a` | `choice_smm_card_160_a.png` | Başvezire ölçülü bilgi ver | `smm_card_161` |
| `smm_card_160` | Saray Casusları: Ustanın Sınavı | `bg_mutfak_cati_arasi.png` | B | `smm_card_160_b` | `choice_smm_card_160_b.png` | Başveziri korkuyla zorla | `smm_card_163` |
| `smm_card_161` | Saray Casusları: Kazanın Gölgesi | `bg_sessiz_kutuphane.png` | A | `smm_card_161_a` | `choice_smm_card_161_a.png` | Sırrı Rasih'e eksiksiz götür | `smm_card_162` |
| `smm_card_161` | Saray Casusları: Kazanın Gölgesi | `bg_sessiz_kutuphane.png` | B | `smm_card_161_b` | `choice_smm_card_161_b.png` | Sırrı iki tarafa sat | `smm_card_163` |
| `smm_card_162` | Saray Casusları: Tuz ve Yemin | `bg_saray_camasirhanesi.png` | A | `smm_card_162_a` | `choice_smm_card_162_a.png` | Arşiv notunu mühürle koru | `smm_card_163` |
| `smm_card_162` | Saray Casusları: Tuz ve Yemin | `bg_saray_camasirhanesi.png` | B | `smm_card_162_b` | `choice_smm_card_162_b.png` | Arşiv notunu değiştir | `smm_card_164` |
| `smm_card_163` | Saray Casusları: Kırık Tabak | `bg_gizli_gecit_mumlu.png` | A | `smm_card_163_a` | `choice_smm_card_163_a.png` | Soytarının sözünü dikkatle tart | `smm_card_164` |
| `smm_card_163` | Saray Casusları: Kırık Tabak | `bg_gizli_gecit_mumlu.png` | B | `smm_card_163_b` | `choice_smm_card_163_b.png` | Soytarıyı sahte habere sür | `smm_card_165` |
| `smm_card_164` | Saray Casusları: Mum Işığı | `bg_eski_arsiv_odasi.png` | A | `smm_card_164_a` | `choice_smm_card_164_a.png` | Geçidi kimseye belli etme | `smm_card_165` |
| `smm_card_164` | Saray Casusları: Mum Işığı | `bg_eski_arsiv_odasi.png` | B | `smm_card_164_b` | `choice_smm_card_164_b.png` | Geçide işaret kazı | `smm_card_166` |
| `smm_card_165` | Saray Casusları: Gece Nöbeti | `bg_hizmetkar_koridoru.png` | A | `smm_card_165_a` | `choice_smm_card_165_a.png` | Başvezire ölçülü bilgi ver | `smm_card_166` |
| `smm_card_165` | Saray Casusları: Gece Nöbeti | `bg_hizmetkar_koridoru.png` | B | `smm_card_165_b` | `choice_smm_card_165_b.png` | Başveziri korkuyla zorla | `smm_card_168` |
| `smm_card_166` | Saray Casusları: Kilitli Raf | `bg_mutfak_cati_arasi.png` | A | `smm_card_166_a` | `choice_smm_card_166_a.png` | Sırrı Rasih'e eksiksiz götür | `smm_card_167` |
| `smm_card_166` | Saray Casusları: Kilitli Raf | `bg_mutfak_cati_arasi.png` | B | `smm_card_166_b` | `choice_smm_card_166_b.png` | Sırrı iki tarafa sat | `smm_card_168` |
| `smm_card_167` | Saray Casusları: Sarayın Kulağı | `bg_sessiz_kutuphane.png` | A | `smm_card_167_a` | `choice_smm_card_167_a.png` | Arşiv notunu mühürle koru | `smm_card_168` |
| `smm_card_167` | Saray Casusları: Sarayın Kulağı | `bg_sessiz_kutuphane.png` | B | `smm_card_167_b` | `choice_smm_card_167_b.png` | Arşiv notunu değiştir | `smm_card_169` |
| `smm_card_168` | Saray Casusları: Yağ Halkası | `bg_saray_camasirhanesi.png` | A | `smm_card_168_a` | `choice_smm_card_168_a.png` | Soytarının sözünü dikkatle tart | `smm_card_169` |
| `smm_card_168` | Saray Casusları: Yağ Halkası | `bg_saray_camasirhanesi.png` | B | `smm_card_168_b` | `choice_smm_card_168_b.png` | Soytarıyı sahte habere sür | `smm_card_170` |
| `smm_card_169` | Saray Casusları: Ak Gaga'nın Yolu | `bg_gizli_gecit_mumlu.png` | A | `smm_card_169_a` | `choice_smm_card_169_a.png` | Geçidi kimseye belli etme | `smm_card_170` |
| `smm_card_169` | Saray Casusları: Ak Gaga'nın Yolu | `bg_gizli_gecit_mumlu.png` | B | `smm_card_169_b` | `choice_smm_card_169_b.png` | Geçide işaret kazı | `smm_card_171` |
| `smm_card_170` | Saray Casusları: Gümüş Kaşık | `bg_eski_arsiv_odasi.png` | A | `smm_card_170_a` | `choice_smm_card_170_a.png` | Başvezire ölçülü bilgi ver | `smm_card_171` |
| `smm_card_170` | Saray Casusları: Gümüş Kaşık | `bg_eski_arsiv_odasi.png` | B | `smm_card_170_b` | `choice_smm_card_170_b.png` | Başveziri korkuyla zorla | `smm_card_173` |
| `smm_card_171` | Saray Casusları: Kayıp Ölçü | `bg_hizmetkar_koridoru.png` | A | `smm_card_171_a` | `choice_smm_card_171_a.png` | Sırrı Rasih'e eksiksiz götür | `smm_card_172` |
| `smm_card_171` | Saray Casusları: Kayıp Ölçü | `bg_hizmetkar_koridoru.png` | B | `smm_card_171_b` | `choice_smm_card_171_b.png` | Sırrı iki tarafa sat | `smm_card_173` |
| `smm_card_172` | Saray Casusları: Kuyrukta Ekmek | `bg_mutfak_cati_arasi.png` | A | `smm_card_172_a` | `choice_smm_card_172_a.png` | Arşiv notunu mühürle koru | `smm_card_173` |
| `smm_card_172` | Saray Casusları: Kuyrukta Ekmek | `bg_mutfak_cati_arasi.png` | B | `smm_card_172_b` | `choice_smm_card_172_b.png` | Arşiv notunu değiştir | `smm_card_174` |
| `smm_card_173` | Saray Casusları: Keskin Koku | `bg_sessiz_kutuphane.png` | A | `smm_card_173_a` | `choice_smm_card_173_a.png` | Soytarının sözünü dikkatle tart | `smm_card_174` |
| `smm_card_173` | Saray Casusları: Keskin Koku | `bg_sessiz_kutuphane.png` | B | `smm_card_173_b` | `choice_smm_card_173_b.png` | Soytarıyı sahte habere sür | `smm_card_175` |
| `smm_card_174` | Saray Casusları: Mühre Giden İz | `bg_saray_camasirhanesi.png` | A | `smm_card_174_a` | `choice_smm_card_174_a.png` | Geçidi kimseye belli etme | `smm_card_175` |
| `smm_card_174` | Saray Casusları: Mühre Giden İz | `bg_saray_camasirhanesi.png` | B | `smm_card_174_b` | `choice_smm_card_174_b.png` | Geçide işaret kazı | `smm_card_176` |
| `smm_card_175` | Saray Casusları: Dar Boğaz | `bg_gizli_gecit_mumlu.png` | A | `smm_card_175_a` | `choice_smm_card_175_a.png` | Başvezire ölçülü bilgi ver | `smm_card_176` |
| `smm_card_175` | Saray Casusları: Dar Boğaz | `bg_gizli_gecit_mumlu.png` | B | `smm_card_175_b` | `choice_smm_card_175_b.png` | Başveziri korkuyla zorla | `smm_card_178` |
| `smm_card_176` | Ordu İaşesi: İlk Eşik | `bg_ordu_iase_deposu.png` | A | `smm_card_176_a` | `choice_smm_card_176_a.png` | Tahılı deftere dürüstçe işle | `smm_card_177` |
| `smm_card_176` | Ordu İaşesi: İlk Eşik | `bg_ordu_iase_deposu.png` | B | `smm_card_176_b` | `choice_smm_card_176_b.png` | Tahılı halka gizlice ayır | `smm_card_178` |
| `smm_card_177` | Ordu İaşesi: Sıcak Bakır | `bg_kisla_kapisi.png` | A | `smm_card_177_a` | `choice_smm_card_177_a.png` | Asker payını eksiksiz gönder | `smm_card_178` |
| `smm_card_177` | Ordu İaşesi: Sıcak Bakır | `bg_kisla_kapisi.png` | B | `smm_card_177_b` | `choice_smm_card_177_b.png` | Asker payını varise bağla | `smm_card_179` |
| `smm_card_178` | Ordu İaşesi: Sessiz Bakış | `bg_tahil_kervani_yolu.png` | A | `smm_card_178_a` | `choice_smm_card_178_a.png` | Kervanı güvenli yoldan geçir | `smm_card_179` |
| `smm_card_178` | Ordu İaşesi: Sessiz Bakış | `bg_tahil_kervani_yolu.png` | B | `smm_card_178_b` | `choice_smm_card_178_b.png` | Kervanı karaborsa yoluna kır | `smm_card_180` |
| `smm_card_179` | Ordu İaşesi: İnce Hesap | `bg_liman_tahil_rihtimi.png` | A | `smm_card_179_a` | `choice_smm_card_179_a.png` | Turgut'un hesabını savun | `smm_card_180` |
| `smm_card_179` | Ordu İaşesi: İnce Hesap | `bg_liman_tahil_rihtimi.png` | B | `smm_card_179_b` | `choice_smm_card_179_b.png` | Turgut'u borçla sustur | `smm_card_181` |
| `smm_card_180` | Ordu İaşesi: Gizli Lekeler | `bg_yarali_asker_cadiri.png` | A | `smm_card_180_a` | `choice_smm_card_180_a.png` | Kıtlığı divana açık bildir | `smm_card_181` |
| `smm_card_180` | Ordu İaşesi: Gizli Lekeler | `bg_yarali_asker_cadiri.png` | B | `smm_card_180_b` | `choice_smm_card_180_b.png` | Kıtlığı rakipten sakla | `smm_card_183` |
| `smm_card_181` | Ordu İaşesi: Kör Alev | `bg_ordu_iase_deposu.png` | A | `smm_card_181_a` | `choice_smm_card_181_a.png` | Tahılı deftere dürüstçe işle | `smm_card_182` |
| `smm_card_181` | Ordu İaşesi: Kör Alev | `bg_ordu_iase_deposu.png` | B | `smm_card_181_b` | `choice_smm_card_181_b.png` | Tahılı halka gizlice ayır | `smm_card_183` |
| `smm_card_182` | Ordu İaşesi: Üç Nohut | `bg_kisla_kapisi.png` | A | `smm_card_182_a` | `choice_smm_card_182_a.png` | Asker payını eksiksiz gönder | `smm_card_183` |
| `smm_card_182` | Ordu İaşesi: Üç Nohut | `bg_kisla_kapisi.png` | B | `smm_card_182_b` | `choice_smm_card_182_b.png` | Asker payını varise bağla | `smm_card_184` |
| `smm_card_183` | Ordu İaşesi: Kapıdaki Fısıltı | `bg_tahil_kervani_yolu.png` | A | `smm_card_183_a` | `choice_smm_card_183_a.png` | Kervanı güvenli yoldan geçir | `smm_card_184` |
| `smm_card_183` | Ordu İaşesi: Kapıdaki Fısıltı | `bg_tahil_kervani_yolu.png` | B | `smm_card_183_b` | `choice_smm_card_183_b.png` | Kervanı karaborsa yoluna kır | `smm_card_185` |
| `smm_card_184` | Ordu İaşesi: Aç Karnın Sözü | `bg_liman_tahil_rihtimi.png` | A | `smm_card_184_a` | `choice_smm_card_184_a.png` | Turgut'un hesabını savun | `smm_card_185` |
| `smm_card_184` | Ordu İaşesi: Aç Karnın Sözü | `bg_liman_tahil_rihtimi.png` | B | `smm_card_184_b` | `choice_smm_card_184_b.png` | Turgut'u borçla sustur | `smm_card_186` |
| `smm_card_185` | Ordu İaşesi: Ustanın Sınavı | `bg_yarali_asker_cadiri.png` | A | `smm_card_185_a` | `choice_smm_card_185_a.png` | Kıtlığı divana açık bildir | `smm_card_186` |
| `smm_card_185` | Ordu İaşesi: Ustanın Sınavı | `bg_yarali_asker_cadiri.png` | B | `smm_card_185_b` | `choice_smm_card_185_b.png` | Kıtlığı rakipten sakla | `smm_card_188` |
| `smm_card_186` | Ordu İaşesi: Kazanın Gölgesi | `bg_ordu_iase_deposu.png` | A | `smm_card_186_a` | `choice_smm_card_186_a.png` | Tahılı deftere dürüstçe işle | `smm_card_187` |
| `smm_card_186` | Ordu İaşesi: Kazanın Gölgesi | `bg_ordu_iase_deposu.png` | B | `smm_card_186_b` | `choice_smm_card_186_b.png` | Tahılı halka gizlice ayır | `smm_card_188` |
| `smm_card_187` | Ordu İaşesi: Tuz ve Yemin | `bg_kisla_kapisi.png` | A | `smm_card_187_a` | `choice_smm_card_187_a.png` | Asker payını eksiksiz gönder | `smm_card_188` |
| `smm_card_187` | Ordu İaşesi: Tuz ve Yemin | `bg_kisla_kapisi.png` | B | `smm_card_187_b` | `choice_smm_card_187_b.png` | Asker payını varise bağla | `smm_card_189` |
| `smm_card_188` | Ordu İaşesi: Kırık Tabak | `bg_tahil_kervani_yolu.png` | A | `smm_card_188_a` | `choice_smm_card_188_a.png` | Kervanı güvenli yoldan geçir | `smm_card_189` |
| `smm_card_188` | Ordu İaşesi: Kırık Tabak | `bg_tahil_kervani_yolu.png` | B | `smm_card_188_b` | `choice_smm_card_188_b.png` | Kervanı karaborsa yoluna kır | `smm_card_190` |
| `smm_card_189` | Ordu İaşesi: Mum Işığı | `bg_liman_tahil_rihtimi.png` | A | `smm_card_189_a` | `choice_smm_card_189_a.png` | Turgut'un hesabını savun | `smm_card_190` |
| `smm_card_189` | Ordu İaşesi: Mum Işığı | `bg_liman_tahil_rihtimi.png` | B | `smm_card_189_b` | `choice_smm_card_189_b.png` | Turgut'u borçla sustur | `smm_card_191` |
| `smm_card_190` | Ordu İaşesi: Gece Nöbeti | `bg_yarali_asker_cadiri.png` | A | `smm_card_190_a` | `choice_smm_card_190_a.png` | Kıtlığı divana açık bildir | `smm_card_191` |
| `smm_card_190` | Ordu İaşesi: Gece Nöbeti | `bg_yarali_asker_cadiri.png` | B | `smm_card_190_b` | `choice_smm_card_190_b.png` | Kıtlığı rakipten sakla | `smm_card_193` |
| `smm_card_191` | Ordu İaşesi: Kilitli Raf | `bg_ordu_iase_deposu.png` | A | `smm_card_191_a` | `choice_smm_card_191_a.png` | Tahılı deftere dürüstçe işle | `smm_card_192` |
| `smm_card_191` | Ordu İaşesi: Kilitli Raf | `bg_ordu_iase_deposu.png` | B | `smm_card_191_b` | `choice_smm_card_191_b.png` | Tahılı halka gizlice ayır | `smm_card_193` |
| `smm_card_192` | Ordu İaşesi: Sarayın Kulağı | `bg_kisla_kapisi.png` | A | `smm_card_192_a` | `choice_smm_card_192_a.png` | Asker payını eksiksiz gönder | `smm_card_193` |
| `smm_card_192` | Ordu İaşesi: Sarayın Kulağı | `bg_kisla_kapisi.png` | B | `smm_card_192_b` | `choice_smm_card_192_b.png` | Asker payını varise bağla | `smm_card_194` |
| `smm_card_193` | Ordu İaşesi: Yağ Halkası | `bg_tahil_kervani_yolu.png` | A | `smm_card_193_a` | `choice_smm_card_193_a.png` | Kervanı güvenli yoldan geçir | `smm_card_194` |
| `smm_card_193` | Ordu İaşesi: Yağ Halkası | `bg_tahil_kervani_yolu.png` | B | `smm_card_193_b` | `choice_smm_card_193_b.png` | Kervanı karaborsa yoluna kır | `smm_card_195` |
| `smm_card_194` | Ordu İaşesi: Ak Gaga'nın Yolu | `bg_liman_tahil_rihtimi.png` | A | `smm_card_194_a` | `choice_smm_card_194_a.png` | Turgut'un hesabını savun | `smm_card_195` |
| `smm_card_194` | Ordu İaşesi: Ak Gaga'nın Yolu | `bg_liman_tahil_rihtimi.png` | B | `smm_card_194_b` | `choice_smm_card_194_b.png` | Turgut'u borçla sustur | `smm_card_196` |
| `smm_card_195` | Ordu İaşesi: Gümüş Kaşık | `bg_yarali_asker_cadiri.png` | A | `smm_card_195_a` | `choice_smm_card_195_a.png` | Kıtlığı divana açık bildir | `smm_card_196` |
| `smm_card_195` | Ordu İaşesi: Gümüş Kaşık | `bg_yarali_asker_cadiri.png` | B | `smm_card_195_b` | `choice_smm_card_195_b.png` | Kıtlığı rakipten sakla | `smm_card_198` |
| `smm_card_196` | Ordu İaşesi: Kayıp Ölçü | `bg_ordu_iase_deposu.png` | A | `smm_card_196_a` | `choice_smm_card_196_a.png` | Tahılı deftere dürüstçe işle | `smm_card_197` |
| `smm_card_196` | Ordu İaşesi: Kayıp Ölçü | `bg_ordu_iase_deposu.png` | B | `smm_card_196_b` | `choice_smm_card_196_b.png` | Tahılı halka gizlice ayır | `smm_card_198` |
| `smm_card_197` | Ordu İaşesi: Kuyrukta Ekmek | `bg_kisla_kapisi.png` | A | `smm_card_197_a` | `choice_smm_card_197_a.png` | Asker payını eksiksiz gönder | `smm_card_198` |
| `smm_card_197` | Ordu İaşesi: Kuyrukta Ekmek | `bg_kisla_kapisi.png` | B | `smm_card_197_b` | `choice_smm_card_197_b.png` | Asker payını varise bağla | `smm_card_199` |
| `smm_card_198` | Ordu İaşesi: Keskin Koku | `bg_tahil_kervani_yolu.png` | A | `smm_card_198_a` | `choice_smm_card_198_a.png` | Kervanı güvenli yoldan geçir | `smm_card_199` |
| `smm_card_198` | Ordu İaşesi: Keskin Koku | `bg_tahil_kervani_yolu.png` | B | `smm_card_198_b` | `choice_smm_card_198_b.png` | Kervanı karaborsa yoluna kır | `smm_card_200` |
| `smm_card_199` | Ordu İaşesi: Mühre Giden İz | `bg_liman_tahil_rihtimi.png` | A | `smm_card_199_a` | `choice_smm_card_199_a.png` | Turgut'un hesabını savun | `smm_card_200` |
| `smm_card_199` | Ordu İaşesi: Mühre Giden İz | `bg_liman_tahil_rihtimi.png` | B | `smm_card_199_b` | `choice_smm_card_199_b.png` | Turgut'u borçla sustur | `smm_card_201` |
| `smm_card_200` | Ordu İaşesi: Dar Boğaz | `bg_yarali_asker_cadiri.png` | A | `smm_card_200_a` | `choice_smm_card_200_a.png` | Kıtlığı divana açık bildir | `smm_card_201` |
| `smm_card_200` | Ordu İaşesi: Dar Boğaz | `bg_yarali_asker_cadiri.png` | B | `smm_card_200_b` | `choice_smm_card_200_b.png` | Kıtlığı rakipten sakla | `smm_card_203` |
| `smm_card_201` | Sır Pazarı: İlk Eşik | `bg_arka_sokak_karaborsa.png` | A | `smm_card_201_a` | `choice_smm_card_201_a.png` | Borcu küçük sırla kapat | `smm_card_202` |
| `smm_card_201` | Sır Pazarı: İlk Eşik | `bg_arka_sokak_karaborsa.png` | B | `smm_card_201_b` | `choice_smm_card_201_b.png` | Sırrı yüksek bedelle sat | `smm_card_203` |
| `smm_card_202` | Sır Pazarı: Sıcak Bakır | `bg_sehir_pazari.png` | A | `smm_card_202_a` | `choice_smm_card_202_a.png` | Sahte mührü reddet | `smm_card_203` |
| `smm_card_202` | Sır Pazarı: Sıcak Bakır | `bg_sehir_pazari.png` | B | `smm_card_202_b` | `choice_smm_card_202_b.png` | Sahte mühür kalıbı al | `smm_card_204` |
| `smm_card_203` | Sır Pazarı: Sessiz Bakış | `bg_gizli_muhur_atolyesi.png` | A | `smm_card_203_a` | `choice_smm_card_203_a.png` | Yusuf'un defterini ezberle | `smm_card_204` |
| `smm_card_203` | Sır Pazarı: Sessiz Bakış | `bg_gizli_muhur_atolyesi.png` | B | `smm_card_203_b` | `choice_smm_card_203_b.png` | Yusuf'a yeni borç yazdır | `smm_card_205` |
| `smm_card_204` | Sır Pazarı: İnce Hesap | `bg_bakircilar_carsisi.png` | A | `smm_card_204_a` | `choice_smm_card_204_a.png` | Anahtarı sahibine geri götür | `smm_card_205` |
| `smm_card_204` | Sır Pazarı: İnce Hesap | `bg_bakircilar_carsisi.png` | B | `smm_card_204_b` | `choice_smm_card_204_b.png` | Anahtarı gece pazarlık et | `smm_card_206` |
| `smm_card_205` | Sır Pazarı: Gizli Lekeler | `bg_kiler_gizli_raflar.png` | A | `smm_card_205_a` | `choice_smm_card_205_a.png` | Söylentiyi Rasih'e bildir | `smm_card_206` |
| `smm_card_205` | Sır Pazarı: Gizli Lekeler | `bg_kiler_gizli_raflar.png` | B | `smm_card_205_b` | `choice_smm_card_205_b.png` | Söylentiyi varise taşı | `smm_card_208` |
| `smm_card_206` | Sır Pazarı: Kör Alev | `bg_arka_sokak_karaborsa.png` | A | `smm_card_206_a` | `choice_smm_card_206_a.png` | Borcu küçük sırla kapat | `smm_card_207` |
| `smm_card_206` | Sır Pazarı: Kör Alev | `bg_arka_sokak_karaborsa.png` | B | `smm_card_206_b` | `choice_smm_card_206_b.png` | Sırrı yüksek bedelle sat | `smm_card_208` |
| `smm_card_207` | Sır Pazarı: Üç Nohut | `bg_sehir_pazari.png` | A | `smm_card_207_a` | `choice_smm_card_207_a.png` | Sahte mührü reddet | `smm_card_208` |
| `smm_card_207` | Sır Pazarı: Üç Nohut | `bg_sehir_pazari.png` | B | `smm_card_207_b` | `choice_smm_card_207_b.png` | Sahte mühür kalıbı al | `smm_card_209` |
| `smm_card_208` | Sır Pazarı: Kapıdaki Fısıltı | `bg_gizli_muhur_atolyesi.png` | A | `smm_card_208_a` | `choice_smm_card_208_a.png` | Yusuf'un defterini ezberle | `smm_card_209` |
| `smm_card_208` | Sır Pazarı: Kapıdaki Fısıltı | `bg_gizli_muhur_atolyesi.png` | B | `smm_card_208_b` | `choice_smm_card_208_b.png` | Yusuf'a yeni borç yazdır | `smm_card_210` |
| `smm_card_209` | Sır Pazarı: Aç Karnın Sözü | `bg_bakircilar_carsisi.png` | A | `smm_card_209_a` | `choice_smm_card_209_a.png` | Anahtarı sahibine geri götür | `smm_card_210` |
| `smm_card_209` | Sır Pazarı: Aç Karnın Sözü | `bg_bakircilar_carsisi.png` | B | `smm_card_209_b` | `choice_smm_card_209_b.png` | Anahtarı gece pazarlık et | `smm_card_211` |
| `smm_card_210` | Sır Pazarı: Ustanın Sınavı | `bg_kiler_gizli_raflar.png` | A | `smm_card_210_a` | `choice_smm_card_210_a.png` | Söylentiyi Rasih'e bildir | `smm_card_211` |
| `smm_card_210` | Sır Pazarı: Ustanın Sınavı | `bg_kiler_gizli_raflar.png` | B | `smm_card_210_b` | `choice_smm_card_210_b.png` | Söylentiyi varise taşı | `smm_card_213` |
| `smm_card_211` | Sır Pazarı: Kazanın Gölgesi | `bg_arka_sokak_karaborsa.png` | A | `smm_card_211_a` | `choice_smm_card_211_a.png` | Borcu küçük sırla kapat | `smm_card_212` |
| `smm_card_211` | Sır Pazarı: Kazanın Gölgesi | `bg_arka_sokak_karaborsa.png` | B | `smm_card_211_b` | `choice_smm_card_211_b.png` | Sırrı yüksek bedelle sat | `smm_card_213` |
| `smm_card_212` | Sır Pazarı: Tuz ve Yemin | `bg_sehir_pazari.png` | A | `smm_card_212_a` | `choice_smm_card_212_a.png` | Sahte mührü reddet | `smm_card_213` |
| `smm_card_212` | Sır Pazarı: Tuz ve Yemin | `bg_sehir_pazari.png` | B | `smm_card_212_b` | `choice_smm_card_212_b.png` | Sahte mühür kalıbı al | `smm_card_214` |
| `smm_card_213` | Sır Pazarı: Kırık Tabak | `bg_gizli_muhur_atolyesi.png` | A | `smm_card_213_a` | `choice_smm_card_213_a.png` | Yusuf'un defterini ezberle | `smm_card_214` |
| `smm_card_213` | Sır Pazarı: Kırık Tabak | `bg_gizli_muhur_atolyesi.png` | B | `smm_card_213_b` | `choice_smm_card_213_b.png` | Yusuf'a yeni borç yazdır | `smm_card_215` |
| `smm_card_214` | Sır Pazarı: Mum Işığı | `bg_bakircilar_carsisi.png` | A | `smm_card_214_a` | `choice_smm_card_214_a.png` | Anahtarı sahibine geri götür | `smm_card_215` |
| `smm_card_214` | Sır Pazarı: Mum Işığı | `bg_bakircilar_carsisi.png` | B | `smm_card_214_b` | `choice_smm_card_214_b.png` | Anahtarı gece pazarlık et | `smm_card_216` |
| `smm_card_215` | Sır Pazarı: Gece Nöbeti | `bg_kiler_gizli_raflar.png` | A | `smm_card_215_a` | `choice_smm_card_215_a.png` | Söylentiyi Rasih'e bildir | `smm_card_216` |
| `smm_card_215` | Sır Pazarı: Gece Nöbeti | `bg_kiler_gizli_raflar.png` | B | `smm_card_215_b` | `choice_smm_card_215_b.png` | Söylentiyi varise taşı | `smm_card_218` |
| `smm_card_216` | Sır Pazarı: Kilitli Raf | `bg_arka_sokak_karaborsa.png` | A | `smm_card_216_a` | `choice_smm_card_216_a.png` | Borcu küçük sırla kapat | `smm_card_217` |
| `smm_card_216` | Sır Pazarı: Kilitli Raf | `bg_arka_sokak_karaborsa.png` | B | `smm_card_216_b` | `choice_smm_card_216_b.png` | Sırrı yüksek bedelle sat | `smm_card_218` |
| `smm_card_217` | Sır Pazarı: Sarayın Kulağı | `bg_sehir_pazari.png` | A | `smm_card_217_a` | `choice_smm_card_217_a.png` | Sahte mührü reddet | `smm_card_218` |
| `smm_card_217` | Sır Pazarı: Sarayın Kulağı | `bg_sehir_pazari.png` | B | `smm_card_217_b` | `choice_smm_card_217_b.png` | Sahte mühür kalıbı al | `smm_card_219` |
| `smm_card_218` | Sır Pazarı: Yağ Halkası | `bg_gizli_muhur_atolyesi.png` | A | `smm_card_218_a` | `choice_smm_card_218_a.png` | Yusuf'un defterini ezberle | `smm_card_219` |
| `smm_card_218` | Sır Pazarı: Yağ Halkası | `bg_gizli_muhur_atolyesi.png` | B | `smm_card_218_b` | `choice_smm_card_218_b.png` | Yusuf'a yeni borç yazdır | `smm_card_220` |
| `smm_card_219` | Sır Pazarı: Ak Gaga'nın Yolu | `bg_bakircilar_carsisi.png` | A | `smm_card_219_a` | `choice_smm_card_219_a.png` | Anahtarı sahibine geri götür | `smm_card_220` |
| `smm_card_219` | Sır Pazarı: Ak Gaga'nın Yolu | `bg_bakircilar_carsisi.png` | B | `smm_card_219_b` | `choice_smm_card_219_b.png` | Anahtarı gece pazarlık et | `smm_card_221` |
| `smm_card_220` | Sır Pazarı: Gümüş Kaşık | `bg_kiler_gizli_raflar.png` | A | `smm_card_220_a` | `choice_smm_card_220_a.png` | Söylentiyi Rasih'e bildir | `smm_card_221` |
| `smm_card_220` | Sır Pazarı: Gümüş Kaşık | `bg_kiler_gizli_raflar.png` | B | `smm_card_220_b` | `choice_smm_card_220_b.png` | Söylentiyi varise taşı | `smm_card_223` |
| `smm_card_221` | Sır Pazarı: Kayıp Ölçü | `bg_arka_sokak_karaborsa.png` | A | `smm_card_221_a` | `choice_smm_card_221_a.png` | Borcu küçük sırla kapat | `smm_card_222` |
| `smm_card_221` | Sır Pazarı: Kayıp Ölçü | `bg_arka_sokak_karaborsa.png` | B | `smm_card_221_b` | `choice_smm_card_221_b.png` | Sırrı yüksek bedelle sat | `smm_card_223` |
| `smm_card_222` | Sır Pazarı: Kuyrukta Ekmek | `bg_sehir_pazari.png` | A | `smm_card_222_a` | `choice_smm_card_222_a.png` | Sahte mührü reddet | `smm_card_223` |
| `smm_card_222` | Sır Pazarı: Kuyrukta Ekmek | `bg_sehir_pazari.png` | B | `smm_card_222_b` | `choice_smm_card_222_b.png` | Sahte mühür kalıbı al | `smm_card_224` |
| `smm_card_223` | Sır Pazarı: Keskin Koku | `bg_gizli_muhur_atolyesi.png` | A | `smm_card_223_a` | `choice_smm_card_223_a.png` | Yusuf'un defterini ezberle | `smm_card_224` |
| `smm_card_223` | Sır Pazarı: Keskin Koku | `bg_gizli_muhur_atolyesi.png` | B | `smm_card_223_b` | `choice_smm_card_223_b.png` | Yusuf'a yeni borç yazdır | `smm_card_225` |
| `smm_card_224` | Sır Pazarı: Mühre Giden İz | `bg_bakircilar_carsisi.png` | A | `smm_card_224_a` | `choice_smm_card_224_a.png` | Anahtarı sahibine geri götür | `smm_card_225` |
| `smm_card_224` | Sır Pazarı: Mühre Giden İz | `bg_bakircilar_carsisi.png` | B | `smm_card_224_b` | `choice_smm_card_224_b.png` | Anahtarı gece pazarlık et | `smm_card_226` |
| `smm_card_225` | Sır Pazarı: Dar Boğaz | `bg_kiler_gizli_raflar.png` | A | `smm_card_225_a` | `choice_smm_card_225_a.png` | Söylentiyi Rasih'e bildir | `smm_card_226` |
| `smm_card_225` | Sır Pazarı: Dar Boğaz | `bg_kiler_gizli_raflar.png` | B | `smm_card_225_b` | `choice_smm_card_225_b.png` | Söylentiyi varise taşı | `smm_card_228` |
| `smm_card_226` | Halk Ayaklanması: İlk Eşik | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_226_a` | `choice_smm_card_226_a.png` | Ekmeği halka düzenli ulaştır | `smm_card_227` |
| `smm_card_226` | Halk Ayaklanması: İlk Eşik | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_226_b` | `choice_smm_card_226_b.png` | Ekmeği isyan silahı yap | `smm_card_228` |
| `smm_card_227` | Halk Ayaklanması: Sıcak Bakır | `bg_sarap_mahzeni_isyan.png` | A | `smm_card_227_a` | `choice_smm_card_227_a.png` | Bildiriyi şiddetsiz çağrı yap | `smm_card_228` |
| `smm_card_227` | Halk Ayaklanması: Sıcak Bakır | `bg_sarap_mahzeni_isyan.png` | B | `smm_card_227_b` | `choice_smm_card_227_b.png` | Bildiriyi ateşli yaz | `smm_card_229` |
| `smm_card_228` | Halk Ayaklanması: Sessiz Bakış | `bg_halk_meydani.png` | A | `smm_card_228_a` | `choice_smm_card_228_a.png` | Meryem'e kaçış yolu çiz | `smm_card_229` |
| `smm_card_228` | Halk Ayaklanması: Sessiz Bakış | `bg_halk_meydani.png` | B | `smm_card_228_b` | `choice_smm_card_228_b.png` | Meryem'i varise yaklaştır | `smm_card_230` |
| `smm_card_229` | Halk Ayaklanması: İnce Hesap | `bg_bahar_festivali_sokak.png` | A | `smm_card_229_a` | `choice_smm_card_229_a.png` | Kemal'den açık söz iste | `smm_card_230` |
| `smm_card_229` | Halk Ayaklanması: İnce Hesap | `bg_bahar_festivali_sokak.png` | B | `smm_card_229_b` | `choice_smm_card_229_b.png` | Kemal'in sözünü abart | `smm_card_231` |
| `smm_card_230` | Halk Ayaklanması: Gizli Lekeler | `bg_yeralti_sarnici.png` | A | `smm_card_230_a` | `choice_smm_card_230_a.png` | Mahzeni gizlice koru | `smm_card_231` |
| `smm_card_230` | Halk Ayaklanması: Gizli Lekeler | `bg_yeralti_sarnici.png` | B | `smm_card_230_b` | `choice_smm_card_230_b.png` | Mahzeni saraya ihbar et | `smm_card_233` |
| `smm_card_231` | Halk Ayaklanması: Kör Alev | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_231_a` | `choice_smm_card_231_a.png` | Ekmeği halka düzenli ulaştır | `smm_card_232` |
| `smm_card_231` | Halk Ayaklanması: Kör Alev | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_231_b` | `choice_smm_card_231_b.png` | Ekmeği isyan silahı yap | `smm_card_233` |
| `smm_card_232` | Halk Ayaklanması: Üç Nohut | `bg_sarap_mahzeni_isyan.png` | A | `smm_card_232_a` | `choice_smm_card_232_a.png` | Bildiriyi şiddetsiz çağrı yap | `smm_card_233` |
| `smm_card_232` | Halk Ayaklanması: Üç Nohut | `bg_sarap_mahzeni_isyan.png` | B | `smm_card_232_b` | `choice_smm_card_232_b.png` | Bildiriyi ateşli yaz | `smm_card_234` |
| `smm_card_233` | Halk Ayaklanması: Kapıdaki Fısıltı | `bg_halk_meydani.png` | A | `smm_card_233_a` | `choice_smm_card_233_a.png` | Meryem'e kaçış yolu çiz | `smm_card_234` |
| `smm_card_233` | Halk Ayaklanması: Kapıdaki Fısıltı | `bg_halk_meydani.png` | B | `smm_card_233_b` | `choice_smm_card_233_b.png` | Meryem'i varise yaklaştır | `smm_card_235` |
| `smm_card_234` | Halk Ayaklanması: Aç Karnın Sözü | `bg_bahar_festivali_sokak.png` | A | `smm_card_234_a` | `choice_smm_card_234_a.png` | Kemal'den açık söz iste | `smm_card_235` |
| `smm_card_234` | Halk Ayaklanması: Aç Karnın Sözü | `bg_bahar_festivali_sokak.png` | B | `smm_card_234_b` | `choice_smm_card_234_b.png` | Kemal'in sözünü abart | `smm_card_236` |
| `smm_card_235` | Halk Ayaklanması: Ustanın Sınavı | `bg_yeralti_sarnici.png` | A | `smm_card_235_a` | `choice_smm_card_235_a.png` | Mahzeni gizlice koru | `smm_card_236` |
| `smm_card_235` | Halk Ayaklanması: Ustanın Sınavı | `bg_yeralti_sarnici.png` | B | `smm_card_235_b` | `choice_smm_card_235_b.png` | Mahzeni saraya ihbar et | `smm_card_238` |
| `smm_card_236` | Halk Ayaklanması: Kazanın Gölgesi | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_236_a` | `choice_smm_card_236_a.png` | Ekmeği halka düzenli ulaştır | `smm_card_237` |
| `smm_card_236` | Halk Ayaklanması: Kazanın Gölgesi | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_236_b` | `choice_smm_card_236_b.png` | Ekmeği isyan silahı yap | `smm_card_238` |
| `smm_card_237` | Halk Ayaklanması: Tuz ve Yemin | `bg_sarap_mahzeni_isyan.png` | A | `smm_card_237_a` | `choice_smm_card_237_a.png` | Bildiriyi şiddetsiz çağrı yap | `smm_card_238` |
| `smm_card_237` | Halk Ayaklanması: Tuz ve Yemin | `bg_sarap_mahzeni_isyan.png` | B | `smm_card_237_b` | `choice_smm_card_237_b.png` | Bildiriyi ateşli yaz | `smm_card_239` |
| `smm_card_238` | Halk Ayaklanması: Kırık Tabak | `bg_halk_meydani.png` | A | `smm_card_238_a` | `choice_smm_card_238_a.png` | Meryem'e kaçış yolu çiz | `smm_card_239` |
| `smm_card_238` | Halk Ayaklanması: Kırık Tabak | `bg_halk_meydani.png` | B | `smm_card_238_b` | `choice_smm_card_238_b.png` | Meryem'i varise yaklaştır | `smm_card_240` |
| `smm_card_239` | Halk Ayaklanması: Mum Işığı | `bg_bahar_festivali_sokak.png` | A | `smm_card_239_a` | `choice_smm_card_239_a.png` | Kemal'den açık söz iste | `smm_card_240` |
| `smm_card_239` | Halk Ayaklanması: Mum Işığı | `bg_bahar_festivali_sokak.png` | B | `smm_card_239_b` | `choice_smm_card_239_b.png` | Kemal'in sözünü abart | `smm_card_241` |
| `smm_card_240` | Halk Ayaklanması: Gece Nöbeti | `bg_yeralti_sarnici.png` | A | `smm_card_240_a` | `choice_smm_card_240_a.png` | Mahzeni gizlice koru | `smm_card_241` |
| `smm_card_240` | Halk Ayaklanması: Gece Nöbeti | `bg_yeralti_sarnici.png` | B | `smm_card_240_b` | `choice_smm_card_240_b.png` | Mahzeni saraya ihbar et | `smm_card_243` |
| `smm_card_241` | Halk Ayaklanması: Kilitli Raf | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_241_a` | `choice_smm_card_241_a.png` | Ekmeği halka düzenli ulaştır | `smm_card_242` |
| `smm_card_241` | Halk Ayaklanması: Kilitli Raf | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_241_b` | `choice_smm_card_241_b.png` | Ekmeği isyan silahı yap | `smm_card_243` |
| `smm_card_242` | Halk Ayaklanması: Sarayın Kulağı | `bg_sarap_mahzeni_isyan.png` | A | `smm_card_242_a` | `choice_smm_card_242_a.png` | Bildiriyi şiddetsiz çağrı yap | `smm_card_243` |
| `smm_card_242` | Halk Ayaklanması: Sarayın Kulağı | `bg_sarap_mahzeni_isyan.png` | B | `smm_card_242_b` | `choice_smm_card_242_b.png` | Bildiriyi ateşli yaz | `smm_card_244` |
| `smm_card_243` | Halk Ayaklanması: Yağ Halkası | `bg_halk_meydani.png` | A | `smm_card_243_a` | `choice_smm_card_243_a.png` | Meryem'e kaçış yolu çiz | `smm_card_244` |
| `smm_card_243` | Halk Ayaklanması: Yağ Halkası | `bg_halk_meydani.png` | B | `smm_card_243_b` | `choice_smm_card_243_b.png` | Meryem'i varise yaklaştır | `smm_card_245` |
| `smm_card_244` | Halk Ayaklanması: Ak Gaga'nın Yolu | `bg_bahar_festivali_sokak.png` | A | `smm_card_244_a` | `choice_smm_card_244_a.png` | Kemal'den açık söz iste | `smm_card_245` |
| `smm_card_244` | Halk Ayaklanması: Ak Gaga'nın Yolu | `bg_bahar_festivali_sokak.png` | B | `smm_card_244_b` | `choice_smm_card_244_b.png` | Kemal'in sözünü abart | `smm_card_246` |
| `smm_card_245` | Halk Ayaklanması: Gümüş Kaşık | `bg_yeralti_sarnici.png` | A | `smm_card_245_a` | `choice_smm_card_245_a.png` | Mahzeni gizlice koru | `smm_card_246` |
| `smm_card_245` | Halk Ayaklanması: Gümüş Kaşık | `bg_yeralti_sarnici.png` | B | `smm_card_245_b` | `choice_smm_card_245_b.png` | Mahzeni saraya ihbar et | `smm_card_248` |
| `smm_card_246` | Halk Ayaklanması: Kayıp Ölçü | `bg_halk_ekmek_kuyrugu.png` | A | `smm_card_246_a` | `choice_smm_card_246_a.png` | Ekmeği halka düzenli ulaştır | `smm_card_247` |
| `smm_card_246` | Halk Ayaklanması: Kayıp Ölçü | `bg_halk_ekmek_kuyrugu.png` | B | `smm_card_246_b` | `choice_smm_card_246_b.png` | Ekmeği isyan silahı yap | `smm_card_248` |
| `smm_card_247` | Halk Ayaklanması: Kuyrukta Ekmek | `bg_sarap_mahzeni_isyan.png` | A | `smm_card_247_a` | `choice_smm_card_247_a.png` | Bildiriyi şiddetsiz çağrı yap | `smm_card_248` |
| `smm_card_247` | Halk Ayaklanması: Kuyrukta Ekmek | `bg_sarap_mahzeni_isyan.png` | B | `smm_card_247_b` | `choice_smm_card_247_b.png` | Bildiriyi ateşli yaz | `smm_card_249` |
| `smm_card_248` | Halk Ayaklanması: Keskin Koku | `bg_halk_meydani.png` | A | `smm_card_248_a` | `choice_smm_card_248_a.png` | Meryem'e kaçış yolu çiz | `smm_card_249` |
| `smm_card_248` | Halk Ayaklanması: Keskin Koku | `bg_halk_meydani.png` | B | `smm_card_248_b` | `choice_smm_card_248_b.png` | Meryem'i varise yaklaştır | `smm_card_250` |
| `smm_card_249` | Halk Ayaklanması: Mühre Giden İz | `bg_bahar_festivali_sokak.png` | A | `smm_card_249_a` | `choice_smm_card_249_a.png` | Kemal'den açık söz iste | `smm_card_250` |
| `smm_card_249` | Halk Ayaklanması: Mühre Giden İz | `bg_bahar_festivali_sokak.png` | B | `smm_card_249_b` | `choice_smm_card_249_b.png` | Kemal'in sözünü abart | `smm_card_251` |
| `smm_card_250` | Halk Ayaklanması: Dar Boğaz | `bg_yeralti_sarnici.png` | A | `smm_card_250_a` | `choice_smm_card_250_a.png` | Mahzeni gizlice koru | `smm_card_251` |
| `smm_card_250` | Halk Ayaklanması: Dar Boğaz | `bg_yeralti_sarnici.png` | B | `smm_card_250_b` | `choice_smm_card_250_b.png` | Mahzeni saraya ihbar et | `smm_card_253` |
| `smm_card_251` | Varisler ve Fraksiyonlar: İlk Eşik | `bg_divan_salonu.png` | A | `smm_card_251_a` | `choice_smm_card_251_a.png` | Varisleri aynı masaya çağır | `smm_card_252` |
| `smm_card_251` | Varisler ve Fraksiyonlar: İlk Eşik | `bg_divan_salonu.png` | B | `smm_card_251_b` | `choice_smm_card_251_b.png` | Bir varisi gizlice güçlendir | `smm_card_253` |
| `smm_card_252` | Varisler ve Fraksiyonlar: Sıcak Bakır | `bg_taht_salonu.png` | A | `smm_card_252_a` | `choice_smm_card_252_a.png` | Arslan'a şartlı destek ver | `smm_card_253` |
| `smm_card_252` | Varisler ve Fraksiyonlar: Sıcak Bakır | `bg_taht_salonu.png` | B | `smm_card_252_b` | `choice_smm_card_252_b.png` | Arslan'a ordu defterini göster | `smm_card_254` |
| `smm_card_253` | Varisler ve Fraksiyonlar: Sessiz Bakış | `bg_elcilik_salonu.png` | A | `smm_card_253_a` | `choice_smm_card_253_a.png` | Safira'nın dış ittifakını tart | `smm_card_254` |
| `smm_card_253` | Varisler ve Fraksiyonlar: Sessiz Bakış | `bg_elcilik_salonu.png` | B | `smm_card_253_b` | `choice_smm_card_253_b.png` | Safira'ya elçi yüzüğünü sun | `smm_card_255` |
| `smm_card_254` | Varisler ve Fraksiyonlar: İnce Hesap | `bg_harem_arkasi_fisiltisi.png` | A | `smm_card_254_a` | `choice_smm_card_254_a.png` | Kemal'in halk sözünü kayda geçir | `smm_card_255` |
| `smm_card_254` | Varisler ve Fraksiyonlar: İnce Hesap | `bg_harem_arkasi_fisiltisi.png` | B | `smm_card_254_b` | `choice_smm_card_254_b.png` | Kemal'e isyan kapısını aç | `smm_card_256` |
| `smm_card_255` | Varisler ve Fraksiyonlar: Gizli Lekeler | `bg_kaz_avlusu.png` | A | `smm_card_255_a` | `choice_smm_card_255_a.png` | Başvezire tarafsız kalacağını söyle | `smm_card_256` |
| `smm_card_255` | Varisler ve Fraksiyonlar: Gizli Lekeler | `bg_kaz_avlusu.png` | B | `smm_card_255_b` | `choice_smm_card_255_b.png` | Başvezirin mührünü zorla | `smm_card_258` |
| `smm_card_256` | Varisler ve Fraksiyonlar: Kör Alev | `bg_muhur_odasi.png` | A | `smm_card_256_a` | `choice_smm_card_256_a.png` | Varisleri aynı masaya çağır | `smm_card_257` |
| `smm_card_256` | Varisler ve Fraksiyonlar: Kör Alev | `bg_muhur_odasi.png` | B | `smm_card_256_b` | `choice_smm_card_256_b.png` | Bir varisi gizlice güçlendir | `smm_card_258` |
| `smm_card_257` | Varisler ve Fraksiyonlar: Üç Nohut | `bg_sessiz_kutuphane.png` | A | `smm_card_257_a` | `choice_smm_card_257_a.png` | Arslan'a şartlı destek ver | `smm_card_258` |
| `smm_card_257` | Varisler ve Fraksiyonlar: Üç Nohut | `bg_sessiz_kutuphane.png` | B | `smm_card_257_b` | `choice_smm_card_257_b.png` | Arslan'a ordu defterini göster | `smm_card_259` |
| `smm_card_258` | Varisler ve Fraksiyonlar: Kapıdaki Fısıltı | `bg_divan_salonu.png` | A | `smm_card_258_a` | `choice_smm_card_258_a.png` | Safira'nın dış ittifakını tart | `smm_card_259` |
| `smm_card_258` | Varisler ve Fraksiyonlar: Kapıdaki Fısıltı | `bg_divan_salonu.png` | B | `smm_card_258_b` | `choice_smm_card_258_b.png` | Safira'ya elçi yüzüğünü sun | `smm_card_260` |
| `smm_card_259` | Varisler ve Fraksiyonlar: Aç Karnın Sözü | `bg_taht_salonu.png` | A | `smm_card_259_a` | `choice_smm_card_259_a.png` | Kemal'in halk sözünü kayda geçir | `smm_card_260` |
| `smm_card_259` | Varisler ve Fraksiyonlar: Aç Karnın Sözü | `bg_taht_salonu.png` | B | `smm_card_259_b` | `choice_smm_card_259_b.png` | Kemal'e isyan kapısını aç | `smm_card_261` |
| `smm_card_260` | Varisler ve Fraksiyonlar: Ustanın Sınavı | `bg_elcilik_salonu.png` | A | `smm_card_260_a` | `choice_smm_card_260_a.png` | Başvezire tarafsız kalacağını söyle | `smm_card_261` |
| `smm_card_260` | Varisler ve Fraksiyonlar: Ustanın Sınavı | `bg_elcilik_salonu.png` | B | `smm_card_260_b` | `choice_smm_card_260_b.png` | Başvezirin mührünü zorla | `smm_card_263` |
| `smm_card_261` | Varisler ve Fraksiyonlar: Kazanın Gölgesi | `bg_harem_arkasi_fisiltisi.png` | A | `smm_card_261_a` | `choice_smm_card_261_a.png` | Varisleri aynı masaya çağır | `smm_card_262` |
| `smm_card_261` | Varisler ve Fraksiyonlar: Kazanın Gölgesi | `bg_harem_arkasi_fisiltisi.png` | B | `smm_card_261_b` | `choice_smm_card_261_b.png` | Bir varisi gizlice güçlendir | `smm_card_263` |
| `smm_card_262` | Varisler ve Fraksiyonlar: Tuz ve Yemin | `bg_kaz_avlusu.png` | A | `smm_card_262_a` | `choice_smm_card_262_a.png` | Arslan'a şartlı destek ver | `smm_card_263` |
| `smm_card_262` | Varisler ve Fraksiyonlar: Tuz ve Yemin | `bg_kaz_avlusu.png` | B | `smm_card_262_b` | `choice_smm_card_262_b.png` | Arslan'a ordu defterini göster | `smm_card_264` |
| `smm_card_263` | Varisler ve Fraksiyonlar: Kırık Tabak | `bg_muhur_odasi.png` | A | `smm_card_263_a` | `choice_smm_card_263_a.png` | Safira'nın dış ittifakını tart | `smm_card_264` |
| `smm_card_263` | Varisler ve Fraksiyonlar: Kırık Tabak | `bg_muhur_odasi.png` | B | `smm_card_263_b` | `choice_smm_card_263_b.png` | Safira'ya elçi yüzüğünü sun | `smm_card_265` |
| `smm_card_264` | Varisler ve Fraksiyonlar: Mum Işığı | `bg_sessiz_kutuphane.png` | A | `smm_card_264_a` | `choice_smm_card_264_a.png` | Kemal'in halk sözünü kayda geçir | `smm_card_265` |
| `smm_card_264` | Varisler ve Fraksiyonlar: Mum Işığı | `bg_sessiz_kutuphane.png` | B | `smm_card_264_b` | `choice_smm_card_264_b.png` | Kemal'e isyan kapısını aç | `smm_card_266` |
| `smm_card_265` | Varisler ve Fraksiyonlar: Gece Nöbeti | `bg_divan_salonu.png` | A | `smm_card_265_a` | `choice_smm_card_265_a.png` | Başvezire tarafsız kalacağını söyle | `smm_card_266` |
| `smm_card_265` | Varisler ve Fraksiyonlar: Gece Nöbeti | `bg_divan_salonu.png` | B | `smm_card_265_b` | `choice_smm_card_265_b.png` | Başvezirin mührünü zorla | `smm_card_268` |
| `smm_card_266` | Varisler ve Fraksiyonlar: Kilitli Raf | `bg_taht_salonu.png` | A | `smm_card_266_a` | `choice_smm_card_266_a.png` | Varisleri aynı masaya çağır | `smm_card_267` |
| `smm_card_266` | Varisler ve Fraksiyonlar: Kilitli Raf | `bg_taht_salonu.png` | B | `smm_card_266_b` | `choice_smm_card_266_b.png` | Bir varisi gizlice güçlendir | `smm_card_268` |
| `smm_card_267` | Varisler ve Fraksiyonlar: Sarayın Kulağı | `bg_elcilik_salonu.png` | A | `smm_card_267_a` | `choice_smm_card_267_a.png` | Arslan'a şartlı destek ver | `smm_card_268` |
| `smm_card_267` | Varisler ve Fraksiyonlar: Sarayın Kulağı | `bg_elcilik_salonu.png` | B | `smm_card_267_b` | `choice_smm_card_267_b.png` | Arslan'a ordu defterini göster | `smm_card_269` |
| `smm_card_268` | Varisler ve Fraksiyonlar: Yağ Halkası | `bg_harem_arkasi_fisiltisi.png` | A | `smm_card_268_a` | `choice_smm_card_268_a.png` | Safira'nın dış ittifakını tart | `smm_card_269` |
| `smm_card_268` | Varisler ve Fraksiyonlar: Yağ Halkası | `bg_harem_arkasi_fisiltisi.png` | B | `smm_card_268_b` | `choice_smm_card_268_b.png` | Safira'ya elçi yüzüğünü sun | `smm_card_270` |
| `smm_card_269` | Varisler ve Fraksiyonlar: Ak Gaga'nın Yolu | `bg_kaz_avlusu.png` | A | `smm_card_269_a` | `choice_smm_card_269_a.png` | Kemal'in halk sözünü kayda geçir | `smm_card_270` |
| `smm_card_269` | Varisler ve Fraksiyonlar: Ak Gaga'nın Yolu | `bg_kaz_avlusu.png` | B | `smm_card_269_b` | `choice_smm_card_269_b.png` | Kemal'e isyan kapısını aç | `smm_card_271` |
| `smm_card_270` | Varisler ve Fraksiyonlar: Gümüş Kaşık | `bg_muhur_odasi.png` | A | `smm_card_270_a` | `choice_smm_card_270_a.png` | Başvezire tarafsız kalacağını söyle | `smm_card_271` |
| `smm_card_270` | Varisler ve Fraksiyonlar: Gümüş Kaşık | `bg_muhur_odasi.png` | B | `smm_card_270_b` | `choice_smm_card_270_b.png` | Başvezirin mührünü zorla | `smm_card_273` |
| `smm_card_271` | Varisler ve Fraksiyonlar: Kayıp Ölçü | `bg_sessiz_kutuphane.png` | A | `smm_card_271_a` | `choice_smm_card_271_a.png` | Varisleri aynı masaya çağır | `smm_card_272` |
| `smm_card_271` | Varisler ve Fraksiyonlar: Kayıp Ölçü | `bg_sessiz_kutuphane.png` | B | `smm_card_271_b` | `choice_smm_card_271_b.png` | Bir varisi gizlice güçlendir | `smm_card_273` |
| `smm_card_272` | Varisler ve Fraksiyonlar: Kuyrukta Ekmek | `bg_divan_salonu.png` | A | `smm_card_272_a` | `choice_smm_card_272_a.png` | Arslan'a şartlı destek ver | `smm_card_273` |
| `smm_card_272` | Varisler ve Fraksiyonlar: Kuyrukta Ekmek | `bg_divan_salonu.png` | B | `smm_card_272_b` | `choice_smm_card_272_b.png` | Arslan'a ordu defterini göster | `smm_card_274` |
| `smm_card_273` | Varisler ve Fraksiyonlar: Keskin Koku | `bg_taht_salonu.png` | A | `smm_card_273_a` | `choice_smm_card_273_a.png` | Safira'nın dış ittifakını tart | `smm_card_274` |
| `smm_card_273` | Varisler ve Fraksiyonlar: Keskin Koku | `bg_taht_salonu.png` | B | `smm_card_273_b` | `choice_smm_card_273_b.png` | Mührü başvezir yetkisine bağla | `` |
| `smm_card_274` | Varisler ve Fraksiyonlar: Mühre Giden İz | `bg_elcilik_salonu.png` | A | `smm_card_274_a` | `choice_smm_card_274_a.png` | Kemal'in halk sözünü kayda geçir | `smm_card_275` |
| `smm_card_274` | Varisler ve Fraksiyonlar: Mühre Giden İz | `bg_elcilik_salonu.png` | B | `smm_card_274_b` | `choice_smm_card_274_b.png` | Sofrayı hükümdarın güvenine sun | `` |
| `smm_card_275` | Varisler ve Fraksiyonlar: Dar Boğaz | `bg_harem_arkasi_fisiltisi.png` | A | `smm_card_275_a` | `choice_smm_card_275_a.png` | Başvezire tarafsız kalacağını söyle | `smm_card_276` |
| `smm_card_275` | Varisler ve Fraksiyonlar: Dar Boğaz | `bg_harem_arkasi_fisiltisi.png` | B | `smm_card_275_b` | `choice_smm_card_275_b.png` | Casus ağını tek elde topla | `` |
| `smm_card_276` | Mühür ve Sonlar: İlk Eşik | `bg_final_sabahi_saray_avlusu.png` | A | `smm_card_276_a` | `choice_smm_card_276_a.png` | Mühür odasına bir adım yaklaş | `smm_card_277` |
| `smm_card_276` | Mühür ve Sonlar: İlk Eşik | `bg_final_sabahi_saray_avlusu.png` | B | `smm_card_276_b` | `choice_smm_card_276_b.png` | Halk kapısını son kez aç | `` |
| `smm_card_277` | Mühür ve Sonlar: Sıcak Bakır | `bg_muhur_odasi.png` | A | `smm_card_277_a` | `choice_smm_card_277_a.png` | Geçmiş borçları tek tek tart | `smm_card_278` |
| `smm_card_277` | Mühür ve Sonlar: Sıcak Bakır | `bg_muhur_odasi.png` | B | `smm_card_277_b` | `choice_smm_card_277_b.png` | Yeni hükümdarı mühürle çıkar | `` |
| `smm_card_278` | Mühür ve Sonlar: Sessiz Bakış | `bg_muhur_odasi_kapanis.png` | A | `smm_card_278_a` | `choice_smm_card_278_a.png` | Varisleri son kez dinle | `smm_card_279` |
| `smm_card_278` | Mühür ve Sonlar: Sessiz Bakış | `bg_muhur_odasi_kapanis.png` | B | `smm_card_278_b` | `choice_smm_card_278_b.png` | Mühürden vazgeçip hana kaç | `` |
| `smm_card_279` | Mühür ve Sonlar: İnce Hesap | `bg_taht_salonu.png` | A | `smm_card_279_a` | `choice_smm_card_279_a.png` | Kazın işaretini kayıt altına al | `smm_card_280` |
| `smm_card_279` | Mühür ve Sonlar: İnce Hesap | `bg_taht_salonu.png` | B | `smm_card_279_b` | `choice_smm_card_279_b.png` | Şüpheli kadehi yalnız tat | `` |
| `smm_card_280` | Mühür ve Sonlar: Gizli Lekeler | `bg_mahkeme_avlusu.png` | A | `smm_card_280_a` | `choice_smm_card_280_a.png` | Çorba alametini sorguya aç | `smm_card_281` |
| `smm_card_280` | Mühür ve Sonlar: Gizli Lekeler | `bg_mahkeme_avlusu.png` | B | `smm_card_280_b` | `choice_smm_card_280_b.png` | Yanlış varisin emrine güven | `` |
| `smm_card_281` | Mühür ve Sonlar: Kör Alev | `bg_kapanis_han_mutfagi.png` | A | `smm_card_281_a` | `choice_smm_card_281_a.png` | Mühür odasına bir adım yaklaş | `smm_card_282` |
| `smm_card_281` | Mühür ve Sonlar: Kör Alev | `bg_kapanis_han_mutfagi.png` | B | `smm_card_281_b` | `choice_smm_card_281_b.png` | Ak Gaga'nın işaretini yorumla | `` |
| `smm_card_282` | Mühür ve Sonlar: Üç Nohut | `bg_final_sabahi_saray_avlusu.png` | A | `smm_card_282_a` | `choice_smm_card_282_a.png` | Geçmiş borçları tek tek tart | `smm_card_283` |
| `smm_card_282` | Mühür ve Sonlar: Üç Nohut | `bg_final_sabahi_saray_avlusu.png` | B | `smm_card_282_b` | `choice_smm_card_282_b.png` | Çorbayı kutsal alamet ilan et | `` |
| `smm_card_283` | Mühür ve Sonlar: Kapıdaki Fısıltı | `bg_muhur_odasi.png` | A | `smm_card_283_a` | `choice_smm_card_283_a.png` | Varisleri son kez dinle | `smm_card_284` |
| `smm_card_283` | Mühür ve Sonlar: Kapıdaki Fısıltı | `bg_muhur_odasi.png` | B | `smm_card_283_b` | `choice_smm_card_283_b.png` | Tatlı ittifakını evlilikle mühürle | `` |
| `smm_card_284` | Mühür ve Sonlar: Aç Karnın Sözü | `bg_muhur_odasi_kapanis.png` | A | `smm_card_284_a` | `choice_smm_card_284_a.png` | Kazın işaretini kayıt altına al | `smm_card_285` |
| `smm_card_284` | Mühür ve Sonlar: Aç Karnın Sözü | `bg_muhur_odasi_kapanis.png` | B | `smm_card_284_b` | `choice_smm_card_284_b.png` | Tahıl defterlerini devral | `` |
| `smm_card_285` | Mühür ve Sonlar: Ustanın Sınavı | `bg_taht_salonu.png` | A | `smm_card_285_a` | `choice_smm_card_285_a.png` | Çorba alametini sorguya aç | `smm_card_286` |
| `smm_card_285` | Mühür ve Sonlar: Ustanın Sınavı | `bg_taht_salonu.png` | B | `smm_card_285_b` | `choice_smm_card_285_b.png` | Sır pazarını tekeline al | `` |
| `smm_card_286` | Mühür ve Sonlar: Kazanın Gölgesi | `bg_mahkeme_avlusu.png` | A | `smm_card_286_a` | `choice_smm_card_286_a.png` | Mühür odasına bir adım yaklaş | `smm_card_287` |
| `smm_card_286` | Mühür ve Sonlar: Kazanın Gölgesi | `bg_mahkeme_avlusu.png` | B | `smm_card_286_b` | `choice_smm_card_286_b.png` | Şifahane sürgününü kabul et | `` |
| `smm_card_287` | Mühür ve Sonlar: Tuz ve Yemin | `bg_kapanis_han_mutfagi.png` | A | `smm_card_287_a` | `choice_smm_card_287_a.png` | Geçmiş borçları tek tek tart | `smm_card_288` |
| `smm_card_287` | Mühür ve Sonlar: Tuz ve Yemin | `bg_kapanis_han_mutfagi.png` | B | `smm_card_287_b` | `choice_smm_card_287_b.png` | Sahte mührü son kez bas | `` |
| `smm_card_288` | Mühür ve Sonlar: Kırık Tabak | `bg_final_sabahi_saray_avlusu.png` | A | `smm_card_288_a` | `choice_smm_card_288_a.png` | Varisleri son kez dinle | `smm_card_289` |
| `smm_card_288` | Mühür ve Sonlar: Kırık Tabak | `bg_final_sabahi_saray_avlusu.png` | B | `smm_card_288_b` | `choice_smm_card_288_b.png` | Canın pahasına sırrı sakla | `` |
| `smm_card_289` | Mühür ve Sonlar: Mum Işığı | `bg_muhur_odasi.png` | A | `smm_card_289_a` | `choice_smm_card_289_a.png` | Kazın işaretini kayıt altına al | `smm_card_290` |
| `smm_card_289` | Mühür ve Sonlar: Mum Işığı | `bg_muhur_odasi.png` | B | `smm_card_289_b` | `choice_smm_card_289_b.png` | Arslan'a demir yolu aç | `` |
| `smm_card_290` | Mühür ve Sonlar: Gece Nöbeti | `bg_muhur_odasi_kapanis.png` | A | `smm_card_290_a` | `choice_smm_card_290_a.png` | Çorba alametini sorguya aç | `smm_card_291` |
| `smm_card_290` | Mühür ve Sonlar: Gece Nöbeti | `bg_muhur_odasi_kapanis.png` | B | `smm_card_290_b` | `choice_smm_card_290_b.png` | Safira'nın ittifakını mühürle | `` |
| `smm_card_291` | Mühür ve Sonlar: Kilitli Raf | `bg_taht_salonu.png` | A | `smm_card_291_a` | `choice_smm_card_291_a.png` | Mühür odasına bir adım yaklaş | `smm_card_292` |
| `smm_card_291` | Mühür ve Sonlar: Kilitli Raf | `bg_taht_salonu.png` | B | `smm_card_291_b` | `choice_smm_card_291_b.png` | Kemal'in ekmek yasasını duyur | `` |
| `smm_card_292` | Mühür ve Sonlar: Sarayın Kulağı | `bg_mahkeme_avlusu.png` | A | `smm_card_292_a` | `choice_smm_card_292_a.png` | Geçmiş borçları tek tek tart | `smm_card_293` |
| `smm_card_292` | Mühür ve Sonlar: Sarayın Kulağı | `bg_mahkeme_avlusu.png` | B | `smm_card_292_b` | `choice_smm_card_292_b.png` | Mührü Ak Gaga'ya emanet et | `` |
| `smm_card_293` | Mühür ve Sonlar: Yağ Halkası | `bg_kapanis_han_mutfagi.png` | A | `smm_card_293_a` | `choice_smm_card_293_a.png` | Varisleri son kez dinle | `smm_card_294` |
| `smm_card_293` | Mühür ve Sonlar: Yağ Halkası | `bg_kapanis_han_mutfagi.png` | B | `smm_card_293_b` | `choice_smm_card_293_b.png` | Çifte alameti tahta taşı | `` |
| `smm_card_294` | Mühür ve Sonlar: Ak Gaga'nın Yolu | `bg_final_sabahi_saray_avlusu.png` | A | `smm_card_294_a` | `choice_smm_card_294_a.png` | Kazın işaretini kayıt altına al | `smm_card_295` |
| `smm_card_294` | Mühür ve Sonlar: Ak Gaga'nın Yolu | `bg_final_sabahi_saray_avlusu.png` | B | `smm_card_294_b` | `choice_smm_card_294_b.png` | Kırık ittifakı barışa çevir | `` |
| `smm_card_295` | Mühür ve Sonlar: Gümüş Kaşık | `bg_muhur_odasi.png` | A | `smm_card_295_a` | `choice_smm_card_295_a.png` | Çorba alametini sorguya aç | `smm_card_296` |
| `smm_card_295` | Mühür ve Sonlar: Gümüş Kaşık | `bg_muhur_odasi.png` | B | `smm_card_295_b` | `choice_smm_card_295_b.png` | Kiler defterini halka aç | `` |
| `smm_card_296` | Mühür ve Sonlar: Kayıp Ölçü | `bg_muhur_odasi_kapanis.png` | A | `smm_card_296_a` | `choice_smm_card_296_a.png` | Mühür odasına bir adım yaklaş | `smm_card_297` |
| `smm_card_296` | Mühür ve Sonlar: Kayıp Ölçü | `bg_muhur_odasi_kapanis.png` | B | `smm_card_296_b` | `choice_smm_card_296_b.png` | Zehir yasasını yeniden yaz | `` |
| `smm_card_297` | Mühür ve Sonlar: Kuyrukta Ekmek | `bg_taht_salonu.png` | A | `smm_card_297_a` | `choice_smm_card_297_a.png` | Geçmiş borçları tek tek tart | `smm_card_298` |
| `smm_card_297` | Mühür ve Sonlar: Kuyrukta Ekmek | `bg_taht_salonu.png` | B | `smm_card_297_b` | `choice_smm_card_297_b.png` | Sırları ateşe ver | `` |
| `smm_card_298` | Mühür ve Sonlar: Keskin Koku | `bg_mahkeme_avlusu.png` | A | `smm_card_298_a` | `choice_smm_card_298_a.png` | Varisleri son kez dinle | `smm_card_299` |
| `smm_card_298` | Mühür ve Sonlar: Keskin Koku | `bg_mahkeme_avlusu.png` | B | `smm_card_298_b` | `choice_smm_card_298_b.png` | Halkla sarayı aynı kapıya çağır | `` |
| `smm_card_299` | Mühür ve Sonlar: Mühre Giden İz | `bg_kapanis_han_mutfagi.png` | A | `smm_card_299_a` | `choice_smm_card_299_a.png` | Kazın işaretini kayıt altına al | `smm_card_300` |
| `smm_card_299` | Mühür ve Sonlar: Mühre Giden İz | `bg_kapanis_han_mutfagi.png` | B | `smm_card_299_b` | `choice_smm_card_299_b.png` | Sessiz sürgün hükmünü kabul et | `` |
| `smm_card_300` | Mühür ve Sonlar: Dar Boğaz | `bg_final_sabahi_saray_avlusu.png` | A | `smm_card_300_a` | `choice_smm_card_300_a.png` | Üç varisi ortak meclise bağla | `` |
| `smm_card_300` | Mühür ve Sonlar: Dar Boğaz | `bg_final_sabahi_saray_avlusu.png` | B | `smm_card_300_b` | `choice_smm_card_300_b.png` | Canın pahasına sırrı sakla | `` |
