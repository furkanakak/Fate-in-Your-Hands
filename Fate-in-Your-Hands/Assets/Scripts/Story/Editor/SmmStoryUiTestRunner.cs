using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace FateInYourHands.Story.Editor
{
    public static class SmmStoryUiTestRunner
    {
        private const string OutputDirectory = @"C:\Users\furkan\Desktop\Saray_UI_Redesign_Test";
        private const string ReportPath = OutputDirectory + @"\ui_card_flip_validation.json";
        private const string ScenePath = "Assets/Scenes/SampleScene.unity";
        private const string FirstCardId = "smm_card_001";

        private static readonly List<string> Checks = new List<string>();
        private static readonly List<string> Failures = new List<string>();
        private static readonly List<string> Screenshots = new List<string>();
        private static readonly Stack<IEnumerator> RoutineStack = new Stack<IEnumerator>();
        private static bool previousEnterPlayModeOptionsEnabled;
        private static EnterPlayModeOptions previousEnterPlayModeOptions;
        private static bool capturedEditorSettings;

        private struct CanvasState
        {
            public Canvas canvas;
            public RenderMode renderMode;
            public Camera worldCamera;
            public float planeDistance;
        }

        public static void Run()
        {
            Directory.CreateDirectory(OutputDirectory);
            Checks.Clear();
            Failures.Clear();
            Screenshots.Clear();

            try
            {
                EditorSceneManager.OpenScene(ScenePath);
                CaptureAndApplyPlayModeSettings();
                RoutineStack.Clear();
                RoutineStack.Push(RunRoutine());
                EditorApplication.update -= Pump;
                EditorApplication.update += Pump;
                Debug.Log("SmmStoryUiTestRunner started.");
            }
            catch (Exception exception)
            {
                Failures.Add(exception.ToString());
                Finish();
            }
        }

        private static IEnumerator RunRoutine()
        {
            PlayerPrefs.DeleteKey(SmmStoryGameController.AutosaveKey);
            PlayerPrefs.Save();

            Debug.Log("SmmStoryUiTestRunner entering Play Mode.");
            EditorApplication.isPlaying = true;
            while (!EditorApplication.isPlaying)
            {
                yield return null;
            }

            Debug.Log("SmmStoryUiTestRunner waiting for story controller.");
            yield return WaitFrames(30);

            var controller = UnityEngine.Object.FindAnyObjectByType<SmmStoryGameController>();
            for (var i = 0; controller == null && i < 120; i++)
            {
                yield return null;
                controller = UnityEngine.Object.FindAnyObjectByType<SmmStoryGameController>();
            }

            Require(controller != null, "story controller exists in Play Mode");
            if (controller == null)
            {
                yield break;
            }

            controller.ForceStartNewStoryForTest();
            Debug.Log("SmmStoryUiTestRunner opened first card.");
            yield return WaitFrames(20);
            Require(controller.CurrentCardId == FirstCardId, "first card opens as smm_card_001");
            Require(controller.ChoiceAView != null && controller.ChoiceAView.ArtImage.sprite != null, "choice A sprite is loaded");
            Require(controller.ChoiceBView != null && controller.ChoiceBView.ArtImage.sprite != null, "choice B sprite is loaded");
            Require(VisibleProductionTextIsClean(), "production UI hides technical ids");
            Require(StoryTextFieldsDoNotTruncate(controller), "choice and result text fields do not truncate sentences");
            Require(VisibleChoiceTextIsClean(controller), "choice text is concise, punctuated, and free of meta wording");
            Require(NormalizerFallbacksAreComplete(), "empty or malformed API text receives complete fallback copy");
            Require(!controller.IsModalResultVisibleForTest, "central result modal is not visible on initial screen");
            controller.FocusChoiceForTest(1);
            yield return WaitFrames(8);
            Require(controller.ChoiceBView.IsFocusedForTest, "keyboard/gamepad focus can move to choice B");
            controller.FocusChoiceForTest(0);
            yield return WaitFrames(8);
            Require(controller.ChoiceAView.IsFocusedForTest, "keyboard/gamepad focus can move to choice A");

            yield return CaptureAtResolution("initial_screen_1280x720.png", 1280, 720);
            yield return CaptureAtResolution("initial_screen_1366x768.png", 1366, 768);
            yield return CaptureAtResolution("initial_screen_1920x1080.png", 1920, 1080);

            controller.ChoiceAView.SetHoverPreview(true);
            yield return WaitFrames(18);
            yield return Capture("choice_a_hover_1920x1080.png", 1920, 1080);
            controller.ChoiceAView.SetHoverPreview(false);
            yield return WaitFrames(10);

            controller.ChoiceBView.SetHoverPreview(true);
            yield return WaitFrames(18);
            yield return Capture("choice_b_hover_1920x1080.png", 1920, 1080);
            controller.ChoiceBView.SetHoverPreview(false);
            yield return WaitFrames(10);

            controller.ForceStartNewStoryForTest();
            yield return WaitFrames(12);
            Debug.Log("SmmStoryUiTestRunner selecting choice A.");
            var beforeDoubleClickCount = controller.SelectionTriggerCountForTest;
            controller.TriggerChoiceForTest(0);
            controller.TriggerChoiceForTest(0);
            yield return WaitFrames(4);
            Require(controller.SelectionTriggerCountForTest == beforeDoubleClickCount + 1, "double-click does not trigger the same choice twice");
            yield return WaitUntil(() => controller.IsChoiceFlipAnimatingForTest, 30);
            Require(controller.IsChoiceFlipAnimatingForTest, "choice A starts card flip animation");
            yield return WaitFrames(12);
            yield return Capture("card_mid_flip_1920x1080.png", 1920, 1080);
            yield return WaitUntil(() => controller.IsResultOpen, 120);
            Require(controller.IsResultOpen, "choice A shows result on card back face");
            Require(!controller.IsModalResultVisibleForTest, "central result modal stays hidden after choice A");
            Require(controller.CurrentResultTextForTest.Contains("Mutfaktaki bakışlar yumuşar"), "back face shows choice A resultText");
            Require(ResultTextIsClean(controller.CurrentResultTextForTest, "Lekeyi üstlenip gerçeği söyle."), "choice A result is concise and does not repeat the choice");
            yield return Capture("result_back_face_1920x1080.png", 1920, 1080);
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_002", 120);
            Require(controller.CurrentCardId == "smm_card_002", "choice A continue routes to smm_card_002");
            Require(controller.GetCounterValueForTest("kitchen_favor") == 1, "choice A hidden counter delta is applied after continue");
            Require(controller.HasFlagForTest("flag_truth_told"), "choice A setFlags are applied after continue");

            controller.ForceStartNewStoryForTest();
            yield return WaitFrames(12);
            Debug.Log("SmmStoryUiTestRunner selecting choice B.");
            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 120);
            Require(controller.IsResultOpen, "choice B shows result on card back face");
            Require(!controller.IsModalResultVisibleForTest, "central result modal stays hidden after choice B");
            Require(controller.CurrentResultTextForTest.Contains("Sarayın bakışları keskinleşir"), "choice B shows its own failure resultText");
            Require(ResultTextIsClean(controller.CurrentResultTextForTest, "Suçu kıdemsiz çırağa yönelt."), "choice B result is concise and does not repeat the choice");
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_003", 120);
            Require(controller.CurrentCardId == "smm_card_003", "choice B continue routes to smm_card_003");
            Require(controller.GetCounterValueForTest("palace_suspicion") == 2, "choice B hidden counter delta is applied after continue");

            controller.ForceStartNewStoryForTest();
            yield return CaptureAtResolution("mobile_layout_390x844.png", 390, 844);
            Require(ChoiceCardsAreStackedForPortrait(controller), "mobile layout stacks choice cards vertically");
            Debug.Log("SmmStoryUiTestRunner finished routine.");
        }

        private static bool VisibleProductionTextIsClean()
        {
            var forbidden = new[] { "smm_card_", "choice_smm_", "bg_", "Current card:", "Background:" };
            var texts = UnityEngine.Object.FindObjectsByType<Text>(FindObjectsInactive.Exclude);
            return texts.All(text => forbidden.All(token => !text.text.Contains(token)));
        }

        private static bool StoryTextFieldsDoNotTruncate(SmmStoryGameController controller)
        {
            return controller?.ChoiceAView?.LabelText != null
                && controller.ChoiceBView?.LabelText != null
                && controller.ChoiceAView.ResultText != null
                && controller.ChoiceBView.ResultText != null
                && controller.ChoiceAView.LabelText.verticalOverflow == VerticalWrapMode.Overflow
                && controller.ChoiceBView.LabelText.verticalOverflow == VerticalWrapMode.Overflow
                && controller.ChoiceAView.ResultText.verticalOverflow == VerticalWrapMode.Overflow
                && controller.ChoiceBView.ResultText.verticalOverflow == VerticalWrapMode.Overflow;
        }

        private static bool VisibleChoiceTextIsClean(SmmStoryGameController controller)
        {
            if (controller?.ChoiceAView?.LabelText == null || controller.ChoiceBView?.LabelText == null)
            {
                return false;
            }

            var values = new[] { controller.ChoiceAView.LabelText.text, controller.ChoiceBView.LabelText.text };
            return values.All(value =>
                !string.IsNullOrWhiteSpace(value)
                && HasSentenceEnd(value)
                && WordCount(value) <= 8
                && !SmmStoryTextNormalizer.ContainsForbiddenMeta(value)
                && !value.Contains("..."));
        }

        private static bool ResultTextIsClean(string resultText, string choiceText)
        {
            return !string.IsNullOrWhiteSpace(resultText)
                && HasSentenceEnd(resultText)
                && WordCount(resultText) <= 18
                && !SmmStoryTextNormalizer.ContainsForbiddenMeta(resultText)
                && !NormalizeForComparison(resultText).Contains(NormalizeForComparison(choiceText));
        }

        private static bool HasSentenceEnd(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                && (value.EndsWith(".") || value.EndsWith("!") || value.EndsWith("?"));
        }

        private static bool NormalizerFallbacksAreComplete()
        {
            var empty = SmmStoryTextNormalizer.NormalizeResultText(string.Empty, "Kapıyı sessizce aç.", 0);
            var meta = SmmStoryTextNormalizer.NormalizeResultText("Şu yolu seçersin: kapıyı açmak.", "Kapıyı sessizce aç.", -1);
            var badChoice = SmmStoryTextNormalizer.NormalizeChoiceText("Adama tekme...");
            return ResultTextIsClean(empty, "Kapıyı sessizce aç.")
                && ResultTextIsClean(meta, "Kapıyı sessizce aç.")
                && badChoice == "Sakin kalıp ilerle.";
        }

        private static int WordCount(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? 0
                : value.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private static string NormalizeForComparison(string value)
        {
            var builder = new StringBuilder();
            foreach (var character in (value ?? string.Empty).ToLowerInvariant())
            {
                builder.Append(char.IsLetterOrDigit(character) ? character : ' ');
            }

            return string.Join(" ", builder.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private static IEnumerator CaptureAtResolution(string fileName, int width, int height)
        {
            Screen.SetResolution(width, height, false);
            var controller = UnityEngine.Object.FindAnyObjectByType<SmmStoryGameController>();
            controller?.ApplyViewportForTest(width, height);
            Canvas.ForceUpdateCanvases();
            yield return WaitFrames(24);
            yield return Capture(fileName, width, height);
        }

        private static bool ChoiceCardsAreStackedForPortrait(SmmStoryGameController controller)
        {
            if (controller?.ChoiceAView == null || controller.ChoiceBView == null)
            {
                return false;
            }

            Canvas.ForceUpdateCanvases();
            var aCorners = new Vector3[4];
            var bCorners = new Vector3[4];
            controller.ChoiceAView.RectTransform.GetWorldCorners(aCorners);
            controller.ChoiceBView.RectTransform.GetWorldCorners(bCorners);

            var aCenter = (aCorners[0] + aCorners[2]) * 0.5f;
            var bCenter = (bCorners[0] + bCorners[2]) * 0.5f;
            var aWidth = Mathf.Abs(aCorners[2].x - aCorners[0].x);
            var bWidth = Mathf.Abs(bCorners[2].x - bCorners[0].x);
            var aHeight = Mathf.Abs(aCorners[2].y - aCorners[0].y);
            var bHeight = Mathf.Abs(bCorners[2].y - bCorners[0].y);
            var horizontalAlignmentTolerance = Mathf.Max(20f, Mathf.Min(aWidth, bWidth) * 0.25f);
            var verticalSeparationMinimum = Mathf.Min(aHeight, bHeight) * 0.75f;
            return Mathf.Abs(aCenter.x - bCenter.x) <= horizontalAlignmentTolerance
                && Mathf.Abs(aCenter.y - bCenter.y) >= verticalSeparationMinimum;
        }

        private static IEnumerator Capture(string fileName, int expectedWidth, int expectedHeight)
        {
            var path = Path.Combine(OutputDirectory, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            CaptureUiToPng(path, expectedWidth, expectedHeight);
            yield return null;

            Require(File.Exists(path), $"screenshot exists: {fileName}");
            if (!File.Exists(path))
            {
                yield break;
            }

            Screenshots.Add(path);
            var size = ReadPngSize(path);
            Require(size.x == expectedWidth && size.y == expectedHeight, $"screenshot size {fileName} is {expectedWidth}x{expectedHeight}");
            var fileInfo = new FileInfo(path);
            Require(fileInfo.Length > 12000, $"screenshot is non-empty: {fileName}");
        }

        private static void CaptureUiToPng(string path, int width, int height)
        {
            var captureCameraObject = new GameObject("Smm UI Capture Camera");
            var captureCamera = captureCameraObject.AddComponent<Camera>();
            captureCamera.clearFlags = CameraClearFlags.SolidColor;
            captureCamera.backgroundColor = Color.black;
            captureCamera.orthographic = true;
            captureCamera.orthographicSize = 5f;
            captureCamera.nearClipPlane = 0.01f;
            captureCamera.farClipPlane = 100f;
            captureCamera.allowHDR = false;
            captureCamera.allowMSAA = false;
            captureCamera.cullingMask = ~0;
            captureCamera.transform.position = new Vector3(0f, 0f, -10f);

            var renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
            var previousActive = RenderTexture.active;
            var states = UnityEngine.Object.FindObjectsByType<Canvas>(FindObjectsInactive.Exclude)
                .Select(canvas => new CanvasState
                {
                    canvas = canvas,
                    renderMode = canvas.renderMode,
                    worldCamera = canvas.worldCamera,
                    planeDistance = canvas.planeDistance
                })
                .ToArray();

            try
            {
                foreach (var state in states)
                {
                    state.canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    state.canvas.worldCamera = captureCamera;
                    state.canvas.planeDistance = 10f;
                }

                Canvas.ForceUpdateCanvases();
                captureCamera.targetTexture = renderTexture;
                RenderTexture.active = renderTexture;
                captureCamera.Render();

                var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
                texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                texture.Apply(false, false);
                File.WriteAllBytes(path, texture.EncodeToPNG());
                UnityEngine.Object.DestroyImmediate(texture);
            }
            finally
            {
                foreach (var state in states)
                {
                    if (state.canvas == null)
                    {
                        continue;
                    }

                    state.canvas.renderMode = state.renderMode;
                    state.canvas.worldCamera = state.worldCamera;
                    state.canvas.planeDistance = state.planeDistance;
                }

                captureCamera.targetTexture = null;
                RenderTexture.active = previousActive;
                UnityEngine.Object.DestroyImmediate(renderTexture);
                UnityEngine.Object.DestroyImmediate(captureCameraObject);
                Canvas.ForceUpdateCanvases();
            }
        }

        private static Vector2Int ReadPngSize(string path)
        {
            var bytes = File.ReadAllBytes(path);
            if (bytes.Length < 24)
            {
                return Vector2Int.zero;
            }

            var width = ReadBigEndianInt(bytes, 16);
            var height = ReadBigEndianInt(bytes, 20);
            return new Vector2Int(width, height);
        }

        private static int ReadBigEndianInt(byte[] bytes, int offset)
        {
            return (bytes[offset] << 24) | (bytes[offset + 1] << 16) | (bytes[offset + 2] << 8) | bytes[offset + 3];
        }

        private static IEnumerator WaitFrames(int frameCount)
        {
            for (var i = 0; i < frameCount; i++)
            {
                yield return null;
            }
        }

        private static IEnumerator WaitUntil(Func<bool> condition, int maxFrames)
        {
            for (var i = 0; i < maxFrames && !condition(); i++)
            {
                yield return null;
            }
        }

        private static void Require(bool condition, string check)
        {
            Checks.Add((condition ? "PASS: " : "FAIL: ") + check);
            if (!condition)
            {
                Failures.Add(check);
            }
        }

        private static void Pump()
        {
            try
            {
                while (RoutineStack.Count > 0)
                {
                    var currentRoutine = RoutineStack.Peek();
                    if (!currentRoutine.MoveNext())
                    {
                        RoutineStack.Pop();
                        continue;
                    }

                    if (currentRoutine.Current is IEnumerator nestedRoutine)
                    {
                        RoutineStack.Push(nestedRoutine);
                        continue;
                    }

                    return;
                }
            }
            catch (Exception exception)
            {
                Failures.Add(exception.ToString());
            }

            Finish();
        }

        private static void Finish()
        {
            EditorApplication.update -= Pump;

            WriteReport();

            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            RestorePlayModeSettings();
            EditorApplication.Exit(Failures.Count == 0 ? 0 : 1);
        }

        private static void CaptureAndApplyPlayModeSettings()
        {
            if (capturedEditorSettings)
            {
                return;
            }

            previousEnterPlayModeOptionsEnabled = EditorSettings.enterPlayModeOptionsEnabled;
            previousEnterPlayModeOptions = EditorSettings.enterPlayModeOptions;
            capturedEditorSettings = true;

            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
        }

        private static void RestorePlayModeSettings()
        {
            if (!capturedEditorSettings)
            {
                return;
            }

            EditorSettings.enterPlayModeOptionsEnabled = previousEnterPlayModeOptionsEnabled;
            EditorSettings.enterPlayModeOptions = previousEnterPlayModeOptions;
            capturedEditorSettings = false;
        }

        private static void WriteReport()
        {
            var builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine($"  \"status\": \"{(Failures.Count == 0 ? "PASS" : "FAIL")}\",");
            builder.AppendLine("  \"checks\": [");
            AppendJsonArray(builder, Checks, "  ");
            builder.AppendLine("  ],");
            builder.AppendLine("  \"screenshots\": [");
            AppendJsonArray(builder, Screenshots, "  ");
            builder.AppendLine("  ],");
            builder.AppendLine("  \"failures\": [");
            AppendJsonArray(builder, Failures, "  ");
            builder.AppendLine("  ]");
            builder.AppendLine("}");
            File.WriteAllText(ReportPath, builder.ToString(), Encoding.UTF8);
        }

        private static void AppendJsonArray(StringBuilder builder, IReadOnlyList<string> values, string indent)
        {
            for (var i = 0; i < values.Count; i++)
            {
                var comma = i + 1 < values.Count ? "," : string.Empty;
                builder.Append(indent);
                builder.Append("  \"");
                builder.Append(EscapeJson(values[i]));
                builder.Append('"');
                builder.AppendLine(comma);
            }
        }

        private static string EscapeJson(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }
    }
}
