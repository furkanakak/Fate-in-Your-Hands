# CODEX PROMPT — Saray Mutfağından Mühre Tam Hikâye Paketi Üretimi

Bu dosyayı Codex'e ver. Amaç Unity kodu yazdırmak değil; önce eksiksiz, okunabilir, doğrulanabilir ve doğrudan oyuna bağlanabilecek **tam story content package** üretmektir.

---

## 1. Önce Oku

Önce repodaki şu dosyaları sırayla oku:

1. `GAME_RULES.md`
2. `GDD.md`
3. `STORY_GENERATOR_SPEC.md`
4. `STORY_PRODUCTION_CONTRACT.md`
5. Varsa `ART_STYLE_GUIDE.md`
6. Varsa `AGENTS.md`

Bu dosyalarda çelişki varsa şu öncelik sırasını uygula:

1. Kullanıcının bu prompttaki açık isteği
2. `STORY_PRODUCTION_CONTRACT.md`
3. `STORY_GENERATOR_SPEC.md`
4. `GAME_RULES.md`
5. `GDD.md`

---

## 2. Bu Aşamada Yapılmayacaklar

Bu aşamada:

- Unity kodu yazma.
- C# script yazma.
- Scene, prefab, UI component üretme.
- Asset dosyası üretme.
- Görsel çizme.
- Görsel indirme.
- Oyuna entegrasyon yapma.
- Sadece blueprint yazıp bırakma.
- “TODO”, “later”, “devam edecek”, “placeholder” bırakma.
- Hikâyeyi kısa özet geçme.

Bu aşamanın tek amacı:

> `Saray Mutfağından Mühre` için tüm kartları, tüm seçimleri, tüm sonuçları, tüm endingleri, tüm karakter metinlerini, tüm image promptlarını ve tüm state bağlantılarını içeren eksiksiz hikâye dosyalarını üretmek.

---

## 3. Hikâye Konsepti

Story title:

```txt
Saray Mutfağından Mühre
```

Story ID:

```txt
saray_mutfagindan_muhre
```

Başlangıç:

```txt
Büyük ve çok kültürlü bir imparatorluğun başkentinde yetim bir çocuk olarak yaşarsın.
Açlıktan kurtulmak için sarayın mutfağında bulaşıkçı olarak çalışmaya başlarsın.
```

Ana hayat amacı:

```txt
Sarayın en alt katından yükselmek, imparatorluk mührünü taşıyan en güçlü devlet görevlilerinden biri olmak ve tahtın geleceğini belirlemek.
```

Ana hayat yolları:

```txt
- Aşçı ve saray mutfağı yöneticisi olmak
- Zehir tadıcılığı üzerinden hükümdara yaklaşmak
- Saray casuslarına katılmak
- Ordu iaşesini yöneten görevli olmak
- Hekim ve şifacıların yanında çalışmak
- Sarayın sırlarını dışarı satan entrikacı olmak
- Halk ayaklanmasına gizlice yardım etmek
- Varis krizinde tahtın geleceğini belirlemek
```

Büyük kriz:

```txt
Yaşlanan hükümdarın birden fazla varisi vardır.
Saray içindeki gruplar oyuncu karakterini kendi taraflarına çekmek ister.
Çocukken tabaklarını yıkadığı insanlar artık ondan tahtın sahibini belirlemesini istemektedir.
```

Zorunlu absürt ama mantıklı olay zincirleri:

```txt
- Yapılan bir çorbanın kehanet sayılması
- Hükümdarın bazı devlet kararlarını saray kazının davranışına göre vermesi
- Yanlış tatlı yüzünden yabancı bir elçinin evlilik teklifinde bulunması
```

Absürtlük kuralı:

```txt
Absürt olaylar rastgele, kopuk veya “şaka olsun diye” yazılmayacak.
Her absürt olay önce kurulacak, sonra birkaç kart sonra hikâyede geri dönecek, finalde ise en az bir ending veya kritik karar üzerinde payoff alacak.
```

---

## 4. Ölçek Hedefi

