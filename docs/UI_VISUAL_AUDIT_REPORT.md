# Unity Narrative UI Visual Audit Report

Audit date: 2026-06-19  
Scope: mevcut Unity narrative UI ekranı, mevcut referans/current PNG'leri, runtime UI hiyerarşisi, `smm_card_001` verisi ve ilk kart görselleri.  
Constraint: kod değiştirilmedi, asset üretilmedi, yalnız rapor oluşturuldu.

## İncelenen Kanıtlar

- `docs/CODEX_UI_MASTER_REDESIGN_PACKAGE/ui_reference/current_ui_front.png`
- `docs/CODEX_UI_MASTER_REDESIGN_PACKAGE/ui_reference/current_ui_result.png`
- `docs/CODEX_UI_MASTER_REDESIGN_PACKAGE/ui_reference/reference_choice_front.png`
- `docs/CODEX_UI_MASTER_REDESIGN_PACKAGE/ui_reference/reference_card_result.png`
- `Fate-in-Your-Hands/Assets/Scenes/SampleScene.unity`
- `Fate-in-Your-Hands/Assets/Scripts/Story/SmmStoryAutoStart.cs`
- `Fate-in-Your-Hands/Assets/Scripts/Story/SmmStoryGameController.cs`
- `Fate-in-Your-Hands/Assets/Scripts/Story/SmmChoiceCardView.cs`
- `Fate-in-Your-Hands/Assets/Scripts/Story/HealthHeartsView.cs`
- `Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/story_data.json`
- `Fate-in-Your-Hands/Assets/Resources/Stories/saray_mutfagindan_muhre/story_data.json`
- `choice_smm_card_001_a.png`, `choice_smm_card_001_b.png`
- `bg_saray_bulasikhanesi_sabah.png`

Not: Repo içinde `sfmm_card_001` veri kartı bulunmuyor. Referans dokümanda bu ad geçiyor, fakat aktif story id `smm_card_001`. Bu raporda `sfmm_card_001` özel notları aktif kart olan `smm_card_001` üzerinden değerlendirilmiştir.

## Genel Karar

UI teknik olarak bir seçim akışını çalıştırıyor ve bazı önceki sorunlar kısmen azaltılmış: gameplay başlığı ve `Can` label'ı mevcut kodda gizlenmiş, choice sonucu ayrı modal yerine kart arkasına taşınmış, kalpler text glyph yerine sprite olarak üretiliyor. Buna rağmen ekran hâlâ profesyonel bir Choice-of-Life benzeri kart deneyimine yaklaşmıyor.

Ana sebep tek tek renk veya ölçü hatası değil; UI'ın sahne/prefab/design-system olarak tasarlanmaması. `SampleScene` neredeyse boş, runtime `SmmStoryAutoStart` ile tek controller yaratılıyor, controller da bütün Canvas, panel, text, card, result, settings ve debug hiyerarşisini kodla kuruyor. Bu yaklaşım hızlı prototip için uygun, fakat görsel kaliteyi inspector/prefab/tasarım tokenları yerine sabit renkler, anchor oranları, prosedürel ikonlar ve generic `Image`/`Text` bileşenlerine bağlıyor.

## Kesin Problem Listesi

