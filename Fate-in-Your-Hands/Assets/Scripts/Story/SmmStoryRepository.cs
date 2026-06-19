using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FateInYourHands.Story
{
    public sealed class SmmStoryRepository
    {
        public const string StoryResourcePath = "Stories/saray_mutfagindan_muhre/story_data";
        public const string StoryFilePath = "Assets/Stories/saray_mutfagindan_muhre/story_data.json";

        private readonly Dictionary<string, SmmCardData> cardsById = new Dictionary<string, SmmCardData>();
        private readonly Dictionary<string, SmmChoiceData> choicesById = new Dictionary<string, SmmChoiceData>();
        private readonly Dictionary<string, SmmEndingData> endingsById = new Dictionary<string, SmmEndingData>();
        private readonly Dictionary<string, SmmImageAsset> assetsById = new Dictionary<string, SmmImageAsset>();
        private readonly Dictionary<string, SmmCounterDefinition> countersById = new Dictionary<string, SmmCounterDefinition>();

        public SmmStoryPackage Package { get; private set; }

        public IEnumerable<SmmCounterDefinition> CounterDefinitions => countersById.Values;

        public static SmmStoryRepository LoadDefault()
        {
            var json = LoadStoryJson();
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException("Saray Mutfağından Mühre story_data.json could not be loaded.");
            }

            var package = JsonUtility.FromJson<SmmStoryPackage>(json);
            if (package == null || string.IsNullOrWhiteSpace(package.storyId))
            {
                throw new InvalidOperationException("Saray Mutfağından Mühre story_data.json is invalid.");
            }

            SmmStoryTextNormalizer.NormalizePackage(package);

            var repository = new SmmStoryRepository { Package = package };
            repository.BuildLookups();
            return repository;
        }

        public SmmCardData GetCard(string cardId)
        {
            return !string.IsNullOrWhiteSpace(cardId) && cardsById.TryGetValue(cardId, out var card)
                ? card
                : null;
        }

        public SmmChoiceData GetChoice(string choiceId)
        {
            return !string.IsNullOrWhiteSpace(choiceId) && choicesById.TryGetValue(choiceId, out var choice)
                ? choice
                : null;
        }

        public SmmEndingData GetEnding(string endingId)
        {
            return !string.IsNullOrWhiteSpace(endingId) && endingsById.TryGetValue(endingId, out var ending)
                ? ending
                : null;
        }

        public SmmImageAsset GetAsset(string assetId)
        {
            return !string.IsNullOrWhiteSpace(assetId) && assetsById.TryGetValue(assetId, out var asset)
                ? asset
                : null;
        }

        public SmmCounterDefinition GetCounterDefinition(string counterId)
        {
            return !string.IsNullOrWhiteSpace(counterId) && countersById.TryGetValue(counterId, out var definition)
                ? definition
                : null;
        }

        public int ClampHealth(int value)
        {
            var health = Package.health;
            return Mathf.Clamp(value, health != null ? health.min : 0, health != null ? health.max : 10);
        }

        public int ClampCounter(string counterId, int value)
        {
            var definition = GetCounterDefinition(counterId);
            return definition == null ? value : Mathf.Clamp(value, definition.min, definition.max);
        }

        public string ResolveUnityAssetPath(string targetPath)
        {
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                return string.Empty;
            }

            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrWhiteSpace(projectRoot))
            {
                return string.Empty;
            }

            return Path.Combine(projectRoot, targetPath.Replace('/', Path.DirectorySeparatorChar));
        }

        private static string LoadStoryJson()
        {
            var textAsset = Resources.Load<TextAsset>(StoryResourcePath);
            if (textAsset != null)
            {
                return textAsset.text;
            }

            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrWhiteSpace(projectRoot))
            {
                return string.Empty;
            }

            var filePath = Path.Combine(projectRoot, StoryFilePath.Replace('/', Path.DirectorySeparatorChar));
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        private void BuildLookups()
        {
            cardsById.Clear();
            choicesById.Clear();
            endingsById.Clear();
            assetsById.Clear();
            countersById.Clear();

            foreach (var card in Package.cards ?? Array.Empty<SmmCardData>())
            {
                cardsById[card.cardId] = card;
                foreach (var choice in card.choices ?? Array.Empty<SmmChoiceData>())
                {
                    choicesById[choice.choiceId] = choice;
                }
            }

            foreach (var ending in Package.endings ?? Array.Empty<SmmEndingData>())
            {
                endingsById[ending.endingId] = ending;
            }

            foreach (var asset in Package.assets ?? Array.Empty<SmmImageAsset>())
            {
                assetsById[asset.assetId] = asset;
            }

            foreach (var counter in Package.hiddenCounters ?? Array.Empty<SmmCounterDefinition>())
            {
                countersById[counter.counterId] = counter;
            }
        }
    }
}