Bu hikâye küçük bir örnek olmayacak.

Minimum hedefler:

```yaml
minimumTargets:
  totalCards: 250
  totalChoices: 500
  totalEndings: 24
  majorScenarioBranches: 30
  reusableBackgrounds: 40
  focusImages: 250
  importantNPCs: 15
  majorRoutes: 8
```

Tercih edilen hedef:

```yaml
preferredTargets:
  totalCards: 300
  totalChoices: 600
  totalEndings: 28
  majorScenarioBranches: 36
  reusableBackgrounds: 50
  focusImages: 300
  importantNPCs: 18
```

Her kartta tam olarak 2 seçim olacak.

```txt
1 kart = 2 seçim
250 kart = 500 seçim
300 kart = 600 seçim
```

“Seçim” derken yalnızca choice text değil, tam seçim objesi kastedilir:

```yaml
choice:
  choiceId:
  text:
  resultText:
  healthDelta:
  setFlags:
  clearFlags:
  hiddenCounterDeltas:
  nextCardId OR endingId:
```

---

## 5. Çıktı Klasörü

Şu klasörü oluştur:

```txt
generated_stories/saray_mutfagindan_muhre/
```

Bu klasörün altına aşağıdaki dosyaların tamamını üret.

Eksik dosya bırakma.

---

## 6. Üretilecek Dosyalar

### 6.1 `README_STORY_PACKAGE.md`

İçerik:

- Paket özeti
- Dosya listesi
- Kart sayısı
- Seçim sayısı
- Ending sayısı
- Ana rotalar
- Absürt zincirler
- Validation durumunun özeti
- Codex’in sonraki aşamada bu paketi nasıl kullanacağı

---

### 6.2 `STORY_OVERVIEW.md`

İçerik:

- Hikâyenin tam özeti
- Oyuncu başlangıcı
- Erken oyun
- Orta oyun
- Geç oyun
- Büyük kriz
- Final evresi
- Ana temalar
- Ton
- Oyuncu deneyimi
- Absürt olayların hikâyeye nasıl mantıklı bağlandığı
- Hikâyede tekrar eden motifler
- Hangi seçim tiplerinin oyuncuya nasıl hissettireceği

---

### 6.3 `STORY_BIBLE.md`

İçerik:

- İmparatorluk yapısı
- Başkent yapısı
- Saray hiyerarşisi
- Mutfak hiyerarşisi
- Helvahane
- Kiler
- Zehir tadım odası
- Şifahane
- Casus ağı
- Ordu iaşe sistemi
- Halkın açlık durumu
- Varisler
- Fraksiyonlar
- İmparatorluk mührü nedir?
- Mühre kim erişebilir?
- Mührün siyasi gücü nedir?
- Saray kazı neden önemlidir?
- Kehanet çorbası olayı neden ciddiye alınır?
- Yanlış tatlı krizi neden diplomatik felakete dönüşür?

---

### 6.4 `NPC_BIBLE.yaml`

En az 18 NPC üret.

Her NPC için şu alanlar zorunlu:

```yaml
npcId:
name:
title:
role:
routeFunction:
firstAppearanceCardId:
personality:
speechStyle:
relationshipToPlayer:
wants:
fears:
secrets:
canHelpPlayerBy:
canHurtPlayerBy:
importantFlags:
importantCards:
possibleFinalStates:
sampleDialogueLines:
  - emotion:
    text:
```

Zorunlu NPC rolleri:

```txt
- Oyuncu karakteri / yetim bulaşıkçı
- Baş aşçı / mentor
- Fırın veya helvahane kalfası
- Kiler sorumlusu
- Zehir tadıcısı
- Hekim / şifacı
- Casusbaşı
- Ordu iaşe görevlisi
- Halk isyanı bağlantısı
- Karaborsa sır satıcısı
- Yaşlanan hükümdar
- En az 3 farklı varis
- Saray kazı bakıcısı
- Yabancı elçi
- Başvezir veya mühür odası yetkilisi
- Saray soytarısı veya söylenti taşıyıcı
```

