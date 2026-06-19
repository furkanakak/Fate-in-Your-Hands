# IMAGE GENERATION REPORT

Story package: `saray_mutfagindan_muhre`

## Batch: `BACKGROUND_AND_CHOICE_FULL_01`

Status: `complete`
Generation mode: built-in `image_gen`
Target project folder: `C:\Users\furkan\Desktop\Fate-in-Your-Hands\Fate-in-Your-Hands\Assets\Stories\saray_mutfagindan_muhre`

### Completed Output

- Background images: `50 / 50`
- Choice action images: `600 / 600`
- Total generated project PNGs: `650 / 650`
- Background size: `1920x1080`
- Choice size: `1024x1280`
- Format: `PNG`

### Project Paths

- Backgrounds: `Assets/Stories/saray_mutfagindan_muhre/Backgrounds/<backgroundId>.png`
- Choices: `Assets/Stories/saray_mutfagindan_muhre/Choices/choice_smm_card_###_a.png`
- Choices: `Assets/Stories/saray_mutfagindan_muhre/Choices/choice_smm_card_###_b.png`
- Unity binding index: `Assets/Stories/saray_mutfagindan_muhre/image_bindings.json`
- Story package binding index: `generated_stories/saray_mutfagindan_muhre/IMAGE_BINDING_INDEX.json`
- Story package binding table: `generated_stories/saray_mutfagindan_muhre/IMAGE_BINDING_INDEX.csv`
- Human-readable binding table: `generated_stories/saray_mutfagindan_muhre/IMAGE_BINDING_INDEX.md`

### Naming and Binding Rule

Names are intentionally ID-based so card binding is unambiguous.

- Background filename is the background asset id, for example `bg_saray_bulasikhanesi_sabah.png`.
- Choice filename is the choice image asset id, for example `choice_smm_card_001_a.png`.
- `choice_smm_card_001_a.png` binds to card `smm_card_001`, choice `A`.
- `choice_smm_card_001_b.png` binds to card `smm_card_001`, choice `B`.

### Verification

- Manifest target check: `650` background/choice manifest targets found on disk.
- Dimension and PNG check: `50` backgrounds at `1920x1080`, `600` choices at `1024x1280`, `0` invalid PNGs.
- Binding index check: `300` cards and `600` choice image references.
- Unity `.meta` files were not generated manually; Unity import should create them.

### Notes

The generated choice images follow the simplified action-icon direction: one close-up action motif on a warm beige panel, without card UI, text, logo, watermark, full character scenes, or room backgrounds. `choice_smm_card_001_a` was regenerated after the full batch to remove unwanted side bands.

---

Batch: `STYLE_TEST_00`
Output folder: `C:\Users\furkan\Desktop\SarayMutfagi_ImageTest`

## Summary

Bu test batch icin yalnizca 3 asset secildi ve uretildi:

- `bg_saray_bulasikhanesi_sabah` - background
- `focus_smm_001_intro` - focus/card image
- `cover_saray_mutfagindan_muhre` - cover

Proje `Assets` klasorune yeni PNG yazilmadi. PNG dosyalari yalnizca Desktop altindaki test klasorune kaydedildi.

## Generated Assets

### 1. bg_saray_bulasikhanesi_sabah

- Type: `background`
- Source assetId: `bg_saray_bulasikhanesi_sabah`
- Saved path: `C:\Users\furkan\Desktop\SarayMutfagi_ImageTest\bg_saray_bulasikhanesi_sabah.png`
- Generation status: `success`
- Reason for selection: Hikayenin baslangic atmosferini temsil eden saray bulasikhanesi; tas, bakir, buhar ve hizmet ritmini test etmek icin iyi bir ana mekan.

Prompt used:

```text
Use case: illustration-story
Asset type: story background test PNG
Primary request: 2D illustrated storybook style background for an Ottoman-inspired palace scullery in the morning.
Scene/backdrop: a palace dishwashing room with worn stone sinks, copper basins, stacked plates, wet stone floor, soft steam, simple aprons hanging on hooks, distant palace service atmosphere.
Subject: environment only; no character performing a specific action.
Style/medium: hand-drawn painterly 2D illustration, warm muted colors, readable silhouettes, slightly textured brushwork, storybook art.
Composition/framing: landscape wide background, calm readable staging, leave some quieter open space suitable for UI overlay, no close-up cropping.
Lighting/mood: morning light filtered through high palace windows, warm brass and stone tones, humble but atmospheric.
Color palette: copper, warm stone, faded gold, dirty cream, muted sage accents, gentle blue-gray shadows.
Constraints: no text, no logo, no watermark, no photorealism, no 3D render look, no modern objects, no readable signage, no card-specific action.
Avoid: photorealism, 3D render, cinematic live action, glossy CGI, text in image, letters, logo, watermark, cluttered unreadable crowd.
```

