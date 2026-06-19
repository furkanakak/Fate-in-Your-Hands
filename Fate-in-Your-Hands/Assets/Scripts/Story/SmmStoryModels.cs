using System;

namespace FateInYourHands.Story
{
    [Serializable]
    public sealed class SmmStoryPackage
    {
        public string storyId;
        public string title;
        public string language;
        public string initialCardId;
        public string zeroHealthEndingId;
        public string coverImageId;
        public SmmVisibleStat health;
        public SmmCounterDefinition[] hiddenCounters;
        public SmmFlagDefinition[] flags;
        public SmmCardData[] cards;
        public SmmEndingData[] endings;
        public SmmImageAsset[] assets;
        public SmmSmokeRoute[] smokeRoutes;
        public SmmSourceFile[] sourceFiles;
    }

    [Serializable]
    public sealed class SmmVisibleStat
    {
        public string statId;
        public string displayName;
        public int min;
        public int max;
        public int defaultValue;
    }

    [Serializable]
    public sealed class SmmCounterDefinition
    {
        public string counterId;
        public string displayName;
        public string description;
        public int min;
        public int max;
        public int defaultValue;
    }

    [Serializable]
    public sealed class SmmFlagDefinition
    {
        public string flagId;
        public string description;
    }

    [Serializable]
    public sealed class SmmCardData
    {
        public string cardId;
        public string storyId;
        public string chapterId;
        public string stage;
        public string routeFamily;
        public string title;
        public string bodyText;
        public string backgroundId;
        public string focusImageId;
        public string[] presentCharacters;
        public SmmDialogueLine[] dialogue;
        public SmmChoiceData[] choices;
        public string sourceFile;
    }

    [Serializable]
    public sealed class SmmDialogueLine
    {
        public string speakerId;
        public string emotion;
        public string text;
    }

    [Serializable]
    public sealed class SmmChoiceData
    {
        public string choiceId;
        public string slot;
        public string choiceImageId;
        public string text;
        public string resultText;
        public int healthDelta;
        public SmmCounterDelta[] hiddenCounterDeltas;
        public string[] setFlags;
        public string[] clearFlags;
        public string nextCardId;
        public string endingId;
    }

    [Serializable]
    public sealed class SmmCounterDelta
    {
        public string counterId;
        public int delta;
    }

    [Serializable]
    public sealed class SmmEndingData
    {
        public string endingId;
        public string title;
        public string endingType;
        public string fullEndingText;
        public string endingImageId;
        public string[] requiredFlags;
        public string[] forbiddenFlags;
        public SmmCounterCondition[] requiredHiddenCounters;
        public SmmCounterCondition[] forbiddenHiddenCounters;
        public string typicalRoute;
        public string[] importantPastChoices;
        public string whyThisEndingHappens;
        public string payoffExplanation;
    }

    [Serializable]
    public sealed class SmmCounterCondition
    {
        public string counterId;
        public int min;
        public int max;
    }

    [Serializable]
    public sealed class SmmImageAsset
    {
        public string assetId;
        public string storyId;
        public string type;
        public string targetPath;
        public string format;
        public string declaredSize;
        public int width;
        public int height;
        public bool exists;
    }

    [Serializable]
    public sealed class SmmSmokeRoute
    {
        public string routeId;
        public string label;
        public string targetEndingId;
        public string startCardId;
        public bool reachesTarget;
        public int stepCount;
        public SmmSmokeStep[] steps;
    }

    [Serializable]
    public sealed class SmmSmokeStep
    {
        public string cardId;
        public string choiceId;
        public string slot;
        public string choiceText;
    }

    [Serializable]
    public sealed class SmmSourceFile
    {
        public string fileName;
        public int byteCount;
        public string sha256;
    }

    [Serializable]
    public sealed class SmmStorySaveData
    {
        public string storyId;
        public string currentCardId;
        public string currentEndingId;
        public int health;
        public SmmCounterValue[] hiddenCounters;
        public string[] flags;
    }

    [Serializable]
    public sealed class SmmCounterValue
    {
        public string counterId;
        public int value;
    }
}