---

### 6.5 `STATE_MODEL.yaml`

Görünür stat:

```yaml
visibleStats:
  health:
    displayName: "Can"
    min: 0
    max: 10
    default: 5
```

Gizli sayaçlar:

```yaml
hiddenCounters:
  kitchen_favor:
  palace_suspicion:
  poison_knowledge:
  spy_network:
  healer_trust:
  logistics_mastery:
  rebellion_sympathy:
  black_market_debt:
  heir_arslan_support:
  heir_safira_support:
  heir_kemal_support:
  seal_access:
  absurd_soup_omen_score:
  absurd_goose_omen_score:
  absurd_dessert_diplomacy_score:
```

Her hidden counter için:

```yaml
counterId:
description:
min:
max:
default:
increasedBy:
decreasedBy:
usedInCards:
usedInEndings:
```

Flag listesi üret.

Her flag için:

```yaml
flagId:
description:
setByChoiceIds:
checkedByCardIds:
checkedByEndingIds:
payoffDescription:
```

---

### 6.6 `VISUAL_STYLE_GUIDE.md`

İçerik:

- Genel sanat stili
- Renk paleti
- Işık dili
- Mutfak sahneleri
- Saray sahneleri
- Halk/sokak sahneleri
- Zehir/şifa sahneleri
- Casusluk sahneleri
- Absürt sahnelerin görsel tonu
- Yasak görsel öğeler
- Choice card görselleri için kompozisyon kuralları
- Background görselleri için kompozisyon kuralları
- Ending görselleri için kompozisyon kuralları

Zorunlu stil:

```txt
2D illustrated storybook style, hand-drawn or painterly feeling, warm muted colors, readable silhouettes, no photorealism, no 3D render look, no text in image, no watermark, no logo.
```

---

### 6.7 `BACKGROUND_LIBRARY.yaml`

En az 40 background üret.

Her background için:

```yaml
backgroundId:
title:
locationName:
locationType:
usagePurpose:
mood:
timeOfDay:
weatherOrAtmosphere:
reusableCardIds:
prompt:
negativePrompt:
```

Background prompt sadece ortamı anlatacak.
O karta özel eylemi anlatmayacak.

Zorunlu background kategorileri:

```txt
- Saray bulaşıkhanesi
- Büyük mutfak
- Helvahane
- Kiler
- Baharat odası
- Zehir tadım odası
- Şifahane
- Hizmetkâr koridoru
- Gizli geçit
- Mühür odası
- Divan salonu
- Taht salonu
- Saray avlusu
- Saray kazının bulunduğu avlu
- Elçilik salonu
- Halk ekmek kuyruğu
- Şehir pazarı
- Arka sokak karaborsası
- Ordu iaşe deposu
- Kışla kapısı
- Şarap mahzeni / isyan toplantısı
- Final sabahı saray avlusu
```

---

### 6.8 `PROP_CATALOG.yaml`

En az 80 prop üret.

Her prop için:

```yaml
propId:
name:
description:
storyFunction:
usedByCardIds:
visualPromptNotes:
relatedFlags:
```

Zorunlu prop örnekleri:

```txt
- bakır kazan
- yanık tencere
- gümüş kaşık
- zehir şişesi
- panzehir kavanozu
- kehanet çorbası
- üç nohut işareti
- saray kazı tüyü
- kaz yemi
- yanlış tatlı
- gül şerbeti
- bal mumu
- imparatorluk mührü
- sahte mühür
- kiler anahtarı
- ekmek çuvalı
- ordu tahıl defteri
- gizli bildiri
- isyan haritası
- yabancı elçinin yüzüğü
```

---

### 6.9 `ASSET_MANIFEST.yaml`

Tüm görsel assetleri içer.

Her asset için:

```yaml
assetId:
storyId:
type: cover | background | focus | ending
usedBy:
targetPath:
size:
format:
background:
prompt:
negativePrompt:
```

Kurallar:

