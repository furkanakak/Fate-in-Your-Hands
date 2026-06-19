using UnityEngine;

namespace FateInYourHands.Story
{
    internal static class NarrativeUiTheme
    {
        internal static class Colors
        {
            public static readonly Color Ink = new Color(0.19f, 0.12f, 0.09f, 1f);
            public static readonly Color WarmInk = new Color(0.28f, 0.18f, 0.12f, 1f);
            public static readonly Color MutedInk = new Color(0.45f, 0.34f, 0.25f, 1f);
            public static readonly Color PassiveInk = new Color(0.58f, 0.48f, 0.39f, 1f);

            public static readonly Color Parchment = new Color(0.90f, 0.75f, 0.48f, 0.98f);
            public static readonly Color ParchmentLight = new Color(0.97f, 0.86f, 0.60f, 1f);
            public static readonly Color ParchmentDeep = new Color(0.76f, 0.55f, 0.34f, 1f);
            public static readonly Color Walnut = new Color(0.30f, 0.19f, 0.14f, 1f);
            public static readonly Color WalnutDeep = new Color(0.18f, 0.11f, 0.08f, 1f);
            public static readonly Color WoodMid = new Color(0.56f, 0.34f, 0.21f, 1f);
            public static readonly Color Brass = new Color(0.84f, 0.66f, 0.30f, 1f);
            public static readonly Color BrassLight = new Color(1.00f, 0.86f, 0.52f, 1f);
            public static readonly Color Terracotta = new Color(0.78f, 0.30f, 0.22f, 1f);
            public static readonly Color Sage = new Color(0.45f, 0.54f, 0.42f, 1f);
            public static readonly Color DustyBlue = new Color(0.38f, 0.50f, 0.58f, 1f);
            public static readonly Color CardBack = new Color(0.39f, 0.30f, 0.26f, 1f);
            public static readonly Color CardBackDeep = new Color(0.24f, 0.17f, 0.15f, 1f);
            public static readonly Color Shadow = new Color(0.08f, 0.045f, 0.025f, 0.40f);
            public static readonly Color Backdrop = new Color(0.08f, 0.05f, 0.035f, 0.24f);
        }

        internal static class Borders
        {
            public const float Thin = 1.2f;
            public const float Medium = 2.0f;
            public const float Heavy = 3.0f;
        }

        internal static class Spacing
        {
            public const float Small = 8f;
            public const float Medium = 14f;
            public const float Large = 24f;
            public const float CardGapDesktop = 68f;
            public const float CardGapWide = 84f;
        }

        internal static class Typography
        {
            public const int NarrativeMainDesktop = 22;
            public const int NarrativeMainCompact = 20;
            public const int NarrativeSecondaryDesktop = 18;
            public const int NarrativeSecondaryCompact = 16;
            public const int QuoteDesktop = 17;
            public const int QuoteCompact = 15;
            public const int CardTitleDesktop = 25;
            public const int CardTitleCompact = 21;
            public const int ResultDesktop = 24;
            public const int ResultCompact = 20;
        }

        internal static class Motion
        {
            public const float HoverLift = 6f;
            public const float FocusLift = 7f;
            public const float PressedDrop = 2f;
            public const float HoverScale = 1.018f;
            public const float FocusScale = 1.022f;
            public const float PressedScale = 0.996f;
            public const float SelectedScale = 1.035f;
            public const float FlipMoveDuration = 0.30f;
            public const float FlipHalfDuration = 0.18f;
            public const float ResultRevealDuration = 0.18f;
            public const float ExitDuration = 0.22f;
        }
    }
}