| ID | Öncelik | Problem | Kök neden | Önerilen çözüm |
|---|---|---|---|---|
| P01 | critical | Layout hiyerarşisi profesyonel UI sistemi gibi değil, runtime'da elle çizilmiş prototip gibi davranıyor. | `SmmStoryGameController.BuildUi()` bütün UI'ı kodla yaratıyor; prefab, theme asset, reusable visual component ve editor-authored layout yok. | Narrative ekranını prefab tabanlı hiyerarşiye ayır: `BackgroundLayer`, `FloatingHUDLayer`, `NarrativeScrollLayer`, `ChoiceLayer`, `CardAnimationLayer`, `DebugLayer`. Renk/ölçü/font/animasyon değerlerini `NarrativeUITheme` benzeri tek kaynağa taşı. |
| P02 | high | HUD hâlâ tam anlamıyla "floating HUD" hissi vermiyor. | Kodda title/label gizlense de `hudPanel` tüm safe area'yı kaplıyor; settings ve hearts hard-coded anchorlarla yerleştiriliyor. Eski current screenshot'ta geniş toolbar çok baskın. | HUD'u gerçek yüzen elemanlara indir: sol üstte medallion settings, üst merkezde büyük kalpler, gerekirse sağ üstte küçük rank badge. HUD background/bar algısını tamamen kaldır. |
| P03 | high | Can/heart UI karakterli değil; küçük durum ikonu gibi kalıyor. | `HealthHeartsView` kalpleri 64 px procedural texture olarak formülle üretiyor; authored sprite, kırık kalp, dolu/yarım/boş arası güçlü siluet ve per-heart feedback yok. Pulse tüm gruba uygulanıyor. | Authored veya vektörel 3-4 durumlu heart seti kullan. Desktop 38-48 px, net outline, full/half/empty/broken ayrımı, hasar/iyileşmede ilgili kalpte pop/shake/glow animasyonu. `Can` label görünmemeli. |
| P04 | critical | Narrative text paneli hâlâ kart deneyiminin üstüne yapıştırılmış panel gibi duruyor. | "Narrative Scroll" adı verilmiş olsa da panel procedural dikdörtgen, iki renkli cap ve outline'lardan oluşuyor; gerçek parşömen/scroll asset veya 9-slice karakter yok. Metin `resizeTextForBestFit` ile sıkıştırılıyor. | Üç parça veya 9-slice parşömen scroll kullan. Olay metnini 2-4 satırda, yüksek kontrastlı, paginated ve en az 18 px kalacak şekilde göster. Dialogue ve body tek doğal anlatı bloğu olarak ele alınmalı. |
| P05 | high | Font okunurluğu zayıf ve üretim kalitesinde değil. | UI `UnityEngine.UI.Text` + `LegacyRuntime.ttf`/`Arial.ttf` kullanıyor; TextMeshPro font atlası, Türkçe karakter kalite kontrolü, tasarlanmış line-height ve font pairing yok. | TMP'ye geç; Türkçe karakter destekli tek ana font ve opsiyonel başlık fontu seç. Body, caption ve result için farklı ama tutarlı ölçü/line-height tokenları tanımla. |
| P06 | critical | Aktif story JSON içinde `smm_card_001` metninde literal `?` karakterleri var. | `story_data.json` ve Resources kopyasında Türkçe karakterler bozulmuş görünüyor: `kap?s?n?n`, `so?u?u`, `s?yledi?ini`, `bula??k??`. | Kaynak story verisini UTF-8 olarak düzelt ve Unity'nin okuduğu iki JSON kopyasını aynı içerikle doğrula. Bu düzelmeden hiçbir UI metin sunumu kaliteli görünmez. |
| P07 | high | Choice kartları ölçü olarak artık hedefe yaklaşsa da fiziksel, keyifli seçilebilir nesne hissi zayıf. | Kart front yüzü generic `Image` yüzey, düz outline, tek koyu caption bandı ve art mask'ten oluşuyor. Çerçeve, iç yüzey, hover accent ve kart materyali authored değil. | Tek güçlü outer frame, karakterli kart formu, açık hiyerarşili caption ribbon, büyük art alanı, daha belirgin hover/focus/pressed state ve bütün kartı "elde tutulur nesne" gibi hissettiren gölge/scale/motion tasarla. |
| P08 | high | Kart title kullanımı eski screenshot'ta görsel ağırlık yaratıyor; mevcut kodda gizlenmiş ama sistemde ölü title akışı duruyor. | `ApplyTitlePresentation()` başta return edip title/kicker'ı boşaltıyor; altında unreachable eski title split kodu kalmış. UI hiyerarşisinde title/kicker text objeleri hâlâ yaratılıyor ve layout zihinsel yükü sürdürüyor. | Gameplay'de `card.title` için UI objesi yaratma. Title sadece archive/debug/story map için saklansın. Narrative panel alanı başlık rezervi taşımamalı. |
| P09 | high | Result card hâlâ güçlü bir sonuç nesnesi gibi değil. | Choice result artık kart arkasında gösteriliyor; bu doğru yönde. Ancak back face düz taupe yüzey, text-only sonuç, ikon/motif yok, result metni best-fit ile küçülüyor, continuation affordance görünmez. | Back face'i front ile aynı prestijde tasarla: özel back frame, consequence ikon/motif, kısa doğal result text, text fade/pop, küçük dokunma ipucu veya sembol. Büyük `Sonuç`/`Devam Et` chrome'u choice result için kullanılmamalı. |
| P10 | medium | Result animasyonunun bir kısmı stabil, bir kısmı FPS'e bağlı. | Flip hareketi `unscaledDeltaTime` ile iyi yönde; fakat unselected exit ve continue exit sabit frame döngüsü ile ilerliyor. 30/60/120 FPS hissi eşit olmayabilir. | Tüm motion sürelerini zaman tabanlı yap. Hover, exit, continue ve result reveal için duration/easing tokenları tanımla. |
| P11 | high | Renk paleti tek nota bej/kahverengi algısını sürdürüyor. | Parchment, taupe, brown ve red sabitleri UI'ın çoğunu taşıyor; `Sage`, `DustyBlue`, `Gold` tanımlı olsa da ekran hiyerarşisinde belirleyici değil. Background da çoğunlukla beige wall/floor. | UI materyallerini ayır: parchment scroll, card frame, dark back, terracotta heart, dusty-blue settings, gold/sage micro-accent. Aynı outline ve aynı panel rengi her yerde tekrar edilmemeli. |
| P12 | high | Background bir oyun sahnesi atmosferi kurmak yerine boş duvar + büyük prop gibi kalıyor. | `bg_saray_bulasikhanesi_sabah.png` çok geniş düz duvar ve sol tarafta ağır lavabo/prop kompozisyonuna sahip. UI için safe-zone var ama dramatik derinlik, focal hierarchy ve kartları öne çıkaran ışık/renk kontrastı zayıf. | Background'ları UI kompozisyonuyla birlikte yeniden art-direct et: merkez kart alanı temiz, kenarlarda düşük kontrast dekor, scroll arkasında sakin yüzey, kartların arkasında yeterli değer kontrastı. |
| P13 | medium | Spacing ve ekran dengesi matematiksel olarak çalışıyor ama optik olarak tasarlanmamış. | Anchors doğrudan oranlarla veriliyor: narrative `0.230-0.770 / 0.690-0.855`, choice `0.220-0.780 / 0.060-0.625`. Bu oranlar component içeriğiyle değil ekran kompozisyonuyla elle bağlanmış. | Layout'u content ölçülerinden türet: HUD yüksekliği, scroll yüksekliği, card size, card gap ve bottom safe margin tokenlarıyla responsive layout kur. Optik merkez ve kart-zone merkezi ayrı hesaplanmalı. |
| P14 | high | Mobil uyumluluk minimum düzeyde; özel mobil kart deneyimi yok. | Portrait modda `GridLayoutGroup` kapatılıyor ve iki kart üst üste stretch ediliyor. Carousel, swipe, aktif kart odağı, touch affordance ve küçük ekran font testi yok. | 390x844 için mobile-first varyant tasarla: carousel veya iki kompakt stacked card, 48 px hit target, safe area altı hearts, scroll pagination ve tap/swipe affordance. |
| P15 | medium | Kullanıcıya gösterilen debug/teknik metinler varsayılan olarak gizli ama production gating net değil. | F1 ile `Developer Panel` açılabiliyor; card id, background id, choice image id, counters ve flags gösteriyor. Fatal error UI da doğrudan teknik hata mesajı gösteriyor. | Debug paneli sadece development build/editor flag ile açılmalı. Production fatal state kullanıcı dostu olmalı; teknik detay log'a gitmeli. |
| P16 | medium | Central result overlay hâlâ generic modal olarak sistemde duruyor. | Choice result kart arkasına alınmış, fakat ending/result overlay `Result Parchment`, `Result Title`, `Continue Button` ile ayrı modal dilini sürdürüyor. | Ending için ayrı tasarım yapılacaksa kart-back diliyle akraba, büyük final illustration + sakin CTA şeklinde tasarlanmalı. Choice result ile aynı generic modal sistemi paylaşmamalı. |
| P17 | medium | Settings butonu hâlâ generic UI button gibi. | `CreateIconButton()` procedural cog sprite ve düz dikdörtgen Image background yaratıyor. Medallion, hover rotation, crafted silhouette yok. | Settings'i 56-72 px yüzen icon/medallion olarak tasarla; dusty-blue veya metalik accent, hafif scale/rotation hover ve custom cursor state ile güçlendir. |
| P18 | high | UI asset sistemi authored değil; birçok kritik öğe procedural. | Kalp sprite'ları, cog sprite, panel cap'leri, card back ve scroll yüzeyleri runtime shape/color ile oluşuyor. Bu da oyunun el çizimi asset diliyle UI arasında kalite farkı yaratıyor. | UI asset manifesti oluştur: scroll caps/center, choice frame, back frame, hearts, settings, tap hint, result icons. Import ayarları ve 9-slice borderları denetlenmeli. |

