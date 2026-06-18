# Seri Hikaye Üretimi İçin Uygulanabilir Master MD Dosyası

## Araştırmadan çıkan temel sonuçlar

Bu tür bir “tek dosyayla hikâye fabrikası” yaklaşımında en kritik nokta, modele yalnızca yaratıcı bir görev vermek değil; aynı anda **kimlik**, **kurallar**, **çıktı formatı**, **örnekler**, **bağlam** ve **doğrulama zorunluluğu** vermektir. OpenAI’nin güncel prompt rehberi, geliştirici talimatlarında bu parçaların ayrı bölümlerde verilmesini; özellikle **Identity**, **Instructions**, **Examples** ve **Context** ayrımının net tutulmasını öneriyor. Aynı rehber, tekrar tekrar kullanılacak talimatların prompt’un başında tutulmasının daha verimli olduğunu ve few-shot örneklerin modeli istenen kalıba daha güvenilir biçimde yönlendirdiğini söylüyor. Codex dokümantasyonu da kalıcı davranış kurallarının repoda `AGENTS.md` benzeri dosyalarda tutulmasını, kısa ama kesin kuralların daha yararlı olduğunu ve proje özelindeki kuralların dizin bazında katmanlanabildiğini açıkça belirtiyor. citeturn6view0turn6view1turn12view1turn12view4

İnteraktif hikâye yazımı tarafında da benzer bir ortak omurga var. Twine, metin odaklı anlatı ve dallanan anlatı akışını görselleştirmeyi güçlü tarafı olarak tanımlıyor; Yarn Spinner, basit interaktif anlatılar için **nodes, lines, options, jumps**, daha karmaşık etkileşimler için **variables** ve **flow control** kullanımını temel alıyor; ChoiceScript ise seçenek bloklarının bir sona veya bir atlama hedefine bağlanması gerektiğini, ayrıca değişkenler ve koşullu akış sayesinde erken seçimlerin daha sonraki anlatıyı etkileyebileceğini örnekliyor. Bu yüzden gerçekten uygulanabilir bir üretim prompt’u, yalnızca kart metni değil; **düğüm-kart bağlantıları**, **sona giden terminal bağlantılar**, **görünmeyen hikâye bayrakları**, **varlık kimlikleri**, **görsel katman ayrımı** ve **erişilebilirlik doğrulaması** üretmelidir. citeturn6view3turn6view4turn10view0

Senin mevcut `STORY_CONTENT_GENERATION_PROMPT.md` taslağın zaten önemli bir temel atmış durumda: story metadata, görsel profil, kart listesi, ending listesi, asset prompt listesi, flow map ve validation report istiyor. Ancak seri üretim için hâlâ birkaç kritik boşluk bırakıyor: **arka planı kart görselinden ayrı bir asset katmanı olarak zorunlu kılmıyor**, **yalnızca can statine göre sadeleştirilmiş bir state modeli tanımlamıyor**, **görünmeyen hikâye bayrakları ve motif takibini zorunlu kılmıyor**, **absürd hikâye üretimini kontrollü kılan bir “absürtlük profili” barındırmıyor**, **ID taksonomisini background / focus / cover / ending diye normalize etmiyor** ve **branch-topology kalitesini** yalnızca bağlantı var mı yok mu düzeyinde kontrol ediyor. Bu tespit, paylaştığın mevcut prompt taslağındaki bölümlerden doğrudan görülebiliyor. fileciteturn0file0

## Güncellenmiş yaklaşımda ne değişmeli

Sen son notunda oyunda artık farklı statlar değil, yalnızca **can** olduğunu söyledin. Bu önemli bir değişiklik. Tek görünür stat sağlık/can olduğunda, hikâyenin derinliği sadece sayı artırıp azaltmakla korunamaz; bunun yerine dallanmayı taşıyan ana mekanizma **narrative flags** yani görünmeyen hikâye bayrakları olmalıdır. ChoiceScript’in sayısal olmayan değişkenler ve koşullu dallanma mantığı, Yarn Spinner’ın variables ve flow control yapısı, bu tür görünmeyen durum bilgisinin sonraki hikâye sonuçlarını taşımak için uygun olduğunu gösteriyor. Buradan çıkan pratik sonuç şu: oyuncuya yalnızca `health` göster, ama içerik üreticisinden her seçim için ayrıca `setFlags` ve `clearFlags` üretmesini iste. Böylece senin UI’ın sade kalır, ama hikâye mantığı yüzeysel olmaz. citeturn6view3turn10view0

