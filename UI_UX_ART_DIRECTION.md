# Çoklu Hikâye Kart Oyunu İçin UI Araştırması ve Evrifa Dokümanı

## Araştırmadan çıkan arayüz modeli

Referans aldığın `Choice of Life` çizgisi, özünde “kart tabanlı, iki seçenekli, sonuç odaklı” bir anlatı yapısı kullanıyor. Resmî mağaza açıklamalarında seri açıkça bir kart oyunu ya da kartlı görsel roman olarak tanımlanıyor; renkli 2D görseller, yüzlerce kart, doğrusal olmayan akış, benzersiz sonuçlar ve çok sayıda ölüm durumu temel özellikler arasında sayılıyor. `Wild Islands` tarafında ise seçimler can/heart sistemiyle bağlanıyor; can sıfıra düşünce oyun bitiyor. Bu, senin genel UI’ının da “çok az HUD, çok net karar anı, yüksek tekrar oynanabilirlik, güçlü koleksiyon hissi” etrafında kurulması gerektiğini gösteriyor. citeturn2view4turn1search12turn2view0turn0search1

Aşağıdaki görseller, referans arayüzün ortak kabuğunu net biçimde gösteriyor: üstte can kalpleri, ortada kısa anlatı bandı, altta iki büyük seçim paneli; bazı varyantlarda da ayarlar ve günlük/kitap gibi yardımcı ikonlar yer alıyor. Ana menü tarafında ise karmaşık bir HUD yerine, atmosferik arka plan ve merkezde birkaç büyük giriş butonu tercih ediliyor. Bu gözlem, senin oyunun için en doğru yaklaşımın “tek bir ortak meta-UI kabuğu + her hikâyeye özel kaplama/süsleme” modeli olduğunu düşündürüyor. citeturn3image0turn3image2turn3image4turn3image8

iturn3image0turn3image2turn3image4turn3image8

Buradaki kritik çıkarım şu: Sen artık sadece “tek hikâye kartı” tasarlamıyorsun; “birden fazla hikâyeyi taşıyacak bir oyun kabuğu” tasarlıyorsun. Bu yüzden menü, koleksiyon, hikâye giriş ekranı ve final ekranı; hikâye içi kart UI’ından daha nötr, daha markasal ve daha sistematik olmalı. Hikâye içi sahne orta çağ, bilim kurgu, anime etkili fantastik veya gladyatör temalı olabilir; ama oyunun ana kabuğu bunların hepsini taşıyabilmeli. Referans serinin güçlü yanı da tam olarak bu tutarlılık: tema değişse bile kart-odaklı akış ve karar anı sabit kalıyor. citeturn2view4turn2view2turn3image0turn3image2

## Senin oyun için önerilen ekran mimarisi

Bu proje için en sağlıklı UI mimarisi, ekranları iki katmanda düşünmektir. İlk katman **oyun kabuğu**dur: açılış, ana menü, hikâye koleksiyonu, hikâye detay/başlatma, ayarlar, sonuç ve bitiş ekranları. İkinci katman ise **hikâye kabuğu**dur: kart ekranı, can göstergesi, seçim alanları, pause/geri dönüş overlay’i ve hikâye sonu ekranı. Bu ayrım yapılmazsa, çok farklı dönem ve türlerde geçen hikâyeler aynı oyunda dağınık görünmeye başlar.

**Ana menü**nün görevi görsel şov yapmak değil, oyunun tonunu tek ekranda anlatmaktır. Burada önerim, tam ekran bir “world-neutral” key art kullanmak: çok spesifik bir çağ yerine, kitaplık, pusula, mühür, eski kartlar, metal çerçeve, mum ışığı, kumaş dokusu, sembolik rölyefler gibi hikâye koleksiyonu hissi veren bir sahne. Ortada en fazla dört ana eylem olmalı: `Devam Et`, `Hikâyeler`, `Yeni Oyun`, `Ayarlar`. Eğer ilk sürüm küçük olacaksa `Yeni Oyun` ayrı buton olmak zorunda değil; `Hikâyeler` ekranı zaten yeni oyuna açılabilir.