## Neden Amatör/Prototip Gibi Duruyor?

1. UI objeleri sanat yönetimiyle tasarlanmış prefablar değil, tek controller içinde yaratılan renkli dikdörtgenler.
2. Ekranın ana odağı kartlar olmalı; fakat background prop, narrative panel, HUD ve kartlar aynı görsel ağırlıkta yarışıyor.
3. Kenarlık, gölge ve panel dili her yerde aynı: koyu outline + parchment/taupe yüzey. Bu, ayrı UI öğeleri yerine üst üste konmuş placeholder kutular hissi veriyor.
4. Font sistemi profesyonel değil. Legacy UI Text, best-fit küçültme ve bozuk Türkçe karakterler ekranı hemen prototip seviyesine çekiyor.
5. Motion ve state feedback davranış olarak var, fakat görsel ödül düşük: hover küçük, result text sade, continuation görünmez, kart arkasında hatırlanacak bir motif yok.

## Choice-of-Life Tarzı Kart Deneyimine Neden Yaklaşmıyor?

- Referans deneyimde kartlar ekranın ana olayıdır; burada kartlar hâlâ UI panel sisteminin bir parçası gibi görünüyor.
- Referans HUD hafif, ikonografik ve yüzer; mevcut UI'da HUD hiyerarşisi hâlâ geniş safe-area panel mantığından geliyor.
- Referans result, seçilen kartın aynı nesne olarak dönüşmesidir; mevcut akış bunu teknik olarak yakalamaya başlamış olsa da görsel back face hâlâ boş kart arkası yerine sade text panel gibi.
- Referans kartlar büyük, net, el yapımı çerçeveli ve hover/focus durumunda davetkar; mevcut kartlar düz frame + koyu caption + art crop düzenine sıkışıyor.
- Referans background, UI'ı taşıyan atmosferdir; mevcut background çoğu sahnede geniş boş duvar ve büyük kenar prop'u olarak kalıyor.

