using System;
using UnityEngine;

namespace FateInYourHands.Story
{
    [Serializable]
    public sealed class UIThemeConfig
    {
        [Header("Palette")]
        public Color parchment = new Color(0.94f, 0.79f, 0.52f, 0.99f);
        public Color parchmentLight = new Color(0.99f, 0.89f, 0.66f, 1f);
        public Color parchmentDeep = new Color(0.68f, 0.43f, 0.22f, 1f);
        public Color ink = new Color(0.18f, 0.11f, 0.08f, 1f);
        public Color mutedInk = new Color(0.43f, 0.30f, 0.21f, 1f);
        public Color antiqueGold = new Color(0.86f, 0.65f, 0.28f, 1f);
        public Color antiqueGoldLight = new Color(1f, 0.85f, 0.48f, 1f);
        public Color maroon = new Color(0.34f, 0.08f, 0.10f, 1f);
        public Color bronze = new Color(0.49f, 0.29f, 0.15f, 1f);
        public Color heartFull = new Color(0.86f, 0.14f, 0.10f, 1f);

        [Header("Spacing")]
        public SpacingSettings spacing = new SpacingSettings();

        [Header("Shadow")]
        public ShadowSettings shadow = new ShadowSettings();

        [Header("Motion")]
        public MotionSettings motion = new MotionSettings();

        public static UIThemeConfig CreateDefault()
        {
            return new UIThemeConfig();
        }

        [Serializable]
        public sealed class SpacingSettings
        {
            public float landscapeCardGap = 62f;
            public float wideCardGap = 74f;
            public float eventPanelSafePadding = 0.095f;
            public float choiceCardInnerPadding = 0.065f;
        }

        [Serializable]
        public sealed class ShadowSettings
        {
            public Vector2 panelShadowOffset = new Vector2(0f, -8f);
            public Vector2 cardShadowOffset = new Vector2(0f, -10f);
            public Vector2 cardHoverShadowOffset = new Vector2(0f, -16f);
            [Range(0f, 1f)] public float panelShadowAlpha = 0.34f;
            [Range(0f, 1f)] public float cardShadowAlpha = 0.34f;
            [Range(0f, 1f)] public float cardHoverShadowAlpha = 0.50f;
        }

        [Serializable]
        public sealed class MotionSettings
        {
            [Range(0.1f, 0.8f)] public float dealInDuration = 0.26f;
            [Range(0f, 0.3f)] public float dealInStagger = 0.040f;
            public float dealInYOffset = 32f;
            public float dealInXOffset = 18f;
            [Range(0.8f, 1f)] public float dealInStartScale = 0.965f;

            [Range(1.5f, 5f)] public float idleFloatDuration = 3.2f;
            [Range(0f, 8f)] public float idleFloatPixels = 1.0f;

            [Range(0f, 14f)] public float hoverLift = 6f;
            [Range(0f, 14f)] public float focusLift = 7f;
            [Range(-8f, 0f)] public float pressedDrop = -3f;
            [Range(0.9f, 1.1f)] public float hoverScale = 1.012f;
            [Range(0.9f, 1.1f)] public float focusScale = 1.016f;
            [Range(0.9f, 1.05f)] public float pressedScale = 0.982f;
            [Range(1f, 1.1f)] public float selectedScale = 1.025f;

            [Range(0.15f, 0.5f)] public float flipMoveDuration = 0.14f;
            [Range(0.10f, 0.35f)] public float flipHalfDuration = 0.09f;
            [Range(0.2f, 0.55f)] public float resultRevealDuration = 0.22f;
            [Range(0.12f, 0.45f)] public float exitDuration = 0.18f;

            [Range(0.35f, 0.9f)] public float losingAlpha = 0.55f;
            [Range(0.85f, 1f)] public float losingScale = 0.980f;
        }
    }
}
