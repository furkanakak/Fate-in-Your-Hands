# CODEX MASTER TASK — Narrative Card UI Audit, Redesign and Unity Vertical Slice

## 0. Görevin özeti

Mevcut Unity arayüzü teknik olarak seçim yaptırıyor; ancak görsel tasarım, metin sunumu, kart hiyerarşisi, sonuç gösterimi ve animasyon kalitesi hedefin çok altında.

Bu görev bir “ufak UI rötuşu” değildir.

Önce mevcut ekranı ve referans ekranları ayrıntılı biçimde incele. Ardından mevcut hikâye motorunu bozmadan, profesyonel ve keyifle oynanabilir bir **iki seçimli narrative card UI** oluştur.

Önce yalnızca tek kart üzerinde eksiksiz bir vertical slice tamamla. Vertical slice gerçekten iyi görünmeden bütün hikâyeye yayma.

Bu klasördeki referansları incele:

```txt
docs/ui_reference/reference_choice_front.png
docs/ui_reference/reference_card_result.png
docs/ui_reference/current_ui_front.png
docs/ui_reference/current_ui_result.png
```

Referans ekranları birebir kopyalama. Marka, karakter, arma, harita ve özgün assetleri taklit etme. Yalnızca şu tasarım ilkelerini referans al:

- yüzen ve hafif HUD,
- büyük ve baskın seçim kartları,
- kısa anlatı şeridi,
- çevreyi dolduran bütünlüklü background,
- seçilen kartın arka yüzünde sonuç gösterimi,
- ekranın boşluk ve hiyerarşi açısından dengeli kullanılması,
- sade fakat karakterli 2D kart oyunu hissi.

---

# 1. Mevcut ekranın zorunlu tasarım denetimi

Herhangi bir kod değiştirmeden önce aşağıdaki sorunları mevcut prefab, hierarchy, script ve Game View üzerinden doğrula. `UI_AUDIT_BEFORE.md` dosyasına bulguları yaz.

## 1.1 Genel kompozisyon sorunları

Mevcut ekranda:

- Ekran neredeyse bütünüyle aynı bej-kahverengi tonlardan oluşuyor.
- Background, panel, kart, caption ve HUD birbirinden yeterince ayrışmıyor.
- Üstteki geniş HUD barı ekranı yatay olarak kesiyor; buna rağmen bilgi zayıf ve küçük.
- Background bütünlüklü bir sahne gibi değil; pencere, kapı, lamba, ekmek sepeti gibi büyük clip-art parçalarının sahneye ayrı ayrı yerleştirilmiş hâli gibi görünüyor.
- Büyük dekoratif objeler anlatıdan ve kartlardan daha fazla dikkat çekiyor.
- Ekranın orta-alt kısmında fazla boş alan bulunurken seçim kartları küçük kalıyor.
- Ana etkileşim odağı seçim kartları olması gerekirken anlatı kutusu ve background objeleri aynı görsel ağırlığı taşıyor.
- UI, profesyonel bir oyun ekranından çok birbirine eklenmiş panel ve sprite prototipi gibi duruyor.

## 1.2 HUD sorunları

- “Saray Mutfağından Mühre” başlığı geniş bir bar içinde gereksiz yer kaplıyor.
- Sağlık göstergesi küçük ve uzakta.
- Kalplerin bir bölümü dolu, bir bölümü boş olsa da küçük ölçekte ayırt edilmesi zor.
- `Can` yazısı ve kalpler arasında hizalama sorunu var.
- Kalpler görsel kimliği olmayan küçük ikonlar gibi görünüyor.
- Ayar butonu büyük bir dikdörtgen, içindeki ikon ise çok küçük.
- HUD’da boşluk dağılımı dengesiz.
- Referansın güçlü yüzen ikon yaklaşımı yerine ağır bir toolbar görünümü oluşuyor.

## 1.3 Anlatı paneli sorunları

