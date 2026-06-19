using UnityEngine;

namespace FateInYourHands.Story
{
    public static class SmmStoryAutoStart
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void StartStoryRuntime()
        {
            if (Object.FindAnyObjectByType<SmmStoryGameController>() != null)
            {
                return;
            }

            var runtime = new GameObject("Fate in Your Hands Runtime");
            Object.DontDestroyOnLoad(runtime);
            runtime.AddComponent<SmmStoryGameController>();
        }
    }
}
