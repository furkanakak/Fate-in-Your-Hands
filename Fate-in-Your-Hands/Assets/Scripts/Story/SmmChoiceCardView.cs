using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class SmmChoiceCardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
    {
        private static readonly UIThemeConfig.MotionSettings FallbackMotion = new UIThemeConfig.MotionSettings();
        private static readonly UIThemeConfig.ShadowSettings FallbackShadow = new UIThemeConfig.ShadowSettings();

        private UIThemeConfig theme = UIThemeConfig.CreateDefault();
        private RectTransform animatedRoot;
        private RectTransform cardRotator;
        private RectTransform shadowRoot;
        private RectTransform frontFace;
        private RectTransform backFace;
        private RectTransform animationLayer;
        private Transform homeParent;
        private int homeSiblingIndex;
        private Image shadowImage;
        private Image artImage;
        private Image borderImage;
        private Image glowImage;
        private Image resultIconImage;
        private Text labelText;
        private Text resultText;
        private Text healthText;
        private Text continueHintText;
        private Button button;
        private Button continueButton;
        private Canvas sortingCanvas;
        private CanvasGroup canvasGroup;
        private Action<SmmChoiceData> selected;
        private Action continueRequested;
        private Coroutine flipRoutine;
        private Coroutine entranceRoutine;

        private Vector2 targetOffset;
        private Vector2 currentOffset;
        private Vector2 resultTargetOffset;
        private Vector3 targetScale = Vector3.one;
        private Vector3 resultTargetScale = Vector3.one;
        private float targetAlpha = 1f;
        private bool hovering;
        private bool pressing;
        private bool backFacePressing;
        private bool frontInputLocked;
        private bool focusVisible;
        private bool hiddenForTest;
        private bool centeredForTest;
        private bool inAnimationLayer;
        private bool resultInputUnlocked;
        private bool entranceAnimating;
        private float idlePhase;
        private Color resultTextBaseColor = Color.white;
        private Color resultIconBaseColor = Color.white;
        private Color continueHintBaseColor = Color.white;

        private UIThemeConfig.MotionSettings Motion => theme != null && theme.motion != null ? theme.motion : FallbackMotion;
        private UIThemeConfig.ShadowSettings Shadow => theme != null && theme.shadow != null ? theme.shadow : FallbackShadow;

        public SmmChoiceData Choice { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public Image ArtImage => artImage;
        public Text LabelText => labelText;
        public Text ResultText => resultText;
        public Button ContinueButton => continueButton;
        public bool IsBackVisible { get; private set; }
        public bool IsFlipAnimating { get; private set; }
        public bool IsFocusedForTest => focusVisible;
        public bool IsHiddenForTest => hiddenForTest;
        public bool IsCenteredForTest => centeredForTest;
        public bool IsResultInputUnlockedForTest => resultInputUnlocked;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            sortingCanvas = GetComponent<Canvas>();
            if (sortingCanvas == null)
            {
                sortingCanvas = gameObject.AddComponent<Canvas>();
            }

            sortingCanvas.overrideSorting = true;
            sortingCanvas.sortingOrder = 0;

            if (GetComponent<GraphicRaycaster>() == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }

            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        private void Update()
        {
            if (animatedRoot == null)
            {
                return;
            }

            if (!entranceAnimating)
            {
                currentOffset = Vector2.Lerp(currentOffset, targetOffset, 18f * Time.unscaledDeltaTime);
                var idleOffset = ShouldApplyIdleFloat()
                    ? new Vector2(0f, Mathf.Sin(((Time.unscaledTime + idlePhase) / Mathf.Max(0.1f, Motion.idleFloatDuration)) * Mathf.PI * 2f) * Motion.idleFloatPixels)
                    : Vector2.zero;
                animatedRoot.anchoredPosition = currentOffset + idleOffset;
                animatedRoot.localScale = Vector3.Lerp(animatedRoot.localScale, targetScale, 18f * Time.unscaledDeltaTime);
            }

            if (!IsFlipAnimating)
            {
                var targetTilt = (hovering || focusVisible) && !IsBackVisible ? -1.2f : 0f;
                var rotator = cardRotator != null ? cardRotator : animatedRoot;
                var euler = rotator.localEulerAngles;
                rotator.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(euler.z, targetTilt, 12f * Time.unscaledDeltaTime));
            }

            if (shadowRoot != null)
            {
                shadowRoot.anchoredPosition = Vector2.Lerp(shadowRoot.anchoredPosition, (hovering || focusVisible) ? Shadow.cardHoverShadowOffset : Shadow.cardShadowOffset, 14f * Time.unscaledDeltaTime);
            }

            if (shadowImage != null)
            {
                shadowImage.color = Color.Lerp(shadowImage.color, GetShadowColor((hovering || focusVisible) ? Shadow.cardHoverShadowAlpha : Shadow.cardShadowAlpha), 14f * Time.unscaledDeltaTime);
            }

            if (glowImage != null)
            {
                var glowAlpha = IsFlipAnimating
                    ? 0.24f
                    : IsBackVisible
                        ? 0.12f
                        : hovering || focusVisible
                            ? 0.16f
                            : 0f;
                glowImage.color = Color.Lerp(glowImage.color, WithAlpha(NarrativeUiTheme.Colors.BrassLight, glowAlpha), 14f * Time.unscaledDeltaTime);
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 14f * Time.unscaledDeltaTime);
            }
        }

        public void ApplyTheme(UIThemeConfig config)
        {
            theme = config ?? UIThemeConfig.CreateDefault();
        }

        public void Configure(
            RectTransform animatedRoot,
            RectTransform cardRotator,
            RectTransform shadowRoot,
            RectTransform frontFace,
            RectTransform backFace,
            Image shadowImage,
            Image borderImage,
            Image artImage,
            Text labelText,
            Text resultText,
            Text healthText,
            Button button,
            Button continueButton,
            RectTransform animationLayer,
            Image resultIconImage = null,
            Text continueHintText = null,
            Image glowImage = null)
        {
            this.animatedRoot = animatedRoot;
            this.cardRotator = cardRotator;
            this.shadowRoot = shadowRoot;
            this.frontFace = frontFace;
            this.backFace = backFace;
            this.animationLayer = animationLayer;
            this.shadowImage = shadowImage;
            this.borderImage = borderImage;
            this.artImage = artImage;
            this.labelText = labelText;
            this.resultText = resultText;
            this.healthText = healthText;
            this.button = button;
            this.continueButton = continueButton;
            this.resultIconImage = resultIconImage;
            this.continueHintText = continueHintText;
            this.glowImage = glowImage;
            resultTextBaseColor = resultText != null ? resultText.color : Color.white;
            resultIconBaseColor = resultIconImage != null ? resultIconImage.color : Color.white;
            continueHintBaseColor = continueHintText != null ? continueHintText.color : Color.white;

            if (this.button != null)
            {
                this.button.onClick.RemoveAllListeners();
                this.button.onClick.AddListener(OnCardButtonClicked);
            }

            if (this.continueButton != null)
            {
                this.continueButton.onClick.RemoveAllListeners();
                this.continueButton.onClick.AddListener(() =>
                {
                    if (IsBackVisible && !IsFlipAnimating && resultInputUnlocked)
                    {
                        continueRequested?.Invoke();
                    }
                });
            }

            ShowFrontImmediate();
        }

        public void Configure(
            RectTransform animatedRoot,
            RectTransform shadowRoot,
            RectTransform frontFace,
            RectTransform backFace,
            Image shadowImage,
            Image borderImage,
            Image artImage,
            Text labelText,
            Text resultText,
            Text healthText,
            Button button,
            Button continueButton)
        {
            Configure(animatedRoot, animatedRoot, shadowRoot, frontFace, backFace, shadowImage, borderImage, artImage, labelText, resultText, healthText, button, continueButton, null);
        }

        public void Bind(SmmChoiceData choice, Sprite sprite, Color fallbackColor, Action<SmmChoiceData> onSelected, Action onContinue)
        {
            Choice = choice;
            selected = onSelected;
            continueRequested = onContinue;
            ResetPresentation();

            if (artImage != null)
            {
                artImage.sprite = sprite;
                artImage.color = sprite != null ? Color.white : fallbackColor;
                artImage.preserveAspect = true;
            }

            if (labelText != null)
            {
                labelText.text = choice != null ? SmmStoryTextNormalizer.NormalizeChoiceText(choice.text) : string.Empty;
            }
        }

        public void ResetPresentation()
        {
            if (flipRoutine != null)
            {
                StopCoroutine(flipRoutine);
                flipRoutine = null;
            }

            if (entranceRoutine != null)
            {
                StopCoroutine(entranceRoutine);
                entranceRoutine = null;
            }

            frontInputLocked = false;
            hovering = false;
            pressing = false;
            backFacePressing = false;
            focusVisible = false;
            IsBackVisible = false;
            IsFlipAnimating = false;
            hiddenForTest = false;
            centeredForTest = false;
            resultInputUnlocked = false;
            entranceAnimating = false;
            idlePhase = (Mathf.Abs(GetInstanceID()) % 100) * 0.031f;
            currentOffset = Vector2.zero;
            targetOffset = Vector2.zero;
            targetScale = Vector3.one;
            targetAlpha = 1f;
            SmmCursorManager.ClearSource(this);
            RestoreHomeParent();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }

            if (animatedRoot != null)
            {
                animatedRoot.anchoredPosition = Vector2.zero;
                animatedRoot.localScale = Vector3.one;
                animatedRoot.localEulerAngles = Vector3.zero;
            }

            if (cardRotator != null)
            {
                cardRotator.localEulerAngles = Vector3.zero;
                cardRotator.localScale = Vector3.one;
            }

            if (continueButton != null)
            {
                continueButton.interactable = false;
                continueButton.gameObject.SetActive(false);
            }

            if (glowImage != null)
            {
                glowImage.color = WithAlpha(NarrativeUiTheme.Colors.BrassLight, 0f);
            }

            SetResultReveal(1f, 0f);

            if (button != null)
            {
                button.interactable = true;
            }

            ShowFrontImmediate();
            RefreshBorder();
            SetRenderPriority(0);
        }

        public void SetRenderPriority(int sortingOrder)
        {
            if (sortingCanvas != null)
            {
                sortingCanvas.sortingOrder = sortingOrder;
            }
        }

        public void SetFrontInteractable(bool interactable)
        {
            frontInputLocked = !interactable;

            if (button != null)
            {
                button.interactable = interactable && !IsBackVisible;
            }

            if (!interactable)
            {
                hovering = false;
                pressing = false;
                backFacePressing = false;
                focusVisible = false;
                UpdateTargets();
                SmmCursorManager.ClearSource(this);
            }
        }

        public void PlayDealIn(float delay, float lateralDirection)
        {
            if (!gameObject.activeInHierarchy || animatedRoot == null)
            {
                return;
            }

            if (entranceRoutine != null)
            {
                StopCoroutine(entranceRoutine);
            }

            entranceRoutine = StartCoroutine(DealInRoutine(delay, Mathf.Sign(lateralDirection == 0f ? 1f : lateralDirection)));
        }

        public void SetTextSizes(int captionMaxSize, int captionMinSize, int resultMaxSize, int resultMinSize)
        {
            if (labelText != null)
            {
                labelText.fontSize = captionMaxSize;
                labelText.resizeTextForBestFit = true;
                labelText.resizeTextMaxSize = captionMaxSize;
                labelText.resizeTextMinSize = captionMinSize;
            }

            if (resultText != null)
            {
                resultText.fontSize = resultMaxSize;
                resultText.resizeTextForBestFit = true;
                resultText.resizeTextMaxSize = resultMaxSize;
                resultText.resizeTextMinSize = resultMinSize;
            }
        }

        public void SetHoverPreview(bool hover)
        {
            if (frontInputLocked && hover && !CanContinueFromBackFace())
            {
                RefreshCursorState(true);
                return;
            }

            hovering = hover && (!IsBackVisible || resultInputUnlocked);
            RefreshBorder();
            UpdateTargets();
            RefreshCursorState(hover);
        }

        public void SetKeyboardFocus(bool focused)
        {
            if (frontInputLocked && focused)
            {
                return;
            }

            focusVisible = focused;
            RefreshBorder();
            UpdateTargets();
        }

        public void ClickForTest()
        {
            SelectWithAnimation();
        }

        public void PlayUnselectedExit(Vector2 exitOffset)
        {
            SetFrontInteractable(false);
            hiddenForTest = false;
            targetAlpha = Motion.losingAlpha;
            targetOffset = exitOffset;
            targetScale = Vector3.one * Motion.losingScale;

            if (flipRoutine != null)
            {
                StopCoroutine(flipRoutine);
            }

            flipRoutine = StartCoroutine(UnselectedExitRoutine(exitOffset));
        }

        public void PlayResultFlip(string result, int healthDelta, Vector2 resultOffset)
        {
            SetBackText(result, healthDelta);
            SetFrontInteractable(false);
            MoveToAnimationLayer();
            resultInputUnlocked = false;
            hiddenForTest = false;
            centeredForTest = false;
            targetAlpha = 1f;
            currentOffset = animatedRoot != null ? animatedRoot.anchoredPosition : Vector2.zero;
            resultTargetOffset = RoundVector2(resultOffset);
            resultTargetScale = Vector3.one * Motion.selectedScale;
            targetOffset = resultTargetOffset;
            targetScale = resultTargetScale;

            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }

            if (flipRoutine != null)
            {
                StopCoroutine(flipRoutine);
            }

            flipRoutine = StartCoroutine(FlipToBackRoutine());
        }

        public IEnumerator PlayContinueExit()
        {
            resultInputUnlocked = false;

            if (continueButton != null)
            {
                continueButton.interactable = false;
                continueButton.gameObject.SetActive(false);
            }

            if (button != null)
            {
                button.interactable = false;
            }

            var startOffset = animatedRoot != null ? animatedRoot.anchoredPosition : Vector2.zero;
            var startScale = animatedRoot != null ? animatedRoot.localScale : Vector3.one;
            var startAlpha = canvasGroup != null ? canvasGroup.alpha : 1f;
            var endOffset = startOffset + new Vector2(0f, 24f);
            var elapsed = 0f;
            var duration = Motion.exitDuration;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = EaseInOut(Mathf.Clamp01(elapsed / duration));
                if (animatedRoot != null)
                {
                    animatedRoot.anchoredPosition = Vector2.Lerp(startOffset, endOffset, t);
                    animatedRoot.localScale = Vector3.Lerp(startScale, Vector3.one * 0.98f, t);
                }

                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);
                }

                yield return null;
            }

            if (animatedRoot != null)
            {
                animatedRoot.anchoredPosition = endOffset;
                animatedRoot.localScale = Vector3.one * 0.98f;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHoverPreview(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovering = false;
            pressing = false;
            backFacePressing = false;
            RefreshBorder();
            UpdateTargets();
            SmmCursorManager.ClearSource(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData != null && eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (CanContinueFromBackFace())
            {
                backFacePressing = true;
                RefreshCursorState(true);
                return;
            }

            if (frontInputLocked || IsBackVisible)
            {
                RefreshCursorState(true);
                return;
            }

            pressing = true;
            RefreshBorder();
            UpdateTargets();
            RefreshCursorState(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var shouldContinueFromBack = backFacePressing && CanContinueFromBackFace() && !WasContinueButtonPressed(eventData);
            pressing = false;
            backFacePressing = false;
            RefreshBorder();
            UpdateTargets();
            RefreshCursorState(hovering || CanContinueFromBackFace());

            if (shouldContinueFromBack)
            {
                continueRequested?.Invoke();
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            SetKeyboardFocus(true);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            SetKeyboardFocus(false);
        }

        private void SelectWithAnimation()
        {
            if (frontInputLocked || Choice == null || IsBackVisible || button == null || !button.interactable)
            {
                return;
            }

            frontInputLocked = true;
            selected?.Invoke(Choice);
        }

        private void OnCardButtonClicked()
        {
            if (IsBackVisible)
            {
                if (!IsFlipAnimating && resultInputUnlocked)
                {
                    continueRequested?.Invoke();
                }

                return;
            }

            SelectWithAnimation();
        }

        private IEnumerator FlipToBackRoutine()
        {
            IsFlipAnimating = true;
            IsBackVisible = false;
            resultInputUnlocked = false;
            frontFace.gameObject.SetActive(true);
            backFace.gameObject.SetActive(false);
            SetResultReveal(0f, 0f);

            if (continueButton != null)
            {
                continueButton.interactable = false;
                continueButton.gameObject.SetActive(false);
            }

            var startOffset = animatedRoot.anchoredPosition;
            var startScale = animatedRoot.localScale;
            var elapsed = 0f;
            var moveDuration = Motion.flipMoveDuration;
            while (elapsed < moveDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / moveDuration);
                animatedRoot.anchoredPosition = Vector2.Lerp(startOffset, resultTargetOffset, EaseInOut(t));
                animatedRoot.localScale = Vector3.Lerp(startScale, resultTargetScale, EaseInOut(t));
                if (cardRotator != null)
                {
                    cardRotator.localEulerAngles = Vector3.zero;
                    cardRotator.localScale = Vector3.one;
                }

                yield return null;
            }

            animatedRoot.anchoredPosition = resultTargetOffset;
            animatedRoot.localScale = resultTargetScale;
            targetOffset = resultTargetOffset;
            targetScale = resultTargetScale;
            currentOffset = resultTargetOffset;
            centeredForTest = true;
            frontFace.gameObject.SetActive(true);
            backFace.gameObject.SetActive(false);

            elapsed = 0f;
            var halfFlipDuration = Motion.flipHalfDuration;
            while (elapsed < halfFlipDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / halfFlipDuration);
                animatedRoot.anchoredPosition = resultTargetOffset;
                animatedRoot.localScale = resultTargetScale;
                if (cardRotator != null)
                {
                    cardRotator.localScale = new Vector3(Mathf.Lerp(1f, 0.04f, EaseInOut(t)), 1f + Mathf.Sin(t * Mathf.PI) * 0.025f, 1f);
                }

                yield return null;
            }

            frontFace.gameObject.SetActive(false);
            backFace.gameObject.SetActive(true);
            SetResultReveal(0f, 0f);

            elapsed = 0f;
            while (elapsed < halfFlipDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / halfFlipDuration);
                animatedRoot.anchoredPosition = resultTargetOffset;
                animatedRoot.localScale = resultTargetScale;
                if (cardRotator != null)
                {
                    cardRotator.localScale = new Vector3(Mathf.Lerp(0.04f, 1f, EaseInOut(t)), 1f + Mathf.Sin(t * Mathf.PI) * 0.025f, 1f);
                }

                yield return null;
            }

            if (cardRotator != null)
            {
                cardRotator.localEulerAngles = Vector3.zero;
                cardRotator.localScale = Vector3.one;
            }

            animatedRoot.anchoredPosition = resultTargetOffset;
            animatedRoot.localScale = resultTargetScale;
            currentOffset = resultTargetOffset;
            IsBackVisible = true;
            IsFlipAnimating = false;

            elapsed = 0f;
            var revealDuration = Motion.resultRevealDuration;
            while (elapsed < revealDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = EaseOut(Mathf.Clamp01(elapsed / revealDuration));
                SetResultReveal(t, 0f);
                if (resultIconImage != null)
                {
                    resultIconImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(0.82f, 1f, t);
                }

                yield return null;
            }

            SetResultReveal(1f, 0f);

            if (button != null)
            {
                button.interactable = false;
            }

            yield return new WaitForSecondsRealtime(0.16f);
            resultInputUnlocked = true;

            if (button != null)
            {
                button.interactable = true;
            }

            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
                continueButton.interactable = true;
            }

            SetResultReveal(1f, 1f);

            SmmCursorManager.SetGlobalInputLocked(false);
            flipRoutine = null;
        }

        private IEnumerator UnselectedExitRoutine(Vector2 exitOffset)
        {
            var startOffset = animatedRoot != null ? animatedRoot.anchoredPosition : Vector2.zero;
            var startScale = animatedRoot != null ? animatedRoot.localScale : Vector3.one;
            var startAlpha = canvasGroup != null ? canvasGroup.alpha : 1f;

            var elapsed = 0f;
            var duration = Motion.exitDuration;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = EaseInOut(Mathf.Clamp01(elapsed / duration));
                if (animatedRoot != null)
                {
                    animatedRoot.anchoredPosition = Vector2.Lerp(startOffset, exitOffset, t);
                    animatedRoot.localScale = Vector3.Lerp(startScale, Vector3.one * Motion.losingScale, t);
                }

                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, Motion.losingAlpha, t);
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.interactable = false;
                }

                yield return null;
            }

            hiddenForTest = true;
            if (canvasGroup != null)
            {
                canvasGroup.alpha = Motion.losingAlpha;
            }

            flipRoutine = null;
        }

        private IEnumerator DealInRoutine(float delay, float lateralDirection)
        {
            entranceAnimating = true;
            var startOffset = new Vector2(lateralDirection * Motion.dealInXOffset, Motion.dealInYOffset);
            var endOffset = Vector2.zero;
            var startScale = Vector3.one * Motion.dealInStartScale;
            var endScale = targetScale;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }

            currentOffset = startOffset;
            targetOffset = endOffset;
            if (animatedRoot != null)
            {
                animatedRoot.anchoredPosition = startOffset;
                animatedRoot.localScale = startScale;
            }

            if (delay > 0f)
            {
                yield return new WaitForSecondsRealtime(delay);
            }

            var elapsed = 0f;
            var duration = Motion.dealInDuration;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = EaseOut(Mathf.Clamp01(elapsed / duration));
                currentOffset = Vector2.Lerp(startOffset, endOffset, t);
                if (animatedRoot != null)
                {
                    animatedRoot.anchoredPosition = currentOffset;
                    animatedRoot.localScale = Vector3.Lerp(startScale, endScale, t);
                }

                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(0f, targetAlpha, t);
                }

                yield return null;
            }

            currentOffset = endOffset;
            targetOffset = endOffset;
            entranceAnimating = false;
            if (animatedRoot != null)
            {
                animatedRoot.anchoredPosition = endOffset;
                animatedRoot.localScale = endScale;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = targetAlpha;
            }

            entranceRoutine = null;
        }

        private void SetBackText(string result, int healthDelta)
        {
            if (resultText != null)
            {
                resultText.text = SmmStoryTextNormalizer.NormalizeResultText(result, Choice != null ? Choice.text : string.Empty, healthDelta);
            }

            if (continueHintText != null)
            {
                continueHintText.text = "Karta dokun";
            }

            if (healthText == null)
            {
                return;
            }

            healthText.text = string.Empty;
            healthText.gameObject.SetActive(false);
        }

        private void ShowFrontImmediate()
        {
            resultInputUnlocked = false;

            if (frontFace != null)
            {
                frontFace.gameObject.SetActive(true);
            }

            if (backFace != null)
            {
                backFace.gameObject.SetActive(false);
            }

            if (cardRotator != null)
            {
                cardRotator.localEulerAngles = Vector3.zero;
                cardRotator.localScale = Vector3.one;
            }

            SetResultReveal(1f, 0f);
        }

        private void RefreshBorder()
        {
            if (borderImage == null)
            {
                return;
            }

            if (frontInputLocked && !IsBackVisible && !IsFlipAnimating)
            {
                borderImage.color = new Color(0.84f, 0.80f, 0.72f, 0.92f);
                return;
            }

            borderImage.color = focusVisible
                ? new Color(1f, 0.97f, 0.88f, 1f)
                : hovering && !IsBackVisible
                    ? new Color(1f, 0.94f, 0.82f, 1f)
                    : Color.white;
        }

        private void UpdateTargets()
        {
            if (IsBackVisible && !IsFlipAnimating)
            {
                targetOffset = resultTargetOffset;
                targetScale = resultTargetScale * (backFacePressing ? 0.988f : hovering ? 1.006f : 1f);
                return;
            }

            if (IsFlipAnimating)
            {
                return;
            }

            if (pressing)
            {
                targetOffset = new Vector2(0f, Motion.pressedDrop);
                targetScale = Vector3.one * Motion.pressedScale;
                return;
            }

            if (hovering || focusVisible)
            {
                targetOffset = new Vector2(0f, focusVisible ? Motion.focusLift : Motion.hoverLift);
                targetScale = Vector3.one * (focusVisible ? Motion.focusScale : Motion.hoverScale);
                return;
            }

            targetOffset = Vector2.zero;
            targetScale = Vector3.one;
        }

        private bool CanContinueFromBackFace()
        {
            return IsBackVisible
                && !IsFlipAnimating
                && continueButton != null
                && continueButton.interactable
                && continueButton.gameObject.activeInHierarchy;
        }

        private bool CanSelectFromFront()
        {
            return !frontInputLocked
                && !IsBackVisible
                && Choice != null
                && button != null
                && button.interactable;
        }

        private void RefreshCursorState(bool pointerOverCard)
        {
            if (!pointerOverCard)
            {
                SmmCursorManager.ClearSource(this);
                return;
            }

            var interactive = CanSelectFromFront() || CanContinueFromBackFace();
            var disabled = !interactive && frontInputLocked;
            SmmCursorManager.SetTargetState(this, interactive, pressing || backFacePressing, disabled);
        }

        private bool WasContinueButtonPressed(PointerEventData eventData)
        {
            if (continueButton == null || eventData == null || eventData.pointerPress == null)
            {
                return false;
            }

            return eventData.pointerPress == continueButton.gameObject
                || eventData.pointerPress.transform.IsChildOf(continueButton.transform);
        }

        private void MoveToAnimationLayer()
        {
            if (animatedRoot == null || animationLayer == null || inAnimationLayer)
            {
                return;
            }

            var corners = new Vector3[4];
            animatedRoot.GetWorldCorners(corners);
            var localMin = animationLayer.InverseTransformPoint(corners[0]);
            var localMax = animationLayer.InverseTransformPoint(corners[2]);
            var localCenter = RoundVector2((Vector2)((localMin + localMax) * 0.5f));
            var localSize = RoundVector2(new Vector2(Mathf.Abs(localMax.x - localMin.x), Mathf.Abs(localMax.y - localMin.y)));

            homeParent = animatedRoot.parent;
            homeSiblingIndex = animatedRoot.GetSiblingIndex();
            animatedRoot.SetParent(animationLayer, false);
            animatedRoot.anchorMin = new Vector2(0.5f, 0.5f);
            animatedRoot.anchorMax = new Vector2(0.5f, 0.5f);
            animatedRoot.pivot = new Vector2(0.5f, 0.5f);
            animatedRoot.sizeDelta = localSize;
            animatedRoot.anchoredPosition = localCenter;
            animatedRoot.localScale = Vector3.one;
            animatedRoot.SetAsLastSibling();
            currentOffset = localCenter;
            inAnimationLayer = true;
        }

        private static Vector2 RoundVector2(Vector2 value)
        {
            return new Vector2(Mathf.Round(value.x), Mathf.Round(value.y));
        }

        private void RestoreHomeParent()
        {
            if (animatedRoot == null || !inAnimationLayer || homeParent == null)
            {
                return;
            }

            animatedRoot.SetParent(homeParent, false);
            animatedRoot.SetSiblingIndex(Mathf.Clamp(homeSiblingIndex, 0, homeParent.childCount - 1));
            Stretch(animatedRoot, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            inAnimationLayer = false;
        }

        private static void Stretch(RectTransform transform, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            transform.anchorMin = anchorMin;
            transform.anchorMax = anchorMax;
            transform.offsetMin = offsetMin;
            transform.offsetMax = offsetMax;
        }

        private static float EaseInOut(float t)
        {
            return t * t * (3f - 2f * t);
        }

        private static float EaseOut(float t)
        {
            var inverse = 1f - t;
            return 1f - (inverse * inverse * inverse);
        }

        private void SetResultReveal(float textAlpha, float hintAlpha)
        {
            if (resultText != null)
            {
                resultText.color = WithAlpha(resultTextBaseColor, textAlpha);
            }

            if (resultIconImage != null)
            {
                resultIconImage.color = WithAlpha(resultIconBaseColor, textAlpha * resultIconBaseColor.a);
            }

            if (continueHintText != null)
            {
                continueHintText.color = WithAlpha(continueHintBaseColor, hintAlpha * continueHintBaseColor.a);
            }
        }

        private bool ShouldApplyIdleFloat()
        {
            return !IsBackVisible
                && !IsFlipAnimating
                && !frontInputLocked
                && !pressing
                && !entranceAnimating
                && Motion.idleFloatPixels > 0f;
        }

        private static Color GetShadowColor(float alpha)
        {
            return new Color(NarrativeUiTheme.Colors.Shadow.r, NarrativeUiTheme.Colors.Shadow.g, NarrativeUiTheme.Colors.Shadow.b, Mathf.Clamp01(alpha));
        }

        private static Color WithAlpha(Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }
    }
}