- “Ustanın Sınavı”, “Kırık Tabak” gibi kart başlıkları oyun ekranında gereğinden fazla öne çıkıyor.
- Chapter adı ile kart başlığı iki kez bilgi veriyor.
- `Bölüm 1 • Yetimlikten Bulaşıkhaneye` gibi küçük metadata ile büyük kart başlığı aynı panelde gereksiz bir bilgi katmanı oluşturuyor.
- Kullanıcı karar vermek için olay metnini okumalı; başlık metadata’sını değil.
- Body text çok geniş satırlar hâlinde ve çok küçük fontla gösteriliyor.
- Panel masaüstünde bile okunması zor.
- Metin rengi ile panel rengi arasındaki kontrast yetersiz.
- Alıntı/dialogue metni fazla küçük ve silik.
- Panel büyük bir dikdörtgen olarak background’un üstüne yapıştırılmış görünüyor.
- Referans ekranlardaki parşömen/scroll anlatı şeridinin karakteri yok.
- Uzun body text tek seferde küçültülerek sığdırılıyor; font küçültmek çözüm olmamalı.
- Bazı metinler “başlık + bölüm adı + aynı olayın tekrarlandığı AI şablonu” hissi veriyor.

## 1.4 Seçim kartı sorunları

- Kartlar ekran için çok küçük.
- Seçim kartları oyunun ana odak noktası gibi görünmüyor.
- Görsel, kart yüzünü doldurmak yerine küçük bir resim kutusu gibi duruyor.
- Kartın etrafında ve içinde birden fazla border/panel kullanılıyor.
- Caption alanı görselden kopuk ve generic UI button gibi.
- Seçim metni küçük.
- Kartların dış silueti karakterli değil.
- Kartlar background’dan yeterince ayrılmıyor.
- Hover, focus ve selected durumları görsel olarak güçlü değil.
- Kartların birbirine göre konumu ve gap değeri zayıf.
- Choice görsellerinin bazıları yalnızca ekmek, anahtar, el, kaşık gibi nesneleri gösteriyor; seçimin fiilini göstermiyor.
- İki choice görseli eylem olarak ilk bakışta ayrışmıyor.
- Kartlar bütünüyle tıklanabilir bir fiziksel nesne hissi vermiyor.

## 1.5 Sonuç ve flip sorunları

Mevcut sonuç ekranı kabul edilmez:

- Seçilen kart döndükten sonra normal kart boyutunu kaybedip dar ve küçük bir kutuya dönüşüyor.
- Kart merkezde doğru görsel ölçüde konumlanmıyor.
- Result back, seçim kartının gerçek arka yüzü gibi değil; ayrı bir modal/panel gibi görünüyor.
- “Sonuç” başlığı gereksiz.
- “Devam Et” butonu kart arkasında büyük ve kaba görünüyor.
- Referans davranışta sonuç, kartın arka yüzünde doğrudan okunuyor; ayrı buton ağırlığı yok.
- Result metni küçük ve dar kolonda.
- Kart dönerken pozisyon atlıyor.
- Flip süreci akıcı değil.
- Front ve back yüzlerin ölçüsü/pivotu aynı olmayabilir.
- LayoutGroup kartın animasyon sırasında konumunu tekrar yazıyor olabilir.
- Parent değiştirme sırasında world position korunmuyor olabilir.
- Kart back face tam opak değilse ghost/transparency oluşabilir.
- Seçilmeyen kart doğru zamanda kaybolmuyor olabilir.
- Seçilen kartın arka yüzünde gerçek `resultText` yerine şablon sistem cümlesi gösteriliyor.
- Hidden counter veya mekanik sonuçlar oyuncuya doğal olmayan cümlelerle açıklanıyor.

## 1.6 Tipografi sorunları

- Font küçük ve bulanık.
- TextMeshPro font atlası düşük çözünürlükte olabilir.
- Birden fazla metin seviyesi arasında yeterli fark yok.
- Başlıklar küçük ekranda bile ağır, body ise zayıf.
- Auto-size metni gereğinden fazla küçültüyor olabilir.
- Türkçe karakterlerin render kalitesi kontrol edilmeli.
- Satır uzunluğu ve line-height okunabilir değil.

## 1.7 Background sorunları

- Background bir illüstrasyon değil, büyük dekoratif sprite katmanları gibi duruyor.
- Objeler UI safe-zone’a giriyor.
- Background, seçim kartlarının görsel diliyle aynı sahnenin parçası gibi görünmüyor.
- Tek renkli ve boş duvar, ekranı bitmemiş gösteriyor.
- Büyük objeler, anlatı ve seçim kartlarıyla rekabet ediyor.
- Derinlik, atmosfer ve focal hierarchy bulunmuyor.

