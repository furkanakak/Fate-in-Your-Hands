using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FateInYourHands.Story
{
    public static class SmmStoryTextNormalizer
    {
        private const int MaxChoiceWords = 8;
        private const int MaxResultWords = 18;
        private const string ChoiceFallback = "Sakin kalıp ilerle.";

        private static readonly string[] ForbiddenMetaPatterns =
        {
            "şu yolu seçersin",
            "bu yolu seçersin",
            "yolunu seçersin",
            "bu seçimi yaparsan",
            "seçimin sonucunda",
            "bu hamle",
            "karar, hikâyeyi"
        };

        public static void NormalizePackage(SmmStoryPackage package)
        {
            if (package == null)
            {
                return;
            }

            foreach (var card in package.cards ?? Array.Empty<SmmCardData>())
            {
                foreach (var choice in card.choices ?? Array.Empty<SmmChoiceData>())
                {
                    if (choice == null)
                    {
                        continue;
                    }

                    choice.text = NormalizeChoiceText(choice.text);
                    choice.resultText = NormalizeResultText(choice.resultText, choice.text, choice.healthDelta);
                }

                EnsureDistinctChoiceResults(card.choices);
            }
        }

        public static string NormalizeChoiceText(string rawText)
        {
            var text = NormalizeWhitespace(rawText);
            if (NeedsChoiceFallback(text))
            {
                return ChoiceFallback;
            }

            return EnsureSentenceEnd(text);
        }

        public static string NormalizeResultText(string rawText, string choiceText, int healthDelta)
        {
            var text = NormalizeWhitespace(rawText);
            if (NeedsResultFallback(text, choiceText))
            {
                return FallbackResultText(healthDelta);
            }

            return EnsureSentenceEnd(text);
        }

        public static bool ContainsForbiddenMeta(string text)
        {
            var normalized = NormalizeWhitespace(text);
            return ForbiddenMetaPatterns.Any(pattern =>
                normalized.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static bool NeedsChoiceFallback(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                || ContainsForbiddenMeta(text)
                || ContainsManualEllipsis(text)
                || CountWords(text) > MaxChoiceWords;
        }

        private static bool NeedsResultFallback(string text, string choiceText)
        {
            return string.IsNullOrWhiteSpace(text)
                || ContainsForbiddenMeta(text)
                || ContainsManualEllipsis(text)
                || CountWords(text) > MaxResultWords
                || CountSentences(text) > 2
                || RepeatsChoice(text, choiceText);
        }

        private static string NormalizeWhitespace(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : Regex.Replace(value.Trim(), @"\s+", " ");
        }

        private static string EnsureSentenceEnd(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var text = value.Trim();
            var last = text[text.Length - 1];
            return last == '.' || last == '!' || last == '?' ? text : $"{text}.";
        }

        private static bool ContainsManualEllipsis(string value)
        {
            return !string.IsNullOrEmpty(value) && (value.Contains("...") || value.Contains("…"));
        }

        private static int CountWords(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? 0
                : Regex.Matches(value.Trim(), @"\S+").Count;
        }

        private static int CountSentences(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? 0
                : value.Count(character => character == '.' || character == '!' || character == '?');
        }

        private static bool RepeatsChoice(string resultText, string choiceText)
        {
            var result = NormalizeForComparison(resultText);
            var choice = NormalizeForComparison(choiceText);
            return choice.Length > 10 && result.Contains(choice);
        }

        private static void EnsureDistinctChoiceResults(SmmChoiceData[] choices)
        {
            if (choices == null || choices.Length != 2 || choices[0] == null || choices[1] == null)
            {
                return;
            }

            if (choices[0].resultText != choices[1].resultText)
            {
                return;
            }

            choices[1].resultText = NormalizeResultText(CreateDistinctResultText(choices[1]), choices[1].text, choices[1].healthDelta);
            if (choices[0].resultText == choices[1].resultText)
            {
                choices[1].resultText = AlternateFallbackResultText(choices[1].healthDelta);
            }
        }

        private static string CreateDistinctResultText(SmmChoiceData choice)
        {
            if (choice == null)
            {
                return FallbackResultText(0);
            }

            if (choice.healthDelta < 0)
            {
                return FallbackResultText(choice.healthDelta);
            }

            var dominantDelta = (choice.hiddenCounterDeltas ?? Array.Empty<SmmCounterDelta>())
                .Where(delta => delta != null && delta.delta != 0)
                .OrderByDescending(delta => IsRiskCounter(delta.counterId) ? 100 + Math.Abs(delta.delta) : Math.Abs(delta.delta))
                .FirstOrDefault();

            if (dominantDelta != null && CounterResultTexts.TryGetValue(dominantDelta.counterId, out var variants))
            {
                if (dominantDelta.delta > 0 && variants.Positive != null)
                {
                    return variants.Positive;
                }

                if (dominantDelta.delta < 0 && variants.Negative != null)
                {
                    return variants.Negative;
                }
            }

            return FallbackResultText(choice.healthDelta);
        }

        private static bool IsRiskCounter(string counterId)
        {
            return counterId == "palace_suspicion" || counterId == "black_market_debt";
        }

        private static string NormalizeForComparison(string value)
        {
            return Regex.Replace(NormalizeWhitespace(value).ToLowerInvariant(), @"[^\p{L}\p{N}]+", " ").Trim();
        }

        private static string FallbackResultText(int healthDelta)
        {
            if (healthDelta > 0)
            {
                return "Mutfaktaki bakışlar yumuşadı ve önün açıldı.";
            }

            if (healthDelta < 0)
            {
                return "Sarayın bakışları keskinleşti ve adın deftere düştü.";
            }

            return "Koridordaki hava değişti ve yeni bir yol açıldı.";
        }

        private static string AlternateFallbackResultText(int healthDelta)
        {
            if (healthDelta > 0)
            {
                return "Sessiz destek büyüdü ve kapılar sana aralandı.";
            }

            if (healthDelta < 0)
            {
                return "Koridordaki fısıltı sertleşti ve izlerin görünür oldu.";
            }

            return "Odada sessizlik çözüldü ve yolun değişti.";
        }

        private static readonly Dictionary<string, CounterResultVariant> CounterResultTexts =
            new Dictionary<string, CounterResultVariant>
            {
                { "palace_suspicion", new CounterResultVariant("Koridordaki kuşku büyüdü ve adın daha sık anıldı.", "Sarayın kuşkusu yatıştı ve bakışlar senden uzaklaştı.") },
                { "black_market_debt", new CounterResultVariant("Pazar borcu büyüdü ve gölgeler peşine düştü.", "Pazar borcu hafifledi ve gölgeler geride kaldı.") },
                { "kitchen_favor", new CounterResultVariant("Mutfaktaki güven büyüdü ve kapılar sana aralandı.", null) },
                { "absurd_soup_omen_score", new CounterResultVariant("Çorbadaki işaret yayıldı; kaşıklar havada kaldı.", null) },
                { "absurd_goose_omen_score", new CounterResultVariant("Ak Gaga bağırdı; avludaki söylenti büyüdü.", null) },
                { "absurd_dessert_diplomacy_score", new CounterResultVariant("Tatlı tabağı el değiştirdi; salondaki denge bozuldu.", null) },
                { "poison_knowledge", new CounterResultVariant("Zehir kokusu netleşti ve tehlikeyi daha iyi okudun.", null) },
                { "healer_trust", new CounterResultVariant("Şifahanedeki güven güçlendi ve kapılar aralandı.", null) },
                { "spy_network", new CounterResultVariant("Fısıltı ağı genişledi ve haberler sana aktı.", null) },
                { "logistics_mastery", new CounterResultVariant("Defterler düzene girdi ve yolun hesabı güçlendi.", null) },
                { "rebellion_sympathy", new CounterResultVariant("Kalabalık adını yumuşak andı ve kapı açıldı.", null) },
                { "seal_access", new CounterResultVariant("Mühür odasına giden iz biraz daha açıldı.", null) },
                { "heir_arslan_support", new CounterResultVariant("Arslan'ın bakışı yumuşadı ve askerler geri çekildi.", null) },
                { "heir_kemal_support", new CounterResultVariant("Kemal notunu sakladı ve halk kapısı aralandı.", null) },
                { "heir_safira_support", new CounterResultVariant("Safira sessizce gülümsedi ve elçiler yol verdi.", null) },
            };

        private sealed class CounterResultVariant
        {
            public CounterResultVariant(string positive, string negative)
            {
                Positive = positive;
                Negative = negative;
            }

            public string Positive { get; }
            public string Negative { get; }
        }
    }
}