### 2. focus_smm_001_intro

- Type: `focus`
- Source assetId: `focus_smm_001_intro`
- Saved path: `C:\Users\furkan\Desktop\SarayMutfagi_ImageTest\focus_smm_001_intro.png`
- Generation status: `success`
- Reason for selection: Yetim bulasikcinin ilk mutfak esigini ve Bahir Usta ile gerilimli karar anini temsil ediyor.

Prompt used:

```text
Use case: illustration-story
Asset type: focus/card image test PNG
Primary request: 2D illustrated storybook style focus image for the first threshold of an orphan dish washer entering palace kitchen service.
Scene/backdrop: palace scullery / dishwashing room in the morning, stone sinks, copper basins, steam, worn aprons, palace service tools.
Subject: a young orphan dish washer in plain worn clothes standing at the edge of the scullery, facing Bahir Usta, an older stern but tired palace kitchen master; a tense choice moment about truth and survival, no written words.
Style/medium: hand-drawn painterly 2D storybook illustration, warm muted colors, readable silhouettes, soft brush texture, expressive but grounded faces.
Composition/framing: square card-style composition, 1-2 clear characters, orphan slightly foregrounded, Bahir Usta near copper basins, decision object implied by a stained or cracked plate, background readable but secondary.
Lighting/mood: humble morning light, warm copper reflections, quiet tension rather than melodrama.
Color palette: copper, warm stone, muted cream, faded gold, sage cloth accents, soft shadow blues.
Constraints: no text, no logo, no watermark, no photorealism, no 3D render look, no modern objects, no gore, no readable labels.
Avoid: photorealism, 3D render, CGI, glossy digital render, text in image, letters, logo, watermark, cluttered unreadable scene, exaggerated cartoon comedy.
```

### 3. cover_saray_mutfagindan_muhre

- Type: `cover`
- Source assetId: `cover_saray_mutfagindan_muhre`
- Saved path: `C:\Users\furkan\Desktop\SarayMutfagi_ImageTest\cover_saray_mutfagindan_muhre.png`
- Generation status: `success`
- Reason for selection: Hikayenin ana fikrini tek karede test ediyor: bulasikhaneden buyuk mutfaga, oradan muhure ve saray siyasetine yukselis.

Prompt used:

```text
Use case: illustration-story
Asset type: story cover test PNG
Primary request: 2D illustrated storybook cover art with no text for the story theme: an orphan dish washer rising from palace kitchen service toward the imperial seal.
Scene/backdrop: grand imperial palace kitchen glowing with firelight, huge copper cauldrons, ovens, hanging herbs, stone arches, distant glimpse of a solemn seal chamber or red wax seal motif beyond the kitchen.
Subject: the young orphan dish washer in worn apron at the lower foreground, holding a clean copper ladle or cracked plate, looking toward a symbolic imperial seal on a table in the warm distance; palace workers as simple readable silhouettes only.
Style/medium: hand-drawn painterly 2D illustrated storybook style, warm muted colors, visible brush and pencil-like texture, slightly stylized proportions, not realistic photography.
Composition/framing: wide landscape cover composition, strong silhouette path from humble kitchen foreground to imperial seal symbol, dramatic but readable, no title space needed, no typography.
Lighting/mood: warm firelight from the kitchen, muted gold and red wax accents, hopeful but politically tense palace atmosphere.
Color palette: copper, red wax, warm stone, faded gold, dirty cream, muted sage, controlled deep blue shadows.
Constraints: no text, no logo, no watermark, no readable marks, no photorealism, no 3D render look, no modern objects, no gore.
Avoid: photorealism, photographic lighting, 3D render, CGI, vector flat icon style, text in image, letters, symbols that look like writing, logo, watermark, overly comic tone.
```

## Style Evaluation

Uretim basarili. Uc gorsel de ayni dunyada duruyor: sicak tas, bakir, soluk altin ve mutfak buhari hissi tutarli. Background en temiz ve stil rehberine en yakin sonuc oldu.

Focus ve cover gorselleri 2D painterly hissini tasiyor, fakat ozellikle karakter isleme ve isik dili biraz fazla gercekci/painterly realism tarafina yaklasiyor. Tam batch oncesi promptlara `more stylized storybook proportions`, `less realistic anatomy`, `visible simplified brush shapes` gibi daha sert stil kilitleri eklenmesi iyi olur. Cover'daki muhur ve duvar suslemelerinde yaziya benzeyen dekoratif izler olusabildigi icin sonraki uretimlerde `no calligraphy-like marks, no glyphs, no letter-like ornament` negatifleri de eklenmeli.