---

# 2. Hedef ekran: zorunlu bilgi mimarisi

Yeni ekran şu katmanlardan oluşmalı:

```txt
NarrativeGameScreen
├── BackgroundLayer
│   ├── EnvironmentBackground
│   └── AmbientOverlay
├── FloatingHUDLayer
│   ├── SettingsButton
│   ├── HealthHearts
│   └── OptionalRankBadge
├── NarrativeScrollLayer
│   └── NarrativeScroll
├── ChoiceLayer
│   ├── ChoiceSlotLeft
│   └── ChoiceSlotRight
├── CardAnimationLayer
├── TransitionLayer
└── DebugLayer
```

## 2.1 Wide top toolbar kaldırılacak

Mevcut geniş yatay HUD barını kaldır.

Yeni HUD:

- Sol üst: yüzen settings/gears butonu.
- Üst orta: büyük ve net health hearts.
- Sağ üst: gerekiyorsa mevcut rol/rütbe için küçük arma veya ribbon.
- Oyun adı gameplay ekranında zorunlu değil.
- Story title pause menüsünde veya story select ekranında gösterilebilir.
- Gameplay ekranında teknik ID, card ID, chapter ID veya debug metni gösterilmez.

## 2.2 Hearts

Kalpler referanstaki gibi üst orta bölgede ana HUD göstergesi olmalı.

- 5 kalp göster.
- Her kalp 2 health temsil edebilir.
- Full, half, empty sprite durumları olmalı.
- Desktop kalp yüksekliği yaklaşık 38–48 px.
- Kalpler arası 8–12 px.
- Dolu kalp warm red/terracotta.
- Boş kalp parchment interior + dark outline.
- Sağlık kaybında kısa squash/pop + küçük shake.
- İyileşmede kısa glow/pop.
- `Can` yazısını kaldır veya çok küçük yardımcı label yap.
- Unicode/emoji/glyph kalp kullanma.
- Kalpler özel sprite veya kaliteli vector UI olmalı.

Eksikse image generation kullanarak şeffaf PNG üret:

```txt
ui_heart_full.png
ui_heart_half.png
ui_heart_empty.png
ui_heart_broken.png
```

## 2.3 Settings

- 56–72 px yüzen icon.
- Büyük dikdörtgen bar butonu olmasın.
- 2–3 dişli veya tek dekoratif dişli sprite olabilir.
- Hover/focus sırasında hafif dönme ve scale.
- Şeffaf background veya küçük parchment medallion.
- Image-gen gerekiyorsa `ui_settings_gears.png` üret.

## 2.4 Narrative scroll

Mevcut generic dikdörtgen paneli kaldır.

Yerine 9-slice veya üç parçalı parşömen scroll banner kullan:

- Sol scroll cap
- Stretchable center parchment
- Sağ scroll cap

Önerilen desktop ölçü:

- Genişlik: 760–980 px
- Yükseklik: 120–170 px
- Üstten: hearts sonrasında 20–30 px
- Yatay merkez

Anlatı scroll’u:

- yalnızca olay metnini gösterir;
- gameplay sırasında `card.title` büyük başlık olarak gösterilmez;
- `Ustanın Sınavı`, `Kırık Tabak` gibi başlıkları varsayılan UI’dan kaldır;
- title metadata debug/archive için korunabilir;
- speaker varsa `Speaker: “Line”` biçiminde metne doğal şekilde eklenebilir;
- metin 21–27 px arasında, net ve koyu;
- desktopta 2–4 satır;
- line-height 1.25–1.4;
- max line width yaklaşık 55–75 karakter;
- text center veya çok uzun metinde left aligned olabilir.

### Uzun metin

Fontu küçültüp tek panele sıkıştırma.

`NarrativePaginator` uygula:

- metni cümle sınırlarından 2–4 satırlık sayfalara böl;
- scroll içinde ilk metin sayfasını göster;
- tap/click/space ile sonraki metin sayfasına geç;
- choices yalnız son narrative sayfası görüldükten sonra aktif olur;
- küçük bir `tap_hand` veya aşağı ok göstergesi kullanılabilir;
- metin hiçbir zaman 18 px altına düşmesin.

