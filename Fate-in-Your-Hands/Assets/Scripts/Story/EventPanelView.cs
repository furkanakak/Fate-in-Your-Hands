using UnityEngine;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public sealed class EventPanelView : MonoBehaviour
    {
        private Text mainText;
        private Text secondaryText;
        private Text quoteText;

        public void Configure(Text main, Text secondary, Text quote)
        {
            mainText = main;
            secondaryText = secondary;
            quoteText = quote;
        }

        public void SetContent(string main, string secondary, string quote)
        {
            if (mainText != null)
            {
                mainText.text = main ?? string.Empty;
            }

            if (secondaryText != null)
            {
                secondaryText.text = secondary ?? string.Empty;
                secondaryText.gameObject.SetActive(!string.IsNullOrWhiteSpace(secondaryText.text));
            }

            if (quoteText != null)
            {
                quoteText.text = quote ?? string.Empty;
                quoteText.gameObject.SetActive(!string.IsNullOrWhiteSpace(quoteText.text));
            }
        }

        public void SetTextSizes(int mainSize, int secondarySize, int quoteSize)
        {
            ApplySize(mainText, mainSize, Mathf.Max(17, mainSize - 8));
            ApplySize(secondaryText, secondarySize, Mathf.Max(14, secondarySize - 5));
            ApplySize(quoteText, quoteSize, Mathf.Max(13, quoteSize - 4));
        }

        private static void ApplySize(Text text, int maxSize, int minSize)
        {
            if (text == null)
            {
                return;
            }

            text.fontSize = maxSize;
            text.resizeTextForBestFit = true;
            text.resizeTextMaxSize = maxSize;
            text.resizeTextMinSize = minSize;
        }
    }
}