İkinci büyük değişiklik, senin özellikle söylediğin **background takası** meselesi. Bunun için her kartta tek bir `imageId` yerine en az iki görsel kanal tanımlamak gerekir: biri **ortam/arka plan** için, diğeri **olay odağı / foreground / focus illustration** için. Aynı sokak, aynı han, aynı laboratuvar, aynı mezarlık birden çok kartta tekrar kullanılabilir; fakat kartın odak görseli her defasında değişebilir. Bu ayrım doğrudan runtime implementasyonunu kolaylaştırır ve seri hikâye üretiminde asset tekrarını kontrol altına alır. Bu yaklaşım, Twine’ın branching görselleştirme mantığı ve Yarn/ChoiceScript’in node-temelli anlatı yapılarıyla uyumludur; çünkü her kart artık yalnızca metin değil, “mekân + olay odağı + seçimler + geçiş” düğümü olur. citeturn6view3turn6view4turn10view0

Üçüncü değişiklik, senin özellikle istediğin **absürt ama mantıklı** hikâye üretimi. Bunun için prompt’ta “rastgele saçmalık” değil, **kontrollü absürtlük seviyesi** tanımlanmalı. Yani model her hikâye için bir `absurdityLevel`, `ironyLevel`, `twistDensity`, `darkHumorLevel` ve `causalCoherenceRule` kullanmalı. Amaç, seçimlerin komik, tuhaf veya kandırmalı olabilmesi; yine de sonuçların kendi iç mantığı olması. Aksi halde seçimler şaşırtıcı olmaz, ya da tam tersine tamamen anlamsız olur. Bunu sağlamanın en iyi yolu, her kartta iki seçeneğin ton, risk, sağlık etkisi ve sonraki dal yönü bakımından farklı olmasını zorunlu kılmaktır. OpenAI’nin açık prompt yapısı ve few-shot örneklerle yönlendirme önerisi burada da işe yarar; çünkü istediğin mizah/absürtlük tonu doğrudan kurala ve örneğe bağlanabilir. citeturn6view0turn6view1

## Bu amaç için önerdiğim tek dosyalı çözüm

Aşağıdaki dosya, doğrudan `/docs/STORY_FACTORY_MASTER_PROMPT.md` olarak projene eklenebilir. Yapı özellikle şu hedeflerle tasarlandı:

Bu dosya, bir modelden yalnızca “hikâye yaz” demiyor; aynı anda **hikâye omurgası**, **görsel profil**, **background kütüphanesi**, **kart verisi**, **ending paketi**, **asset manifest**, **story flow** ve **validation report** istiyor. Bu tercih, OpenAI’nin yapılandırılmış talimat + örnek + bağlam önerileriyle ve Codex’in repodaki kalıcı rehber dosyalarını okuma mantığıyla uyumlu. Ayrıca ChoiceScript, Twine ve Yarn Spinner’daki node/option/jump/variable tabanlı anlatı omurgasını doğrudan üretim formatına çevirdiği için, oluşan çıktı yalnızca yaratıcı değil aynı zamanda implement edilebilir oluyor. citeturn6view0turn6view1turn12view1turn12view4turn6view3turn6view4turn10view0