### Başlık verisi

UI’da büyük kart başlığı gösterme.

İstenirse yalnız küçük bir chapter ribbon:

```txt
Bölüm 1
```

kullanılabilir. Bu da zorunlu değil.

`card.title` story archive, debug inspector ve analytics için korunur; gameplay ana ekranında varsayılan olarak gizlenir.

---

# 3. Choice kart tasarımı

## 3.1 Yerleşim

1920×1080 referansında:

- Kart genişliği: 350–410 px
- Kart yüksekliği: 480–560 px
- Kart gap: 50–80 px
- Kartlar alt-orta bölgede
- Kartların üstü narrative scroll ile çakışmaz
- Alt safe margin 50–80 px

1366×768’de:

- Kart genişliği yaklaşık 270–310 px
- Kart yüksekliği yaklaşık 370–430 px

Kartlar ekranın ana etkileşim odağıdır.

## 3.2 Front-face düzeni

Referans yaklaşımına yakın ama özgün bir kart:

```txt
ChoiceCardFront
├── CardFrame
├── ChoiceCaptionRibbon
└── ChoiceArtwork
```

- Tek bir güçlü outer frame.
- İç içe 3–4 border kullanma.
- Caption üstte kısa band/ribbon olarak yer alabilir.
- Choice text 20–26 px.
- Art kalan büyük alanı doldurur.
- Art küçük thumbnail gibi görünmez.
- Frame warm parchment.
- Inner art background choice görseli tarafından doldurulur.
- Border dark brown, 2–4 px.
- Corner radius veya el çizimi düzensiz köşe.
- Hafif shadow.

Choice text en fazla 2–3 satır.
Tüm kart tıklanabilir.

## 3.3 Artwork

Choice art:

- seçimin eylemini gösterir;
- yalnızca nesne göstermesi yeterli değildir;
- oyuncu choice text’i okumadan yaklaşık eylemi anlayabilir;
- bir ana karakter, gerekiyorsa bir yan karakter;
- eylemde kullanılan prop büyük ve okunur;
- yüz, el ve beden hareketi açık;
- background yalnızca 1–2 mekân ipucu;
- kart boyutunda net.

Hatalı örnek:

```txt
A hand, a bowl and a feather.
```

Doğru örnek:

```txt
The orphan dramatically imitates the palace goose’s walk while pointing toward the grain bowl, as a kitchen servant watches in disbelief.
```

Hatalı:

```txt
A loaf of bread.
```

Doğru:

```txt
The orphan places half of a loaf into a hungry child’s open hands while keeping the remaining half close to the apron.
```

Eksik/hatalı art varsa image-gen ile yeni 4:5 PNG üret, manifesti güncelle.

## 3.4 Card states

`ChoiceCardView` şu state’lere sahip olmalı:

```txt
Hidden
Entering
Idle
Hovered
Focused
Pressed
Selected
Exiting
MovingToResult
FlipFront
FlipBack
ShowingResult
LeavingResult
```

Hover:

- 150–180 ms
- scale 1 → 1.035
- y +8 px
- shadow intensity artar
- border accent olur
- hafif highlight sweep olabilir

Press:

- 80–120 ms
- scale 1.035 → 1.01

Keyboard/gamepad focus hover ile aynı kaliteyi taşımalı.

---

# 4. Sonuç gösterimi: “Devam Et” butonu olmayacak

Bu zorunlu bir değişikliktir.

## 4.1 Ayrı modal yasak

- Merkezî sonuç modalı kullanma.
- Küçük ayrı result panel kullanma.
- “Sonuç” başlığı kullanma.
- Kart arkasında büyük “Devam Et” butonu kullanma.

## 4.2 Kartın arka yüzü

Seçilen kart:

1. diğer kart kaybolduktan sonra merkeze gelir,
2. aynı dış ölçüyü korur,
3. döner,
4. sonucu kendi back face’inde gösterir.

Back face:

