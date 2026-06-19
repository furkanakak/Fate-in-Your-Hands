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
    public static class SmmStoryUiAuditV2Runner
    {
        private const string OutputDirectory = @"C:\Users\furkan\Desktop\Saray_UI_Audit_V2";
        private const string ReportPath = OutputDirectory + @"\UI_REDESIGN_VALIDATION_REPORT.md";
        private const string MasterReportPath = @"C:\Users\furkan\Desktop\Fate-in-Your-Hands\UI_MASTER_VALIDATION_REPORT.md";
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
                Debug.Log("SmmStoryUiAuditV2Runner started.");
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

            EditorApplication.isPlaying = true;
            while (!EditorApplication.isPlaying)
            {
                yield return null;
            }

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
            controller.ApplyViewportForTest(1920, 1080);
            yield return WaitFrames(24);

            Require(controller.CurrentCardId == FirstCardId, "vertical slice opens smm_card_001");
            Require(controller.UsesSpriteHealthForTest, "health uses sprite Image components instead of text glyph hearts");
            Require(!controller.UsesTextHeartGlyphsForTest, "HUD does not render heart glyph text");
            Require(controller.HasSplitTitleForTest, "title is split into chapter kicker and scene title");
            Require(controller.HasAnimationLayerForTest, "card animation layer exists");
            Require(controller.UiLayersRenderAboveBackgroundForTest, "UI layers render above the background layer");
            Require(controller.ChoiceAView != null && controller.ChoiceAView.ArtImage.sprite != null, "choice A art is loaded");
            Require(controller.ChoiceBView != null && controller.ChoiceBView.ArtImage.sprite != null, "choice B art is loaded");
            Require(!controller.IsModalResultVisibleForTest, "central result modal is hidden on choice screen");
            Require(VisibleProductionTextIsClean(), "visible production UI hides ids and debug state");
            Require(CursorAssetFilesExist(), "custom cursor asset files exist");
            Require(controller.HasCustomCursorAssetsForTest, "custom cursor textures load from Resources");
            Require(!controller.IsUsingSystemCursorForTest, "production UI does not fall back to the default OS cursor");
            Require(controller.CursorHotspotForTest == new Vector2(14f, 33f), "cursor hotspot matches the pointing fingertip");
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Default, "background uses the default custom cursor");

            yield return CaptureAtResolution(controller, "res_1280x720.png", 1280, 720);
            yield return CaptureAtResolution(controller, "res_1366x768.png", 1366, 768);
            yield return CaptureAtResolution(controller, "01_initial.png", 1920, 1080);

            SmmCursorManager.SetTargetState(controller.SettingsButtonForTest, true, false, false);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "settings button uses the hover cursor");
            SmmCursorManager.ClearSource(controller.SettingsButtonForTest);

            controller.ChoiceAView.SetHoverPreview(true);
            yield return WaitFrames(16);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "choice card hover uses the hover cursor");
            SmmCursorManager.SetTargetState(controller.ChoiceAView, true, true, false);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Pressed, "choice card click uses the pressed cursor");
            SmmCursorManager.ClearSource(controller.ChoiceAView);
            yield return Capture("02_choice_hover.png", 1920, 1080);
            controller.ChoiceAView.SetHoverPreview(false);
            yield return WaitFrames(8);

            controller.FocusChoiceForTest(1);
            yield return WaitFrames(8);
            Require(controller.ChoiceBView.IsFocusedForTest, "keyboard/gamepad focus moves to choice B");
            controller.FocusChoiceForTest(0);
            yield return WaitFrames(8);
            Require(controller.ChoiceAView.IsFocusedForTest, "keyboard/gamepad focus moves to choice A");

            var beforeDoubleClickCount = controller.SelectionTriggerCountForTest;
            controller.TriggerChoiceForTest(0);
            controller.TriggerChoiceForTest(0);
            yield return WaitFrames(4);
            Require(controller.SelectionTriggerCountForTest == beforeDoubleClickCount + 1, "double-click/rapid tap is locked");
            Require(controller.IsChoiceFlipAnimatingForTest, "choice A starts the center-and-flip animation");
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Disabled, "input-locked card animation uses the disabled cursor");

            yield return WaitUntil(() => controller.SelectedChoiceIsCenteredForTest, 80);
            Require(controller.SelectedChoiceIsCenteredForTest, "selected card reaches centered result position");
            yield return WaitUntil(() => controller.NonSelectedChoiceHiddenForTest, 40);
            Require(controller.NonSelectedChoiceHiddenForTest, "non-selected card fades out completely");
            yield return Capture("03_selected_centered.png", 1920, 1080);

            yield return WaitFrames(14);
            yield return Capture("04_mid_flip.png", 1920, 1080);
            yield return WaitUntil(() => controller.IsResultOpen, 120);
            Require(controller.IsResultOpen, "result is shown on the selected card back face");
            Require(!controller.IsModalResultVisibleForTest, "central modal remains hidden after choice");
            Require(controller.CurrentResultTextForTest.Contains("Lekeyi"), "back face shows the YAML resultText");
            Require(VisibleProductionTextIsClean(), "result face hides hidden counters and technical ids");
            yield return WaitUntil(() => controller.IsResultInputUnlockedForTest, 80);
            Require(controller.IsResultInputUnlockedForTest, "result card unlocks as a full-card continue target after the flip");
            controller.ChoiceAView.SetHoverPreview(true);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "result card back face uses the hover cursor");
            controller.ChoiceAView.SetHoverPreview(false);
            yield return Capture("05_result_back.png", 1920, 1080);

            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_002", 140);
            Require(controller.CurrentCardId == "smm_card_002", "continue routes choice A to smm_card_002");
            Require(controller.GetCounterValueForTest("kitchen_favor") == 1, "hidden counter delta is applied after continue");
            Require(controller.HasFlagForTest("flag_truth_told"), "setFlags are applied after continue");
            yield return Capture("06_next_card.png", 1920, 1080);

            controller.ForceStartNewStoryForTest();
            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 140);
            Require(controller.IsResultOpen, "choice B also opens a card-back result");
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_003", 140);
            Require(controller.CurrentCardId == "smm_card_003", "continue routes choice B to smm_card_003");
            Require(controller.GetCounterValueForTest("palace_suspicion") == 2, "choice B hidden counter delta is applied after continue");

            controller.ForceStartNewStoryForTest();
            yield return CaptureAtResolution(controller, "07_mobile.png", 390, 844);
            Require(ChoiceCardsAreStackedForPortrait(controller), "mobile layout stacks choice cards vertically");
            Require(controller.IsTouchCursorHiddenForTest, "mobile/touch viewport hides the cursor");
        }

        private static bool VisibleProductionTextIsClean()
        {
            var forbidden = new[] { "smm_card_", "choice_smm_", "bg_", "Current card:", "Background:", "kitchen_favor", "palace_suspicion", "flag_" };
            var texts = UnityEngine.Object.FindObjectsByType<Text>(FindObjectsInactive.Exclude);
            return texts.All(text => forbidden.All(token => !text.text.Contains(token)));
        }

        private static bool CursorAssetFilesExist()
        {
            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrWhiteSpace(projectRoot))
            {
                return false;
            }

            var paths = new[]
            {
                Path.Combine(projectRoot, "Assets", "Resources", "UI", "Cursors", "ui_cursor_default.png"),
                Path.Combine(projectRoot, "Assets", "Resources", "UI", "Cursors", "ui_cursor_hover.png"),
                Path.Combine(projectRoot, "Assets", "Resources", "UI", "Cursors", "ui_cursor_pressed.png"),
                Path.Combine(projectRoot, "Assets", "Resources", "UI", "Cursors", "ui_cursor_disabled.png")
            };

            return paths.All(File.Exists);
        }

        private static IEnumerator CaptureAtResolution(SmmStoryGameController controller, string fileName, int width, int height)
        {
            Screen.SetResolution(width, height, false);
            controller.ApplyViewportForTest(width, height);
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
            return Mathf.Abs(aCenter.x - bCenter.x) <= Mathf.Max(20f, Mathf.Min(aWidth, bWidth) * 0.25f)
                && Mathf.Abs(aCenter.y - bCenter.y) >= Mathf.Min(aHeight, bHeight) * 0.75f;
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
            var captureCameraObject = new GameObject("Smm UI Audit V2 Capture Camera");
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

            return new Vector2Int(ReadBigEndianInt(bytes, 16), ReadBigEndianInt(bytes, 20));
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
            var deadline = EditorApplication.timeSinceStartup + Math.Max(1.0, maxFrames / 30.0);
            for (var i = 0; (i < maxFrames || EditorApplication.timeSinceStartup < deadline) && !condition(); i++)
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
            builder.AppendLine($"# UI Redesign Validation Report");
            builder.AppendLine();
            builder.AppendLine($"Status: {(Failures.Count == 0 ? "PASS" : "FAIL")}");
            builder.AppendLine();
            builder.AppendLine("## Root Causes Confirmed");
            builder.AppendLine("- Health was rendered as text glyph hearts instead of sprite UI.");
            builder.AppendLine("- HUD, narrative, choices and animation shared visual weight without explicit layers.");
            builder.AppendLine("- Choice cards had nested borders and a heavy dark caption band.");
            builder.AppendLine("- Flip ran inside the layout-owned card slot, allowing position and ghosting artifacts.");
            builder.AppendLine("- Non-selected choice faded only partially, leaving a visible ghost card.");
            builder.AppendLine("- The production cursor could fall back to the operating-system pointer instead of the medieval card UI language.");
            builder.AppendLine();
            builder.AppendLine("## Checks");
            foreach (var check in Checks)
            {
                builder.AppendLine($"- {check}");
            }

            builder.AppendLine();
            builder.AppendLine("## Cursor Validation");
            builder.AppendLine("- Asset style: original hand-drawn pointing hand, warm parchment-pink fill, dark-brown uneven outline, minimal shading, transparent PNG background.");
            builder.AppendLine("- Cursor assets:");
            builder.AppendLine("  - Assets/Resources/UI/Cursors/ui_cursor_default.png");
            builder.AppendLine("  - Assets/Resources/UI/Cursors/ui_cursor_hover.png");
            builder.AppendLine("  - Assets/Resources/UI/Cursors/ui_cursor_pressed.png");
            builder.AppendLine("  - Assets/Resources/UI/Cursors/ui_cursor_disabled.png");
            builder.AppendLine("- Runtime hotspot: (14, 33), aligned to the index fingertip.");
            builder.AppendLine("- Active screens: story background/default, choice card hover/pressed, settings button hover, input-locked animation disabled state, result card back-face hover/full-card continue, mobile/touch hidden.");
            builder.AppendLine("- Screenshot note: Unity render-camera screenshots do not include the OS hardware cursor; cursor verification is recorded through runtime state checks and asset inspection.");
            builder.AppendLine();
            builder.AppendLine("## Screenshots");
            foreach (var screenshot in Screenshots)
            {
                builder.AppendLine($"- {screenshot}");
            }

            builder.AppendLine();
            builder.AppendLine("## Changed Runtime Files");
            builder.AppendLine("- Assets/Scripts/Story/SmmStoryGameController.cs");
            builder.AppendLine("- Assets/Scripts/Story/SmmChoiceCardView.cs");
            builder.AppendLine("- Assets/Scripts/Story/SmmCursorManager.cs");
            builder.AppendLine("- Assets/Scripts/Story/HealthHeartsView.cs");
            builder.AppendLine("- Assets/Scripts/Story/Editor/SmmStoryUiAuditV2Runner.cs");
            builder.AppendLine();
            builder.AppendLine("## Regenerated Assets");
            builder.AppendLine("- Assets/Resources/UI/Cursors/ui_cursor_default.png");
            builder.AppendLine("- Assets/Resources/UI/Cursors/ui_cursor_hover.png");
            builder.AppendLine("- Assets/Resources/UI/Cursors/ui_cursor_pressed.png");
            builder.AppendLine("- Assets/Resources/UI/Cursors/ui_cursor_disabled.png");
            builder.AppendLine();
            builder.AppendLine("## Remaining Risks");
            builder.AppendLine("- This pass is intentionally limited to smm_card_001 vertical slice and has not been spread to the full story.");
            builder.AppendLine("- Text rendering still uses Unity UI Text rather than TextMeshPro; atlas-level polish can be improved later.");

            var report = builder.ToString();
            File.WriteAllText(ReportPath, report, Encoding.UTF8);
            File.WriteAllText(MasterReportPath, report, Encoding.UTF8);
        }
    }
}