## Kartlar Neden Keyifli Seçilebilir Hissettirmiyor?

- Caption ribbon seçim metnini taşıyor ama kararın duygusunu büyütmüyor.
- Art alanı büyük olsa bile choice art direction çoğu zaman "eylemin dramatik anı" değil "tek nesne/jest" gösteriyor.
- Hover/focus state yalnız scale, y-offset, shadow ve border rengiyle sınırlı; kartın üstüne gelince beklenen oyun hissi, ses/ışık/çerçeve accent'i yok.
- Kart çerçevesi authored değil; dolayısıyla kartlar dünyaya ait nesnelerden çok runtime UI container gibi algılanıyor.
- İki seçenek arasında duygusal risk/ödül farkı ilk bakışta yeterince okunmuyor.

## Can/Heart UI Neden Kötü Duruyor?

- Prosedürel kalp formu temiz ama marka/oyun kimliği taşımıyor.
- Boş/yarım/dolu ayrımı küçük ölçekte yeterince karakterli değil.
- Hasar ve iyileşme feedback'i tüm health grubunu pulse ediyor; hangi kalbin değiştiği net hissedilmiyor.
- Heart seti background ve kart materyaliyle aynı sanat pipeline'ından gelmediği için HUD küçük teknik gösterge gibi duruyor.

## Result Card Neden Zayıf Duruyor?

- Back face yalnız düz koyu yüzey ve ortalanmış result text ile kuruluyor.
- Sonucun tonu ikon, motif, renk değişimi, kısa animasyon veya kart sırtı süslemesiyle desteklenmiyor.
- Result text `resizeTextForBestFit` ile küçülebiliyor; uzun consequence metni kart arkasında prestijli değil, sığdırılmış hissediyor.
- Full-card continue davranışı doğru yönde, fakat oyuncuya ince bir dokunma/continue affordance verilmezse "ne yapacağım?" anı oluşabilir.

## Kaldırılması Gereken Görsel Öğeler

- Eski/current screenshot'taki geniş üst toolbar görünümü.
- Gameplay ekranındaki story title, chapter kicker ve büyük card title rezervleri.
- Choice result için `Sonuç` başlığı ve görünür `Devam Et` butonu kalıntıları.
- Generic dikdörtgen panel/outline tekrarları.
- Production'da F1 ile açılabilen teknik id/counter/flag paneli.
- Runtime procedural cog/heart/scroll cap'lerinin production görsel dili olarak kullanılması.
- `CreateChoiceCardLegacy()` gibi eski result/button/title dilini taşıyan legacy UI yolu.