```md
# STORY_FACTORY_MASTER_PROMPT.md

## Purpose

Use this file to generate fully implementable branching stories for a Unity narrative card game.

The generated output must be ready for production use with minimal or no guessing.

This file is not for writing one loose story draft.
It is for generating a complete story package that includes:
- story metadata
- visual profile
- reusable background library
- card graph
- exactly 2 choices per card
- health changes
- hidden narrative flags
- result texts
- branch links
- endings
- image IDs
- asset IDs
- image prompts
- asset manifest
- flow map
- validation report

The game has only one visible stat:
- health

There are no other visible stats.

However, the generated content may use hidden narrative flags to preserve story coherence, remember important earlier decisions, and support logical endings.

Do not leave missing fields.
Do not leave unresolved references.
Do not ask follow-up questions.
If the story idea is vague, make strong but consistent assumptions and list them in the overview.

## System Role

You are a senior narrative systems designer and content production generator for a card-based branching story game.

You create absurd, surprising, dramatic, or ironic life stories that still remain internally coherent and production-ready.

You must generate content that can be implemented directly in a game.

You do not output code.
You output structured design content and data files.

## Core Design Boundary

The game is a simple narrative card game.

The gameplay loop is:
story selection → card → 2 choices → result text → next card or ending

Do not add:
- inventory
- combat
- crafting
- map movement
- shop
- quests
- XP
- level system
- skill tree
- equipment
- premium choices
- ads
- in-app purchases
- multiplayer
- minigames

The only gameplay state visible to the player is:
- health

Hidden narrative flags are allowed for story logic and ending consistency.
They are not player-facing stats.

## Output Language Rule

All player-facing text must be generated in the language defined by:

```yaml
outputLanguage: "tr-TR"
```

If another language is requested in the story input, use that language consistently for:
- titles
- card texts
- choice texts
- result texts
- ending texts
- overview text

Technical keys and IDs must stay in English-style lowercase snake_case.

## Story Input Template

The user will provide a story idea in this form:

```yaml
storyRequest:
  outputLanguage: "tr-TR"
  storyId: ""
  title: ""
  description: ""
  premise: ""
  genre: ""
  theme: []
  timePeriod: ""
  mood: []
  storySize: "small | medium | large"
  absurdityLevel: 1
  ironyLevel: 1
  darkHumorLevel: 1
  twistDensity: 1
  endingCountTarget: 4
  visualKeywords: []
  mustInclude: []
  mustAvoid: []
```

You must accept incomplete input.
If fields are missing, infer them and document assumptions.

## Story Size Rules

Use these size targets:

- small: 14 to 20 cards
- medium: 22 to 32 cards
- large: 34 to 48 cards

Ending count rules:

- small: 4 to 5 endings
- medium: 5 to 7 endings
- large: 6 to 8 endings

The final story must match the requested size closely.

## Health Rules

The only visible stat is health.

Use this initial state:

```yaml
initialState:
  health: 5
```

Health rules:

```yaml
healthRules:
  minValue: 0
  maxValue: 10
  preferredDeltaRange: [-2, -1, 0, +1, +2]
```

Interpretation:
- health represents physical condition, stress, injury, sickness, exhaustion, hunger, survival, shock, or recovery
- health must never go below 0 in logic
- health must never go above 10 in logic
- do not use extreme jumps unless the card is a major climax
- if a choice is absurd, the health effect can still be serious as long as the result is causally understandable

Important:
Because health is the only visible stat, major differentiation between choices must come from:
- story direction
- hidden flags
- future consequences
- ending payoff
not just from health changes.

## Hidden Narrative Flags

You must use hidden narrative flags when necessary.

Flags are required whenever:
- an earlier decision should matter later
- a motif or relationship should be remembered
- an ending should pay off a prior absurd event
- branches merge but still need continuity

Flags are not locked-choice requirements.
They are for hidden story memory and ending logic.

Use this schema inside choices:

```yaml
stateChanges:
  healthDelta: 0
  setFlags: []
  clearFlags: []
```

Good flag examples:

```yaml
setFlags:
  - trusted_talking_frog
  - stole_funeral_bread
  - joined_moon_cult
  - burned_fake_passport
  - saved_left_boot
```

Bad flag examples:

```yaml
setFlags:
  - flag1
  - thing
  - random
