using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class HealthHeartsView : MonoBehaviour
    {
        private const int UnitsPerHeart = 2;

        private readonly List<Image> hearts = new List<Image>();
        private Color fullColor = NarrativeUiTheme.Colors.Terracotta;
        private Color halfColor = NarrativeUiTheme.Colors.Terracotta;
        private Color emptyColor = NarrativeUiTheme.Colors.ParchmentLight;
        private Color outlineColor = NarrativeUiTheme.Colors.WalnutDeep;
        private Color highlightColor = new Color(1f, 0.68f, 0.54f, 1f);
        private Sprite fullSprite;
        private Sprite halfSprite;
        private Sprite emptySprite;
        private int maxHearts = 3;
        private int currentHealthUnits = -1;
        private Coroutine pulseRoutine;

        public bool UsesSpriteImagesForTest => hearts.Count > 0 && hearts.TrueForAll(heart => heart != null && heart.sprite != null);
        public bool UsesDistinctHalfSpriteForTest => halfSprite != null && halfSprite != fullSprite && halfSprite != emptySprite;

        public void ApplyTheme(UIThemeConfig theme)
        {
            if (theme == null)
            {
                return;
            }

            fullColor = theme.heartFull;
            halfColor = theme.heartFull;
            emptyColor = new Color(theme.parchmentLight.r, theme.parchmentLight.g, theme.parchmentLight.b, 1f);
            outlineColor = new Color(theme.ink.r, theme.ink.g, theme.ink.b, 1f);
            highlightColor = Color.Lerp(theme.heartFull, Color.white, 0.36f);
            fullSprite = null;
            halfSprite = null;
            emptySprite = null;
            EnsureSprites();
            Refresh(false);
        }

        public void Configure(int healthMax, float heartSize, float spacing)
        {
            maxHearts = Mathf.Max(1, healthMax);
            EnsureSprites();
            EnsureLayout(heartSize, spacing);

            var heartCount = maxHearts;
            while (hearts.Count < heartCount)
            {
                var heartObject = new GameObject($"Heart {hearts.Count + 1}", typeof(RectTransform), typeof(Image));
                heartObject.transform.SetParent(transform, false);
                var rect = heartObject.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(heartSize, heartSize);
                var image = heartObject.GetComponent<Image>();
                image.preserveAspect = true;
                image.raycastTarget = false;
                hearts.Add(image);
            }

            for (var i = 0; i < hearts.Count; i++)
            {
                hearts[i].gameObject.SetActive(i < heartCount);
                hearts[i].rectTransform.sizeDelta = new Vector2(heartSize, heartSize);
            }

            Refresh(false);
        }

        public void SetHealth(int value, bool animate)
        {
            var clamped = Mathf.Clamp(value, 0, maxHearts) * UnitsPerHeart;
            var previous = currentHealthUnits;
            currentHealthUnits = clamped;
            Refresh(false);

            if (animate && previous >= 0 && previous != clamped)
            {
                AnimateChangedHeart(previous, clamped);
            }
        }

        private void Refresh(bool animate)
        {
            if (currentHealthUnits < 0)
            {
                currentHealthUnits = maxHearts * UnitsPerHeart;
            }

            for (var i = 0; i < hearts.Count; i++)
            {
                var remaining = currentHealthUnits - (i * UnitsPerHeart);
                hearts[i].sprite = remaining >= UnitsPerHeart
                    ? fullSprite
                    : remaining == 1
                        ? halfSprite
                        : emptySprite;
            }

            if (animate)
            {
                if (pulseRoutine != null)
                {
                    StopCoroutine(pulseRoutine);
                }

                pulseRoutine = StartCoroutine(PulseRoutine());
            }
        }

        private void AnimateChangedHeart(int previousHealth, int nextHealth)
        {
            if (hearts.Count == 0)
            {
                return;
            }

            var changedHealth = Mathf.Max(previousHealth, nextHealth) - 1;
            var changedIndex = Mathf.Clamp(changedHealth / UnitsPerHeart, 0, hearts.Count - 1);
            if (pulseRoutine != null)
            {
                StopCoroutine(pulseRoutine);
            }

            pulseRoutine = StartCoroutine(HeartPulseRoutine(hearts[changedIndex].rectTransform, nextHealth < previousHealth));
        }

        private IEnumerator HeartPulseRoutine(RectTransform heartTransform, bool damage)
        {
            var elapsed = 0f;
            const float duration = 0.24f;
            var startPosition = heartTransform.anchoredPosition;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                var pop = 1f + Mathf.Sin(t * Mathf.PI) * 0.18f;
                var shake = damage ? Mathf.Sin(t * Mathf.PI * 6f) * (1f - t) * 4f : 0f;
                heartTransform.localScale = Vector3.one * pop;
                heartTransform.anchoredPosition = startPosition + new Vector2(shake, 0f);
                yield return null;
            }

            heartTransform.localScale = Vector3.one;
            heartTransform.anchoredPosition = startPosition;
            pulseRoutine = null;
        }

        private IEnumerator PulseRoutine()
        {
            var elapsed = 0f;
            const float duration = 0.22f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                var scale = 1f + Mathf.Sin(t * Mathf.PI) * 0.10f;
                transform.localScale = Vector3.one * scale;
                yield return null;
            }

            transform.localScale = Vector3.one;
            pulseRoutine = null;
        }

        private void EnsureLayout(float heartSize, float spacing)
        {
            var layout = GetComponent<HorizontalLayoutGroup>();
            if (layout == null)
            {
                layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            }

            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.spacing = spacing;

            var rect = GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2((maxHearts * heartSize) + (spacing * Mathf.Max(0, maxHearts - 1)), heartSize);
        }

        private void EnsureSprites()
        {
            if (fullSprite != null)
            {
                return;
            }

            fullSprite = CreateHeartSprite(HeartFill.Full);
            halfSprite = CreateHeartSprite(HeartFill.Half);
            emptySprite = CreateHeartSprite(HeartFill.Empty);
        }

        private Sprite CreateHeartSprite(HeartFill fill)
        {
            const int size = 96;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var nx = ((x + 0.5f) / size - 0.5f) * 2.35f;
                    var ny = ((y + 0.5f) / size - 0.42f) * 2.35f;
                    var inside = IsHeart(nx, ny);
                    var nearEdge = !inside && HasHeartNeighbor(x, y, size);
                    var color = Color.clear;

                    if (nearEdge)
                    {
                        color = outlineColor;
                    }
                    else if (inside)
                    {
                        if (fill == HeartFill.Full)
                        {
                            color = fullColor;
                        }
                        else if (fill == HeartFill.Half)
                        {
                            color = x < size * 0.5f ? halfColor : emptyColor;
                            if (Mathf.Abs(x - (size * 0.5f)) <= 1f)
                            {
                                color = outlineColor;
                            }
                        }
                        else
                        {
                            color = emptyColor;
                        }

                        var highlight = fill != HeartFill.Empty
                            && x < size * 0.42f
                            && y > size * 0.57f
                            && y < size * 0.75f
                            && inside;
                        if (highlight)
                        {
                            color = Color.Lerp(color, highlightColor, 0.38f);
                        }
                    }

                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply(false, true);
            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }

        private static bool HasHeartNeighbor(int x, int y, int size)
        {
            for (var oy = -2; oy <= 2; oy++)
            {
                for (var ox = -2; ox <= 2; ox++)
                {
                    var nxPixel = Mathf.Clamp(x + ox, 0, size - 1);
                    var nyPixel = Mathf.Clamp(y + oy, 0, size - 1);
                    var nx = ((nxPixel + 0.5f) / size - 0.5f) * 2.35f;
                    var ny = ((nyPixel + 0.5f) / size - 0.42f) * 2.35f;
                    if (IsHeart(nx, ny))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsHeart(float x, float y)
        {
            var value = x * x + y * y - 1f;
            return value * value * value - x * x * y * y * y <= 0f;
        }

        private enum HeartFill
        {
            Empty,
            Half,
            Full
        }
    }
}