## Yeniden Tasarlanması Gereken Öğeler

- HUD: gerçek floating settings medallion + üst merkez heart seti.
- Health hearts: authored full/half/empty/broken sprite veya kaliteli vector UI.
- Narrative scroll: 9-slice/üç parçalı parşömen banner, pagination ve TMP typography.
- Choice card frame: tek güçlü outer frame, karakterli caption, net hover/focus/pressed state.
- Result back: same-size card back, consequence icon/motif, short readable text, full-card continue hint.
- Background: UI-safe unified environment illustration, değer kontrastı ve kenar dekor dengesi.
- Mobile layout: stacked cards yerine carousel veya özel kompakt mobile card mode.
- Theme system: renk, font, spacing, card size, animation duration ve sprite referansları merkezi tokenlarla yönetilmeli.

## `sfmm_card_001` / `smm_card_001` Özel Notları

- Aktif kart id'si `smm_card_001`; `sfmm_card_001` repoda yalnız referans doküman typo'su olarak görünüyor.
- `smm_card_001` başlangıç kartı ve vertical slice için doğru odak noktası.
- Kart title'ı `Yetimlikten Bulaşıkhaneye: İlk Eşik`; mevcut runtime gameplay'de title gizlenmiş. Bu doğru, korunmalı.
- `story_data.json` içinde `smm_card_001` body/result metinleri bozuk Türkçe karakter taşıyor. Örnek: `Saray kap?s?n?n`, `so?u?u`, `s?yledi?ini`, `bula??k??`. Bu critical görsel/okunurluk sorunudur.
- `choice_smm_card_001_a.png` görseli kırık tabak ve durdurma eliyle dürüstlük/itiraf fikrine yakın; ama karakter/bedel/ortam ilişkisi zayıf. Seçimin dramatik sonucu ilk bakışta anlaşılmıyor.
- `choice_smm_card_001_b.png` suç yöneltme fikrini işaret eden parmakla anlatıyor; ancak A seçeneğiyle aynı "el + tabak" ailesinde kaldığı için iki kart arasında duygusal ve görsel kontrast düşük.
- İlk kart background'u `bg_saray_bulasikhanesi_sabah.png` boş duvar ve büyük sol lavabo kompozisyonu taşıyor. UI safe-zone açısından kullanılabilir, fakat atmosfer ve derinlik zayıf; kartların prestijini artırmak yerine sahneyi boş gösteriyor.
- A/B choice metinleri kısa ve uygun uzunlukta; asıl sorun text içeriğinden çok kart yüzeyi, art direction ayrışması ve encoding bozulması.
- `smm_card_001` için öncelik sırası: önce UTF-8 text düzeltmesi, sonra authored card/back/heart/scroll UI, sonra iki choice görselinin daha dramatik ayrışması.

## Öncelik Özeti

critical:
- Runtime/prototip UI hiyerarşisinin design-system'e dönüştürülmesi.
- Narrative scroll ve text presentation kalitesi.
- `smm_card_001` Türkçe karakter bozulmasının düzeltilmesi.

high:
- HUD/heart redesign.
- Choice card physicality ve hover/focus polish.
- Result card back redesign.
- Renk paleti ve background composition.
- Mobil layout'un gerçek mobile card experience'a çevrilmesi.

medium:
- FPS bağımsız motion tamamlama.
- Debug panel production gating.
- Ending modal/result overlay dilinin ayrıştırılması.
- Settings button polish.

low:
- Eski unreachable title split ve legacy UI yollarının temizlenmesi.
- İnce spacing/token standardizasyonu ve screenshot checklist otomasyonu.

## Sonuç

Mevcut UI bir vertical slice'ın teknik omurgasına sahip, fakat görsel kaliteyi belirleyen parçalar hâlâ runtime-generated placeholder sisteminde kalıyor. Choice-of-Life tarzı deneyime yaklaşmak için önce kodun "çalışan ekran" üretmesi değil, tasarımın "oyuncunun dokunmak istediği kart nesnesi" üretmesi gerekiyor. En büyük kaldıraçlar: gerçek UI asset sistemi, güçlü kart/back face, okunur TMP typography, authored hearts, bozuk Türkçe metinlerin düzeltilmesi ve mobil için ayrı kart deneyimi.