**Hikâye koleksiyonu** bu oyunun en kritik ekranıdır, çünkü burada oyuncu “tek bir hikâyeye girmek” yerine “hangi hayatı yaşayacağını” seçer. Bu nedenle burada klasik liste görünümü yerine “büyük hikâye kapak kartları” kullanılmalı. Her kartta en az şu öğeler bulunmalı: kapak görseli, hikâye adı, kısa tek satır kanca metni, durum rozeti (`Yeni`, `Devam Ediyor`, `Tamamlandı`, `Kilitli`), isteğe bağlı dönem/tür etiketi ve ilerleme yüzdesi ya da “ulaşılan son sayısı”. Bu ekran kart oyunundan çok “yaşam koleksiyonu rafı” gibi hissettirmeli.

**Hikâye detay/başlatma ekranı** koleksiyon ile kart oyununu birbirine bağlayan ara yüzdür. Oyuncu hikâye kapağına dokunduğunda doğrudan ilk karta düşmek yerine önce kısa bir giriş ekranı görmelidir: büyük kapak art’ı, 2–3 cümlelik özet, zorluk/ton etiketleri, “bu hikâyede ölüm mümkündür” uyarısı, varsa daha önce ulaşılan sonların sayısı ve ana çağrı butonu olarak `Hayata Başla` ya da `Devam Et`. Bu ekran, oyuncunun girilecek hayatı bilinçli seçmesini sağlar.

**Oyun içi kart ekranı** için sen zaten temel sistemi tanımlamışsın; ama genel UI açısından şu netliği eklemek gerekir: hikâye kartı ekranı, oyunun geri kalanından görsel olarak daha sade olmalı. Üstte can; ortada olay başlığı ve kısa anlatı; altta iki büyük seçim. Eğer yardımcı ikonlar olacaksa bunlar yalnızca `Pause`, `Ayarlar`, `Hikâye Akışı/Journal` gibi bir-iki işlevle sınırlı kalmalı. Referans görsellerde de karar anı ön planda bırakılıyor; HUD kalabalıklaşmıyor. citeturn3image0turn3image2turn3image8

**Oyun bitiş ekranı** ikiye ayrılmalı: `ölüm sonu` ve `normal son`. Ölüm ekranında dramatik tek bir sahne illüstrasyonu, son başlığı, kısa iki satırlık özet ve belirgin `Yeniden Dene` butonu yeterlidir. Normal sonda ise bunun yanında açılan son rozetini, toplam hayatta kalınan kart sayısını, ulaşılan sonun ismini ve `Bu Hayatı Yeniden Oyna` / `Koleksiyona Dön` çağrılarını göstermek gerekir. Senin oyunun koleksiyon tarafı önemli olduğu için, bitiş ekranı yalnızca “oyun bitti” dememeli; “bu hikâyede şu kaderi elde ettin” demelidir.

**Ayarlar ve pause ekranı** ana kabuğun parçası gibi davranmalı; hikâye temasına göre tamamen değişmemeli. Oyuncu her hikâyede aynı ayar düzenini bulmalı. Ses, metin büyüklüğü, titreşim, dil, otomatik ilerleme var/yok, seçim onayı, renk/kontrast varyantı gibi ayarlar burada toplanmalı. Bu bölüm sonradan kodlanacağı için, şu aşamada görsel üretimde de hep aynı layout korunmalı.

## Görsel dil ve üretim prensipleri

