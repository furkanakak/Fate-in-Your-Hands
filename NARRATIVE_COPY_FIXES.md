# Narrative Copy Fixes

Scope: only `smm_card_001`, `smm_card_002`, `smm_card_003`, and `smm_card_004`.

Updated files:
- `Fate-in-Your-Hands/Assets/Stories/saray_mutfagindan_muhre/story_data.json`
- `Fate-in-Your-Hands/Assets/Resources/Stories/saray_mutfagindan_muhre/story_data.json`

Fields changed:
- `title`
- `bodyText`
- `dialogue[].text`
- `choices[].text`
- `choices[].resultText`

Fields explicitly preserved:
- `cardId`
- `choiceId`
- `nextCardId`
- `endingId`
- `healthDelta`
- `hiddenCounterDeltas`
- `setFlags`
- `clearFlags`

## Changes

- `smm_card_001`: removed chapter/title echo from the opening kitchen trial and rewrote both result texts as natural consequences.
- `smm_card_002`: replaced repeated template body copy with a focused night-kitchen setup and rewrote both result texts without exposing counters.
- `smm_card_003`: replaced repeated template body copy with a concise bread/mercy setup and rewrote both result texts as visible story consequences.
- `smm_card_004`: replaced repeated template body copy with a concise pantry/measurement setup and rewrote both result texts without mechanical stat language.

Routing/state validation: PASS. A structured update script compared all preserved fields before and after writing both story JSON files.