- front ile aynı genişlik/yükseklik;
- dark muted brown / warm taupe inner surface;
- parchment border;
- ortada büyük, kısa ve okunabilir `resultText`;
- optional result icon:
  - broken heart,
  - small heart,
  - coin,
  - suspicion eye,
  - neutral swirl;
- hidden flag/counter adı gösterme;
- `Sonuç` başlığı gösterme;
- buton gösterme.

## 4.3 Devam etme davranışı

Result back açıldıktan sonra:

- 350–500 ms input lock;
- sonra kartın tamamı click/tap ile devam edilebilir;
- keyboard/gamepad: Enter, Space veya Submit;
- alt bölümde küçük, zarif bir yardımcı metin veya ikon:

```txt
Devam etmek için karta dokun
```

Bu metin de zorunlu değil; `tap_hand` veya küçük chevron yeterli olabilir.

Kartın tamamına tıklama:

- result card hafif scale-down yapar;
- fade/slide çıkar;
- state uygulanır;
- autosave;
- next card veya ending yüklenir.

Ayrı buton kullanılmaz.

---

# 5. Flip animasyonunun teknik çözümü

Mevcut Y-rotation/layout yaklaşımı zıplama ve ghost üretiyorsa bunu yamama.

UI için stabil, iki aşamalı **scale-X flip** kullan.

## 5.1 Prefab hierarchy

```txt
ChoiceSlot
├── SlotPlaceholder
└── ChoiceCardRoot
    ├── Shadow
    ├── CardFront
    │   ├── Frame
    │   ├── Caption
    │   └── Artwork
    └── CardBack
        ├── BackSurface
        ├── ResultText
        ├── ResultIcon
        └── ContinueHint
```

- `ChoiceSlot` layout tarafından yönetilir.
- Kart seçilince `ChoiceCardRoot`, world position korunarak `CardAnimationLayer` altına alınır.
- Slot içinde aynı ölçüde placeholder kalır.
- Layout bu sırada kartın animasyon pozisyonunu değiştiremez.
- Front ve Back aynı RectTransform ölçüsü/pivotu kullanır.
- Pivot `(0.5, 0.5)`.
- Back başlangıçta inactive.
- İki yüz hiçbir zaman aynı anda görünmez.
- Back tam opak.

## 5.2 Sequence

Önerilen sequence:

### Phase A — selection

- input lock
- nonselected card:
  - 180–220 ms
  - alpha 1 → 0
  - x dışa 20–35 px
  - scale 1 → 0.96
  - deactivate
- selected card:
  - slight press pulse

### Phase B — move to center

- selected card reparent to animation layer
- preserve world position
- target center: screen center x, card-zone center y
- 280–360 ms
- ease: `EaseInOutCubic` veya `EaseOutQuart`
- scale 1 → 1.05–1.12
- no position snap

### Phase C — flip

- 180–240 ms: `scaleX 1 → 0`
- scaleX yaklaşık 0.05 olduğunda:
  - front deactivate
  - back activate
- 180–240 ms: `scaleX 0 → 1`
- toplam flip 360–480 ms
- slight scaleY pulse 1 → 1.03 → 1
- optional subtle whoosh SFX
- card center position sabit kalır

### Phase D — result

- result text fade 0 → 1, 150–220 ms
- result icon pop 0.8 → 1
- card becomes clickable after lock delay

### Phase E — continue

- card click/submit
- scale 1 → 0.98
- alpha 1 → 0
- y -10 or +20 px
- 200–280 ms
- apply state
- autosave
- load next card

Animasyon `unscaledDeltaTime` ile çalışmalı.
30, 60 ve 120 FPS’te aynı timing.

DOTween varsa sequence kullan.
Yoksa coroutine + AnimationCurve.

## 5.3 Ghost ve zıplama engeli

- Front/back aynı anda render edilmez.
- CanvasGroup alpha ve `SetActive` açıkça yönetilir.
- LayoutGroup animation root’u kontrol etmez.
- Reparent sırasında:
  - world corners al,
  - animation layer local position’a dönüştür,
  - sizeDelta koru.
- Animation tamamlanana kadar rebuild/layout tetikleme.
- Aynı choice iki kez seçilemez.
- Result state sırasında diğer kart active olmamalı.

---

# 6. Yeni renk ve UI materyal sistemi