Bu oyun için en doğru sanat yaklaşımı, **tek bir meta-UI kimliği** ile **hikâye bazlı tema yüzeylerinin** ayrılmasıdır. Meta-UI; çerçeveler, buton formu, gölgeler, tipografi hiyerarşisi, rozet sistemi, ikon dili ve overlay yapısıdır. Hikâye teması ise kapak görseli, arka plan, renk vurgu tonu ve dekoratif mikro öğelerdir. Böylece gladyatör hikâyesi ile bilim kurgu hikâyesi aynı oyunda olmasına rağmen “başka stüdyolar yapmış” gibi görünmez.

Buradaki en önemli tasarım kararı, **ana kabuğu dönem bağımsız yapmak**tır. Yani bütün oyunu parşömen/orta çağ estetiğine kilitlememek gerekir. Referans oyunlarda parşömen ve el çizimi paneller güçlü bir dil oluşturuyor; fakat sen çoklu tema hedeflediğin için bunu birebir kopyalamak yerine “kitaplık, koleksiyon, kader kartı, yaşam arşivi” şeklinde daha soyut ve evrensel bir üst kimliğe çevirmelisin. Referansın ortak noktası kart vurgusu ve kısa anlatı bandı; sen de bunu koruyup kaplama malzemesini evrenselleştirmelisin. citeturn3image0turn3image2turn2view4

Bu yüzden önerdiğim görsel formül şudur: arka planda hafif atmosferik sahne; ön planda kabartmalı, sıcak ama nötr bir kart/container sistemi; vurgu renkleri hikâyeye göre değişen rozetler ve highlight’lar. Orta çağ hikâyesinde bordo-altın, bilim kurguda koyu lacivert-cyan, anime-esintili fantastikte mor-pembe/altın, gladyatör hikâyesinde kum-kızıl-bronz gibi varyantlar uygulanabilir; ama buton şekilleri, padding, tipografi basamakları ve ekran akışları değişmemelidir.

Ayrıca görsel mockup üretirken metnin tamamını görsele gömmek yerine, **metin alanlarını net bırakmak** gerekir. WCAG, mümkün olan yerlerde gerçek metnin görsele gömülü bitmap yazı yerine gerçek text olarak kullanılmasını önerir; çünkü bu, kontrast ve yeniden boyutlandırma kurallarını daha iyi karşılar. Bu yüzden Evrifa’dan bekleyeceğin çıktı “bitmiş ve implemente edilmiş UI” değil, “entegrasyona hazır yüksek sadakatli UI konsepti” olmalı. citeturn6search1turn6search6

## Erişilebilirlik ve teknik sınırlar

Bu dosya henüz kod istemediğin için konsept düzeyinde kalmalı; ama sonra üretime sorun çıkarmasın diye bazı sınırların şimdiden yazılması gerekir. Resmî Android ve Material rehberleri, dokunmatik öğeler için en az `48x48dp` hedef alan öneriyor; Apple da butonlar için en az `44x44pt` hit region tavsiye ediyor. Eğer sen hikâye koleksiyonu ekranında küçük rozetler, minik filtre ikonları veya dar geri butonları tasarlarsan, mockup güzel görünse bile gerçek üründe kullanımı zorlaşır. citeturn5search0turn5search3turn5search7turn5search13

Metin boyutunda da erken karar önemli. Microsoft’un Xbox Accessibility Guideline 101 belgesinde, varsayılan oyun metni için konsolda 1080p’de en az `26 px`, PC/VR’da `18 px`, mobil/streaming tarafında da `18 px` ve üstü öneriliyor. Bu, özellikle hikâye koleksiyonundaki açıklama satırları, sonuç ekranındaki özet metinleri ve ayarlar sayfası için önemli. Eğer şimdi görsel promptlarda aşırı küçük yazı alanları üretirsen, sonradan ya metni kısaltmak ya da layout’u bozmak zorunda kalırsın. citeturn5search2

