using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class SmmStoryGameController : MonoBehaviour
    {
        public const string AutosaveKey = "fiyh.saray_mutfagindan_muhre.autosave";

        private enum ScreenMode
        {
            MainMenu,
            StorySelect,
            StoryGameplay
        }

        [SerializeField] private UIThemeConfig uiTheme = UIThemeConfig.CreateDefault();

        private static readonly Color Ink = NarrativeUiTheme.Colors.Ink;
        private static readonly Color WarmInk = NarrativeUiTheme.Colors.WarmInk;
        private static readonly Color Parchment = NarrativeUiTheme.Colors.Parchment;
        private static readonly Color ParchmentLight = NarrativeUiTheme.Colors.ParchmentLight;
        private static readonly Color ParchmentDark = NarrativeUiTheme.Colors.Walnut;
        private static readonly Color CardBack = NarrativeUiTheme.Colors.CardBack;
        private static readonly Color CardBackDeep = NarrativeUiTheme.Colors.CardBackDeep;
        private static readonly Color RedSoft = NarrativeUiTheme.Colors.Terracotta;
        private static readonly Color Sage = NarrativeUiTheme.Colors.Sage;
        private static readonly Color DustyBlue = NarrativeUiTheme.Colors.DustyBlue;
        private static readonly Color Gold = NarrativeUiTheme.Colors.Brass;
        private static readonly Color ShadowSoft = NarrativeUiTheme.Colors.Shadow;
        private static Font builtInUiFont;

        private readonly Dictionary<string, int> counters = new Dictionary<string, int>();
        private readonly HashSet<string> flags = new HashSet<string>();

        private SmmStoryRepository repository;
        private SmmStoryImageLoader imageLoader;
        private SmmCursorManager cursorManager;
        private SmmStorySaveData saveData;
        private SmmCardData currentCard;
        private SmmEndingData currentEnding;
        private SmmChoiceData pendingChoice;
        private SmmChoiceCardView selectedChoiceView;

        private Canvas canvas;
        private RectTransform safeAreaRoot;
        private RectTransform backgroundLayer;
        private RectTransform menuLayer;
        private RectTransform storySelectLayer;
        private RectTransform frontOverlayLayer;
        private RectTransform hudLayer;
        private RectTransform narrativeLayer;
        private RectTransform choiceLayer;
        private RectTransform cardAnimationLayer;
        private RectTransform debugLayer;
        private Image backgroundImage;
        private AspectRatioFitter backgroundFitter;
        private RectTransform hudPanel;
        private RectTransform narrativePanel;
        private RectTransform choicePanel;
        private GridLayoutGroup choiceGrid;
        private RectTransform resultOverlay;
        private RectTransform resultPanel;
        private RectTransform settingsPanel;
        private RectTransform frontSettingsPanel;
        private RectTransform developerPanel;
        private RectTransform hudStripPanel;
        private RectTransform hudTitleTag;
        private TopHudView topHudView;
        private EventPanelView eventPanelView;
        private ResultPanelView resultPanelView;

        private Text hudTitleText;
        private Text healthLabelText;
        private Text hudRoleText;
        private Text hudDayText;
        private HealthHeartsView healthHeartsView;
        private Image settingsIconImage;
        private Image hudCrownImage;
        private Text kickerText;
        private Text titleText;
        private Text bodyText;
        private Text secondaryText;
        private Text dialogueText;
        private Text resultTitleText;
        private Text resultText;
        private Text developerText;
        private Text continueButtonText;

        private Button settingsButton;
        private Button continueButton;
        private Button mainMenuStartButton;
        private Button mainMenuSettingsButton;
        private Button mainMenuExitButton;
        private Button mainMenuGearButton;
        private Button storySelectBackButton;
        private Button storySelectGearButton;
        private InputField cardJumpInput;
        private InputField healthInput;
        private InputField endingInput;

        private SmmChoiceCardView choiceAView;
        private SmmChoiceCardView choiceBView;

        private int lastScreenWidth = -1;
        private int lastScreenHeight = -1;
        private int focusedChoiceIndex;
        private int selectionTriggerCount;
        private bool isContinuingFromChoice;
        private bool isPortraitLayout;
        private bool useViewportOverrideForTest;
        private int viewportOverrideWidth;
        private int viewportOverrideHeight;
        private bool initialized;
        private ScreenMode activeScreen = ScreenMode.MainMenu;

        private UIThemeConfig Theme => uiTheme ?? (uiTheme = UIThemeConfig.CreateDefault());
        private UIThemeConfig.SpacingSettings ThemeSpacing => Theme.spacing ?? (Theme.spacing = new UIThemeConfig.SpacingSettings());
        private UIThemeConfig.ShadowSettings ThemeShadow => Theme.shadow ?? (Theme.shadow = new UIThemeConfig.ShadowSettings());
        private UIThemeConfig.MotionSettings ThemeMotion => Theme.motion ?? (Theme.motion = new UIThemeConfig.MotionSettings());

        public string CurrentCardId => currentCard != null ? currentCard.cardId : string.Empty;
        public string CurrentEndingId => currentEnding != null ? currentEnding.endingId : string.Empty;
        public bool IsResultOpen => selectedChoiceView != null && selectedChoiceView.IsBackVisible;
        public bool IsModalResultVisibleForTest => resultOverlay != null && resultOverlay.gameObject.activeSelf;
        public bool IsChoiceFlipAnimatingForTest => selectedChoiceView != null && selectedChoiceView.IsFlipAnimating;
        public bool IsResultInputUnlockedForTest => selectedChoiceView != null && selectedChoiceView.IsResultInputUnlockedForTest;
        public string CurrentResultTextForTest => selectedChoiceView != null && selectedChoiceView.ResultText != null ? selectedChoiceView.ResultText.text : string.Empty;
        public string CurrentVisibleTextBlobForTest
        {
            get
            {
                var texts = new List<string>();
                if (bodyText != null)
                {
                    texts.Add(bodyText.text);
                }

                if (secondaryText != null)
                {
                    texts.Add(secondaryText.text);
                }

                if (dialogueText != null)
                {
                    texts.Add(dialogueText.text);
                }

                if (choiceAView != null && choiceAView.LabelText != null)
                {
                    texts.Add(choiceAView.LabelText.text);
                }

                if (choiceBView != null && choiceBView.LabelText != null)
                {
                    texts.Add(choiceBView.LabelText.text);
                }

                if (selectedChoiceView != null && selectedChoiceView.ResultText != null)
                {
                    texts.Add(selectedChoiceView.ResultText.text);
                }

                return string.Join("\n", texts.Where(value => !string.IsNullOrWhiteSpace(value)));
            }
        }
        public int SelectionTriggerCountForTest => selectionTriggerCount;
        public bool UsesSpriteHealthForTest => healthHeartsView != null && healthHeartsView.UsesSpriteImagesForTest;
        public bool UsesDistinctHalfHeartSpriteForTest => healthHeartsView != null && healthHeartsView.UsesDistinctHalfSpriteForTest;
        public bool UsesTextHeartGlyphsForTest => healthLabelText != null && healthLabelText.text.Contains("\u2665");
        public bool HasGameplayTitleHiddenForTest => (hudTitleText == null || !hudTitleText.gameObject.activeInHierarchy || string.IsNullOrWhiteSpace(hudTitleText.text))
            && (kickerText == null || !kickerText.gameObject.activeInHierarchy || string.IsNullOrWhiteSpace(kickerText.text))
            && (titleText == null || !titleText.gameObject.activeInHierarchy || string.IsNullOrWhiteSpace(titleText.text));
        public bool HasSplitTitleForTest => HasGameplayTitleHiddenForTest;
        public bool HasAnimationLayerForTest => cardAnimationLayer != null;
        public bool UiLayersRenderAboveBackgroundForTest => backgroundLayer != null && safeAreaRoot != null && backgroundLayer.GetSiblingIndex() < safeAreaRoot.GetSiblingIndex();
        public bool SelectedChoiceIsCenteredForTest => selectedChoiceView != null && selectedChoiceView.IsCenteredForTest;
        public bool HasCustomCursorAssetsForTest => cursorManager != null && cursorManager.HasAllCursorAssetsForTest;
        public bool IsUsingSystemCursorForTest => cursorManager == null || cursorManager.IsUsingSystemCursorForTest;
        public bool IsCursorVisibleForTest => cursorManager != null && cursorManager.IsCursorVisibleForTest;
        public bool IsTouchCursorHiddenForTest => cursorManager != null && cursorManager.IsTouchModeForTest && !cursorManager.IsCursorVisibleForTest;
        public Vector2 CursorHotspotForTest => cursorManager != null ? cursorManager.HotspotForTest : Vector2.zero;
        public SmmCursorState CurrentCursorStateForTest => cursorManager != null ? cursorManager.CurrentStateForTest : SmmCursorState.Default;
        public bool NonSelectedChoiceHiddenForTest
        {
            get
            {
                if (selectedChoiceView == null)
                {
                    return false;
                }

                var other = selectedChoiceView == choiceAView ? choiceBView : choiceAView;
                return other != null && other.IsHiddenForTest;
            }
        }
        public SmmChoiceCardView ChoiceAView => choiceAView;
        public SmmChoiceCardView ChoiceBView => choiceBView;
        public Button ContinueButton => continueButton;
        public Button SettingsButtonForTest => settingsButton;

        public string GetNormalizedChoiceResultTextForTest(int choiceIndex)
        {
            if (currentCard?.choices == null || choiceIndex < 0 || choiceIndex >= currentCard.choices.Length)
            {
                return string.Empty;
            }

            var choice = currentCard.choices[choiceIndex];
            return choice == null ? string.Empty : SmmStoryTextNormalizer.NormalizeResultText(choice.resultText, choice.text, choice.healthDelta);
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            RefreshResponsiveLayoutIfNeeded();
            HandleKeyboardInput();
        }

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            try
            {
                EnsureEventSystem();
                repository = SmmStoryRepository.LoadDefault();
                imageLoader = new SmmStoryImageLoader(repository);
                BuildUi();
                ShowMainMenu();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                BuildFatalErrorUi(exception.Message);
            }
        }

        public void StartNewStory()
        {
            ShowGameplayScreen();
            saveData = CreateFreshSave();
            LoadStateFromSave();
            ShowCard(repository.Package.initialCardId);
            SaveAutosave();
        }

        public void ForceStartNewStoryForTest()
        {
            PlayerPrefs.DeleteKey(AutosaveKey);
            selectionTriggerCount = 0;
            StartNewStory();
        }

        public void JumpToCard(string cardId)
        {
            if (repository.GetCard(cardId) == null)
            {
                Debug.LogWarning($"Unknown card id: {cardId}");
                return;
            }

            pendingChoice = null;
            ShowCard(cardId);
            SaveAutosave();
        }

        public void OpenEnding(string endingId)
        {
            if (repository.GetEnding(endingId) == null)
            {
                Debug.LogWarning($"Unknown ending id: {endingId}");
                return;
            }

            pendingChoice = null;
            ShowEnding(endingId);
            SaveAutosave();
        }

        public void SetHealth(int value)
        {
            saveData.health = repository.ClampHealth(value);
            RefreshHud();
            RefreshDeveloperPanel();
            SaveAutosave();
        }

        public void TriggerChoiceForTest(int index)
        {
            if (index == 0)
            {
                choiceAView?.ClickForTest();
            }
            else if (index == 1)
            {
                choiceBView?.ClickForTest();
            }
        }

        public void FocusChoiceForTest(int index)
        {
            SetChoiceFocus(index);
        }

        public void ContinueForTest()
        {
            if (IsResultOpen)
            {
                ContinueChoiceResult();
                return;
            }

            if (continueButton != null && continueButton.interactable && continueButton.gameObject.activeInHierarchy)
            {
                continueButton.onClick.Invoke();
            }
        }

        public int GetCounterValueForTest(string counterId)
        {
            return counters.TryGetValue(counterId, out var value) ? value : 0;
        }

        public bool HasFlagForTest(string flagId)
        {
            return flags.Contains(flagId);
        }

        private void HandleKeyboardInput()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (activeScreen != ScreenMode.StoryGameplay)
            {
                if (activeScreen == ScreenMode.StorySelect && keyboard.escapeKey.wasPressedThisFrame)
                {
                    ShowMainMenu();
                }

                return;
            }

            if (keyboard.f1Key.wasPressedThisFrame && developerPanel != null && DeveloperToolsAllowed())
            {
                developerPanel.gameObject.SetActive(!developerPanel.gameObject.activeSelf);
                RefreshDeveloperPanel();
            }

            var gamepad = Gamepad.current;

            if (IsResultOpen)
            {
                if (keyboard.enterKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame || (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame))
                {
                    ContinueForTest();
                }

                return;
            }

            if (currentCard == null || pendingChoice != null)
            {
                return;
            }

            if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame || (gamepad != null && (gamepad.dpad.left.wasPressedThisFrame || gamepad.leftStick.left.wasPressedThisFrame)))
            {
                SetChoiceFocus(0);
            }
            else if (keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame || (gamepad != null && (gamepad.dpad.right.wasPressedThisFrame || gamepad.leftStick.right.wasPressedThisFrame)))
            {
                SetChoiceFocus(1);
            }

            if (keyboard.enterKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame || (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame))
            {
                TriggerChoiceForTest(focusedChoiceIndex);
            }
        }

        private void OnChoiceSelected(SmmChoiceData choice)
        {
            if (choice == null || pendingChoice != null || currentCard == null || isContinuingFromChoice)
            {
                return;
            }

            pendingChoice = choice;
            selectionTriggerCount++;
            SmmCursorManager.SetGlobalInputLocked(true);
            settingsPanel.gameObject.SetActive(false);

            var isChoiceA = currentCard.choices != null && currentCard.choices.Length > 0 && currentCard.choices[0] == choice;
            selectedChoiceView = isChoiceA ? choiceAView : choiceBView;
            var otherChoiceView = isChoiceA ? choiceBView : choiceAView;

            selectedChoiceView.SetRenderPriority(10);
            otherChoiceView.SetRenderPriority(0);
            var resultText = SmmStoryTextNormalizer.NormalizeResultText(choice.resultText, choice.text, choice.healthDelta);
            selectedChoiceView.PlayResultFlip(resultText, choice.healthDelta, CalculateSelectedResultOffset(isChoiceA));
            otherChoiceView.PlayUnselectedExit(CalculateUnselectedExitOffset(isChoiceA));
            RefreshDeveloperPanel();
        }

        private void ContinueChoiceResult()
        {
            if (pendingChoice == null || selectedChoiceView == null || isContinuingFromChoice)
            {
                return;
            }

            StartCoroutine(ContinueChoiceResultRoutine());
        }

        private IEnumerator ContinueChoiceResultRoutine()
        {
            isContinuingFromChoice = true;
            SmmCursorManager.SetGlobalInputLocked(true);
            yield return selectedChoiceView.PlayContinueExit();
            ResolvePendingChoice();
            isContinuingFromChoice = false;
        }

        private Vector2 CalculateSelectedResultOffset(bool isChoiceA)
        {
            if (selectedChoiceView == null)
            {
                return Vector2.zero;
            }

            return isPortraitLayout ? new Vector2(0f, -24f) : new Vector2(0f, -52f);
        }

        private Vector2 CalculateUnselectedExitOffset(bool selectedIsChoiceA)
        {
            var otherView = selectedIsChoiceA ? choiceBView : choiceAView;
            if (otherView == null)
            {
                return Vector2.zero;
            }

            if (isPortraitLayout)
            {
                var verticalDistance = Mathf.Clamp(otherView.RectTransform.rect.height * 0.34f, 58f, 96f);
                return new Vector2(0f, selectedIsChoiceA ? -verticalDistance : verticalDistance);
            }

            var horizontalDistance = Mathf.Clamp(otherView.RectTransform.rect.width * 0.34f, 105f, 160f);
            return new Vector2(selectedIsChoiceA ? horizontalDistance : -horizontalDistance, -6f);
        }

        private void ResolvePendingChoice()
        {
            if (pendingChoice == null)
            {
                return;
            }

            var choice = pendingChoice;
            pendingChoice = null;

            saveData.health = repository.ClampHealth(saveData.health + choice.healthDelta);

            foreach (var delta in choice.hiddenCounterDeltas ?? Array.Empty<SmmCounterDelta>())
            {
                var currentValue = counters.TryGetValue(delta.counterId, out var value) ? value : 0;
                counters[delta.counterId] = repository.ClampCounter(delta.counterId, currentValue + delta.delta);
            }

            foreach (var flag in choice.setFlags ?? Array.Empty<string>())
            {
                if (!string.IsNullOrWhiteSpace(flag))
                {
                    flags.Add(flag);
                }
            }

            foreach (var flag in choice.clearFlags ?? Array.Empty<string>())
            {
                if (!string.IsNullOrWhiteSpace(flag))
                {
                    flags.Remove(flag);
                }
            }

            WriteStateToSave();

            if (saveData.health <= 0)
            {
                ShowEnding(repository.Package.zeroHealthEndingId);
            }
            else if (!string.IsNullOrWhiteSpace(choice.endingId))
            {
                ShowEnding(choice.endingId);
            }
            else
            {
                ShowCard(choice.nextCardId);
            }

            SaveAutosave();
        }

        private void ShowCard(string cardId)
        {
            var card = repository.GetCard(cardId);
            if (card == null)
            {
                Debug.LogError($"Cannot show missing card: {cardId}");
                return;
            }

            currentCard = card;
            currentEnding = null;
            selectedChoiceView = null;
            isContinuingFromChoice = false;
            ShowGameplayScreen();
            SmmCursorManager.ResetForStoryScreen();
            saveData.currentCardId = card.cardId;
            saveData.currentEndingId = string.Empty;

            pendingChoice = null;
            resultOverlay.gameObject.SetActive(false);
            settingsPanel.gameObject.SetActive(false);
            choicePanel.gameObject.SetActive(true);

            ApplyTitlePresentation(card.title);
            eventPanelView?.SetContent(FormatBodyForPresentation(card), FormatSecondaryForPresentation(card), FormatDialogue(card));

            ApplyBackgroundSprite(card.backgroundId, new Color(0.49f, 0.38f, 0.27f, 1f));
            BindChoice(choiceAView, card.choices != null && card.choices.Length > 0 ? card.choices[0] : null);
            BindChoice(choiceBView, card.choices != null && card.choices.Length > 1 ? card.choices[1] : null);
            SetChoiceFocus(0);

            RefreshHud();
            RefreshDeveloperPanel();
            RefreshResponsiveLayout(true);
            PlayChoiceDealIn();
        }

        private void ShowEnding(string endingId)
        {
            var ending = repository.GetEnding(endingId);
            if (ending == null)
            {
                Debug.LogError($"Cannot show missing ending: {endingId}");
                return;
            }

            currentEnding = ending;
            currentCard = null;
            selectedChoiceView = null;
            isContinuingFromChoice = false;
            ShowGameplayScreen();
            SmmCursorManager.ResetForStoryScreen();
            saveData.currentEndingId = ending.endingId;
            saveData.currentCardId = string.Empty;

            pendingChoice = null;
            choicePanel.gameObject.SetActive(false);
            settingsPanel.gameObject.SetActive(false);

            kickerText.text = "Son";
            titleText.text = ending.title;
            bodyText.text = ending.fullEndingText;
            secondaryText.text = string.Empty;
            dialogueText.text = string.Empty;
            secondaryText.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(false);

            ApplyBackgroundSprite(ending.endingImageId, new Color(0.36f, 0.27f, 0.21f, 1f));

            resultPanelView?.SetContent(ending.title, ending.fullEndingText, "Bastan Baslat");
            resultOverlay.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(StartNewStory);
            continueButton.gameObject.SetActive(true);

            RefreshHud();
            RefreshDeveloperPanel();
            RefreshResponsiveLayout(true);
            resultPanelView?.PlayReveal();
        }

        private void BindChoice(SmmChoiceCardView view, SmmChoiceData choice)
        {
            if (view == null)
            {
                return;
            }

            if (choice == null)
            {
                view.gameObject.SetActive(false);
                return;
            }

            view.gameObject.SetActive(true);
            var sprite = imageLoader.LoadSprite(choice.choiceImageId);
            view.Bind(choice, sprite, CardBack, OnChoiceSelected, ContinueChoiceResult);
        }

        private void PlayChoiceDealIn()
        {
            choiceAView?.PlayDealIn(0f, -1f);
            choiceBView?.PlayDealIn(ThemeMotion.dealInStagger, 1f);
        }

        private void ApplyBackgroundSprite(string assetId, Color fallbackColor)
        {
            var sprite = imageLoader.LoadSprite(assetId);
            backgroundImage.sprite = sprite;
            backgroundImage.color = sprite != null ? Color.white : fallbackColor;
            backgroundImage.preserveAspect = false;

            if (backgroundFitter != null && sprite != null)
            {
                backgroundFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
            }
        }

        private bool LoadAutosave()
        {
            if (!PlayerPrefs.HasKey(AutosaveKey))
            {
                return false;
            }

            var json = PlayerPrefs.GetString(AutosaveKey, string.Empty);
            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            saveData = JsonUtility.FromJson<SmmStorySaveData>(json);
            if (saveData == null || saveData.storyId != repository.Package.storyId)
            {
                return false;
            }

            LoadStateFromSave();
            if (!string.IsNullOrWhiteSpace(saveData.currentEndingId))
            {
                ShowEnding(saveData.currentEndingId);
            }
            else if (!string.IsNullOrWhiteSpace(saveData.currentCardId))
            {
                ShowCard(saveData.currentCardId);
            }
            else
            {
                return false;
            }

            return true;
        }

        private void SaveAutosave()
        {
            WriteStateToSave();
            PlayerPrefs.SetString(AutosaveKey, JsonUtility.ToJson(saveData));
            PlayerPrefs.Save();
        }

        private SmmStorySaveData CreateFreshSave()
        {
            return new SmmStorySaveData
            {
                storyId = repository.Package.storyId,
                currentCardId = repository.Package.initialCardId,
                currentEndingId = string.Empty,
                health = repository.Package.health.defaultValue,
                hiddenCounters = repository.CounterDefinitions
                    .Select(counter => new SmmCounterValue { counterId = counter.counterId, value = counter.defaultValue })
                    .ToArray(),
                flags = Array.Empty<string>()
            };
        }

        private void LoadStateFromSave()
        {
            counters.Clear();
            flags.Clear();

            foreach (var definition in repository.CounterDefinitions)
            {
                counters[definition.counterId] = definition.defaultValue;
            }

            foreach (var value in saveData.hiddenCounters ?? Array.Empty<SmmCounterValue>())
            {
                counters[value.counterId] = repository.ClampCounter(value.counterId, value.value);
            }

            foreach (var flag in saveData.flags ?? Array.Empty<string>())
            {
                if (!string.IsNullOrWhiteSpace(flag))
                {
                    flags.Add(flag);
                }
            }

            saveData.health = repository.ClampHealth(saveData.health);
        }

        private void WriteStateToSave()
        {
            saveData.hiddenCounters = counters
                .OrderBy(pair => pair.Key)
                .Select(pair => new SmmCounterValue { counterId = pair.Key, value = pair.Value })
                .ToArray();
            saveData.flags = flags.OrderBy(flag => flag).ToArray();
        }

        private void RefreshHud()
        {
            if (repository == null || saveData == null)
            {
                return;
            }

            hudTitleText.text = string.Empty;
            healthLabelText.text = string.Empty;
            topHudView?.SetContent("Baharatci Adayi", "1. Gun - Ilkbahar", saveData.health, true);
        }

        private void RefreshDeveloperPanel()
        {
            if (developerText == null || developerPanel == null || !developerPanel.gameObject.activeSelf)
            {
                return;
            }

            var cardRef = currentCard != null
                ? $"Current card: {currentCard.cardId}\nBackground: {currentCard.backgroundId}\nChoice A: {currentCard.choices[0].choiceImageId}\nChoice B: {currentCard.choices[1].choiceImageId}"
                : "Current card: -";
            var endingRef = currentEnding != null
                ? $"\nCurrent ending: {currentEnding.endingId}\nEnding image: {currentEnding.endingImageId}"
                : string.Empty;
            var counterText = string.Join("\n", counters.OrderBy(pair => pair.Key).Select(pair => $"{pair.Key}: {pair.Value}"));
            var flagText = flags.Count == 0 ? "-" : string.Join(", ", flags.OrderBy(flag => flag));

            developerText.text =
                $"{cardRef}{endingRef}\n\nHealth: {saveData.health}\n\nCounters:\n{counterText}\n\nFlags:\n{flagText}";
        }

        private static string FormatDialogue(SmmCardData card)
        {
            var lines = card.dialogue ?? Array.Empty<SmmDialogueLine>();
            if (lines.Length == 0)
            {
                return string.Empty;
            }

            return string.Join("\n", lines.Where(line => !string.IsNullOrWhiteSpace(line.text)).Select(line => $"\"{line.text}\""));
        }

        private void ApplyTitlePresentation(string rawTitle)
        {
            kickerText.text = string.Empty;
            titleText.text = string.Empty;
        }

        private static string FormatBodyForPresentation(SmmCardData card)
        {
            if (card == null || string.IsNullOrWhiteSpace(card.bodyText))
            {
                return string.Empty;
            }

            var body = card.bodyText.Trim();
            if (!string.IsNullOrWhiteSpace(card.title))
            {
                body = body.Replace($"{card.title} aninda ", string.Empty);
                body = body.Replace($"{card.title} aninda", string.Empty);
            }

            return SplitSentences(body).FirstOrDefault() ?? body;
        }

        private static string FormatSecondaryForPresentation(SmmCardData card)
        {
            if (card == null || string.IsNullOrWhiteSpace(card.bodyText))
            {
                return string.Empty;
            }

            var sentences = SplitSentences(card.bodyText.Trim()).Skip(1).ToList();
            if (sentences.Count == 0)
            {
                return string.Empty;
            }

            var hasDialogue = card.dialogue != null && card.dialogue.Any(line => !string.IsNullOrWhiteSpace(line.text));
            return string.Join(" ", sentences.Take(hasDialogue ? 1 : 2));
        }

        private static List<string> SplitSentences(string text)
        {
            var sentences = new List<string>();
            if (string.IsNullOrWhiteSpace(text))
            {
                return sentences;
            }

            var start = 0;
            for (var i = 0; i < text.Length; i++)
            {
                var terminal = text[i] == '.' || text[i] == '!' || text[i] == '?';
                if (!terminal)
                {
                    continue;
                }

                var sentence = text.Substring(start, i - start + 1).Trim();
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    sentences.Add(sentence);
                }

                start = i + 1;
            }

            if (start < text.Length)
            {
                var sentence = text.Substring(start).Trim();
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    sentences.Add(sentence);
                }
            }

            return sentences;
        }

        private void ShowMainMenu()
        {
            activeScreen = ScreenMode.MainMenu;
            currentCard = null;
            currentEnding = null;
            pendingChoice = null;
            selectedChoiceView = null;
            isContinuingFromChoice = false;

            SmmCursorManager.ResetForStoryScreen();
            SetGameplayLayersActive(false);
            SetLayerActive(storySelectLayer, false);
            SetLayerActive(menuLayer, true);
            SetLayerActive(frontOverlayLayer, false);
            settingsPanel?.gameObject.SetActive(false);
            menuLayer?.SetAsLastSibling();
            frontOverlayLayer?.SetAsLastSibling();
        }

        private void ShowStorySelect()
        {
            activeScreen = ScreenMode.StorySelect;
            currentCard = null;
            currentEnding = null;
            pendingChoice = null;
            selectedChoiceView = null;
            isContinuingFromChoice = false;

            SmmCursorManager.ResetForStoryScreen();
            SetGameplayLayersActive(false);
            SetLayerActive(menuLayer, false);
            SetLayerActive(storySelectLayer, true);
            SetLayerActive(frontOverlayLayer, false);
            settingsPanel?.gameObject.SetActive(false);
            storySelectLayer?.SetAsLastSibling();
            frontOverlayLayer?.SetAsLastSibling();
        }

        private void ShowGameplayScreen()
        {
            activeScreen = ScreenMode.StoryGameplay;
            SetLayerActive(menuLayer, false);
            SetLayerActive(storySelectLayer, false);
            SetLayerActive(frontOverlayLayer, false);
            SetGameplayLayersActive(true);
        }

        private void SetGameplayLayersActive(bool active)
        {
            SetLayerActive(backgroundLayer, active);
            SetLayerActive(hudLayer, active);
            SetLayerActive(narrativeLayer, active);
            SetLayerActive(choiceLayer, active);
            SetLayerActive(cardAnimationLayer, active);
            SetLayerActive(debugLayer, active);

            if (!active)
            {
                resultOverlay?.gameObject.SetActive(false);
                settingsPanel?.gameObject.SetActive(false);
                developerPanel?.gameObject.SetActive(false);
            }
        }

        private static void SetLayerActive(RectTransform layer, bool active)
        {
            if (layer != null && layer.gameObject.activeSelf != active)
            {
                layer.gameObject.SetActive(active);
            }
        }

        private void ToggleFrontSettingsPanel()
        {
            if (frontOverlayLayer == null)
            {
                return;
            }

            frontOverlayLayer.gameObject.SetActive(!frontOverlayLayer.gameObject.activeSelf);
            frontOverlayLayer.SetAsLastSibling();
        }

        private void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void BuildFrontScreens()
        {
            menuLayer = CreateLayer("Main Menu Layer", canvas.transform);
            storySelectLayer = CreateLayer("Story Select Layer", canvas.transform);
            frontOverlayLayer = CreateLayer("Front Screen Overlay Layer", canvas.transform);

            BuildMainMenuScreen(menuLayer);
            BuildStorySelectScreen(storySelectLayer);
            BuildFrontSettingsOverlay(frontOverlayLayer);

            menuLayer.gameObject.SetActive(false);
            storySelectLayer.gameObject.SetActive(false);
            frontOverlayLayer.gameObject.SetActive(false);
            frontOverlayLayer.SetAsLastSibling();
        }

        private void BuildMainMenuScreen(RectTransform parent)
        {
            var artwork = CreateReferenceArtwork("Main Menu Artwork", parent, "UI/Menu/fiyh_main_menu_reference", new Color(0.13f, 0.09f, 0.06f, 1f));
            artwork.raycastTarget = false;

            mainMenuStartButton = CreateTransparentButton("Main Menu Start Hitbox", parent, new Vector2(0.353f, 0.403f), new Vector2(0.645f, 0.527f), ShowStorySelect);
            mainMenuSettingsButton = CreateTransparentButton("Main Menu Settings Hitbox", parent, new Vector2(0.353f, 0.259f), new Vector2(0.645f, 0.378f), ToggleFrontSettingsPanel);
            mainMenuExitButton = CreateTransparentButton("Main Menu Exit Hitbox", parent, new Vector2(0.353f, 0.124f), new Vector2(0.645f, 0.242f), QuitApplication);
            mainMenuGearButton = CreateTransparentButton("Main Menu Gear Hitbox", parent, new Vector2(0.919f, 0.866f), new Vector2(0.979f, 0.965f), ToggleFrontSettingsPanel);
        }

        private void BuildStorySelectScreen(RectTransform parent)
        {
            var artwork = CreateReferenceArtwork("Story Select Artwork", parent, "UI/Menu/fiyh_story_select_reference", new Color(0.12f, 0.08f, 0.05f, 1f));
            artwork.raycastTarget = false;

            storySelectBackButton = CreateTransparentButton("Story Select Back Hitbox", parent, new Vector2(0.014f, 0.881f), new Vector2(0.063f, 0.966f), ShowMainMenu);
            storySelectGearButton = CreateTransparentButton("Story Select Gear Hitbox", parent, new Vector2(0.937f, 0.881f), new Vector2(0.985f, 0.966f), ToggleFrontSettingsPanel);

            CreateTransparentButton("Story Card 1 Hitbox", parent, new Vector2(0.182f, 0.443f), new Vector2(0.392f, 0.843f), StartNewStory);
            CreateTransparentButton("Story Card 2 Hitbox", parent, new Vector2(0.393f, 0.443f), new Vector2(0.607f, 0.843f), StartNewStory);
            CreateTransparentButton("Story Card 3 Hitbox", parent, new Vector2(0.610f, 0.443f), new Vector2(0.821f, 0.843f), StartNewStory);
            CreateTransparentButton("Story Card 4 Hitbox", parent, new Vector2(0.182f, 0.018f), new Vector2(0.392f, 0.428f), StartNewStory);
            CreateTransparentButton("Story Card 5 Hitbox", parent, new Vector2(0.393f, 0.018f), new Vector2(0.607f, 0.428f), StartNewStory);
            CreateTransparentButton("Story Card 6 Hitbox", parent, new Vector2(0.610f, 0.018f), new Vector2(0.821f, 0.428f), StartNewStory);
        }

        private void BuildFrontSettingsOverlay(RectTransform parent)
        {
            var dimmer = CreateImage("Front Settings Dimmer", parent, new Color(0.03f, 0.02f, 0.015f, 0.54f));
            Stretch(dimmer.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            frontSettingsPanel = CreatePanel("Front Settings Panel", parent, new Color(ParchmentLight.r, ParchmentLight.g, ParchmentLight.b, 0.98f), true);
            ApplySlicedSprite(frontSettingsPanel.GetComponent<Image>(), CreateParchmentPlaqueSprite(360, 210, true));
            AddShadow(frontSettingsPanel.gameObject, new Vector2(0f, -9f), new Color(0.05f, 0.025f, 0.015f, 0.42f));
            Stretch(frontSettingsPanel, new Vector2(0.375f, 0.355f), new Vector2(0.625f, 0.600f), Vector2.zero, Vector2.zero);

            var title = CreateText("Front Settings Title", frontSettingsPanel, 28, FontStyle.Bold, TextAnchor.MiddleCenter, Ink);
            title.text = "Ayarlar";
            Stretch(title.rectTransform, new Vector2(0.10f, 0.58f), new Vector2(0.90f, 0.84f), Vector2.zero, Vector2.zero);

            var closeButton = CreateButton("Front Settings Close Button", frontSettingsPanel, "Kapat", out _);
            Stretch(closeButton.GetComponent<RectTransform>(), new Vector2(0.24f, 0.20f), new Vector2(0.76f, 0.42f), Vector2.zero, Vector2.zero);
            closeButton.onClick.AddListener(ToggleFrontSettingsPanel);
        }

        private static Image CreateReferenceArtwork(string name, Transform parent, string resourcePath, Color fallbackColor)
        {
            var image = CreateImage(name, parent, fallbackColor);
            Stretch(image.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var texture = Resources.Load<Texture2D>(resourcePath);
            if (texture == null)
            {
                return image;
            }

            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
            image.color = Color.white;
            image.preserveAspect = false;
            return image;
        }

        private static Button CreateTransparentButton(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Action onClick)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            gameObject.transform.SetParent(parent, false);
            Stretch(gameObject.GetComponent<RectTransform>(), anchorMin, anchorMax, Vector2.zero, Vector2.zero);

            var image = gameObject.GetComponent<Image>();
            image.color = new Color(1f, 1f, 1f, 0f);

            var button = gameObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            var colors = button.colors;
            colors.normalColor = new Color(1f, 1f, 1f, 0f);
            colors.highlightedColor = new Color(1f, 0.91f, 0.66f, 0f);
            colors.pressedColor = new Color(0.88f, 0.70f, 0.42f, 0f);
            colors.selectedColor = new Color(1f, 0.91f, 0.66f, 0f);
            colors.disabledColor = new Color(0.3f, 0.3f, 0.3f, 0f);
            button.colors = colors;
            if (onClick != null)
            {
                button.onClick.AddListener(() => onClick());
            }

            gameObject.AddComponent<SmmCursorHoverTarget>();
            return button;
        }

        private void BuildUi()
        {
            cursorManager = SmmCursorManager.Ensure();
            SmmCursorManager.ResetForStoryScreen();

            canvas = CreateCanvas("Saray Mutfagindan Muhre Canvas");
            safeAreaRoot = CreateLayer("Safe Area", canvas.transform);
            backgroundLayer = CreateLayer("BackgroundLayer", canvas.transform);
            hudLayer = CreateLayer("HUDLayer", safeAreaRoot);
            narrativeLayer = CreateLayer("NarrativeLayer", safeAreaRoot);
            choiceLayer = CreateLayer("ChoiceLayer", safeAreaRoot);
            cardAnimationLayer = CreateLayer("CardAnimationLayer", safeAreaRoot);
            debugLayer = CreateLayer("DebugLayer", safeAreaRoot);
            backgroundLayer.SetAsFirstSibling();
            safeAreaRoot.SetAsLastSibling();

            backgroundImage = CreateImage("Story Background", backgroundLayer, Color.black);
            Stretch(backgroundImage.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            backgroundFitter = backgroundImage.gameObject.AddComponent<AspectRatioFitter>();
            backgroundFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
            backgroundFitter.aspectRatio = 16f / 9f;

            var warmWash = CreateImage("BackgroundDimmer", backgroundLayer, new Color(0.08f, 0.05f, 0.035f, 0.30f));
            Stretch(warmWash.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var coolWash = CreateImage("Dusty Blue Ambient Wash", backgroundLayer, new Color(DustyBlue.r, DustyBlue.g, DustyBlue.b, 0.06f));
            Stretch(coolWash.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var stageVignette = CreateImage("Stage Readability Vignette", backgroundLayer, new Color(0f, 0f, 0f, 0.30f));
            stageVignette.sprite = CreateSoftVignetteSprite();
            stageVignette.raycastTarget = false;
            Stretch(stageVignette.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            hudPanel = CreateRect("Floating HUD", hudLayer);
            hudTitleText = CreateText("Story Name", hudPanel, 24, FontStyle.Bold, TextAnchor.MiddleLeft, Ink);
            Stretch(hudTitleText.rectTransform, new Vector2(0.026f, 0f), new Vector2(0.58f, 1f), Vector2.zero, Vector2.zero);
            hudTitleText.gameObject.SetActive(false);

            healthLabelText = CreateText("Health Label", hudPanel, 14, FontStyle.Bold, TextAnchor.MiddleRight, RedSoft);
            Stretch(healthLabelText.rectTransform, new Vector2(0.59f, 0f), new Vector2(0.655f, 1f), Vector2.zero, Vector2.zero);
            healthLabelText.gameObject.SetActive(false);

            hudStripPanel = CreateRect("Simple Top UI", hudPanel);
            Stretch(hudStripPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var healthObject = new GameObject("Health Hearts", typeof(RectTransform), typeof(HealthHeartsView));
            healthObject.transform.SetParent(hudStripPanel, false);
            healthHeartsView = healthObject.GetComponent<HealthHeartsView>();
            healthHeartsView.ApplyTheme(Theme);
            AddShadow(healthObject, new Vector2(0f, -3f), new Color(0.08f, 0.04f, 0.02f, 0.28f));

            var titleTagImage = CreateImage("Simple Day Title Tag", hudStripPanel, Color.white);
            titleTagImage.raycastTarget = false;
            ApplySlicedSprite(titleTagImage, CreateSimpleTagSprite(320, 86));
            AddShadow(titleTagImage.gameObject, new Vector2(0f, -5f), new Color(0.08f, 0.04f, 0.02f, 0.30f));
            hudTitleTag = titleTagImage.rectTransform;

            hudRoleText = CreateText("Top Stat Role", hudTitleTag, 22, FontStyle.Bold, TextAnchor.LowerCenter, Theme.ink);
            hudRoleText.text = "Baharatci Adayi";
            hudRoleText.resizeTextForBestFit = true;
            hudRoleText.resizeTextMinSize = 16;
            hudRoleText.resizeTextMaxSize = 23;
            Stretch(hudRoleText.rectTransform, new Vector2(0.080f, 0.485f), new Vector2(0.920f, 0.840f), Vector2.zero, Vector2.zero);

            hudDayText = CreateText("Top Stat Day", hudTitleTag, 19, FontStyle.Bold, TextAnchor.UpperCenter, Color.Lerp(Theme.ink, Theme.mutedInk, 0.18f));
            hudDayText.text = "1. Gun - Ilkbahar";
            hudDayText.resizeTextForBestFit = true;
            hudDayText.resizeTextMinSize = 14;
            hudDayText.resizeTextMaxSize = 20;
            Stretch(hudDayText.rectTransform, new Vector2(0.080f, 0.140f), new Vector2(0.920f, 0.505f), Vector2.zero, Vector2.zero);
            topHudView = hudStripPanel.gameObject.AddComponent<TopHudView>();
            topHudView.Configure(healthHeartsView, hudRoleText, hudDayText);
            topHudView.ApplyTheme(Theme);

            settingsButton = CreateIconButton("Settings Button", hudPanel, out settingsIconImage);
            Stretch(settingsButton.GetComponent<RectTransform>(), new Vector2(0.030f, 0.890f), new Vector2(0.074f, 0.968f), Vector2.zero, Vector2.zero);
            settingsButton.onClick.AddListener(ToggleSettingsPanel);

            narrativePanel = CreatePanel("Narrative Panel", narrativeLayer, ParchmentLight, true);
            ApplySlicedSprite(narrativePanel.GetComponent<Image>(), CreateSimpleScrollBodySprite(720, 190));
            AddShadow(narrativePanel.gameObject, new Vector2(0f, -8f), new Color(ShadowSoft.r, ShadowSoft.g, ShadowSoft.b, 0.38f));

            var narrativeLeftRoll = CreateImage("Narrative Left Scroll Roll", narrativePanel, Color.white);
            narrativeLeftRoll.sprite = CreateScrollRollSprite(82, 180);
            narrativeLeftRoll.raycastTarget = false;
            Stretch(narrativeLeftRoll.rectTransform, new Vector2(-0.030f, -0.060f), new Vector2(0.065f, 1.060f), Vector2.zero, Vector2.zero);

            var narrativeRightRoll = CreateImage("Narrative Right Scroll Roll", narrativePanel, Color.white);
            narrativeRightRoll.sprite = CreateScrollRollSprite(82, 180);
            narrativeRightRoll.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
            narrativeRightRoll.raycastTarget = false;
            Stretch(narrativeRightRoll.rectTransform, new Vector2(0.935f, -0.060f), new Vector2(1.030f, 1.060f), Vector2.zero, Vector2.zero);

            var narrativeRule = CreateImage("Narrative Minimal Divider", narrativePanel, new Color(Theme.bronze.r, Theme.bronze.g, Theme.bronze.b, 0.32f));
            narrativeRule.raycastTarget = false;
            narrativeRule.sprite = CreateDividerSprite();
            Stretch(narrativeRule.rectTransform, new Vector2(0.365f, 0.250f), new Vector2(0.635f, 0.315f), Vector2.zero, Vector2.zero);

            kickerText = CreateText("Chapter Kicker", narrativePanel, 14, FontStyle.Bold, TextAnchor.UpperCenter, new Color(0.48f, 0.31f, 0.18f, 1f));
            Stretch(kickerText.rectTransform, new Vector2(0.07f, 0.78f), new Vector2(0.93f, 0.91f), Vector2.zero, Vector2.zero);
            kickerText.gameObject.SetActive(false);

            titleText = CreateText("Card Title", narrativePanel, 28, FontStyle.Bold, TextAnchor.UpperCenter, Ink);
            Stretch(titleText.rectTransform, new Vector2(0.07f, 0.63f), new Vector2(0.93f, 0.79f), Vector2.zero, Vector2.zero);
            titleText.gameObject.SetActive(false);

            bodyText = CreateText("Card Body", narrativePanel, 36, FontStyle.Bold, TextAnchor.MiddleCenter, Theme.ink);
            bodyText.lineSpacing = 1.10f;
            bodyText.resizeTextForBestFit = true;
            bodyText.resizeTextMinSize = 24;
            bodyText.resizeTextMaxSize = 38;
            Stretch(bodyText.rectTransform, new Vector2(0.130f, 0.475f), new Vector2(0.870f, 0.810f), Vector2.zero, Vector2.zero);

            secondaryText = CreateText("Card Secondary Body", narrativePanel, 22, FontStyle.Bold, TextAnchor.MiddleCenter, Color.Lerp(Theme.ink, Theme.mutedInk, 0.25f));
            secondaryText.lineSpacing = 1.12f;
            secondaryText.resizeTextForBestFit = true;
            secondaryText.resizeTextMinSize = 16;
            secondaryText.resizeTextMaxSize = 23;
            Stretch(secondaryText.rectTransform, new Vector2(0.145f, 0.330f), new Vector2(0.855f, 0.460f), Vector2.zero, Vector2.zero);

            dialogueText = CreateText("Dialogue", narrativePanel, 21, FontStyle.Italic, TextAnchor.MiddleCenter, new Color(0.35f, 0.18f, 0.12f, 1f));
            dialogueText.resizeTextForBestFit = true;
            dialogueText.resizeTextMinSize = 15;
            dialogueText.resizeTextMaxSize = 22;
            Stretch(dialogueText.rectTransform, new Vector2(0.150f, 0.125f), new Vector2(0.850f, 0.255f), Vector2.zero, Vector2.zero);
            eventPanelView = narrativePanel.gameObject.AddComponent<EventPanelView>();
            eventPanelView.Configure(bodyText, secondaryText, dialogueText);

            choicePanel = new GameObject("Choice Cards", typeof(RectTransform), typeof(GridLayoutGroup)).GetComponent<RectTransform>();
            choicePanel.SetParent(choiceLayer, false);
            choiceGrid = choicePanel.GetComponent<GridLayoutGroup>();
            choiceGrid.childAlignment = TextAnchor.MiddleCenter;
            choiceGrid.startAxis = GridLayoutGroup.Axis.Horizontal;
            choiceGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            choiceGrid.constraintCount = 2;
            choiceGrid.spacing = new Vector2(42f, 14f);

            choiceAView = CreateChoiceCard(choicePanel, "Choice A");
            choiceBView = CreateChoiceCard(choicePanel, "Choice B");

            resultOverlay = CreateImage("Result Overlay", cardAnimationLayer, new Color(0.09f, 0.04f, 0.02f, 0.34f)).rectTransform;
            Stretch(resultOverlay, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            resultPanel = CreatePanel("Ending Result Panel", resultOverlay, ParchmentLight, true);
            ApplySlicedSprite(resultPanel.GetComponent<Image>(), CreateSimpleChoiceCardSprite());
            AddShadow(resultPanel.gameObject, ThemeShadow.panelShadowOffset, new Color(0.10f, 0.05f, 0.02f, ThemeShadow.panelShadowAlpha));

            var resultInner = CreateImage("Ending Result Inner Surface", resultPanel, new Color(Parchment.r, Parchment.g, Parchment.b, 0.72f));
            resultInner.raycastTarget = false;
            ApplySlicedSprite(resultInner, CreateSimpleParchmentInsetSprite(360, 160));
            Stretch(resultInner.rectTransform, new Vector2(0.052f, 0.070f), new Vector2(0.948f, 0.930f), Vector2.zero, Vector2.zero);

            var resultBand = CreateImage("Ending Result Title Band", resultPanel, new Color(0.20f, 0.34f, 0.46f, 0.96f));
            resultBand.raycastTarget = false;
            ApplySlicedSprite(resultBand, CreateTitleBandSprite(320, 64));
            resultBand.color = new Color(0.20f, 0.34f, 0.46f, 0.96f);
            Stretch(resultBand.rectTransform, new Vector2(0.190f, 0.760f), new Vector2(0.810f, 0.895f), Vector2.zero, Vector2.zero);

            resultTitleText = CreateText("Result Title", resultBand.transform, 24, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.98f, 0.91f, 0.74f, 1f));
            Stretch(resultTitleText.rectTransform, new Vector2(0.060f, 0.120f), new Vector2(0.940f, 0.880f), Vector2.zero, Vector2.zero);

            resultText = CreateText("Result Text", resultPanel, 22, FontStyle.Normal, TextAnchor.MiddleCenter, WarmInk);
            resultText.lineSpacing = 1.14f;
            resultText.resizeTextForBestFit = true;
            resultText.resizeTextMinSize = 16;
            resultText.resizeTextMaxSize = 22;
            Stretch(resultText.rectTransform, new Vector2(0.110f, 0.290f), new Vector2(0.890f, 0.720f), Vector2.zero, Vector2.zero);

            continueButton = CreateButton("Continue Button", resultPanel, "Devam Et", out continueButtonText);
            Stretch(continueButton.GetComponent<RectTransform>(), new Vector2(0.355f, 0.105f), new Vector2(0.645f, 0.230f), Vector2.zero, Vector2.zero);
            resultPanelView = resultPanel.gameObject.AddComponent<ResultPanelView>();
            resultPanelView.Configure(resultTitleText, resultText, continueButtonText);
            resultOverlay.gameObject.SetActive(false);

            settingsPanel = CreatePanel("Settings Panel", hudLayer, new Color(ParchmentLight.r, ParchmentLight.g, ParchmentLight.b, 0.96f), true);
            ApplySlicedSprite(settingsPanel.GetComponent<Image>(), CreateParchmentPlaqueSprite(300, 180, true));
            AddShadow(settingsPanel.gameObject, new Vector2(0f, -7f), new Color(0.10f, 0.05f, 0.02f, 0.38f));
            AddCornerOrnaments(settingsPanel, "Settings Panel", new Color(Theme.bronze.r, Theme.bronze.g, Theme.bronze.b, 0.58f), 0.035f, 0.100f, 0.155f, 0.265f);
            var settingsTitle = CreateText("Settings Title", settingsPanel, 22, FontStyle.Bold, TextAnchor.MiddleCenter, Ink);
            settingsTitle.text = "Ayarlar";
            Stretch(settingsTitle.rectTransform, new Vector2(0.08f, 0.72f), new Vector2(0.92f, 0.92f), Vector2.zero, Vector2.zero);
            var restartButton = CreateButton("Restart Button", settingsPanel, "Bastan Baslat", out _);
            Stretch(restartButton.GetComponent<RectTransform>(), new Vector2(0.11f, 0.43f), new Vector2(0.89f, 0.64f), Vector2.zero, Vector2.zero);
            restartButton.onClick.AddListener(StartNewStory);
            var closeButton = CreateButton("Close Settings Button", settingsPanel, "Devam", out _);
            Stretch(closeButton.GetComponent<RectTransform>(), new Vector2(0.11f, 0.16f), new Vector2(0.89f, 0.37f), Vector2.zero, Vector2.zero);
            closeButton.onClick.AddListener(() => settingsPanel.gameObject.SetActive(false));
            settingsPanel.gameObject.SetActive(false);

            developerPanel = CreatePanel("Developer Panel", debugLayer, new Color(0.08f, 0.06f, 0.05f, 0.94f), true);
            BuildDeveloperPanel(developerPanel);
            developerPanel.gameObject.SetActive(false);

            BuildFrontScreens();
            RefreshResponsiveLayout(true);
        }

        private void ToggleSettingsPanel()
        {
            if (settingsPanel == null)
            {
                return;
            }

            settingsPanel.gameObject.SetActive(!settingsPanel.gameObject.activeSelf);
        }

        private void SetChoiceFocus(int index)
        {
            if (currentCard == null || pendingChoice != null)
            {
                return;
            }

            focusedChoiceIndex = Mathf.Clamp(index, 0, 1);
            choiceAView?.SetKeyboardFocus(focusedChoiceIndex == 0);
            choiceBView?.SetKeyboardFocus(focusedChoiceIndex == 1);

            var selectedObject = focusedChoiceIndex == 0 ? choiceAView?.gameObject : choiceBView?.gameObject;
            if (selectedObject != null && EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(selectedObject);
            }
        }

        private SmmChoiceCardView CreateChoiceCard(Transform parent, string name)
        {
            var slot = new GameObject(name, typeof(RectTransform), typeof(LayoutElement), typeof(CanvasGroup), typeof(SmmChoiceCardView));
            slot.transform.SetParent(parent, false);
            var layoutElement = slot.GetComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1f;
            layoutElement.flexibleHeight = 1f;

            var placeholder = CreateImage($"{name} Placeholder", slot.transform, new Color(0f, 0f, 0f, 0f));
            placeholder.raycastTarget = false;
            Stretch(placeholder.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var cardRoot = CreateRect($"{name} ChoiceCardRoot", slot.transform);
            Stretch(cardRoot, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var shadow = CreateImage($"{name} Shadow", cardRoot, new Color(0.07f, 0.04f, 0.025f, 0.38f));
            shadow.raycastTarget = false;
            shadow.sprite = CreateCardShadowSprite();
            shadow.type = Image.Type.Sliced;
            Stretch(shadow.rectTransform, Vector2.zero, Vector2.one, new Vector2(-8f, -24f), new Vector2(8f, -16f));

            var glow = CreateImage($"{name} Warm Selection Glow", cardRoot, new Color(Gold.r, Gold.g, Gold.b, 0f));
            glow.sprite = CreateSoftGlowSprite();
            glow.raycastTarget = false;
            Stretch(glow.rectTransform, new Vector2(-0.080f, -0.070f), new Vector2(1.080f, 1.070f), Vector2.zero, Vector2.zero);

            var stateFrame = CreateImage($"{name} State Frame", cardRoot, NarrativeUiTheme.Colors.Walnut);
            ApplySlicedSprite(stateFrame, CreateSimpleChoiceCardSprite());
            Stretch(stateFrame.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            AddOutline(stateFrame.gameObject, new Color(0.16f, 0.10f, 0.065f, 0.70f), new Vector2(NarrativeUiTheme.Borders.Thin, -NarrativeUiTheme.Borders.Thin));
            var button = stateFrame.gameObject.AddComponent<Button>();
            button.targetGraphic = stateFrame;
            button.transition = Selectable.Transition.None;
            stateFrame.gameObject.AddComponent<SmmCursorHoverTarget>();

            var rotator = CreateRect($"{name} CardRotator", stateFrame.transform);
            Stretch(rotator, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var frontFace = CreateRect($"{name} Front Face", rotator);
            Stretch(frontFace, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var frontSurface = CreateImage($"{name} Parchment Surface", frontFace, NarrativeUiTheme.Colors.Parchment);
            frontSurface.raycastTarget = false;
            ApplySlicedSprite(frontSurface, CreateSimpleParchmentInsetSprite(320, 460));
            Stretch(frontSurface.rectTransform, new Vector2(0.030f, 0.030f), new Vector2(0.970f, 0.970f), Vector2.zero, Vector2.zero);

            var titleBandColor = name.EndsWith("A", StringComparison.Ordinal)
                ? new Color(0.20f, 0.34f, 0.46f, 0.96f)
                : new Color(0.18f, 0.18f, 0.17f, 0.96f);
            var titleBand = CreateImage($"{name} Title Band", frontFace, titleBandColor);
            ApplySlicedSprite(titleBand, CreateTitleBandSprite(320, 74));
            titleBand.color = titleBandColor;
            Stretch(titleBand.rectTransform, new Vector2(0.070f, 0.790f), new Vector2(0.930f, 0.930f), Vector2.zero, Vector2.zero);

            var artFrame = CreateImage($"{name} Art Frame", frontFace, NarrativeUiTheme.Colors.WoodMid);
            ApplySlicedSprite(artFrame, CreateSimpleParchmentInsetSprite(280, 340));
            Stretch(artFrame.rectTransform, new Vector2(0.070f, 0.105f), new Vector2(0.930f, 0.765f), Vector2.zero, Vector2.zero);
            AddOutline(artFrame.gameObject, new Color(0.39f, 0.28f, 0.17f, 0.46f), new Vector2(NarrativeUiTheme.Borders.Thin, -NarrativeUiTheme.Borders.Thin));

            var artMask = CreateImage($"{name} Art Mask", artFrame.transform, new Color(0f, 0f, 0f, 0f));
            artMask.gameObject.AddComponent<RectMask2D>();
            Stretch(artMask.rectTransform, new Vector2(0.025f, 0.025f), new Vector2(0.975f, 0.975f), Vector2.zero, Vector2.zero);

            var art = CreateImage($"{name} Art", artMask.transform, NarrativeUiTheme.Colors.ParchmentDeep);
            Stretch(art.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);

            var label = CreateText($"{name} Label", titleBand.transform, NarrativeUiTheme.Typography.CardTitleDesktop, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.98f, 0.91f, 0.74f, 1f));
            label.resizeTextForBestFit = true;
            label.resizeTextMinSize = NarrativeUiTheme.Typography.CardTitleCompact;
            label.resizeTextMaxSize = NarrativeUiTheme.Typography.CardTitleDesktop;
            label.lineSpacing = 1.02f;
            Stretch(label.rectTransform, new Vector2(0.065f, 0.100f), new Vector2(0.935f, 0.900f), Vector2.zero, Vector2.zero);

            var bottomRule = CreateImage($"{name} Minimal Bottom Rule", frontFace, new Color(Theme.bronze.r, Theme.bronze.g, Theme.bronze.b, 0.34f));
            bottomRule.raycastTarget = false;
            bottomRule.sprite = CreateDividerSprite();
            Stretch(bottomRule.rectTransform, new Vector2(0.240f, 0.055f), new Vector2(0.760f, 0.100f), Vector2.zero, Vector2.zero);

            var backFace = CreateImage($"{name} Back Face", rotator, Theme.parchmentLight).rectTransform;
            ApplySlicedSprite(backFace.GetComponent<Image>(), CreateSimpleCardBackSprite());
            Stretch(backFace, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            AddOutline(backFace.gameObject, new Color(0.14f, 0.10f, 0.08f, 0.70f), new Vector2(NarrativeUiTheme.Borders.Thin, -NarrativeUiTheme.Borders.Thin));

            var backInner = CreateImage($"{name} Back Inner Surface", backFace, new Color(Theme.parchment.r, Theme.parchment.g, Theme.parchment.b, 0.90f));
            backInner.raycastTarget = false;
            ApplySlicedSprite(backInner, CreateSimpleParchmentInsetSprite(280, 390));
            Stretch(backInner.rectTransform, new Vector2(0.060f, 0.060f), new Vector2(0.940f, 0.940f), Vector2.zero, Vector2.zero);

            var backRuleTop = CreateImage($"{name} Back Title Rule", backFace, titleBandColor);
            backRuleTop.raycastTarget = false;
            ApplySlicedSprite(backRuleTop, CreateTitleBandSprite(260, 58));
            backRuleTop.color = titleBandColor;
            Stretch(backRuleTop.rectTransform, new Vector2(0.180f, 0.785f), new Vector2(0.820f, 0.900f), Vector2.zero, Vector2.zero);

            var backTitle = CreateText($"{name} Back Title", backRuleTop.transform, 18, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(0.98f, 0.91f, 0.74f, 1f));
            backTitle.text = "Sonuc";
            Stretch(backTitle.rectTransform, new Vector2(0.060f, 0.120f), new Vector2(0.940f, 0.880f), Vector2.zero, Vector2.zero);

            var resultIcon = CreateImage($"{name} Result Motif", backFace, new Color(Gold.r, Gold.g, Gold.b, 0.78f));
            resultIcon.sprite = CreateLeafMotifSprite();
            resultIcon.preserveAspect = true;
            resultIcon.raycastTarget = false;
            Stretch(resultIcon.rectTransform, new Vector2(0.420f, 0.655f), new Vector2(0.580f, 0.775f), Vector2.zero, Vector2.zero);

            var resultBody = CreateText($"{name} Result Text", backFace, NarrativeUiTheme.Typography.ResultDesktop, FontStyle.Bold, TextAnchor.MiddleCenter, Theme.ink);
            resultBody.lineSpacing = 1.14f;
            resultBody.resizeTextForBestFit = true;
            resultBody.resizeTextMinSize = NarrativeUiTheme.Typography.ResultCompact;
            resultBody.resizeTextMaxSize = NarrativeUiTheme.Typography.ResultDesktop;
            Stretch(resultBody.rectTransform, new Vector2(0.120f, 0.270f), new Vector2(0.880f, 0.625f), Vector2.zero, Vector2.zero);

            var healthDelta = CreateText($"{name} Health Delta", backFace, 15, FontStyle.Bold, TextAnchor.MiddleCenter, RedSoft);
            Stretch(healthDelta.rectTransform, new Vector2(0.18f, 0.205f), new Vector2(0.82f, 0.27f), Vector2.zero, Vector2.zero);
            healthDelta.gameObject.SetActive(false);

            var continueHint = CreateText($"{name} Continue Hint", backFace, 17, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(Theme.mutedInk.r, Theme.mutedInk.g, Theme.mutedInk.b, 0.78f));
            continueHint.text = "Karta dokun";
            continueHint.resizeTextForBestFit = true;
            continueHint.resizeTextMinSize = 13;
            continueHint.resizeTextMaxSize = 17;
            Stretch(continueHint.rectTransform, new Vector2(0.18f, 0.125f), new Vector2(0.82f, 0.205f), Vector2.zero, Vector2.zero);

            var backHitTarget = CreateImage($"{name} Back Tap Target", backFace, new Color(0f, 0f, 0f, 0f));
            Stretch(backHitTarget.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var backContinue = backHitTarget.gameObject.AddComponent<Button>();
            backContinue.targetGraphic = backHitTarget;
            backContinue.transition = Selectable.Transition.None;
            backHitTarget.gameObject.AddComponent<SmmCursorHoverTarget>();

            var view = slot.GetComponent<SmmChoiceCardView>();
            view.ApplyTheme(Theme);
            view.Configure(cardRoot, rotator, shadow.rectTransform, frontFace, backFace, shadow, stateFrame, art, label, resultBody, healthDelta, button, backContinue, cardAnimationLayer, resultIcon, continueHint, glow);
            return view;
        }

        private void BuildDeveloperPanel(Transform parent)
        {
            var title = CreateText("Developer Title", parent, 22, FontStyle.Bold, TextAnchor.UpperLeft, ParchmentLight);
            title.text = "Developer Test";
            Stretch(title.rectTransform, new Vector2(0.04f, 0.93f), new Vector2(0.96f, 0.98f), Vector2.zero, Vector2.zero);

            var startButton = CreateButton("Start New", parent, "Hikayeyi Bastan Baslat", out _);
            Stretch(startButton.GetComponent<RectTransform>(), new Vector2(0.04f, 0.86f), new Vector2(0.96f, 0.92f), Vector2.zero, Vector2.zero);
            startButton.onClick.AddListener(StartNewStory);

            cardJumpInput = CreateInput("Card Jump Input", parent, "smm_card_001");
            Stretch(cardJumpInput.GetComponent<RectTransform>(), new Vector2(0.04f, 0.78f), new Vector2(0.66f, 0.84f), Vector2.zero, Vector2.zero);
            var cardButton = CreateButton("Card Jump Button", parent, "Kart", out _);
            Stretch(cardButton.GetComponent<RectTransform>(), new Vector2(0.69f, 0.78f), new Vector2(0.96f, 0.84f), Vector2.zero, Vector2.zero);
            cardButton.onClick.AddListener(() => JumpToCard(cardJumpInput.text.Trim()));

            healthInput = CreateInput("Health Input", parent, "5");
            Stretch(healthInput.GetComponent<RectTransform>(), new Vector2(0.04f, 0.70f), new Vector2(0.66f, 0.76f), Vector2.zero, Vector2.zero);
            var healthButton = CreateButton("Health Button", parent, "Can", out _);
            Stretch(healthButton.GetComponent<RectTransform>(), new Vector2(0.69f, 0.70f), new Vector2(0.96f, 0.76f), Vector2.zero, Vector2.zero);
            healthButton.onClick.AddListener(() =>
            {
                if (int.TryParse(healthInput.text, out var value))
                {
                    SetHealth(value);
                }
            });

            endingInput = CreateInput("Ending Input", parent, "ending_en_guvenilen_saray_ascisi");
            Stretch(endingInput.GetComponent<RectTransform>(), new Vector2(0.04f, 0.62f), new Vector2(0.66f, 0.68f), Vector2.zero, Vector2.zero);
            var endingButton = CreateButton("Ending Button", parent, "Ending", out _);
            Stretch(endingButton.GetComponent<RectTransform>(), new Vector2(0.69f, 0.62f), new Vector2(0.96f, 0.68f), Vector2.zero, Vector2.zero);
            endingButton.onClick.AddListener(() => OpenEnding(endingInput.text.Trim()));

            developerText = CreateText("Developer Text", parent, 15, FontStyle.Normal, TextAnchor.UpperLeft, ParchmentLight);
            Stretch(developerText.rectTransform, new Vector2(0.04f, 0.04f), new Vector2(0.96f, 0.59f), Vector2.zero, Vector2.zero);
        }

        private void BuildFatalErrorUi(string message)
        {
            EnsureEventSystem();

            if (canvas == null)
            {
                canvas = CreateCanvas("Saray Mutfagindan Muhre Error Canvas");
            }

            var text = CreateText("Fatal Error", canvas.transform, 24, FontStyle.Bold, TextAnchor.MiddleCenter, Color.white);
            text.text = "Hikaye yuklenemedi.\nLutfen yeniden deneyin.";
            Stretch(text.rectTransform, new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f), Vector2.zero, Vector2.zero);
        }

        private static bool DeveloperToolsAllowed()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            return true;
#else
            return false;
#endif
        }

        private void RefreshResponsiveLayoutIfNeeded()
        {
            var width = useViewportOverrideForTest ? viewportOverrideWidth : Screen.width;
            var height = useViewportOverrideForTest ? viewportOverrideHeight : Screen.height;
            RefreshResponsiveLayoutForDimensions(width, height, false);
        }

        private void RefreshResponsiveLayout(bool force)
        {
            RefreshResponsiveLayoutForDimensions(Screen.width, Screen.height, force);
        }

        public void ApplyViewportForTest(int width, int height)
        {
            useViewportOverrideForTest = true;
            viewportOverrideWidth = width;
            viewportOverrideHeight = height;
            SmmCursorManager.SetTouchModeForTest(height > width && width <= 600);
            RefreshResponsiveLayoutForDimensions(width, height, true);
        }

        private void RefreshResponsiveLayoutForDimensions(int width, int height, bool force)
        {
            if (!force && width == lastScreenWidth && height == lastScreenHeight)
            {
                return;
            }

            lastScreenWidth = width;
            lastScreenHeight = height;

            var portrait = height > width;
            isPortraitLayout = portrait;
            var tinyLandscape = !portrait && height < 500;
            var compactLandscape = !portrait && (height < 820 || width < 1400);

            ApplySafeArea(width, height);

            Stretch(hudPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            Stretch(hudStripPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var eventWidth = portrait ? Mathf.Clamp(width * 0.90f, 330f, 540f) : Mathf.Clamp(width * 0.58f, compactLandscape ? 650f : 880f, compactLandscape ? 860f : 1120f);
            var eventHeight = portrait ? Mathf.Clamp(height * 0.155f, 116f, 154f) : Mathf.Clamp(height * 0.170f, tinyLandscape ? 100f : 124f, tinyLandscape ? 112f : compactLandscape ? 154f : 184f);
            var choiceAreaWidth = portrait ? Mathf.Clamp(width * 0.92f, 320f, 540f) : Mathf.Clamp(width * 0.58f, width < 1000 ? 620f : 760f, width < 1000 ? 760f : 1120f);
            var choiceAreaHeight = portrait ? Mathf.Clamp(height * 0.610f, 430f, 560f) : Mathf.Clamp(height * 0.560f, height < 500 ? 220f : 380f, compactLandscape ? 430f : 620f);
            var canvasScale = GetCanvasScale(width, height);

            StretchCenteredByPixels(
                settingsButton.GetComponent<RectTransform>(),
                width,
                height,
                portrait ? 46f : tinyLandscape ? 44f : compactLandscape ? 48f : 62f,
                portrait ? 46f : tinyLandscape ? 44f : compactLandscape ? 48f : 62f,
                portrait ? 0.095f : 0.050f,
                portrait ? 0.940f : 0.925f);
            var displayHealthMax = repository != null ? Mathf.Clamp(repository.Package.health.max, 1, 3) : 3;
            var heartSize = portrait ? 32f : tinyLandscape ? 38f : compactLandscape ? 48f : 58f;
            var heartSpacing = portrait ? 6f : tinyLandscape ? 8f : compactLandscape ? 11f : 14f;
            var heartWidth = (displayHealthMax * heartSize) + ((displayHealthMax - 1) * heartSpacing);
            StretchCenteredByPixels(
                healthHeartsView.GetComponent<RectTransform>(),
                width,
                height,
                heartWidth,
                heartSize,
                0.500f,
                portrait ? 0.940f : 0.925f);
            StretchCenteredByPixels(
                hudTitleTag,
                width,
                height,
                portrait ? Mathf.Clamp(width * 0.52f, 190f, 236f) : tinyLandscape ? 216f : compactLandscape ? 220f : 300f,
                portrait ? 54f : tinyLandscape ? 52f : compactLandscape ? 56f : 70f,
                portrait ? 0.720f : 0.860f,
                portrait ? 0.940f : 0.925f);
            Stretch(hudRoleText.rectTransform, new Vector2(0.080f, 0.485f), new Vector2(0.920f, 0.840f), Vector2.zero, Vector2.zero);
            Stretch(hudDayText.rectTransform, new Vector2(0.080f, 0.140f), new Vector2(0.920f, 0.505f), Vector2.zero, Vector2.zero);
            if (tinyLandscape)
            {
                Stretch(bodyText.rectTransform, new Vector2(0.130f, 0.340f), new Vector2(0.870f, 0.805f), Vector2.zero, Vector2.zero);
                Stretch(secondaryText.rectTransform, new Vector2(0.145f, 0.245f), new Vector2(0.855f, 0.340f), Vector2.zero, Vector2.zero);
                Stretch(dialogueText.rectTransform, new Vector2(0.150f, 0.095f), new Vector2(0.850f, 0.235f), Vector2.zero, Vector2.zero);
            }
            else
            {
                Stretch(bodyText.rectTransform, new Vector2(0.130f, 0.475f), new Vector2(0.870f, 0.810f), Vector2.zero, Vector2.zero);
                Stretch(secondaryText.rectTransform, new Vector2(0.145f, 0.330f), new Vector2(0.855f, 0.460f), Vector2.zero, Vector2.zero);
                Stretch(dialogueText.rectTransform, new Vector2(0.150f, 0.125f), new Vector2(0.850f, 0.255f), Vector2.zero, Vector2.zero);
            }
            StretchCenteredByPixels(
                narrativePanel,
                width,
                height,
                eventWidth,
                eventHeight,
                portrait ? 0.735f : tinyLandscape ? 0.690f : 0.735f);
            StretchCenteredByPixels(
                choicePanel,
                width,
                height,
                choiceAreaWidth,
                choiceAreaHeight,
                portrait ? 0.365f : tinyLandscape ? 0.285f : 0.330f);
            StretchCenteredByPixels(
                resultPanel,
                width,
                height,
                portrait ? Mathf.Clamp(width * 0.86f, 310f, 500f) : Mathf.Clamp(width * 0.46f, 600f, 820f),
                portrait ? Mathf.Clamp(height * 0.30f, 245f, 330f) : Mathf.Clamp(height * 0.32f, 278f, 380f),
                portrait ? 0.525f : 0.470f);
            Stretch(settingsPanel, portrait ? new Vector2(0.050f, 0.700f) : new Vector2(0.034f, 0.665f), portrait ? new Vector2(0.560f, 0.915f) : new Vector2(0.260f, 0.890f), Vector2.zero, Vector2.zero);
            Stretch(developerPanel, portrait ? new Vector2(0.05f, 0.06f) : new Vector2(0.68f, 0.05f), portrait ? new Vector2(0.95f, 0.92f) : new Vector2(0.98f, 0.94f), Vector2.zero, Vector2.zero);

            ConfigureChoiceGrid(portrait, width, height);

            hudTitleText.fontSize = ToCanvasFontSize(portrait ? 26 : 24, canvasScale);
            healthLabelText.fontSize = ToCanvasFontSize(portrait ? 18 : 14, canvasScale);
            healthHeartsView.Configure(
                displayHealthMax,
                ToCanvasPixels(heartSize, canvasScale),
                ToCanvasPixels(heartSpacing, canvasScale));
            if (saveData != null)
            {
                topHudView?.SetContent("Baharatci Adayi", "1. Gun - Ilkbahar", saveData.health, false);
            }

            kickerText.fontSize = ToCanvasFontSize(portrait ? 20 : 14, canvasScale);
            titleText.fontSize = ToCanvasFontSize(portrait ? 32 : 28, canvasScale);
            topHudView?.SetTextSizes(ToCanvasFontSize(portrait ? 15 : tinyLandscape ? 15 : compactLandscape ? 18 : 21, canvasScale), ToCanvasFontSize(portrait ? 13 : tinyLandscape ? 13 : compactLandscape ? 16 : 18, canvasScale));
            eventPanelView?.SetTextSizes(ToCanvasFontSize(portrait ? 19 : tinyLandscape ? 18 : compactLandscape ? 27 : 31, canvasScale), ToCanvasFontSize(portrait ? 14 : tinyLandscape ? 12 : compactLandscape ? 18 : 20, canvasScale), ToCanvasFontSize(portrait ? 13 : tinyLandscape ? 11 : compactLandscape ? 16 : 18, canvasScale));
            resultPanelView?.SetTextSizes(ToCanvasFontSize(portrait ? 24 : 24, canvasScale), ToCanvasFontSize(portrait ? 21 : 22, canvasScale));
            choiceAView?.SetTextSizes(ToCanvasFontSize(portrait ? 22 : tinyLandscape ? 16 : compactLandscape ? 25 : 29, canvasScale), ToCanvasFontSize(portrait ? 17 : tinyLandscape ? 12 : compactLandscape ? 18 : 21, canvasScale), ToCanvasFontSize(portrait ? 22 : tinyLandscape ? 17 : 24, canvasScale), ToCanvasFontSize(portrait ? 17 : tinyLandscape ? 12 : 19, canvasScale));
            choiceBView?.SetTextSizes(ToCanvasFontSize(portrait ? 22 : tinyLandscape ? 16 : compactLandscape ? 25 : 29, canvasScale), ToCanvasFontSize(portrait ? 17 : tinyLandscape ? 12 : compactLandscape ? 18 : 21, canvasScale), ToCanvasFontSize(portrait ? 22 : tinyLandscape ? 17 : 24, canvasScale), ToCanvasFontSize(portrait ? 17 : tinyLandscape ? 12 : 19, canvasScale));
        }

        private void ConfigureChoiceGrid(bool portrait, int width, int height)
        {
            if (choiceGrid == null)
            {
                return;
            }

            if (portrait)
            {
                choiceGrid.enabled = false;
                Stretch(choiceAView.RectTransform, new Vector2(0.070f, 0.525f), new Vector2(0.930f, 0.995f), Vector2.zero, Vector2.zero);
                Stretch(choiceBView.RectTransform, new Vector2(0.070f, 0.005f), new Vector2(0.930f, 0.475f), Vector2.zero, Vector2.zero);
            }
            else
            {
                var compactLandscape = height < 820 || width < 1400;
                choiceGrid.enabled = true;
                choiceGrid.startAxis = GridLayoutGroup.Axis.Horizontal;
                choiceGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                choiceGrid.constraintCount = 2;
                var canvasScale = GetCanvasScale(width, height);
                choiceGrid.spacing = new Vector2(ToCanvasPixels(width >= 1800 ? ThemeSpacing.wideCardGap : ThemeSpacing.landscapeCardGap, canvasScale), 0f);
                var heightLimitedWidth = (height * 0.560f) / 1.5f;
                var cardWidth = Mathf.Clamp(
                    Mathf.Min(width * 0.225f, heightLimitedWidth),
                    height < 500 ? 145f : compactLandscape ? 250f : 330f,
                    height < 500 ? 170f : compactLandscape ? 315f : 430f);
                var canvasCardWidth = ToCanvasPixels(cardWidth, canvasScale);
                choiceGrid.cellSize = new Vector2(canvasCardWidth, canvasCardWidth * 1.5f);
            }
        }

        private static float GetCanvasScale(int width, int height)
        {
            var widthScale = Mathf.Max(0.01f, width / 1920f);
            var heightScale = Mathf.Max(0.01f, height / 1080f);
            return Mathf.Sqrt(widthScale * heightScale);
        }

        private static float ToCanvasPixels(float screenPixels, float canvasScale)
        {
            return screenPixels / Mathf.Max(0.01f, canvasScale);
        }

        private static int ToCanvasFontSize(int screenPixels, float canvasScale)
        {
            return Mathf.Max(1, Mathf.RoundToInt(ToCanvasPixels(screenPixels, canvasScale)));
        }

        private static void StretchCenteredByPixels(RectTransform transform, int width, int height, float pixelWidth, float pixelHeight, float centerY)
        {
            StretchCenteredByPixels(transform, width, height, pixelWidth, pixelHeight, 0.5f, centerY);
        }

        private static void StretchCenteredByPixels(RectTransform transform, int width, int height, float pixelWidth, float pixelHeight, float centerX, float centerY)
        {
            var safeWidth = Mathf.Max(1f, width);
            var safeHeight = Mathf.Max(1f, height);
            var halfWidth = Mathf.Clamp01((pixelWidth / safeWidth) * 0.5f);
            var halfHeight = Mathf.Clamp01((pixelHeight / safeHeight) * 0.5f);
            var minX = Mathf.Clamp01(centerX - halfWidth);
            var maxX = Mathf.Clamp01(centerX + halfWidth);
            var minY = Mathf.Clamp01(centerY - halfHeight);
            var maxY = Mathf.Clamp01(centerY + halfHeight);
            Stretch(transform, new Vector2(minX, minY), new Vector2(maxX, maxY), Vector2.zero, Vector2.zero);
        }

        private void ApplySafeArea(int width, int height)
        {
            if (safeAreaRoot == null)
            {
                return;
            }

            var safeArea = useViewportOverrideForTest
                ? new Rect(0f, 0f, width, height)
                : Screen.safeArea;

            if (safeArea.width <= 0f || safeArea.height <= 0f)
            {
                Stretch(safeAreaRoot, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
                return;
            }

            safeAreaRoot.anchorMin = new Vector2(safeArea.xMin / width, safeArea.yMin / height);
            safeAreaRoot.anchorMax = new Vector2(safeArea.xMax / width, safeArea.yMax / height);
            safeAreaRoot.offsetMin = Vector2.zero;
            safeAreaRoot.offsetMax = Vector2.zero;
        }

        private static RectTransform CreateLayer(string name, Transform parent)
        {
            var layer = new GameObject(name, typeof(RectTransform)).GetComponent<RectTransform>();
            layer.SetParent(parent, false);
            Stretch(layer, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            return layer;
        }

        private static Canvas CreateCanvas(string name)
        {
            var canvasObject = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var createdCanvas = canvasObject.GetComponent<Canvas>();
            createdCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;
            return createdCanvas;
        }

        private static Button CreateIconButton(string name, Transform parent, out Image iconImage)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            gameObject.transform.SetParent(parent, false);
            var background = gameObject.GetComponent<Image>();
            background.sprite = CreateSimpleIconDiskSprite();
            background.color = Color.white;
            background.preserveAspect = true;
            AddShadow(gameObject, new Vector2(0f, -3f), new Color(0.08f, 0.04f, 0.02f, 0.28f));

            var button = gameObject.GetComponent<Button>();
            button.targetGraphic = background;
            button.transition = Selectable.Transition.ColorTint;
            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1f, 0.91f, 0.66f, 1f);
            colors.pressedColor = new Color(0.78f, 0.58f, 0.34f, 1f);
            colors.selectedColor = new Color(Gold.r, Gold.g, Gold.b, 0.96f);
            colors.disabledColor = new Color(0.30f, 0.30f, 0.30f, 0.70f);
            colors.fadeDuration = 0.08f;
            colors.colorMultiplier = 1f;
            button.colors = colors;
            gameObject.AddComponent<SmmCursorHoverTarget>();
            gameObject.AddComponent<PremiumButtonMotion>();

            var iconObject = new GameObject($"{name} Icon", typeof(RectTransform), typeof(Image));
            iconObject.transform.SetParent(gameObject.transform, false);
            iconImage = iconObject.GetComponent<Image>();
            iconImage.sprite = CreateCogSprite();
            iconImage.color = new Color(0.20f, 0.12f, 0.08f, 1f);
            iconImage.preserveAspect = true;
            iconImage.raycastTarget = false;
            Stretch(iconImage.rectTransform, new Vector2(0.180f, 0.180f), new Vector2(0.820f, 0.820f), Vector2.zero, Vector2.zero);
            return button;
        }

        private static void ApplySlicedSprite(Image image, Sprite sprite)
        {
            if (image == null || sprite == null)
            {
                return;
            }

            image.sprite = sprite;
            image.type = Image.Type.Sliced;
            image.color = new Color(1f, 1f, 1f, image.color.a);
        }

        private static void AddCornerOrnaments(RectTransform parent, string prefix, Color color, float insetX, float insetY, float sizeX, float sizeY)
        {
            var sprite = CreateCornerFiligreeSprite();
            AddCorner($"{prefix} Corner TL", parent, sprite, color, new Vector2(insetX, 1f - insetY - sizeY), new Vector2(insetX + sizeX, 1f - insetY), new Vector3(1f, 1f, 1f));
            AddCorner($"{prefix} Corner TR", parent, sprite, color, new Vector2(1f - insetX - sizeX, 1f - insetY - sizeY), new Vector2(1f - insetX, 1f - insetY), new Vector3(-1f, 1f, 1f));
            AddCorner($"{prefix} Corner BL", parent, sprite, color, new Vector2(insetX, insetY), new Vector2(insetX + sizeX, insetY + sizeY), new Vector3(1f, -1f, 1f));
            AddCorner($"{prefix} Corner BR", parent, sprite, color, new Vector2(1f - insetX - sizeX, insetY), new Vector2(1f - insetX, insetY + sizeY), new Vector3(-1f, -1f, 1f));
        }

        private static void AddCorner(string name, Transform parent, Sprite sprite, Color color, Vector2 anchorMin, Vector2 anchorMax, Vector3 scale)
        {
            var image = CreateImage(name, parent, color);
            image.sprite = sprite;
            image.preserveAspect = true;
            image.raycastTarget = false;
            image.rectTransform.localScale = scale;
            Stretch(image.rectTransform, anchorMin, anchorMax, Vector2.zero, Vector2.zero);
        }

        private static Sprite CreateParchmentPlaqueSprite(int width, int height, bool ornate)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.98f, 0.88f, 0.66f, 1f);
            var baseColor = new Color(0.90f, 0.73f, 0.46f, 1f);
            var edge = new Color(0.55f, 0.32f, 0.15f, 1f);
            var gold = new Color(0.88f, 0.67f, 0.31f, 1f);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = ornate ? OrnatePlaqueAlpha(px, py) : RoundedRectAlpha(px, py, 0.075f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var noise = (Mathf.Sin((x * 0.33f) + (y * 0.19f)) + Mathf.Sin((x * 0.09f) - (y * 0.27f))) * 0.018f;
                    var color = Color.Lerp(baseColor, light, 0.44f + (py * 0.18f) + noise);
                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    if (edgeDistance < 0.088f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.088f, 0.0f, edgeDistance) * 0.70f);
                    }

                    if (edgeDistance < 0.030f)
                    {
                        color = Color.Lerp(color, gold, 0.62f);
                    }
                    else if (edgeDistance > 0.070f && edgeDistance < 0.104f)
                    {
                        color = Color.Lerp(color, light, 0.16f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.12f, height * 0.22f, width * 0.12f, height * 0.22f));
        }

        private static Sprite CreateParchmentInsetSprite(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.055f);
                    var noise = Mathf.Sin((x * 0.25f) + (y * 0.11f)) * 0.015f;
                    var color = Color.Lerp(new Color(0.86f, 0.66f, 0.39f, 1f), new Color(0.99f, 0.88f, 0.65f, 1f), 0.58f + noise);
                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    if (edgeDistance < 0.035f)
                    {
                        color = Color.Lerp(color, new Color(0.52f, 0.30f, 0.14f, 1f), Mathf.InverseLerp(0.035f, 0f, edgeDistance) * 0.28f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.10f, height * 0.10f, width * 0.10f, height * 0.10f));
        }

        private static Sprite CreateSimpleTagSprite(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.98f, 0.87f, 0.64f, 1f);
            var baseColor = new Color(0.90f, 0.72f, 0.47f, 1f);
            var edge = new Color(0.44f, 0.25f, 0.12f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.075f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var noise = (Mathf.Sin((x * 0.18f) + (y * 0.11f)) + Mathf.Sin((x * 0.07f) - (y * 0.25f))) * 0.012f;
                    var color = Color.Lerp(baseColor, light, 0.55f + (py * 0.08f) + noise);
                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    if (edgeDistance < 0.040f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.040f, 0f, edgeDistance) * 0.58f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            var ink = new Color(0.36f, 0.20f, 0.10f, 0.82f);
            DrawCircleOutline(texture, new Vector2(18f, height - 18f), 6f, ink, 3f);
            DrawCircleOutline(texture, new Vector2(width - 18f, height - 18f), 6f, ink, 3f);
            DrawCircleOutline(texture, new Vector2(18f, 18f), 6f, ink, 3f);
            DrawCircleOutline(texture, new Vector2(width - 18f, 18f), 6f, ink, 3f);
            DrawLine(texture, new Vector2(34f, height - 18f), new Vector2(62f, height - 18f), WithAlpha(ink, 0.55f), 2f);
            DrawLine(texture, new Vector2(width - 62f, height - 18f), new Vector2(width - 34f, height - 18f), WithAlpha(ink, 0.55f), 2f);
            DrawLine(texture, new Vector2(34f, 18f), new Vector2(62f, 18f), WithAlpha(ink, 0.45f), 2f);
            DrawLine(texture, new Vector2(width - 62f, 18f), new Vector2(width - 34f, 18f), WithAlpha(ink, 0.45f), 2f);

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.12f, height * 0.22f, width * 0.12f, height * 0.22f));
        }

        private static Sprite CreateSimpleScrollBodySprite(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.99f, 0.88f, 0.66f, 1f);
            var baseColor = new Color(0.89f, 0.70f, 0.43f, 1f);
            var edge = new Color(0.48f, 0.28f, 0.13f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var left = 0.030f + (Mathf.Sin(py * 19f) * 0.004f);
                    var right = 0.970f + (Mathf.Sin(py * 17f) * 0.004f);
                    var topRough = 0.028f + (Mathf.Sin(x * 0.045f) * 0.007f);
                    var bottomRough = 0.030f + (Mathf.Sin(x * 0.052f + 1.7f) * 0.007f);
                    var notch = 0f;
                    var notchPattern = Mathf.Abs(Mathf.Sin((px * 11.0f) + 0.4f));
                    if (notchPattern > 0.965f)
                    {
                        notch = Mathf.SmoothStep(0.965f, 1f, notchPattern) * 0.030f;
                    }

                    var top = 1f - topRough - notch;
                    var bottom = bottomRough + (notch * 0.55f);
                    if (px < left || px > right || py < bottom || py > top)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var edgeDistance = Mathf.Min(Mathf.Min(px - left, right - px), Mathf.Min(py - bottom, top - py));
                    var noise = (Mathf.Sin((x * 0.17f) + (y * 0.09f)) + Mathf.Sin((x * 0.05f) - (y * 0.23f))) * 0.018f;
                    var color = Color.Lerp(baseColor, light, 0.62f + (py * 0.06f) + noise);
                    if (edgeDistance < 0.040f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.040f, 0f, edgeDistance) * 0.50f);
                    }

                    if (edgeDistance > 0.045f && edgeDistance < 0.060f)
                    {
                        color = Color.Lerp(color, light, 0.12f);
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.14f, height * 0.24f, width * 0.14f, height * 0.24f));
        }

        private static Sprite CreateScrollRollSprite(int width, int height)
        {
            var texture = CreateTransparentTexture(width, height);
            var light = new Color(0.98f, 0.86f, 0.64f, 1f);
            var baseColor = new Color(0.84f, 0.63f, 0.38f, 1f);
            var edge = new Color(0.45f, 0.27f, 0.14f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var halfWidth = 0.31f + (Mathf.Sin(py * Mathf.PI) * 0.035f);
                    var body = Mathf.Abs(px - 0.5f) < halfWidth && py > 0.070f && py < 0.930f;
                    var topCap = ((px - 0.5f) * (px - 0.5f) / 0.115f) + ((py - 0.925f) * (py - 0.925f) / 0.010f) < 1f;
                    var bottomCap = ((px - 0.5f) * (px - 0.5f) / 0.115f) + ((py - 0.075f) * (py - 0.075f) / 0.010f) < 1f;
                    if (!body && !topCap && !bottomCap)
                    {
                        continue;
                    }

                    var side = Mathf.Abs(px - 0.5f) / Mathf.Max(0.01f, halfWidth);
                    var color = Color.Lerp(light, baseColor, side * 0.62f);
                    if (side > 0.84f || py < 0.105f || py > 0.895f)
                    {
                        color = Color.Lerp(color, edge, 0.48f);
                    }

                    color.a = 1f;
                    texture.SetPixel(x, y, color);
                }
            }

            var line = new Color(0.39f, 0.22f, 0.11f, 0.65f);
            DrawLine(texture, new Vector2(width * 0.24f, height * 0.875f), new Vector2(width * 0.76f, height * 0.875f), line, 2f);
            DrawLine(texture, new Vector2(width * 0.24f, height * 0.125f), new Vector2(width * 0.76f, height * 0.125f), line, 2f);
            DrawCircleOutline(texture, new Vector2(width * 0.50f, height * 0.095f), width * 0.18f, WithAlpha(line, 0.58f), 2f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), height);
        }

        private static Sprite CreateSimpleChoiceCardSprite()
        {
            const int width = 240;
            const int height = 360;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.97f, 0.86f, 0.65f, 1f);
            var baseColor = new Color(0.78f, 0.59f, 0.38f, 1f);
            var edge = new Color(0.24f, 0.14f, 0.08f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.055f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    var noise = (Mathf.Sin((x * 0.14f) + (y * 0.08f)) + Mathf.Sin((x * 0.06f) - (y * 0.21f))) * 0.014f;
                    var color = Color.Lerp(baseColor, light, 0.68f + (py * 0.035f) + noise);
                    if (edgeDistance < 0.018f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.018f, 0f, edgeDistance) * 0.86f);
                    }
                    else if (edgeDistance < 0.040f)
                    {
                        color = Color.Lerp(color, new Color(0.49f, 0.31f, 0.17f, 1f), 0.16f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            var ink = new Color(0.31f, 0.17f, 0.09f, 0.72f);
            DrawLine(texture, new Vector2(18f, height - 22f), new Vector2(44f, height - 22f), ink, 2f);
            DrawLine(texture, new Vector2(18f, height - 22f), new Vector2(18f, height - 48f), ink, 2f);
            DrawLine(texture, new Vector2(width - 18f, height - 22f), new Vector2(width - 44f, height - 22f), ink, 2f);
            DrawLine(texture, new Vector2(width - 18f, height - 22f), new Vector2(width - 18f, height - 48f), ink, 2f);
            DrawLine(texture, new Vector2(18f, 22f), new Vector2(44f, 22f), WithAlpha(ink, 0.58f), 2f);
            DrawLine(texture, new Vector2(18f, 22f), new Vector2(18f, 48f), WithAlpha(ink, 0.58f), 2f);
            DrawLine(texture, new Vector2(width - 18f, 22f), new Vector2(width - 44f, 22f), WithAlpha(ink, 0.58f), 2f);
            DrawLine(texture, new Vector2(width - 18f, 22f), new Vector2(width - 18f, 48f), WithAlpha(ink, 0.58f), 2f);

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(22f, 28f, 22f, 28f));
        }

        private static Sprite CreateSimpleParchmentInsetSprite(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.99f, 0.89f, 0.68f, 1f);
            var baseColor = new Color(0.91f, 0.74f, 0.50f, 1f);
            var edge = new Color(0.46f, 0.28f, 0.15f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.045f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    var noise = Mathf.Sin((x * 0.13f) + (y * 0.07f)) * 0.010f;
                    var color = Color.Lerp(baseColor, light, 0.70f + noise);
                    if (edgeDistance < 0.026f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.026f, 0f, edgeDistance) * 0.32f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.08f, height * 0.08f, width * 0.08f, height * 0.08f));
        }

        private static Sprite CreateTitleBandSprite(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.050f);
                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    if (edgeDistance < 0.030f)
                    {
                        alpha *= Mathf.Lerp(0.74f, 1f, edgeDistance / 0.030f);
                    }

                    texture.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(width * 0.10f, height * 0.28f, width * 0.10f, height * 0.28f));
        }

        private static Sprite CreateSimpleCardBackSprite()
        {
            const int width = 240;
            const int height = 360;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            var light = new Color(0.56f, 0.66f, 0.70f, 1f);
            var baseColor = new Color(0.33f, 0.47f, 0.54f, 1f);
            var edge = new Color(0.15f, 0.21f, 0.25f, 1f);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.055f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    var color = Color.Lerp(baseColor, light, 0.28f + (py * 0.10f));
                    if (edgeDistance < 0.040f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.040f, 0f, edgeDistance) * 0.78f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            var line = new Color(0.86f, 0.75f, 0.54f, 0.55f);
            DrawLine(texture, new Vector2(28f, height - 30f), new Vector2(width - 28f, height - 30f), line, 2f);
            DrawLine(texture, new Vector2(28f, 30f), new Vector2(width - 28f, 30f), line, 2f);
            DrawLine(texture, new Vector2(30f, 28f), new Vector2(30f, height - 28f), WithAlpha(line, 0.42f), 2f);
            DrawLine(texture, new Vector2(width - 30f, 28f), new Vector2(width - 30f, height - 28f), WithAlpha(line, 0.42f), 2f);
            DrawDiamond(texture, new Vector2(width * 0.5f, height * 0.5f), 35f, WithAlpha(line, 0.34f));
            DrawCircleOutline(texture, new Vector2(width * 0.5f, height * 0.5f), 42f, WithAlpha(line, 0.26f), 2f);

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(22f, 28f, 22f, 28f));
        }

        private static Sprite CreateSimpleIconDiskSprite()
        {
            const int size = 96;
            var texture = CreateTransparentTexture(size, size);
            var light = new Color(0.97f, 0.84f, 0.59f, 1f);
            var baseColor = new Color(0.74f, 0.49f, 0.25f, 1f);
            var edge = new Color(0.31f, 0.18f, 0.09f, 1f);
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var dx = ((x + 0.5f) / size - 0.5f) * 2f;
                    var dy = ((y + 0.5f) / size - 0.5f) * 2f;
                    var radius = Mathf.Sqrt((dx * dx) + (dy * dy));
                    if (radius > 0.94f)
                    {
                        continue;
                    }

                    var color = Color.Lerp(light, baseColor, radius * 0.64f);
                    if (radius > 0.76f)
                    {
                        color = Color.Lerp(color, edge, Mathf.InverseLerp(0.76f, 0.94f, radius) * 0.72f);
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            DrawCircleOutline(texture, new Vector2(size * 0.5f, size * 0.5f), 36f, new Color(0.23f, 0.14f, 0.08f, 0.58f), 2f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateCardFrameSprite()
        {
            const int width = 220;
            const int height = 296;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.070f);
                    if (alpha <= 0f)
                    {
                        texture.SetPixel(x, y, Color.clear);
                        continue;
                    }

                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    var color = Color.Lerp(new Color(0.42f, 0.20f, 0.10f, 1f), new Color(0.96f, 0.74f, 0.36f, 1f), Mathf.SmoothStep(0.012f, 0.12f, edgeDistance));
                    if (edgeDistance < 0.014f)
                    {
                        color = new Color(0.18f, 0.08f, 0.04f, 1f);
                    }
                    else if (edgeDistance < 0.040f)
                    {
                        color = Color.Lerp(color, new Color(0.74f, 0.42f, 0.18f, 1f), 0.62f);
                    }
                    else if (edgeDistance < 0.072f)
                    {
                        color = Color.Lerp(color, new Color(1f, 0.82f, 0.43f, 1f), 0.58f);
                    }

                    if (py > 0.58f && px < 0.40f && edgeDistance > 0.035f)
                    {
                        color = Color.Lerp(color, new Color(1f, 0.86f, 0.50f, 1f), 0.12f);
                    }

                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(24f, 24f, 24f, 24f));
        }

        private static Sprite CreateCardInsetFrameSprite()
        {
            const int width = 220;
            const int height = 150;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var alpha = RoundedRectAlpha(px, py, 0.050f);
                    var edgeDistance = Mathf.Min(Mathf.Min(px, 1f - px), Mathf.Min(py, 1f - py));
                    var color = edgeDistance < 0.035f
                        ? new Color(0.18f, 0.08f, 0.06f, 1f)
                        : new Color(0.33f, 0.08f, 0.10f, 1f);
                    color.a = alpha;
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(18f, 18f, 18f, 18f));
        }

        private static Sprite CreateCardShadowSprite()
        {
            const int width = 160;
            const int height = 220;
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var nx = ((x + 0.5f) / width - 0.5f) / 0.52f;
                    var ny = ((y + 0.5f) / height - 0.5f) / 0.52f;
                    var radius = nx * nx + ny * ny;
                    var alpha = Mathf.Clamp01(1f - radius);
                    alpha = alpha * alpha * 0.72f;
                    texture.SetPixel(x, y, new Color(0.06f, 0.035f, 0.02f, alpha));
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(36f, 36f, 36f, 36f));
        }

        private static Sprite CreateSoftGlowSprite()
        {
            const int size = 160;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var nx = ((x + 0.5f) / size - 0.5f) * 2f;
                    var ny = ((y + 0.5f) / size - 0.5f) * 2f;
                    var radius = Mathf.Sqrt((nx * nx) + (ny * ny));
                    var alpha = Mathf.Clamp01(1f - radius);
                    alpha = alpha * alpha * 0.70f;
                    texture.SetPixel(x, y, new Color(1f, 0.78f, 0.34f, alpha));
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateCornerFiligreeSprite()
        {
            const int size = 96;
            var texture = CreateTransparentTexture(size, size);
            var color = Color.white;
            DrawLine(texture, new Vector2(10f, 80f), new Vector2(66f, 80f), color, 3f);
            DrawLine(texture, new Vector2(10f, 80f), new Vector2(10f, 28f), color, 3f);
            DrawCircleOutline(texture, new Vector2(38f, 55f), 23f, color, 3f);
            DrawCircleOutline(texture, new Vector2(63f, 74f), 9f, color, 2f);
            DrawCircleOutline(texture, new Vector2(17f, 31f), 9f, color, 2f);
            DrawLine(texture, new Vector2(40f, 80f), new Vector2(80f, 52f), color, 2f);
            DrawDiamond(texture, new Vector2(72f, 72f), 5f, color);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateDividerSprite()
        {
            const int width = 256;
            const int height = 32;
            var texture = CreateTransparentTexture(width, height);
            var color = Color.white;
            DrawLine(texture, new Vector2(14f, 16f), new Vector2(104f, 16f), color, 2f);
            DrawLine(texture, new Vector2(152f, 16f), new Vector2(242f, 16f), color, 2f);
            DrawDiamond(texture, new Vector2(128f, 16f), 8f, color);
            DrawDiamond(texture, new Vector2(112f, 16f), 4f, color);
            DrawDiamond(texture, new Vector2(144f, 16f), 4f, color);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), height);
        }

        private static Sprite CreateSealSprite()
        {
            const int size = 96;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var dx = (x + 0.5f - size * 0.5f) / (size * 0.5f);
                    var dy = (y + 0.5f - size * 0.5f) / (size * 0.5f);
                    var radius = Mathf.Sqrt((dx * dx) + (dy * dy));
                    var color = Color.clear;
                    if (radius <= 0.96f)
                    {
                        color = Color.Lerp(new Color(0.52f, 0.29f, 0.12f, 1f), new Color(0.98f, 0.76f, 0.36f, 1f), Mathf.Clamp01(1f - radius));
                        if (radius > 0.78f)
                        {
                            color = new Color(0.34f, 0.18f, 0.08f, 1f);
                        }
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            DrawCircleOutline(texture, new Vector2(48f, 48f), 31f, new Color(1f, 0.84f, 0.42f, 1f), 2f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateDiamondBadgeSprite()
        {
            const int size = 96;
            var texture = CreateTransparentTexture(size, size);
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var nx = Mathf.Abs(((x + 0.5f) / size - 0.5f) * 2f);
                    var ny = Mathf.Abs(((y + 0.5f) / size - 0.5f) * 2f);
                    var diamond = nx + ny;
                    if (diamond <= 0.98f)
                    {
                        var color = diamond > 0.78f ? new Color(0.22f, 0.12f, 0.07f, 1f) : new Color(0.64f, 0.38f, 0.17f, 1f);
                        if (diamond < 0.56f)
                        {
                            color = Color.Lerp(color, new Color(0.96f, 0.72f, 0.32f, 1f), 0.35f);
                        }

                        texture.SetPixel(x, y, color);
                    }
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateCrownSprite()
        {
            const int size = 96;
            var texture = CreateTransparentTexture(size, size);
            var color = Color.white;
            DrawLine(texture, new Vector2(20f, 31f), new Vector2(76f, 31f), color, 5f);
            DrawLine(texture, new Vector2(25f, 31f), new Vector2(30f, 66f), color, 5f);
            DrawLine(texture, new Vector2(48f, 31f), new Vector2(48f, 72f), color, 5f);
            DrawLine(texture, new Vector2(71f, 31f), new Vector2(66f, 66f), color, 5f);
            DrawLine(texture, new Vector2(30f, 66f), new Vector2(48f, 44f), color, 5f);
            DrawLine(texture, new Vector2(66f, 66f), new Vector2(48f, 44f), color, 5f);
            DrawCircleOutline(texture, new Vector2(30f, 68f), 5f, color, 3f);
            DrawCircleOutline(texture, new Vector2(48f, 75f), 5f, color, 3f);
            DrawCircleOutline(texture, new Vector2(66f, 68f), 5f, color, 3f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateLeafMotifSprite()
        {
            const int size = 96;
            var texture = CreateTransparentTexture(size, size);
            var color = Color.white;
            DrawLine(texture, new Vector2(48f, 18f), new Vector2(48f, 78f), color, 4f);
            DrawLine(texture, new Vector2(48f, 45f), new Vector2(28f, 62f), color, 4f);
            DrawLine(texture, new Vector2(48f, 45f), new Vector2(68f, 62f), color, 4f);
            DrawLine(texture, new Vector2(48f, 56f), new Vector2(33f, 74f), color, 3f);
            DrawLine(texture, new Vector2(48f, 56f), new Vector2(63f, 74f), color, 3f);
            DrawCircleOutline(texture, new Vector2(30f, 63f), 7f, color, 3f);
            DrawCircleOutline(texture, new Vector2(66f, 63f), 7f, color, 3f);
            DrawCircleOutline(texture, new Vector2(35f, 76f), 5f, color, 2f);
            DrawCircleOutline(texture, new Vector2(61f, 76f), 5f, color, 2f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateSettingsBadgeSprite()
        {
            const int width = 104;
            const int height = 136;
            var texture = CreateTransparentTexture(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var px = (x + 0.5f) / width;
                    var py = (y + 0.5f) / height;
                    var inRibbon = py < 0.60f && Mathf.Abs(px - 0.5f) < Mathf.Lerp(0.25f, 0.17f, py / 0.60f);
                    if (inRibbon)
                    {
                        var color = py < 0.11f
                            ? new Color(0.42f, 0.10f, 0.12f, 1f)
                            : new Color(0.30f, 0.08f, 0.10f, 1f);
                        texture.SetPixel(x, y, color);
                    }

                    var dx = (px - 0.5f) / 0.38f;
                    var dy = (py - 0.66f) / 0.29f;
                    var radius = Mathf.Sqrt((dx * dx) + (dy * dy));
                    if (radius <= 1f)
                    {
                        var color = radius > 0.78f
                            ? new Color(0.22f, 0.12f, 0.07f, 1f)
                            : Color.Lerp(new Color(0.50f, 0.28f, 0.13f, 1f), new Color(0.98f, 0.74f, 0.34f, 1f), 1f - radius);
                        texture.SetPixel(x, y, color);
                    }
                }
            }

            DrawLine(texture, new Vector2(38f, 20f), new Vector2(52f, 34f), new Color(0.85f, 0.64f, 0.30f, 1f), 3f);
            DrawLine(texture, new Vector2(66f, 20f), new Vector2(52f, 34f), new Color(0.85f, 0.64f, 0.30f, 1f), 3f);
            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), height);
        }

        private static Texture2D CreateTransparentTexture(int width, int height)
        {
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }

            return texture;
        }

        private static float OrnatePlaqueAlpha(float px, float py)
        {
            var alpha = RoundedRectAlpha(px, py, 0.112f);
            var waist = 1f - Mathf.SmoothStep(0.16f, 0.38f, Mathf.Abs(py - 0.5f));
            var sideInset = Mathf.Lerp(0.010f, 0.030f, waist);
            var sideFade = Mathf.Min(
                Mathf.SmoothStep(0f, 0.018f, px - sideInset),
                Mathf.SmoothStep(0f, 0.018f, (1f - sideInset) - px));

            var topCenterLift = Mathf.SmoothStep(0.830f, 0.915f, py) * (1f - Mathf.SmoothStep(0.000f, 0.145f, Mathf.Abs(px - 0.5f)));
            var bottomCenterLift = Mathf.SmoothStep(0.000f, 0.085f, py) * (1f - Mathf.SmoothStep(0.000f, 0.120f, Mathf.Abs(px - 0.5f)));
            var medallionBlend = Mathf.Max(topCenterLift, bottomCenterLift * 0.35f);

            return Mathf.Clamp01(Mathf.Max(alpha * sideFade, medallionBlend));
        }

        private static float RoundedRectAlpha(float px, float py, float radius)
        {
            var dx = Mathf.Max(Mathf.Max(radius - px, px - (1f - radius)), 0f);
            var dy = Mathf.Max(Mathf.Max(radius - py, py - (1f - radius)), 0f);
            var distance = Mathf.Sqrt((dx * dx) + (dy * dy));
            return 1f - Mathf.SmoothStep(radius * 0.92f, radius, distance);
        }

        private static void DrawLine(Texture2D texture, Vector2 start, Vector2 end, Color color, float width)
        {
            var minX = Mathf.FloorToInt(Mathf.Min(start.x, end.x) - width);
            var maxX = Mathf.CeilToInt(Mathf.Max(start.x, end.x) + width);
            var minY = Mathf.FloorToInt(Mathf.Min(start.y, end.y) - width);
            var maxY = Mathf.CeilToInt(Mathf.Max(start.y, end.y) + width);

            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    var distance = DistanceToSegment(new Vector2(x + 0.5f, y + 0.5f), start, end);
                    if (distance <= width)
                    {
                        BlendPixel(texture, x, y, WithAlpha(color, Mathf.Clamp01(1f - (distance / width))));
                    }
                }
            }
        }

        private static void DrawCircleOutline(Texture2D texture, Vector2 center, float radius, Color color, float width)
        {
            var minX = Mathf.FloorToInt(center.x - radius - width);
            var maxX = Mathf.CeilToInt(center.x + radius + width);
            var minY = Mathf.FloorToInt(center.y - radius - width);
            var maxY = Mathf.CeilToInt(center.y + radius + width);

            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    var distance = Mathf.Abs(Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), center) - radius);
                    if (distance <= width)
                    {
                        BlendPixel(texture, x, y, WithAlpha(color, Mathf.Clamp01(1f - (distance / width))));
                    }
                }
            }
        }

        private static void DrawDiamond(Texture2D texture, Vector2 center, float radius, Color color)
        {
            var minX = Mathf.FloorToInt(center.x - radius);
            var maxX = Mathf.CeilToInt(center.x + radius);
            var minY = Mathf.FloorToInt(center.y - radius);
            var maxY = Mathf.CeilToInt(center.y + radius);
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    var distance = (Mathf.Abs(x + 0.5f - center.x) + Mathf.Abs(y + 0.5f - center.y)) / radius;
                    if (distance <= 1f)
                    {
                        BlendPixel(texture, x, y, WithAlpha(color, Mathf.Clamp01(1f - distance * 0.25f)));
                    }
                }
            }
        }

        private static float DistanceToSegment(Vector2 point, Vector2 start, Vector2 end)
        {
            var segment = end - start;
            var lengthSquared = Vector2.Dot(segment, segment);
            if (lengthSquared <= Mathf.Epsilon)
            {
                return Vector2.Distance(point, start);
            }

            var t = Mathf.Clamp01(Vector2.Dot(point - start, segment) / lengthSquared);
            return Vector2.Distance(point, start + (segment * t));
        }

        private static void BlendPixel(Texture2D texture, int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= texture.width || y >= texture.height || color.a <= 0f)
            {
                return;
            }

            var existing = texture.GetPixel(x, y);
            var alpha = Mathf.Clamp01(color.a + existing.a * (1f - color.a));
            var blended = alpha <= 0f ? Color.clear : ((color * color.a) + (existing * existing.a * (1f - color.a))) / alpha;
            blended.a = alpha;
            texture.SetPixel(x, y, blended);
        }

        private static Color WithAlpha(Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }

        private static Sprite CreateMedallionSprite()
        {
            const int size = 96;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var dx = (x + 0.5f - size * 0.5f) / (size * 0.5f);
                    var dy = (y + 0.5f - size * 0.5f) / (size * 0.5f);
                    var radius = Mathf.Sqrt(dx * dx + dy * dy);
                    var edgeNoise = 0.018f * Mathf.Sin((x * 0.31f) + (y * 0.17f));
                    var color = Color.clear;

                    var clippedCorner = Mathf.Abs(dx) + Mathf.Abs(dy) < 1.34f;
                    var innerPanel = Mathf.Abs(dx) < 0.73f && Mathf.Abs(dy) < 0.73f;
                    if (radius <= 0.98f + edgeNoise && clippedCorner)
                    {
                        var inner = innerPanel ? 0.08f : 0f;
                        color = new Color(0.63f + inner, 0.70f + inner, 0.73f + inner, 1f);
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateSoftVignetteSprite()
        {
            const int size = 128;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var nx = Mathf.Abs(((x + 0.5f) / size - 0.5f) * 2f);
                    var ny = Mathf.Abs(((y + 0.5f) / size - 0.5f) * 2f);
                    var edge = Mathf.Max(nx, ny);
                    var alpha = Mathf.SmoothStep(0.34f, 1f, edge);
                    texture.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateCogSprite()
        {
            const int size = 64;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var dx = (x + 0.5f - size * 0.5f) / (size * 0.5f);
                    var dy = (y + 0.5f - size * 0.5f) / (size * 0.5f);
                    var radius = Mathf.Sqrt(dx * dx + dy * dy);
                    var angle = Mathf.Atan2(dy, dx);
                    var tooth = Mathf.Abs(Mathf.Sin(angle * 6f)) > 0.72f && radius > 0.54f && radius < 0.84f;
                    var ring = radius > 0.35f && radius < 0.62f;
                    var hub = radius < 0.16f;
                    texture.SetPixel(x, y, tooth || ring || hub ? Color.white : Color.clear);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static Sprite CreateResultMotifSprite()
        {
            const int size = 96;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var nx = ((x + 0.5f) / size - 0.5f) * 2f;
                    var ny = ((y + 0.5f) / size - 0.5f) * 2f;
                    var diamond = Mathf.Abs(nx) + Mathf.Abs(ny) < 0.72f;
                    var innerDiamond = Mathf.Abs(nx) + Mathf.Abs(ny) < 0.44f;
                    var eye = (nx * nx / 0.36f) + (ny * ny / 0.10f) < 1f;
                    var pupil = nx * nx + ny * ny < 0.045f;
                    var color = Color.clear;

                    if (diamond && !innerDiamond)
                    {
                        color = Color.white;
                    }
                    else if (eye && !pupil)
                    {
                        color = new Color(0.78f, 0.66f, 0.45f, 0.92f);
                    }
                    else if (pupil)
                    {
                        color = new Color(0.28f, 0.18f, 0.14f, 1f);
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static RectTransform CreatePanel(string name, Transform parent, Color color, bool outlined)
        {
            var image = CreateImage(name, parent, color);
            if (outlined)
            {
                AddOutline(image.gameObject, ParchmentDark, new Vector2(4f, -4f));
            }

            return image.rectTransform;
        }

        private static Image CreateImage(string name, Transform parent, Color color)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            gameObject.transform.SetParent(parent, false);
            var image = gameObject.GetComponent<Image>();
            image.color = color;
            return image;
        }

        private static RectTransform CreateRect(string name, Transform parent)
        {
            var gameObject = new GameObject(name, typeof(RectTransform));
            gameObject.transform.SetParent(parent, false);
            return gameObject.GetComponent<RectTransform>();
        }

        private static Text CreateText(string name, Transform parent, int fontSize, FontStyle fontStyle, TextAnchor alignment, Color color)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Text));
            gameObject.transform.SetParent(parent, false);
            var text = gameObject.GetComponent<Text>();
            text.font = GetBuiltInUiFont();
            text.fontSize = fontSize;
            text.fontStyle = fontStyle;
            text.alignment = alignment;
            text.color = color;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            return text;
        }

        private static Font GetBuiltInUiFont()
        {
            if (builtInUiFont != null)
            {
                return builtInUiFont;
            }

            var candidates = new[] { "Arial.ttf", "LegacyRuntime.ttf" };
            foreach (var candidate in candidates)
            {
                try
                {
                    builtInUiFont = Resources.GetBuiltinResource<Font>(candidate);
                }
                catch (ArgumentException)
                {
                    builtInUiFont = null;
                }

                if (builtInUiFont != null)
                {
                    return builtInUiFont;
                }
            }

            builtInUiFont = Font.CreateDynamicFontFromOSFont("Arial", 16);
            return builtInUiFont;
        }

        private static Button CreateButton(string name, Transform parent, string label, out Text labelText)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
            gameObject.transform.SetParent(parent, false);
            var image = gameObject.GetComponent<Image>();
            image.color = new Color(0.67f, 0.46f, 0.30f, 0.98f);
            AddOutline(gameObject, new Color(0.29f, 0.16f, 0.08f, 0.90f), new Vector2(2f, -2f));

            var button = gameObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            var colors = button.colors;
            colors.normalColor = new Color(0.67f, 0.46f, 0.30f, 0.98f);
            colors.highlightedColor = new Color(0.76f, 0.55f, 0.35f, 1f);
            colors.pressedColor = new Color(0.48f, 0.30f, 0.19f, 1f);
            colors.selectedColor = new Color(Gold.r, Gold.g, Gold.b, 0.95f);
            colors.disabledColor = new Color(0.35f, 0.31f, 0.27f, 0.70f);
            colors.fadeDuration = 0.08f;
            colors.colorMultiplier = 1f;
            button.colors = colors;
            gameObject.AddComponent<SmmCursorHoverTarget>();

            labelText = CreateText($"{name} Label", gameObject.transform, 20, FontStyle.Bold, TextAnchor.MiddleCenter, new Color(1f, 0.88f, 0.62f, 1f));
            labelText.text = label;
            labelText.resizeTextForBestFit = true;
            labelText.resizeTextMinSize = 12;
            labelText.resizeTextMaxSize = 24;
            Stretch(labelText.rectTransform, new Vector2(0.08f, 0.08f), new Vector2(0.92f, 0.92f), Vector2.zero, Vector2.zero);
            return button;
        }

        private static InputField CreateInput(string name, Transform parent, string placeholder)
        {
            var gameObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(InputField));
            gameObject.transform.SetParent(parent, false);
            gameObject.GetComponent<Image>().color = new Color(0.92f, 0.80f, 0.62f, 0.96f);

            var text = CreateText($"{name} Text", gameObject.transform, 16, FontStyle.Normal, TextAnchor.MiddleLeft, Ink);
            Stretch(text.rectTransform, new Vector2(0.04f, 0f), new Vector2(0.96f, 1f), Vector2.zero, Vector2.zero);

            var input = gameObject.GetComponent<InputField>();
            input.textComponent = text;
            input.text = placeholder;
            return input;
        }

        private static void AddOutline(GameObject gameObject, Color color, Vector2 distance)
        {
            var outline = gameObject.AddComponent<Outline>();
            outline.effectColor = color;
            outline.effectDistance = distance;
            outline.useGraphicAlpha = false;
        }

        private static void AddShadow(GameObject gameObject, Vector2 distance, Color color)
        {
            var shadow = gameObject.AddComponent<Shadow>();
            shadow.effectColor = color;
            shadow.effectDistance = distance;
            shadow.useGraphicAlpha = false;
        }

        private static void Stretch(RectTransform transform, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            transform.anchorMin = anchorMin;
            transform.anchorMax = anchorMax;
            transform.offsetMin = offsetMin;
            transform.offsetMax = offsetMax;
        }

        private static void EnsureEventSystem()
        {
            if (FindAnyObjectByType<EventSystem>() != null)
            {
                return;
            }

            var eventSystemObject = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystemObject.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();
        }
    }
}