Mevcut “her şey bej” yaklaşımını kaldır.

Önerilen palet:

```yaml
ink: "#38251F"
parchmentLight: "#F3D9A4"
parchmentMid: "#DEB97C"
cardFrame: "#E7C38A"
cardBack: "#8E796F"
terracotta: "#B95643"
heartRed: "#D65342"
sage: "#71866E"
dustyBlue: "#68859A"
goldAccent: "#D4A94E"
shadow: "#2A1D1A55"
```

Kurallar:

- Background kendi renk kimliğine sahip olabilir.
- Scroll ve kart frame aynı aileden fakat aynı düz renkten oluşmamalı.
- Caption band kart back ile aynı koyu taupe olabilir.
- Heart kırmızı ana accent.
- Settings mavi/dusty-blue olabilir.
- Aynı kahverengi çizgiyi her panelde kullanma.
- Shadow hafif, temiz ve tutarlı.
- Gradient minimum.

---

# 7. UI asset üretimi

Mevcut UI assetleri yetersizse image generation aracını kullan.

Üretilebilecek UI assetleri:

```txt
ui_scroll_left_cap.png
ui_scroll_center_9slice.png
ui_scroll_right_cap.png
ui_choice_frame_9slice.png
ui_choice_back_9slice.png
ui_heart_full.png
ui_heart_half.png
ui_heart_empty.png
ui_heart_broken.png
ui_settings_gears.png
ui_tap_hand.png
ui_result_neutral.png
ui_result_positive.png
ui_result_negative.png
```

UI asset image prompt ortak dili:

```txt
Simple hand-drawn 2D medieval game UI asset, slightly uneven dark-brown ink outline, flat warm colors, minimal one-step shading, clean readable silhouette, playful handcrafted appearance, transparent background, no text, no logo, no watermark, no realistic rendering, no 3D.
```

### Scroll prompt

```txt
A blank horizontal parchment scroll UI banner with curled wooden scroll ends, warm cream paper, slightly uneven dark-brown ink outline, subtle handmade imperfections, a large clean empty center for text, transparent background, designed for 9-slice or three-part Unity UI construction.
```

### Heart prompt

```txt
A simple plump red heart icon for a hand-drawn medieval game UI, dark-brown outline, flat red fill, one small highlight, minimal shading, clean silhouette, transparent background.
```

### Choice frame prompt

```txt
A blank vertical choice-card frame for a hand-drawn medieval narrative game, warm parchment border, slightly uneven dark-brown outline, very subtle corner decoration, large empty artwork area, no text, transparent center or transparent background, suitable for 9-slice Unity UI.
```

Üretilen asseti gerçekten project path’e kaydet ve import ayarlarını yap:

- Sprite (2D and UI)
- uygun pixels per unit
- Filter Mode Bilinear
- Compression kalite kontrolü
- 9-slice border ayarı
- mipmap UI için kapalı

Image-gen kullanılamıyorsa üretmiş gibi davranma. Unity vector/9-slice çözümü kullan veya queue oluştur.

---

# 8. Background düzeni

Current background’daki dev bağımsız objeleri kaldır.

Yeni background:

- bütünlüklü tek environment illustration;
- UI safe-zone’a göre kompoze edilmiş;
- narrative scroll arkasında sade;
- kartların arkasında yeterli kontrast;
- kenarlarda dekor olabilir;
- merkez choice zone temiz;
- foreground objeler kartlara taşmaz;
- 16:9.

Background image prompt:

```txt
Simple hand-drawn 2D medieval palace-kitchen environment, unified scene composition, slightly uneven dark-brown outlines, flat warm colors, minimal shading, simplified architecture and props, clear depth layers, decorative details kept near the screen edges, clean open space in the upper center for a narrative scroll and in the lower center for two choice cards, no main character, no central action, no text, no UI, no photorealism, no 3D.
```

Her background promptunda safe-zone talebi olmalı.

---

# 9. Metin sunumu ve içerik formatter

## 9.1 Gameplay’de title gizle

`Ustanın Sınavı`, `Kırık Tabak` gibi `card.title` değerleri gameplay ana ekranında büyük başlık olarak gösterilmez.

Bu değerler:

- debug,
- story flow,
- archive,
- analytics

için saklanır.

Narrative scroll yalnız gerçek anlatı/dialogue metnini gösterir.

## 9.2 Metadata echo kaldır

UI formatter, bodyText’e chapter/title tekrarını yeniden eklememeli.

Şu yapı gösterilmemeli:

```txt
Yetimlikten Bulaşıkhaneye: Ustanın Sınavı anında...
```

Doğal anlatı gösterilmeli:

```txt
Kapıcı Boran, adını iki kez bir defterin kenarına yazar. Bulaşıkhanede ilk günün başlamadan önce niyetini tartıyor gibidir.
```

Underlying story verisi şablon kokuyorsa:
- ilk vertical slice kartlarında doğal Türkçe edit yap;
- yapılan metin değişikliklerini `NARRATIVE_COPY_FIXES.md` içinde listele;
- card/choice ID ve routing değiştirme.

## 9.3 Result text

Back face yalnız YAML’daki gerçek, doğal `resultText` metnini gösterir.

Şunu üretme:

```txt
“X yolunu seçersin. Bu hamle Y değerini belirgin biçimde yükseltir.”
```

Natural consequence:

```txt
Boran, ölçüyü bozmadan tamamladığını görünce sana ilk kez yalnız bir bulaşıkçı gibi bakmaz. Mutfakta küçük bir güven kazanırsın.
```

Hidden counter adı gösterme.

---

# 10. Responsive davranış

## Desktop 1920×1080

- Settings: top-left
- Hearts: top-center
- Optional rank badge: top-right
- Narrative scroll centered beneath hearts
- Two cards centered lower half

## 1366×768

- Scroll 700–820 px
- Cards 270–310 × 370–430 px
- Vertical space taşmaz

## Mobile 390×844

Seçeneklerden biri:

- iki kart yatay carousel/swipe,
- veya üst üste iki kompakt kart.

Tercihen horizontal carousel:

- aktif kart merkezde büyük,
- diğer kart kısmen görünür,
- swipe + tap,
- choice text okunur,
- hearts safe area altında.

Minimum hit target 48 px.

---

# 11. Input, ses ve his

## Input

- Mouse hover/click
- Touch tap/swipe
- Keyboard:
  - Left/Q = choice A focus
  - Right/E = choice B focus
  - Enter/Space = select/continue
- Gamepad:
  - left/right navigation
  - Submit select/continue
  - Cancel pause

## SFX

Varsa veya basit placeholder SFX ekle:

- card hover: soft paper tick
- select: soft paper tap
- card move: paper slide
- flip: brief paper whoosh
- result negative: muted low knock
- result positive: soft chime
- health loss: soft heartbeat crack

SFX metni bastırmamalı.

## Motion

- UI motion hızlı fakat sert değil.
- `Reduced Motion` ayarı destekle.
- Reduced motion’da:
  - hover scale azalt,
  - card flip yerine short crossfade kullan.

---

# 12. Unity mimarisi

Önerilen reusable bileşenler:

```txt
NarrativeGameScreenController
NarrativeScrollView
NarrativePaginator
HealthHeartsView
ChoiceCardsPresenter
ChoiceCardView
ChoiceCardAnimationController
ResultCardBackView
BackgroundPresenter
SafeAreaFitter
InputNavigationController
NarrativeUITheme
```

## NarrativeUITheme

ScriptableObject oluştur:

```yaml
colors:
fonts:
fontSizes:
spacing:
cardSizes:
animationDurations:
animationCurves:
spriteReferences:
audioReferences:
```

Magic numberları scriptlere dağıtma.

## Prefablar

```txt
PF_NarrativeGameScreen
PF_NarrativeScroll
PF_ChoiceCard
PF_HealthHearts
PF_FloatingSettings
```

Scene’e tek tek hard-code panel üretme.

---

# 13. Uygulama aşamaları

## Aşama 1 — Audit

Şunları üret:

```txt
UI_AUDIT_BEFORE.md
```

İçerik:

- hierarchy sorunları,
- layout ownership,
- pivot/anchor sorunları,
- flip ghost nedeni,
- result copy kaynağı,
- font sorunları,
- current asset sorunları.