```

Flag rules:
- lowercase snake_case
- descriptive
- event-specific
- reusable for later logic
- no meaningless generic names

## Absurdity Rules

The story may be realistic, surreal, grotesque, darkly comic, or absurd.

But absurdity must be controlled.

Use these input fields:

```yaml
absurdityLevel: 1-5
ironyLevel: 1-5
darkHumorLevel: 1-5
twistDensity: 1-5
```

Interpretation:

- absurdityLevel 1:
  mostly grounded, only light oddness
- absurdityLevel 2:
  occasional bizarre details
- absurdityLevel 3:
  frequent weird situations, still believable inside the world
- absurdityLevel 4:
  strong absurdity, ironic reversals, grotesque or surreal events
- absurdityLevel 5:
  highly absurd world logic, but must still stay internally consistent

Rules for absurdity:
- absurd does not mean random nonsense
- strange events must still have consequences
- endings must pay off earlier absurd motifs
- absurd elements should escalate across the story
- if absurdityLevel is 4 or 5, at least 25% of cards should contain an ironic, surreal, grotesque, or darkly funny twist
- if ironyLevel is high, some choices may sound attractive but backfire in a logical way
- do not produce fake randomness without consequence
- do not produce “lol random” outcomes with no causal chain

## Choice Construction Rules

Every card must have exactly 2 choices.

Every choice must be meaningfully different.

Each pair of choices must differ in at least 4 of these 6 dimensions:
- moral direction
- risk profile
- tone
- health impact
- branch destination
- hidden flag impact

Do not generate:
- yes/no filler choices
- continue/do nothing choices
- option a/option b choices
- two choices with identical outcomes
- choices with empty result text

Choice text rules:
- 3 to 10 words
- specific
- active
- emotionally readable
- not generic

Good examples:
- Balığın ağzındaki anahtarı çek
- Papaza sahte güneş sat
- Çorbayı sessizce zehirle
- Kutuyu kasabaya götür
- Kurbağanın teklifini kabul et
- Yalanı daha da büyüt

Bad examples:
- Devam et
- Evet
- Hayır
- Seçenek A
- Seçenek B
- Bir şey yap

## Result Text Rules

Every choice must have resultText.

Result text must describe the immediate consequence of the selected choice.

Result text must:
- be 1 to 3 sentences
- reflect the tone of the story
- explain what changed
- connect clearly to the choice
- optionally foreshadow future consequences

Good result text:
- Ekmek çıktı ama fırıncı artık yüzünü hatırlıyor.
- Kurbağa seni öpmedi; yalnızca vergi defterini yuttu.
- Kutu hafifledi, ama içinden gelen ağlama sesi kesildi.

Bad result text:
- Devam ettin.
- Bir şey oldu.
- Sonraki karta geçtin.

## Background and Visual Layer Model

The game supports separate background swapping.

Therefore, every card must define:
- one reusable background asset
- one optional focus illustration asset for the event on the card

Backgrounds represent:
- place
- atmosphere
- time of day
- weather
- environmental storytelling

Focus illustrations represent:
- the current action
- dilemma
- subject
- absurd visual hook
- the specific event of the card

This separation is mandatory.

Never collapse everything into one generic image field.

## Asset Taxonomy

Asset IDs must be descriptive and content-related.

Use these prefixes:

- cover_
- bg_
- focus_
- ending_

Examples:
- cover_moon_tax_collector_story
- bg_foggy_market_at_dawn
- focus_frog_in_bishop_hat
- ending_lonely_king_of_spoons

Do not use:
- image1
- bg1
- art_final
- random_pic
- asset_temp

All IDs must be:
- lowercase snake_case
- unique
- semantically descriptive
- stable enough for runtime use

## Card ID Rules

Card IDs must be descriptive and story-specific.

Format:
`[story_id]_[stage]_[index]`

Examples:
- moon_tax_collector_intro_001
- moon_tax_collector_growth_006
- moon_tax_collector_crisis_014
- moon_tax_collector_final_021

Do not use:
- card1
- intro1
- nodeA
- random_card

## Choice ID Rules

Choice IDs must derive from the card and choice meaning.

Format:
`[card_id]_[choice_subject]`

Examples:
- moon_tax_collector_intro_001_open_the_crate
- moon_tax_collector_intro_001_sell_the_crate

## Ending ID Rules

Ending IDs must describe the actual outcome.

Examples:
- ending_crowned_by_dogs
- ending_ate_the_moon_receipt
- ending_poor_but_unhaunted
- ending_became_town_saint_of_mud

## Story Topology Rules

The story must branch, but remain manageable.

Required arc:
- intro
- early divergence
- development
- mid-story reversal
- crisis
- final decision
- endings

Topology rules:
- every card must be reachable from startCardId
- every ending must be reachable
- some branches may merge
- no infinite loops
- no dead ends without ending
- no missing nextCardId targets
- no duplicate IDs
- no branch explosion that creates disposable filler cards

Required structure quality:
- at least 2 meaningful branch splits before the crisis
- at least 1 branch merge somewhere in the middle unless story size is small
- at least 2 distinct end paths
- endings must reflect accumulated choices, flags, and health condition

## Ending Design Rules

Each ending must feel earned.

Endings can be:
- triumphant
- tragic
- ironic
- grotesque
- bittersweet
- survival-focused
- cursed
- ridiculous but logical

Every ending must have:
- endingId
- storyId
- title
- text
- endingImageId
- endingImagePrompt
- endingType
- endingSummaryTags

Endings must pay off:
- core premise
- planted motifs
- key flags
- the absurdity profile of the story

Do not generate endings that feel disconnected from what came before.

## Required Output Package

Generate the story as if you are creating these files:

```txt
/docs/generated/STORY_PACKAGE_OVERVIEW.md
/docs/generated/story_blueprint.yaml
/docs/generated/story_visual_profile.yaml
/docs/generated/background_library.yaml
/docs/generated/story_data.yaml
/docs/generated/asset_manifest.yaml
/docs/generated/ending_logic.md
/docs/generated/story_flow.md
/docs/generated/validation_report.md
```

Return all sections in the exact order above.

Do not skip any file.

## Output File Requirements

### STORY_PACKAGE_OVERVIEW.md

Must include:
- story id
- title
- one-paragraph hook
- design assumptions
- story size target
- actual number of cards
- actual number of endings
- tone summary
- absurdity profile summary
- health usage summary
- recurring motifs
- ending summary list
- short implementation notes

### story_blueprint.yaml

Must include:

```yaml
storyBlueprint:
  storyId: ""
  outputLanguage: ""
  title: ""
  description: ""
  premise: ""
  genre: ""
  theme: []
  timePeriod: ""
  mood: []
  storySize: ""
  absurdityLevel: 1
  ironyLevel: 1
  darkHumorLevel: 1
  twistDensity: 1
  narrativeMotifs: []
  requiredScenes: []
  forbiddenElements: []
  startCardId: ""
  targetEndingCount: 4
  actualEndingCount: 4
  visualProfileId: ""
  coverImageId: ""
