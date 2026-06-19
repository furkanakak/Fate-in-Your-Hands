# UI Audit Before Redesign

Source package: `docs/CODEX_UI_MASTER_REDESIGN_PACKAGE/`

Compared files:
- `ui_reference/reference_choice_front.png`
- `ui_reference/reference_card_result.png`
- `ui_reference/current_ui_front.png`
- `ui_reference/current_ui_result.png`

## Visual Comparison

The reference choice screen uses a light floating HUD, a centered parchment scroll, and two large physical-feeling cards. The current screen is dominated by a wide top toolbar, a large rectangular narrative panel, and small thumbnail-like cards. The main interaction is visually weaker than the background props and metadata panel.

The reference result screen keeps the chosen card at card scale and shows the consequence on the card back. The current result screen replaces the card with a narrow centered panel, includes a `Sonuc` heading and a large `Devam Et` button, and no longer reads as the same card object.

## Current Hierarchy And Layout Ownership

- `SmmStoryGameController` builds the entire gameplay UI at runtime instead of using scene-authored prefabs.
- The UI is layered as background, HUD, narrative, choice, animation, and debug layers, but the visible HUD is still a full-width panel.
- `choicePanel` uses a `GridLayoutGroup`; card slots are layout-owned and need a separate animation layer when selected.
- `SmmChoiceCardView.MoveToAnimationLayer` already preserves world corners, but the target offset is relative to the original slot, so selected cards can still land off-center instead of becoming the primary centered result object.
- The current result overlay still exists for endings, but choice results should never use it.

## HUD Issues

- `hudPanel` is stretched across the screen and visually cuts the composition.
- `hudTitleText` shows the story name during gameplay, which the master prompt says is unnecessary for the gameplay screen.
- `healthLabelText` shows the health label near the hearts; this weakens the clean floating heart treatment.
- Settings is a rectangular button inside the toolbar instead of a floating 56-72 px icon/medallion.
- Heart sprites are procedural and not emoji glyphs, but their current size is closer to small toolbar status icons than the requested large top-center HUD.

## Narrative Panel Issues

- `ApplyTitlePresentation` splits `card.title` into a chapter kicker and title, then displays both in gameplay.
- `bodyText` and `dialogueText` are placed inside a generic rectangular panel rather than a scroll/banner.
- `FormatBodyForPresentation` only strips an ASCII `aninda` pattern, so Turkish text with `anında` or mojibake-encoded equivalents can still echo the card title and chapter.
- Body text uses auto-size and can be reduced instead of paginated. For this vertical slice, the first four cards should be shortened enough to stay readable without shrinking below target size.

## Choice Card Issues

- Cards are smaller than the requested dominant 350-410 x 480-560 desktop target.
- The front face still uses nested surfaces: outer card, art mask, caption panel, and outlines.
- Artwork is loaded and is 4:5, but the current card composition makes it feel like a small image box rather than the card's main visual area.
- The first four cards' artwork mostly shows object/action closeups. It is usable for a constrained vertical slice, but stronger staging would be a future art pass.
- Hover/focus exists through scale, y-offset, shadow, and border color, but it needs stronger scale, clearer border accent, and stable whole-card clickability.

## Result And Flip Issues

- Card backs currently include a `Sonuc` label and a `Devam Et` button, both forbidden by the master prompt.
- `SmmChoiceCardView.FlipToBackRoutine` uses Y rotation, which can create UI ghosting and perspective artifacts. The prompt requests a stable two-phase scale-X flip.
- `continueButton` on the back face is the continuation target; the requested behavior is clicking/tapping the whole result card after a short lock.
- `SetBackText` can show mechanical health delta text. Hidden counters and mechanics should remain invisible, and health feedback should be icon/ambient, not system copy.
- The selected result position is calculated as an offset from the original card, not a true center target in `CardAnimationLayer`.

## Copy And Data Issues

- The first four cards contain template-like body text that repeats chapter/title information.
- Their `resultText` values describe the selected route and counter effects in system language.
- Required routing/state fields are present and must not change: `cardId`, `choiceId`, `nextCardId`, `endingId`, health deltas, counters, and flags.
- Copy fixes should therefore be limited to `bodyText` and `resultText` for `smm_card_001` through `smm_card_004`.

## Asset Issues

- Existing story backgrounds are coherent 16:9 images, but some have large decorative props and large empty color fields.
- Existing choice art files are present for the first four cards at 1024 x 1280.
- Missing polished UI raster assets can be replaced in this vertical slice with procedural Unity UI sprites if image generation is not necessary.
- If generated UI PNGs are introduced later, they need real project paths and import settings rather than being reported as generated without files.

## Test Risks To Cover

- First card opens as `smm_card_001`.
- Choice A routes to `smm_card_002`; choice B routes to `smm_card_003`.
- The next three cards remain loadable without broken assets.
- Rapid double-click does not trigger a choice twice.
- Choice results use the real natural `resultText` on the card back.
- The choice result overlay/modal remains hidden.
- Keyboard/gamepad focus and submit still work.
- Screenshots are captured from Play Mode at the requested desktop and mobile sizes.