```txt
- Her card focusImageId burada bulunmalı.
- Her backgroundId burada bulunmalı.
- Her endingImageId burada bulunmalı.
- Asset ID’ler lowercase snake_case olmalı.
- Generic asset adı yasak: image1, bg1, final_image, temp_asset.
```

---

### 6.10 `ENDINGS.yaml`

En az 24 ending üret.

Her ending için:

```yaml
endingId:
title:
endingType:
fullEndingText:
requiredFlags:
forbiddenFlags:
requiredHiddenCounters:
forbiddenHiddenCounters:
typicalRoute:
importantPastChoices:
whyThisEndingHappens:
payoffExplanation:
endingImageId:
endingImagePrompt:
```

Zorunlu endingler:

```txt
- Başvezir ve imparatorluğun gerçek yöneticisi
- Hükümdarın en güvendiği saray aşçısı
- Gizli istihbarat örgütünün başı
- Halk devriminin lideri
- Yeni hükümdarı tahta çıkaran kişi
- Başkentten kaçıp kendi hanını açmak
- Zehirlenmek
- Yanlış varisi desteklediğin için idam edilmek
- Saray kazının yorumcusu olmak
- Kehanet çorbasının kutsal figürüne dönüşmek
- Yanlış tatlı yüzünden diplomatik evlilik ittifakı
- Ordu iaşesinin başına geçmek
- Saray sırlarını satarak karaborsa efendisi olmak
- Hekimlerin yanında sürgün şifacı olmak
- Sahte mühür düzenbazı olmak
- İsimsiz mezar / sağlık sıfır sonu
```

---

## 7. Kart Dosyaları

Kartları tek dosyada yazma.
Aşağıdaki parçalara böl.

```txt
CARDS_PART_01_INTRO.yaml
CARDS_PART_02_KITCHEN_RISE.yaml
CARDS_PART_03_PANTRY_AND_PASTRY.yaml
CARDS_PART_04_SOUP_OMEN_AND_GOOSE.yaml
CARDS_PART_05_POISON_TASTER.yaml
CARDS_PART_06_HEALER_PATH.yaml
CARDS_PART_07_SPY_NETWORK.yaml
CARDS_PART_08_ARMY_SUPPLY.yaml
CARDS_PART_09_BLACK_MARKET.yaml
CARDS_PART_10_REBELLION.yaml
CARDS_PART_11_HEIRS_AND_FACTIONS.yaml
CARDS_PART_12_SEAL_AND_FINALS.yaml
```

Her partta yaklaşık 20-30 kart olsun.
Toplam minimum 250 kart olmalı.
Tercihen 300 kartı hedefle.

---

## 8. Kart Formatı

Her kart şu formatta olacak:

```yaml
cards:
  - cardId:
    storyId: saray_mutfagindan_muhre
    chapterId:
    stage: intro | growth | crisis | final
    routeFamily:
    title:
    bodyText:
    backgroundId:
    focusImageId:
    focusImagePrompt:
    presentCharacters:
      - npcId
    dialogue:
      - speakerId:
        emotion:
        text:
    choices:
      - choiceId:
        text:
        resultText:
        healthDelta:
        setFlags:
          - flagId
        clearFlags:
          - flagId
        hiddenCounterDeltas:
          kitchen_favor: 0
          palace_suspicion: 0
          poison_knowledge: 0
          spy_network: 0
          healer_trust: 0
          logistics_mastery: 0
          rebellion_sympathy: 0
          black_market_debt: 0
          heir_arslan_support: 0
          heir_safira_support: 0
          heir_kemal_support: 0
          seal_access: 0
          absurd_soup_omen_score: 0
          absurd_goose_omen_score: 0
          absurd_dessert_diplomacy_score: 0
        nextCardId:
      - choiceId:
        text:
        resultText:
        healthDelta:
        setFlags:
          - flagId
        clearFlags:
          - flagId
        hiddenCounterDeltas:
          kitchen_favor: 0
          palace_suspicion: 0
          poison_knowledge: 0
          spy_network: 0
          healer_trust: 0
          logistics_mastery: 0
          rebellion_sympathy: 0
          black_market_debt: 0
          heir_arslan_support: 0
          heir_safira_support: 0
          heir_kemal_support: 0
          seal_access: 0
          absurd_soup_omen_score: 0
          absurd_goose_omen_score: 0
          absurd_dessert_diplomacy_score: 0
        nextCardId:
```