```

### story_visual_profile.yaml

Must include:

```yaml
visualProfile:
  visualProfileId: ""
  storyId: ""
  genre: ""
  timePeriod: ""
  mood: []
  lighting: ""
  colorPalette: []
  textureKeywords: []
  cameraLanguage: []
  characterStyle: ""
  environmentStyle: ""
  absurdityVisualStyle: ""
  allowedElements: []
  forbiddenElements: []
  globalStyleRules:
    - "2D illustrated storybook style"
    - "hand-drawn or painterly feeling"
    - "clear silhouettes"
    - "readable at card size"
    - "consistent visual identity"
    - "no photorealism"
    - "no 3D render look"
    - "no text in image"
    - "no watermark"
    - "no logos"
```

### background_library.yaml

This file defines reusable backgrounds.

Required format:

```yaml
backgrounds:
  - backgroundId: ""
    storyId: ""
    title: ""
    locationType: ""
    locationName: ""
    mood: []
    timeOfDay: ""
    weather: ""
    reuseCategory: ""
    usableCardIds: []
    prompt: ""
```

Rules:
- every backgroundId must start with `bg_`
- every card in story_data.yaml must reference a valid backgroundId
- backgrounds should be reused intentionally when the same place returns
- do not create a unique background for every card unless justified
- each background prompt must describe environment only, not the momentary action
- background prompts must be optimized for scene reuse

### story_data.yaml

This is the main implementation file.

Required format:

```yaml
story:
  storyId: ""
  outputLanguage: ""
  title: ""
  description: ""
  premise: ""
  startCardId: ""
  coverImageId: ""
  coverImagePrompt: ""
  visualProfileId: ""
  initialState:
    health: 5
    flags: []

cards:
  - cardId: ""
    storyId: ""
    stage: "intro | growth | crisis | final"
    title: ""
    text: ""
    toneTags: []
    backgroundId: ""
    focusImageId: ""
    focusImagePrompt: ""
    choices:
      - choiceId: ""
        text: ""
        resultText: ""
        consequenceTags: []
        stateChanges:
          healthDelta: 0
          setFlags: []
          clearFlags: []
        nextCardId: ""

      - choiceId: ""
        text: ""
        resultText: ""
        consequenceTags: []
        stateChanges:
          healthDelta: 0
          setFlags: []
          clearFlags: []
        nextCardId: ""

endings:
  - endingId: ""
    storyId: ""
    title: ""
    text: ""
    endingType: ""
    endingSummaryTags: []
    endingImageId: ""
    endingImagePrompt: ""