## Aşama 2 — Static target

Yalnız `sfmm_card_001` için:
- yeni background,
- floating HUD,
- hearts,
- scroll,
- iki kart front-face.

Önce static screenshot üret.

## Aşama 3 — Interaction

- hover,
- focus,
- click,
- nonselected exit,
- selected center move,
- scale-X flip,
- result back,
- click-anywhere continue.

## Aşama 4 — First route

İlk karttan sonraki 3 karta kadar akışı test et.
Yalnız tek ekran mockup yapıp bırakma.

## Aşama 5 — Reusable rollout

Vertical slice PASS olunca tüm hikâyeye aynı prefab ve data binding sistemini uygula.

---

# 14. Unity Play Mode testleri

Gerçek Play Mode testi yap.

## Fonksiyon testleri

- Choice A mouse click
- Choice B mouse click
- keyboard navigation
- gamepad navigation
- rapid double-click lock
- result text doğru choice’a ait
- health doğru güncelleniyor
- nextCardId doğru yükleniyor
- endingId doğru açılıyor
- autosave
- missing asset fallback

## Görsel testler

- front state
- hover
- selected
- nonselected exit
- centered card
- mid flip
- result back
- continue transition
- next card
- mobile

## FPS/timing

- 30 FPS
- 60 FPS
- 120 FPS

## Resolution

- 1280×720
- 1366×768
- 1920×1080
- 2560×1440
- 390×844

---

# 15. Screenshot teslimleri

Şu klasöre kaydet:

```txt
C:\Users\furkan\Desktop\Saray_UI_Master_Redesign\
```

Dosyalar:

```txt
01_reference_notes.png
02_static_front_1920.png
03_front_1366.png
04_choice_hover.png
05_choice_selected.png
06_nonselected_exit.png
07_card_centered.png
08_mid_flip.png
09_result_back.png
10_next_card.png
11_mobile_front.png
12_mobile_result.png
```

Screenshot gerçekten Game View’dan alınmalı.

---

# 16. Kabul kriterleri

Aşağıdakilerin tamamı PASS olmadan bitmiş sayma:

## Layout

- Wide HUD toolbar yok.
- Settings yüzen icon.
- Hearts büyük ve üst merkezde.
- Narrative scroll karakterli ve okunur.
- Gameplay’de büyük kart title yok.
- Choice kartları ekranın ana odağı.
- Choice kartları küçük thumbnail değil.
- Background bütünlüklü tek environment.

## Cards

- Tek outer frame.
- Art alanı büyük.
- Choice text okunur.
- İki card action ilk bakışta ayrışır.
- Tüm kart tıklanabilir.
- Mouse/touch/keyboard/gamepad çalışır.

## Result

- Ayrı modal yok.
- “Sonuç” başlığı yok.
- “Devam Et” butonu yok.
- Seçilen kart aynı ölçüde merkeze gelir.
- Nonselected card görünmez.
- Flip zıplamaz.
- Ghost yok.
- Back tam opak.
- Gerçek natural resultText kullanılır.
- Back card’ın tamamına tıklayarak devam edilir.

## Polish

- Emoji/glyph hearts yok.
- TMP metin keskin.
- Bej-kahverengi kutu yığını görünümü yok.
- Büyük clip-art objeler safe-zone’a girmiyor.
- Hover/flip/transition smooth.
- 30/60/120 FPS timing aynı.
- Mobile layout kullanılabilir.
- SFX ve reduced-motion mevcut.
- Image-gen ile üretilen assetler doğru path ve manifestte.

---

# 17. Rapor dosyaları

Üret:

```txt
UI_AUDIT_BEFORE.md
NARRATIVE_COPY_FIXES.md
UI_ASSET_GENERATION_REPORT.md
UI_MASTER_REDESIGN_REPORT.md
UI_MASTER_VALIDATION_REPORT.md
```

Final rapor:

- tespit edilen kök nedenler,
- değiştirilen scene/prefab/script/style dosyaları,
- üretilen UI ve story assetleri,
- test sonuçları,
- screenshot yolları,
- remaining risks,
- PASS/FAIL.

`UI_MASTER_VALIDATION_REPORT.md` PASS olmadan “tamamlandı” deme.