Eğer seçim ending’e gidiyorsa:

```yaml
endingId:
```

kullan.

Bir seçimde aynı anda `nextCardId` ve `endingId` kullanma.

---

## 9. Kart Yazım Kalitesi

Kart metinleri gerçek hikâye gibi yazılacak.

Kurallar:

```txt
- Her kartın bodyText alanı 2-5 cümle olacak.
- Her kartta sahne, karakter niyeti ve risk anlaşılacak.
- Her seçim metni 3-10 kelime olacak.
- Her resultText 1-3 cümle olacak.
- ResultText seçimin doğrudan sonucunu anlatacak.
- Aynı resultText kalıbı tekrar edilmeyecek.
- “Devam et”, “Evet”, “Hayır”, “Bir şey yapma” gibi boş seçimler yasak.
- Her seçim, diğerinden ahlaki, politik, pratik veya risk açısından farklı olacak.
- Her 5-8 kartta önceki bir kararın izi geri dönecek.
- Absürt olaylar seçimle ilişkili olacak.
```

---

## 10. Zorunlu Hikâye Zincirleri

### 10.1 Çorba Kehaneti Zinciri

En az 20 kartta doğrudan veya dolaylı görünmeli.

Kurulum:

```txt
Oyuncu bir çorbayı yanlış karıştırır veya kurtarmaya çalışır.
Çorbanın yüzeyindeki yağ halkaları, nohut dizilişi veya baharat rengi sarayda kehanet sayılır.
```

Gelişim:

```txt
Mutfak çalışanları bunu önce şaka sanır.
Sonra küçük bir olay çorbadaki işarete denk gelir.
Müneccime veya saraylı biri bunu ciddiye alır.
```

Kriz:

```txt
Varislerden biri bu kehaneti kendi lehine kullanmak ister.
Diğerleri oyuncudan yeni bir “işaret” pişirmesini ister.
```

Payoff:

```txt
- Kehanet çorbası finalde varis seçimini etkileyebilir.
- Oyuncu bunu bilinçli manipülasyon olarak kullanabilir.
- Oyuncu bunu reddedebilir.
- Oyuncu absürt ending olarak “Kehanet Çorbasının Velisi” olabilir.
```

---

### 10.2 Saray Kazı Zinciri

En az 20 kartta doğrudan veya dolaylı görünmeli.

Kurulum:

```txt
Saray kazı Ak Gaga mutfak avlusunda yaşar.
Hükümdar bazen kazın yürüdüğü yöne göre karar verir.
```

Gelişim:

```txt
Oyuncu kazı besleyebilir, kandırabilir, koruyabilir veya onun davranışını yanlış yorumlayabilir.
```

Kriz:

```txt
Bir divan toplantısında kazın davranışı varislerden biri lehine yorumlanır.
Oyuncu bu yorumu destekleyebilir, bozabilir veya sahneleyebilir.
```

Payoff:

```txt
- Kazın davranışı finalde mühür kararını etkileyebilir.
- Oyuncu “Kazın Yorumcusu” absürt endingine gidebilir.
- Kazla ilgili erken seçimler finalde beklenmedik koruma veya felaket yaratabilir.
```

---

### 10.3 Yanlış Tatlı Diplomasi Zinciri

En az 20 kartta doğrudan veya dolaylı görünmeli.

Kurulum:

```txt
Helvahanede yanlış hazırlanan veya yanlış servis edilen bir tatlı yabancı elçiye gider.
Elçi bunu kendi kültüründe evlilik veya ittifak teklifi sayar.
```

Gelişim:

```txt
Oyuncu hatayı saklayabilir, düzeltebilir, diplomatik fırsata çevirebilir veya başka birinin üzerine yıkabilir.
```

Kriz:

```txt
Bu yanlış tatlı, varislerden birinin dış destek kazanmasına veya kaybetmesine neden olabilir.
```

Payoff:

```txt
- Diplomatik evlilik ittifakı endingi açılabilir.
- Yanlış tatlı yüzünden savaş önlenebilir veya başlatılabilir.
- Oyuncu bu zinciri kullanarak Safira, Arslan veya Kemal varislerinden birini güçlendirebilir.
```

---

## 11. STORY_FLOW.md

Şunları üret:

- Tam başlangıç kartı
- Chapter chapter akış
- Her partın ilk ve son kartı
- Merge noktaları
- Büyük branch noktaları
- Endinglere giden yollar
- Absürt zincirlerin hangi kartlarda başladığı, geliştiği ve payoff aldığı
- Mermaid flowchart

---

## 12. CHOICE_REVIEW_TABLE.md

Tüm kartları tek tabloda özetle.

Kolonlar:

```txt
cardId
title
routeFamily
choiceA
targetA
choiceB
targetB
healthDeltaA
healthDeltaB
flagsChanged
importantCounterChanges
```

---

## 13. VALIDATION_REPORT.md

Validation report şu kontrolleri PASS/FAIL olarak içerecek:

```txt
- Toplam kart sayısı 250+ mı?
- Toplam seçim sayısı 500+ mı?
- Toplam ending sayısı 24+ mı?
- Her kartta tam 2 seçim var mı?
- Her seçimde text var mı?
- Her seçimde resultText var mı?
- Her seçimde healthDelta var mı?
- Her seçimde setFlags ve clearFlags var mı?
- Her seçimde hiddenCounterDeltas var mı?
- Her seçimde yalnızca nextCardId veya endingId var mı?
- Bozuk nextCardId var mı?
- Bozuk endingId var mı?
- Ulaşılamayan kart var mı?
- Ulaşılamayan ending var mı?
- Her kartta backgroundId var mı?
- Her kartta focusImageId var mı?
- Her focusImageId asset manifestte var mı?
- Her backgroundId background library’de var mı?
- Her endingImageId asset manifestte var mı?
- NPC_BIBLE içindeki firstAppearanceCardId değerleri gerçek kartlara gidiyor mu?
- Prop usedByCardIds değerleri gerçek kartlara gidiyor mu?
- Çorba kehaneti zinciri en az 20 kartta görünüyor mu?
- Saray kazı zinciri en az 20 kartta görünüyor mu?
- Yanlış tatlı diplomasi zinciri en az 20 kartta görünüyor mu?
- Absürt olaylar finalde payoff alıyor mu?
- Varis krizi tüm ana rotalara bağlanıyor mu?
- Placeholder, TODO, later, devam edecek var mı?
```

Eğer FAIL varsa:

```txt
Görevi tamamlanmış sayma.
Eksikleri otomatik düzelt.
Validation report’u tekrar üret.
```

---

## 14. Final Done Criteria

Görev ancak şunlar olunca tamamlanmış sayılır:

```txt
- generated_stories/saray_mutfagindan_muhre/ klasörü oluştu
- Tüm zorunlu dosyalar oluştu
- 250+ kart var
- 500+ seçim var
- 24+ ending var
- Tüm kartlar gerçek metin içeriyor
- Tüm seçimler gerçek resultText içeriyor
- Tüm nextCardId ve endingId bağlantıları geçerli
- Tüm image promptları yazıldı
- Tüm background promptları yazıldı
- Tüm NPC sample dialogları yazıldı
- Absürt olay zincirleri tamamlandı
- VALIDATION_REPORT.md PASS
```

---

## 15. Son Talimat

Bu işi kısa özet olarak yapma.

Bu bir story idea generation işi değil.

Bu bir production-ready narrative content package üretim işidir.

Eksik bırakma.

Dosyaları gerçekten oluştur.

Tüm kartları, tüm seçimleri, tüm sonuçları, tüm endingleri ve tüm asset promptlarını yaz.

Validation PASS olmadan bitirme.