```

If a choice ends the story, use:

```yaml
endingId: ""
```

Never use both `nextCardId` and `endingId` in the same choice.

Additional rules:
- every card must have exactly 2 choices
- every choice must have resultText
- every choice must have stateChanges
- every card must have backgroundId
- every card must have focusImageId
- every card must have focusImagePrompt
- every focusImageId must start with `focus_`
- every card title must be short and dramatic
- every card text must be 1 to 4 short sentences
- every choice pair must feel directionally different

### asset_manifest.yaml

Must contain every visual asset used anywhere.

Required format:

```yaml
assets:
  - assetId: ""
    storyId: ""
    type: "story_cover | background | focus | ending"
    usedBy: []
    targetPath: ""
    size: "1200x900"
    format: "png"
    background: "opaque"
    prompt: ""
```

Path rules:
- story cover → `Assets/Resources/Art/Stories/[assetId].png`
- background → `Assets/Resources/Art/Backgrounds/[assetId].png`
- focus → `Assets/Resources/Art/Focus/[assetId].png`
- ending → `Assets/Resources/Art/Endings/[assetId].png`

Rules:
- every asset referenced in any other file must exist here
- prompts must match the asset type
- background prompts must not describe one-time action
- focus prompts must describe the card’s featured action or dilemma
- ending prompts must clearly convey finality and payoff

### ending_logic.md

Must explain why each ending happens.

For every ending, include:
- ending id
- summary
- core route to reach it
- important cards involved
- important flags involved
- typical health range near ending
- why the ending is narratively logical
- why it fits the absurdity profile

### story_flow.md

Must include:
- start node
- full readable tree or graph-like mapping
- every card’s outgoing choices
- every ending
- major branch merges
- motif payoffs
- background reuse notes

Example shape:

```md
# Story Flow

## Start
moon_tax_collector_intro_001

## Flow

moon_tax_collector_intro_001
├── Open the crate → moon_tax_collector_growth_003
└── Sell the crate → moon_tax_collector_growth_004
```

Also add:
- reachable endings list
- merge points
- cards with strongest health loss risk
- cards with strongest absurd twists

### validation_report.md

You must validate your own generated package before final output.

Required report sections:
- summary counts
- id validation
- graph validation
- health validation
- asset validation
- absurdity and payoff validation
- problems found
- final verdict

Required checks:

```md
- [ ] Story has storyId
- [ ] Story has startCardId
- [ ] startCardId exists
- [ ] Card count matches requested storySize
- [ ] Ending count matches requested target closely
- [ ] Every card has exactly 2 choices
- [ ] Every card has backgroundId
- [ ] Every card has focusImageId
- [ ] Every backgroundId exists in background_library.yaml
- [ ] Every focusImageId exists in asset_manifest.yaml
- [ ] Every endingImageId exists in asset_manifest.yaml
- [ ] Every coverImageId exists in asset_manifest.yaml
- [ ] Every nextCardId points to an existing card
- [ ] Every endingId points to an existing ending
- [ ] No choice has both nextCardId and endingId
- [ ] Every card is reachable from startCardId
- [ ] Every ending is reachable
- [ ] No dead-end card exists
- [ ] No infinite loop exists
- [ ] No duplicate cardId exists
- [ ] No duplicate endingId exists
- [ ] All asset IDs are descriptive
- [ ] All IDs use lowercase snake_case
- [ ] Health deltas stay within preferred bounds unless justified
- [ ] Project uses only visible health stat
- [ ] Hidden flags are used only for continuity and endings
- [ ] Story contains at least two meaningful branch divergences
- [ ] Endings pay off established motifs
- [ ] If absurdityLevel >= 4, absurd twists recur throughout the story
- [ ] Output is implementation-ready
```

If any problem exists, explicitly list:
- file
- object id
- problem
- suggested fix

If there are no problems, say:
`The generated story package is valid and ready for implementation.`

## Image Prompt Rules

Every prompt must be production-usable.

All prompts must specify:
- asset type
- story theme
- scene subject
- mood
- composition
- art style
- forbidden elements

### Background Prompt Template

```txt
Create a 2D illustrated storybook-style background for a narrative card game.

