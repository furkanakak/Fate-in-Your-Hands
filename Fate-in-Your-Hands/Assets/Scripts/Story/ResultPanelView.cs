using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class ResultPanelView : MonoBehaviour
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Coroutine revealRoutine;
        private Text titleText;
        private Text bodyText;
        private Text continueText;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void Configure(Text title, Text body, Text continueLabel)
        {
            titleText = title;
            bodyText = body;
            continueText = continueLabel;
        }

        public void SetContent(string title, string body, string continueLabel)
        {
            if (titleText != null)
            {
                titleText.text = title ?? string.Empty;
            }

            if (bodyText != null)
            {
                bodyText.text = body ?? string.Empty;
            }

            if (continueText != null)
            {
                continueText.text = continueLabel ?? string.Empty;
            }
        }

        public void SetTextSizes(int titleSize, int bodySize)
        {
            if (titleText != null)
            {
                titleText.fontSize = titleSize;
                titleText.resizeTextForBestFit = true;
                titleText.resizeTextMaxSize = titleSize;
                titleText.resizeTextMinSize = Mathf.Max(17, titleSize - 8);
            }

            if (bodyText != null)
            {
                bodyText.fontSize = bodySize;
                bodyText.resizeTextMaxSize = bodySize;
                bodyText.resizeTextMinSize = Mathf.Max(15, bodySize - 7);
            }
        }

        public void PlayReveal()
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            if (revealRoutine != null)
            {
                StopCoroutine(revealRoutine);
            }

            revealRoutine = StartCoroutine(RevealRoutine());
        }

        private IEnumerator RevealRoutine()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }

            var elapsed = 0f;
            const float duration = 0.30f;
            var startPosition = rectTransform.anchoredPosition + new Vector2(0f, -14f);
            var endPosition = rectTransform.anchoredPosition;
            var startScale = Vector3.one * 0.955f;

            canvasGroup.alpha = 0f;
            rectTransform.anchoredPosition = startPosition;
            rectTransform.localScale = startScale;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = EaseOut(Mathf.Clamp01(elapsed / duration));
                canvasGroup.alpha = t;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                rectTransform.localScale = Vector3.Lerp(startScale, Vector3.one, t);
                yield return null;
            }

            canvasGroup.alpha = 1f;
            rectTransform.anchoredPosition = endPosition;
            rectTransform.localScale = Vector3.one;
            revealRoutine = null;
        }

        private static float EaseOut(float t)
        {
            var inverse = 1f - t;
            return 1f - (inverse * inverse * inverse);
        }
    }
}