Kontrast kuralları da yazılı olmalı. WCAG’e göre normal boyuttaki metinlerde en az `4.5:1`, büyük yazılarda `3:1`, anlam taşıyan non-text UI bileşenlerinde de en az `3:1` kontrast gereklidir. Game Accessibility Guidelines de yüksek kontrast, okunabilir varsayılan yazı boyutu ve bilginin yalnızca renkle aktarılmaması gerektiğini özellikle vurguluyor. Bu nedenle can göstergesi, seçili hikâye durumu, kilitli/açık durumu ve son türleri yalnızca renk üzerinden değil; ikon, metin ve şekil üzerinden de ayrışmalıdır. citeturn6search0turn6search5turn5search5

Senin özel durumunda şu da kritik: **hikâye kapak kartları bilgi yoğun ama tek bakışta anlaşılır olmalı**. Yani kapak üstünde aynı anda 8 ayrı bilgi göstermemek gerekir. En iyi çözüm, ana kartta 3 ana bilgi göstermek; diğerlerini hikâye detay ekranına taşımaktır. Bu, hem okunabilirliği hem de görsel kaliteyi korur.

## Evrifa için prompt stratejisi

Evrifa’ya tek satırlık “ana menü yap” komutu vermek yerine, tutarlı bir **prompt sözleşmesi** vermen gerekir. Bu sözleşme üç şey söylemeli: ne tür bir ekran üretileceği, hangi ortak tasarım sistemine uyacağı ve çıktının kod değil konsept görsel olduğu.

Aşağıdaki ana prompt iskeleti, bütün ekranlarda sabit kalmalı:

```text
Create a high-fidelity 2D game UI mockup for a branching narrative card game with multiple story campaigns.
This is not code and not a playable prototype.
Use a clean production-ready visual design intended for later implementation.

Core UI identity:
- mobile-first portrait layout, premium indie game feel
- one unified meta-UI shell shared across all story themes
- story-specific art appears inside cover cards, backgrounds, and accent colors
- elegant readable typography hierarchy
- large touch-friendly buttons
- high contrast text areas
- minimal HUD
- no clutter
- no fake mobile hands
- no device frame
- no watermark
- no logo distortion
- no illegible gibberish text blocks
- leave text zones implementation-friendly and clearly readable

Visual mood:
- life archive / destiny library / story collection feeling
- painterly 2D backgrounds
- softly layered panels
- subtle texture
- premium, warm, collectible, replayable

Output:
- one polished full-screen mockup
- one version with UI emphasis and cleaner text zones
- show safe areas
- keep spacing consistent
```

Bu ana sözleşmenin üstüne ekran bazlı prompt eklenmeli. Örneğin **ana menü** için:

```text
Design the main menu screen of a branching narrative card game.
The screen should communicate that the player can live many different lives across many different stories.
Show:
- game title area at top center
- primary buttons: Continue, Stories, New Game, Settings
- atmospheric background that feels like a hall of stories / archive of lives
- collectible premium card-game mood
- subtle decorative symbols from different genres without locking the UI to one era
- readable menu hierarchy
- strong focal point
- mobile portrait composition
```

**Hikâye koleksiyonu** için en önemli prompt şu olmalı:

```text
Design the story collection screen of a branching narrative card game with many separate story campaigns.
Show a vertically scrollable premium collection of big story cover cards.
Each story card should include:
- large cover illustration
- story title
- one-line hook
- status chip such as New / In Progress / Completed / Locked
- optional progress marker
- optional theme tag
The screen must feel like choosing which life to live next.
Keep the global UI shell consistent and neutral, while each story card has its own strong theme identity.
```

**Hikâye detay/başlatma** ekranı için:

```text
Design a story detail screen for a branching narrative card game.
Show:
- large cover art
- short premise text
- tone / danger / genre tags
- progress summary
- endings unlocked summary
- primary CTA button: Start Life or Continue
- secondary CTA: Back to Collection
This should feel like standing at the entrance of a new life path.
```

**Bitiş ekranı** için iki ayrı prompt tutulmalı:

```text
Design a dramatic ending screen for a branching narrative card game.
Variant A: death ending
Variant B: successful or major fate ending

Show:
- full-screen ending art
- ending title
- short ending summary
- fate badge or seal
- buttons: Retry, Back to Collection
For successful endings also show:
- endings unlocked count
- replay CTA
The layout should feel rewarding and collectible, not just like a generic game over screen.
```

**Ayarlar / pause** için:

```text
Design a settings and pause overlay for a premium 2D card narrative game.
The visual style must remain neutral across all story themes.
Show:
- music, sfx, language, text size, vibration, accessibility, quit confirmation
- elegant panel overlays
- clear section grouping
- high readability
- no theme-specific clutter
```

Buradaki önemli nokta, Evrifa’ya “UI çiz” değil, **“aynı tasarım sistemine ait ekran ailesi çiz”** komutu vermektir. Eğer bunu yapmazsan ana menü ayrı bir oyun, koleksiyon ayrı bir oyun, bitiş ekranı da bambaşka bir ürün gibi görünür.

## Kopyalanabilir md dokümanı

Aşağıdaki metni doğrudan `/docs/UI_UX_ART_DIRECTION.md` olarak kullanabilirsin.

```md
# UI_UX_ART_DIRECTION

## Purpose

This document defines the non-code visual and UX direction for the game shell around the story card system.
It exists to guide future implementation and visual generation before coding begins.

This document covers:
- main menu
- story collection screen
- story detail / start screen
- in-game shell alignment
- ending screens
- settings / pause overlays
- global UI language
- prompt contract for mockup generation

This document does NOT define gameplay code.
It defines the visual and structural contract for future production.

## Design Goal

The game must feel like:
- a premium branching narrative card game
- a collection of different lives and destinies
- easy to read
- easy to tap
- replayable
- thematically diverse but visually unified

The player should feel:
- “I am choosing which life to live next”
not
- “I am browsing disconnected mini-games”

## Core UX Model

The UI is split into two layers:

### Meta UI Shell
Shared across the entire product:
- title treatment
- panel shapes
- button system
- spacing system
- icon language
- overlays
- collection structure
- ending layout
- settings layout

### Story Theme Layer
Changes per story:
- cover art
- background art
- accent colors
- decorative symbols
- story-specific labels
- mood illustrations

The meta shell must stay consistent.
The story theme layer provides variety.

## Platform Assumption

Primary target:
- mobile-first portrait layout

Secondary adaptation:
- tablet
- PC
- console

All screens must work with safe areas and avoid edge-heavy UI.

## Global Visual Direction

Use a neutral premium UI identity that can support very different story settings.

Keywords:
- destiny archive
- life collection
- illustrated story deck
- premium indie narrative
- warm layered panels
- readable typography
- collectible endings
- strong cover art
- elegant but simple

Avoid locking the whole product into a single era such as only medieval parchment.
The product shell should feel universal.
Individual stories can carry stronger thematic styling.

## Main Menu

### Purpose
Introduce the game as a collection of playable lives.

### Must Show
- title area
- Continue
- Stories
- New Game
- Settings

### Optional
- latest unlocked ending badge
- featured story card preview
- subtle ambient particles
- version number

### Layout Notes
- strong central composition
- minimal text
- immediate clarity
- no overwhelming HUD
- premium static or softly animated background

### Emotional Goal
“I’m entering a library of lives.”

## Story Collection Screen

### Purpose
Let the player choose which life/story to begin or continue.

### Structure
- top header with page title
- optional filter row
- large vertically scrollable story cover cards
- optional tabs for All / In Progress / Completed / Locked

### Story Card Anatomy
Each story card should include:
- cover illustration
- story title
- one-line hook
- status chip
- optional progress indicator
- optional theme tag

### Status Chips
Use a consistent status system:
- New
- In Progress
- Completed
- Locked

Never rely on color alone.
Each status must also differ by label and icon.

### Emotional Goal
“Which life do I want to live next?”

## Story Detail Screen

### Purpose
Bridge the collection page and the first story card.

### Must Show
- large story art
- short premise
- danger or tone indicators
- unlocked endings summary
- Start Life or Continue button
- Back button

### Optional
- estimated story size
- last reached node/date
- story origin tag
- genre tag
- progress track

### Emotional Goal
“I understand what kind of life I’m about to enter.”

## In-Game Shell Alignment

The story card system is already defined elsewhere.
This document only defines how it visually aligns with the game shell.

### Shared Rules
- top-safe health area
- clear title and text hierarchy
- two large decision zones
- minimal utility icons
- strong background readability
- no decorative overload

### Utility Icons
Allowed:
- pause
- settings
- journal / flow
Use sparingly.

## Ending Screens

There must be two presentation families:

### Death Ending
Show:
- dramatic ending artwork
- ending title
- short summary
- Retry
- Back to Collection

### Fate or Success Ending
Show:
- ending artwork
- ending title
- short summary
- fate badge / seal
- endings unlocked count
- replay CTA
- back to collection CTA

### Emotional Goal
The ending screen should feel collectible and memorable.
It should not feel like a generic failure popup.

## Settings and Pause

### Purpose
Provide a clean universal system layer independent of story theme.

### Must Show
- music volume
- sfx volume
- language
- text size
- vibration
- accessibility
- return / quit options

### Style Rules
- clean grouped sections
- consistent neutral panels
- no story-specific clutter
- high readability

## Global Accessibility Rules

### Touch
- all tappable elements must be large enough for touch-first usage
- avoid tiny back buttons and tiny chip controls

### Text
- body text must remain readable at implementation stage
- headings and summaries must preserve hierarchy
- avoid overpacking text in story cards

### Contrast
- text must remain readable against all backgrounds
- meaningful UI elements must maintain visual contrast
- do not convey critical information by color alone

### Implementation Note
Generated UI images are concept assets.
Final product text should be real UI text, not baked image text, wherever possible.

## Prompt Contract for Visual Generation

Use a shared master prompt for all screens.
Then add a screen-specific prompt.

### Master Prompt Rules
- no code
- no device frame
- no watermark
- no fake hand interaction
- high-fidelity 2D UI mockup
- mobile portrait orientation
- consistent spacing
- clean text zones
- premium collectible narrative tone
- one unified meta-UI shell

### Required Screen Prompts
- main menu
- story collection
- story detail
- ending screen
- settings / pause

## Production Deliverables

Before coding starts, prepare:
- main menu concept
- collection screen concept
- story detail screen concept
- ending screen concepts
- settings overlay concept
- UI element sheet
- status chip sheet
- story card cover variations
- color accent set for each story family

## Do

- keep the shell consistent
- let story cards carry theme variety
- design for quick comprehension
- prioritize readability
- make the collection screen emotionally strong
- treat endings as collectible outcomes

## Don’t

- do not make every screen full medieval parchment
- do not overload the collection cards with too much metadata
- do not hide important state only through color
- do not design tiny mobile targets
- do not let each screen feel like a different game
- do not treat the ending screen as a generic popup
```

## Son karar

Bu aşamada sana en çok fayda sağlayacak tek dosya, uygulama öncesi ortak sözleşme olarak çalışacak bir **`UI_UX_ART_DIRECTION.md`** dosyasıdır. Bu dosya, hikâye üretim dökümanlarından ayrı kalmalı; çünkü artık mesele “hikâyenin içeriği” değil, “birden fazla hikâyeyi taşıyan oyun kabuğunun dili”dir. Referans oyunların gücü kart mantığı, büyük seçim alanları, sade HUD ve tekrar oynanabilir akıştan geliyor; senin tarafta buna ek olarak güçlü bir hikâye koleksiyonu ve markasal ana kabuk ihtiyacı var. Bu belge tam olarak o boşluğu kapatır. citeturn2view4turn2view0turn0search1turn3image0turn3image2