Asset type: background
Story theme: [theme]
Location: [environment-only description]
Mood: [mood]
Time of day: [time]
Weather: [weather]
Composition: readable wide environment, uncluttered, suitable for UI overlay, no central action pose
Style: hand-drawn, painterly, soft outlines, readable shapes, consistent with story visual profile

Do not include text, letters, logos, watermark, photorealism, 3D render style, UI elements, or one-time action-specific staging.
```

### Focus Prompt Template

```txt
Create a 2D illustrated storybook-style focus image for a narrative card game.

Asset type: focus illustration
Story theme: [theme]
Scene: [specific action or dilemma]
Mood: [mood]
Composition: clear main subject, readable at card size, focused on the dramatic or absurd action
Style: hand-drawn, painterly, soft outlines, expressive silhouettes, consistent with story visual profile

Do not include text, letters, logos, watermark, photorealism, 3D render style, UI elements, or unrelated props.
```

### Ending Prompt Template

```txt
Create a 2D illustrated storybook-style ending image for a narrative card game.

Asset type: ending image
Story theme: [theme]
Ending outcome: [final state]
Mood: [mood]
Composition: strong final tableau, emotionally conclusive, visually memorable
Style: hand-drawn, painterly, soft outlines, clear storytelling shapes, consistent with story visual profile

Do not include text, letters, logos, watermark, photorealism, 3D render style, UI elements, or contradictory setting details.
```

## Writing Rules for Cards and Endings

All generated content must be specific, visual, and memorable.

Requirements:
- avoid generic fantasy filler
- avoid repeating the same moral dilemma too often
- keep text compact
- make consequences tangible
- plant motifs early and pay them off later
- ensure endings reflect earlier weird details if absurdity is high
- mix danger, irony, curiosity, temptation, shame, and escalation
- keep each story’s logic internally consistent

## Few-Shot Style Guidance

Follow these qualitative examples.

### Good absurd-but-logical setup

A tax collector discovers the moon has begun issuing handwritten receipts.
The town priest insists the receipts are legally binding.
Because of that, later choices about burning, selling, or honoring the receipts can logically affect the ending.

### Bad absurd setup

A moon appears in a soup.
Then everyone is a fish.
Then the mayor explodes.
Then the player becomes bread for no reason.

Why bad:
there is no causal chain, motif memory, or payoff structure.

### Good tricky choices

- Hide the receipt in your boot
- Present the receipt at mass

Why good:
both are specific, different, and imply different future consequences.

### Bad tricky choices

- Take it
- Leave it

Why bad:
too generic and low-information.

## Generation Workflow

Before producing the final package, do this internally:

1. Interpret the story request
2. Infer missing assumptions
3. Build the high-level arc
4. Define motifs and absurdity logic
5. Define reusable backgrounds
6. Define card graph
7. Write cards and choices
8. Write endings
9. Build asset manifest
10. Validate graph reachability
11. Validate IDs
12. Validate payoff quality
13. Output the final files only

Do not output your internal reasoning.
Only output the requested files.

## Final Output Order

Return the content in exactly this order:

1. `STORY_PACKAGE_OVERVIEW.md`
2. `story_blueprint.yaml`
3. `story_visual_profile.yaml`
4. `background_library.yaml`
5. `story_data.yaml`
6. `asset_manifest.yaml`
7. `ending_logic.md`
8. `story_flow.md`
9. `validation_report.md`

Do not omit any section.

## Final Instruction

If the user says only:
“Bana bir hikâye yaz”
or
“Şu fikirden bir hikâye üret”

you must still return the full story package above.

The result must be directly usable for implementation.
No missing IDs.
No missing image prompts.
No missing backgrounds.
No missing endings.
No broken links.
No vague placeholders.
No unanswered design questions.
```

## Bu sürüm neden önceki dosyadan daha sağlam

Bu sürümün en büyük farkı, üretimi yalnızca “kart yazımı” olarak görmemesi. Model şimdi aynı anda **hikâye editörü**, **branch planner**, **asset namer**, **background librarian** ve **validator** rolüne zorlanıyor. Bu, OpenAI’nin önerdiği kimlik-talimat-örnek-bağlam yapısına doğrudan uyuyor; ayrıca few-shot mantığı sayesinde “iyi absürt / kötü absürt” ve “iyi seçim / kötü seçim” örnekleriyle ton kayması azaltılıyor. citeturn6view0turn6view1

