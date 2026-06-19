using UnityEngine;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class TopHudView : MonoBehaviour
    {
        private HealthHeartsView heartsView;
        private Text roleText;
        private Text dayText;

        public void Configure(HealthHeartsView hearts, Text role, Text day)
        {
            heartsView = hearts;
            roleText = role;
            dayText = day;
        }

        public void ApplyTheme(UIThemeConfig theme)
        {
            if (theme == null)
            {
                return;
            }

            heartsView?.ApplyTheme(theme);
            if (roleText != null)
            {
                roleText.color = theme.ink;
            }

            if (dayText != null)
            {
                dayText.color = Color.Lerp(theme.ink, theme.mutedInk, 0.22f);
            }
        }

        public void SetContent(string role, string day, int health, bool animate)
        {
            if (roleText != null)
            {
                roleText.text = role;
            }

            if (dayText != null)
            {
                dayText.text = day;
            }

            heartsView?.SetHealth(health, animate);
        }

        public void SetTextSizes(int roleSize, int daySize)
        {
            if (roleText != null)
            {
                roleText.fontSize = roleSize;
                roleText.resizeTextMaxSize = roleSize;
                roleText.resizeTextMinSize = Mathf.Max(12, roleSize - 6);
            }

            if (dayText != null)
            {
                dayText.fontSize = daySize;
                dayText.resizeTextMaxSize = daySize;
                dayText.resizeTextMinSize = Mathf.Max(11, daySize - 5);
            }
        }
    }
}