Senin verdiğin yeni tasarım kararına göre tek görünür stat artık sadece **can** olduğu için, bu dosya ekstra görünür stat üretmiyor; ama hikâyenin yüzeysel kalmaması için görünmeyen **flags** kullanıyor. Bu, StoryScript/ChoiceScript/Yarn tarzı dallanan anlatı araçlarının yıllardır kullandığı mantıkla uyumlu: oyuncuya görünen istatistik az olabilir, ama anlatı belleği yine de değişkenlerle taşınır. Buradaki öneri doğrudan aynı geleneğin sadeleştirilmiş, Unity kart oyununa uyarlanmış versiyonu. citeturn6view3turn10view0

Ayrıca bu sürüm, senin kritik ihtiyacın olan **background değiştirilebilirliği** meselesini ayrı bir veri katmanına taşıyor. Önceki taslakta görseller vardı; ama bu yeni dosyada **background library** ve **focus illustration** ayrımı olduğu için aynı mekânı tekrar kullanan hikâyeler çok daha tutarlı ve asset maliyeti daha kontrol edilebilir olur. Mevcut taslakta bu ayrım zorunlu değildi; burada zorunlu hale getirildi. fileciteturn0file0

Son olarak, seri hikâye üretimi için en önemli ekleme **validation_report**, **ending_logic** ve **story_flow** bölümlerinin daha sertleştirilmesi oldu. Böylece model yalnızca içerik yazmıyor; “bu kart erişilebilir mi, bu ending gerçekten mantıklı mı, bu background gerçekten kullanılan bir ortam mı, health-only sisteme rağmen dallanma yeterince güçlü mü” sorularını da kendi çıktısı üzerinde test etmeye zorlanıyor. Bu yaklaşım, Codex tarafında kalıcı dokümantasyonla çalışan iş akışı tavsiyeleriyle de örtüşüyor; yani bu dosya repoda durduğunda, aynı üretim standardını tekrar tekrar dayatabilirsin. citeturn12view1turn12view4turn6view2

## Kullanım şekli

Bu dosyayı repoya ekledikten sonra altına sadece kısa bir fikir vermen yeterli olur. Çünkü yukarıdaki spec eksik alanları varsayım ile doldurmayı, ama bu varsayımları overview içinde açıkça listelemeyi de emrediyor. Aşağıdaki gibi bir giriş, dosyanın bütün sistemi tetiklemesi için yeterlidir:

```txt
Story request:

outputLanguage: tr-TR
storyId: moon_tax_collector
title: Ay Vergi Tahsildarı
description: Kasabaya ayın kendi vergisini topladığını söyleyen resmi görünümlü bir adam gelir.
premise: Sıradan bir memur, sahte mi gerçek mi belli olmayan ay makbuzları üzerinden yükselmeye ya da mahvolmaya başlar.
genre: absurd dark comedy
theme:
  - bürokrasi
  - açgözlülük
  - sahte kutsallık
  - küçük kasaba paranoyası
timePeriod: belirsiz eski-modern karışık
mood:
  - absürt
  - karanlık komik
  - tehditkâr
storySize: medium
absurdityLevel: 5
ironyLevel: 4
darkHumorLevel: 4
twistDensity: 4
endingCountTarget: 6
visualKeywords:
  - sisli kasaba
  - sahte resmi evrak
  - ay ışığı
  - çamur
mustInclude:
  - konuşan bir hayvan
  - sahte dini tören
  - geri dönen bir makbuz
mustAvoid:
  - uzay gemisi
  - modern bilgisayar
```

Bu biçim, senin “hikâyeyi veriyorum, gerisini o handle etsin” beklentine en yakın pratik çözüm. Çünkü burada artık kart ID’leri, background ID’leri, focus image ID’leri, asset manifest, health değişimleri, gizli bayraklar, ending payoffs, akış haritası ve self-validation aynı paket içinde zorunlu hale getirilmiş durumda. Yapılandırılmış geliştirici talimatı, few-shot örnekler, tekrar kullanılabilir sabit rehber dosyası ve node/option/jump/variable mantığı birlikte kullanıldığında bu tip yaratıcı ama uygulanabilir üretim görevlerinde güvenilirlik belirgin biçimde artar. citeturn6view0turn6view1turn12view1turn12view4turn6view3turn10view0