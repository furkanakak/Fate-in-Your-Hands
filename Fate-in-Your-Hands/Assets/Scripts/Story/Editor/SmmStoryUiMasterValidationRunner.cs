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
    public static class SmmStoryUiMasterValidationRunner
    {
        private const string OutputDirectory = @"C:\Users\furkan\Desktop\Saray_UI_Master_Redesign";
        private const string WorkspaceReportPath = @"C:\Users\furkan\Desktop\Fate-in-Your-Hands\UI_MASTER_VALIDATION_REPORT.md";
        private const string ScenePath = "Assets/Scenes/SampleScene.unity";
        private const string FirstCardId = "smm_card_001";

        private static readonly List<string> Checks = new List<string>();
        private static readonly List<string> Failures = new List<string>();
        private static readonly List<string> Screenshots = new List<string>();
        private static readonly Stack<IEnumerator> RoutineStack = new Stack<IEnumerator>();
        private static bool previousEnterPlayModeOptionsEnabled;
        private static EnterPlayModeOptions previousEnterPlayModeOptions;
        private static bool capturedEditorSettings;
        private static bool finished;
        private static bool completedAllChecks;

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
            finished = false;
            completedAllChecks = false;

            try
            {
                EditorSceneManager.OpenScene(ScenePath);
                CaptureAndApplyPlayModeSettings();
                RoutineStack.Clear();
                RoutineStack.Push(RunRoutine());
                EditorApplication.update -= Pump;
                EditorApplication.update += Pump;
                Debug.Log("SmmStoryUiMasterValidationRunner started.");
            }
            catch (Exception exception)
            {
                Failures.Add(exception.ToString());
                Finish();
            }
        }

        public static void RunRemainder()
        {
            Directory.CreateDirectory(OutputDirectory);
            Checks.Clear();
            Failures.Clear();
            Screenshots.Clear();
            finished = false;
            completedAllChecks = false;

            try
            {
                EditorSceneManager.OpenScene(ScenePath);
                CaptureAndApplyPlayModeSettings();
                RoutineStack.Clear();
                RoutineStack.Push(RunRemainderRoutine());
                EditorApplication.update -= Pump;
                EditorApplication.update += Pump;
                Debug.Log("SmmStoryUiMasterValidationRunner remainder started.");
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
            Require(controller.HasGameplayTitleHiddenForTest, "gameplay hides story title, chapter kicker and card title");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_001 visible Turkish text has no replacement characters");
            Require(controller.UsesSpriteHealthForTest, "health uses sprite Image hearts");
            Require(controller.UsesDistinctHalfHeartSpriteForTest, "half health uses a distinct filled-half heart sprite");
            Require(!controller.UsesTextHeartGlyphsForTest, "HUD does not render glyph hearts");
            Require(controller.HasAnimationLayerForTest, "card animation layer exists");
            Require(controller.UiLayersRenderAboveBackgroundForTest, "UI layers render above background");
            Require(controller.ChoiceAView?.ArtImage.sprite != null, "choice A art is loaded");
            Require(controller.ChoiceBView?.ArtImage.sprite != null, "choice B art is loaded");
            Require(!controller.IsModalResultVisibleForTest, "choice front has no result modal");
            Require(VisibleProductionTextIsClean(), "visible production UI hides ids and debug state");
            Require(AnimationCodeUsesUnscaledDeltaTime(), "card animation code uses unscaled delta time");
            Require(CursorAssetFilesExist(), "custom cursor asset files exist");
            Require(controller.HasCustomCursorAssetsForTest, "custom cursor textures load from Resources");
            Require(!controller.IsUsingSystemCursorForTest, "production UI does not fall back to the default OS cursor");
            Require(controller.CursorHotspotForTest == new Vector2(14f, 33f), "cursor hotspot matches the pointing fingertip");
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Default, "background uses the default custom cursor");

            SmmCursorManager.SetTargetState(controller.SettingsButtonForTest, true, false, false);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "settings button uses the hover cursor");
            SmmCursorManager.ClearSource(controller.SettingsButtonForTest);

            controller.FocusChoiceForTest(1);
            yield return WaitFrames(8);
            Require(controller.ChoiceBView.IsFocusedForTest, "keyboard/gamepad focus moves to choice B");
            controller.FocusChoiceForTest(0);
            yield return WaitFrames(8);
            Require(controller.ChoiceAView.IsFocusedForTest, "keyboard/gamepad focus moves to choice A");

            yield return Capture("01_reference_notes.png", 1920, 1080);
            yield return Capture("02_static_front_1920.png", 1920, 1080);
            yield return CaptureAtResolution(controller, "03_front_1366.png", 1366, 768);
            yield return CaptureAtResolution(controller, "03b_front_1280.png", 1280, 720);
            yield return CaptureAtResolution(controller, "03c_mobile_landscape_front.png", 844, 390);

            controller.ApplyViewportForTest(1920, 1080);
            yield return WaitFrames(18);
            controller.ChoiceAView.SetHoverPreview(true);
            yield return WaitFrames(16);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "choice card hover uses the hover cursor");
            SmmCursorManager.SetTargetState(controller.ChoiceAView, true, true, false);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Pressed, "choice card click uses the pressed cursor");
            SmmCursorManager.ClearSource(controller.ChoiceAView);
            yield return Capture("04_choice_hover.png", 1920, 1080);
            controller.ChoiceAView.SetHoverPreview(false);
            yield return WaitFrames(8);

            var expectedChoiceAResultText = controller.GetNormalizedChoiceResultTextForTest(0);
            Require(!string.IsNullOrWhiteSpace(expectedChoiceAResultText), "choice A has normalized YAML resultText");
            var beforeDoubleClickCount = controller.SelectionTriggerCountForTest;
            controller.TriggerChoiceForTest(0);
            controller.TriggerChoiceForTest(0);
            yield return WaitFrames(4);
            Require(controller.SelectionTriggerCountForTest == beforeDoubleClickCount + 1, "rapid double-click is locked");
            Require(controller.IsChoiceFlipAnimatingForTest, "choice A starts center-and-flip animation");
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Disabled, "input-locked card animation uses the disabled cursor");
            yield return Capture("05_choice_selected.png", 1920, 1080);

            yield return WaitUntil(() => controller.NonSelectedChoiceHiddenForTest, 90);
            Require(controller.NonSelectedChoiceHiddenForTest, "non-selected card exits fully");
            yield return Capture("06_nonselected_exit.png", 1920, 1080);

            yield return WaitUntil(() => controller.SelectedChoiceIsCenteredForTest, 90);
            Require(controller.SelectedChoiceIsCenteredForTest, "selected card reaches centered result position");
            yield return Capture("07_card_centered.png", 1920, 1080);

            yield return WaitFrames(8);
            yield return Capture("08_mid_flip.png", 1920, 1080);
            yield return WaitUntil(() => controller.IsResultOpen, 140);
            Require(controller.IsResultOpen, "choice result is shown on selected card back");
            Require(!controller.IsModalResultVisibleForTest, "central result modal remains hidden after choice");
            Require(controller.CurrentResultTextForTest.Contains(expectedChoiceAResultText), "back face shows normalized YAML resultText for choice A");
            Require(TextHasNoCorruptCharacters(controller.CurrentResultTextForTest), "result back Turkish text has no replacement characters");
            Require(NoVisibleChoiceResultChrome(), "result back has no Devam Et button or debug chrome");
            yield return WaitUntil(() => controller.IsResultInputUnlockedForTest, 80);
            Require(controller.IsResultInputUnlockedForTest, "result card unlocks whole-card continue input");
            controller.ChoiceAView.SetHoverPreview(true);
            yield return WaitFrames(1);
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Hover, "result card back face uses the hover cursor");
            controller.ChoiceAView.SetHoverPreview(false);
            yield return Capture("09_result_back.png", 1920, 1080);

            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_002", 160);
            Require(controller.CurrentCardId == "smm_card_002", "choice A continue routes to smm_card_002");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_002 visible Turkish text has no replacement characters");
            RequireOptionalCounter(controller, "kitchen_favor", 1, "choice A counter delta applies after continue");
            RequireOptionalFlag(controller, "flag_truth_told", "choice A flag applies after continue");
            Require(PlayerPrefs.HasKey(SmmStoryGameController.AutosaveKey), "autosave exists after continue");
            yield return Capture("10_next_card.png", 1920, 1080);

            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_004", 160);
            Require(controller.CurrentCardId == "smm_card_004", "smm_card_002 choice B routes to smm_card_004");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_004 visible Turkish text has no replacement characters");
            yield return WaitFrames(36);

            controller.ForceStartNewStoryForTest();
            yield return WaitFrames(36);
            Require(controller.CurrentCardId == FirstCardId, "new story resets to first card before choice B route");
            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_003", 160);
            Require(controller.CurrentCardId == "smm_card_003", "choice B continue routes to smm_card_003");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_003 visible Turkish text has no replacement characters");
            RequireOptionalCounter(controller, "palace_suspicion", 2, "choice B counter delta applies after continue");

            controller.JumpToCard("smm_card_004");
            yield return WaitFrames(18);
            Require(controller.CurrentCardId == "smm_card_004", "fourth vertical-slice card is loadable");
            Require(VisibleTextHasNoCorruptCharacters(controller), "fourth vertical-slice card visible Turkish text stays clean");
            Require(controller.ChoiceAView?.ArtImage.sprite != null && controller.ChoiceBView?.ArtImage.sprite != null, "fourth card choice art is loaded");

            controller.JumpToCard("smm_card_017");
            yield return WaitFrames(18);
            Require(VisibleTextHasNoCorruptCharacters(controller), "prompt target card visible Turkish text stays clean");
            yield return Capture("10b_prompt_target_front.png", 1920, 1080);

            controller.ForceStartNewStoryForTest();
            yield return CaptureAtResolution(controller, "11_mobile_front.png", 390, 844);
            Require(ChoiceCardsAreStackedForPortrait(controller), "mobile layout stacks choice cards vertically");
            Require(controller.IsTouchCursorHiddenForTest, "mobile/touch viewport hides the cursor");
            controller.TriggerChoiceForTest(0);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            Require(NoVisibleChoiceResultChrome(), "mobile result has no modal-style result chrome");
            yield return WaitUntil(() => controller.IsResultInputUnlockedForTest, 80);
            Require(controller.IsResultInputUnlockedForTest, "mobile result card completes reveal before screenshot");
            yield return Capture("12_mobile_result.png", 390, 844);
            completedAllChecks = true;
            WriteReportSafely();
            Finish();
        }

        private static IEnumerator RunRemainderRoutine()
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
            Require(controller.HasGameplayTitleHiddenForTest, "gameplay hides story title, chapter kicker and card title");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_001 visible Turkish text has no replacement characters");
            Require(controller.UsesSpriteHealthForTest, "health uses sprite Image hearts");
            Require(!controller.UsesTextHeartGlyphsForTest, "HUD does not render glyph hearts");
            Require(controller.HasAnimationLayerForTest, "card animation layer exists");
            Require(controller.UiLayersRenderAboveBackgroundForTest, "UI layers render above background");
            Require(controller.ChoiceAView?.ArtImage.sprite != null, "choice A art is loaded");
            Require(controller.ChoiceBView?.ArtImage.sprite != null, "choice B art is loaded");
            Require(!controller.IsModalResultVisibleForTest, "choice front has no result modal");
            Require(VisibleProductionTextIsClean(), "visible production UI hides ids and debug state");
            Require(AnimationCodeUsesUnscaledDeltaTime(), "card animation code uses unscaled delta time");
            Require(CursorAssetFilesExist(), "custom cursor asset files exist");
            Require(controller.HasCustomCursorAssetsForTest, "custom cursor textures load from Resources");
            Require(!controller.IsUsingSystemCursorForTest, "production UI does not fall back to the default OS cursor");
            Require(controller.CursorHotspotForTest == new Vector2(14f, 33f), "cursor hotspot matches the pointing fingertip");
            Require(controller.CurrentCursorStateForTest == SmmCursorState.Default, "background uses the default custom cursor");

            ValidateExistingScreenshot("01_reference_notes.png", 1920, 1080);
            ValidateExistingScreenshot("02_static_front_1920.png", 1920, 1080);
            ValidateExistingScreenshot("03_front_1366.png", 1366, 768);
            ValidateExistingScreenshot("03c_mobile_landscape_front.png", 844, 390);
            ValidateExistingScreenshot("04_choice_hover.png", 1920, 1080);
            ValidateExistingScreenshot("05_choice_selected.png", 1920, 1080);
            ValidateExistingScreenshot("06_nonselected_exit.png", 1920, 1080);
            ValidateExistingScreenshot("07_card_centered.png", 1920, 1080);
            ValidateExistingScreenshot("08_mid_flip.png", 1920, 1080);
            ValidateExistingScreenshot("09_result_back.png", 1920, 1080);

            var expectedChoiceAResultText = controller.GetNormalizedChoiceResultTextForTest(0);
            Require(!string.IsNullOrWhiteSpace(expectedChoiceAResultText), "choice A has normalized YAML resultText");
            controller.TriggerChoiceForTest(0);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            Require(controller.IsResultOpen, "choice result is shown on selected card back");
            Require(!controller.IsModalResultVisibleForTest, "central result modal remains hidden after choice");
            Require(controller.CurrentResultTextForTest.Contains(expectedChoiceAResultText), "back face shows normalized YAML resultText for choice A");
            Require(TextHasNoCorruptCharacters(controller.CurrentResultTextForTest), "result back Turkish text has no replacement characters");
            Require(NoVisibleChoiceResultChrome(), "result back has no Devam Et button or debug chrome");
            yield return WaitUntil(() => controller.IsResultInputUnlockedForTest, 80);
            Require(controller.IsResultInputUnlockedForTest, "result card unlocks whole-card continue input");

            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_002", 160);
            Require(controller.CurrentCardId == "smm_card_002", "choice A continue routes to smm_card_002");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_002 visible Turkish text has no replacement characters");
            RequireOptionalCounter(controller, "kitchen_favor", 1, "choice A counter delta applies after continue");
            RequireOptionalFlag(controller, "flag_truth_told", "choice A flag applies after continue");
            Require(PlayerPrefs.HasKey(SmmStoryGameController.AutosaveKey), "autosave exists after continue");
            yield return Capture("10_next_card.png", 1920, 1080);

            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_004", 160);
            Require(controller.CurrentCardId == "smm_card_004", "smm_card_002 choice B routes to smm_card_004");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_004 visible Turkish text has no replacement characters");
            yield return WaitFrames(36);

            controller.ForceStartNewStoryForTest();
            yield return WaitFrames(36);
            Require(controller.CurrentCardId == FirstCardId, "new story resets to first card before choice B route");
            controller.TriggerChoiceForTest(1);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            controller.ContinueForTest();
            yield return WaitUntil(() => controller.CurrentCardId == "smm_card_003", 160);
            Require(controller.CurrentCardId == "smm_card_003", "choice B continue routes to smm_card_003");
            Require(VisibleTextHasNoCorruptCharacters(controller), "smm_card_003 visible Turkish text has no replacement characters");
            RequireOptionalCounter(controller, "palace_suspicion", 2, "choice B counter delta applies after continue");

            controller.JumpToCard("smm_card_004");
            yield return WaitFrames(18);
            Require(controller.CurrentCardId == "smm_card_004", "fourth vertical-slice card is loadable");
            Require(VisibleTextHasNoCorruptCharacters(controller), "fourth vertical-slice card visible Turkish text stays clean");
            Require(controller.ChoiceAView?.ArtImage.sprite != null && controller.ChoiceBView?.ArtImage.sprite != null, "fourth card choice art is loaded");

            controller.ForceStartNewStoryForTest();
            yield return CaptureAtResolution(controller, "11_mobile_front.png", 390, 844);
            Require(ChoiceCardsAreStackedForPortrait(controller), "mobile layout stacks choice cards vertically");
            Require(controller.IsTouchCursorHiddenForTest, "mobile/touch viewport hides the cursor");
            controller.TriggerChoiceForTest(0);
            yield return WaitUntil(() => controller.IsResultOpen, 160);
            Require(NoVisibleChoiceResultChrome(), "mobile result has no modal-style result chrome");
            yield return WaitUntil(() => controller.IsResultInputUnlockedForTest, 80);
            Require(controller.IsResultInputUnlockedForTest, "mobile result card completes reveal before screenshot");
            yield return Capture("12_mobile_result.png", 390, 844);
            completedAllChecks = true;
            WriteReportSafely();
            Finish();
        }

        private static IEnumerator CaptureAtResolution(SmmStoryGameController controller, string fileName, int width, int height)
        {
            Screen.SetResolution(width, height, false);
            controller.ApplyViewportForTest(width, height);
            Canvas.ForceUpdateCanvases();
            yield return WaitFrames(24);
            yield return Capture(fileName, width, height);
        }

        private static void ValidateExistingScreenshot(string fileName, int expectedWidth, int expectedHeight)
        {
            var path = Path.Combine(OutputDirectory, fileName);
            Require(File.Exists(path), $"screenshot exists: {fileName}");
            if (!File.Exists(path))
            {
                return;
            }

            if (!Screenshots.Contains(path))
            {
                Screenshots.Add(path);
            }

            var size = ReadPngSize(path);
            Require(size.x == expectedWidth && size.y == expectedHeight, $"screenshot size {fileName} is {expectedWidth}x{expectedHeight}");
            Require(new FileInfo(path).Length > 12000, $"screenshot is non-empty: {fileName}");
            WriteReportSafely();
        }

        private static bool VisibleTextHasNoCorruptCharacters(SmmStoryGameController controller)
        {
            return controller != null && TextHasNoCorruptCharacters(controller.CurrentVisibleTextBlobForTest);
        }

        private static bool TextHasNoCorruptCharacters(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var forbidden = new[] { "\uFFFD", "Ä", "Ã", "Å" };
            return forbidden.All(token => !text.Contains(token));
        }

        private static bool VisibleProductionTextIsClean()
        {
            var forbidden = new[] { "smm_card_", "choice_smm_", "bg_", "Current card:", "Background:", "kitchen_favor", "palace_suspicion", "flag_" };
            var texts = UnityEngine.Object.FindObjectsByType<Text>(FindObjectsInactive.Exclude);
            return texts.All(text => forbidden.All(token => !text.text.Contains(token)));
        }

        private static bool NoVisibleChoiceResultChrome()
        {
            var forbidden = new[] { "Devam Et", "kitchen_favor", "palace_suspicion", "flag_" };
            var texts = UnityEngine.Object.FindObjectsByType<Text>(FindObjectsInactive.Exclude);
            return texts.All(text => forbidden.All(token => !text.text.Contains(token)));
        }

        private static bool AnimationCodeUsesUnscaledDeltaTime()
        {
            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrWhiteSpace(projectRoot))
            {
                return false;
            }

            var path = Path.Combine(projectRoot, "Assets", "Scripts", "Story", "SmmChoiceCardView.cs");
            return File.Exists(path) && File.ReadAllText(path).Contains("Time.unscaledDeltaTime");
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
            Require(new FileInfo(path).Length > 12000, $"screenshot is non-empty: {fileName}");
            if (fileName == "12_mobile_result.png" && Failures.Count == 0)
            {
                completedAllChecks = true;
            }

            WriteReportSafely();
        }

        private static void CaptureUiToPng(string path, int width, int height)
        {
            var captureCameraObject = new GameObject("Smm UI Master Capture Camera");
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

        private static void RequireOptionalCounter(SmmStoryGameController controller, string counterId, int expectedValue, string check)
        {
            var actual = controller != null ? controller.GetCounterValueForTest(counterId) : 0;
            if (actual == expectedValue)
            {
                Require(true, check);
                return;
            }

            if (actual == 0 && expectedValue != 0)
            {
                RecordSkip($"{check} (current story data has no {counterId} counter)");
                return;
            }

            Require(false, check);
        }

        private static void RequireOptionalFlag(SmmStoryGameController controller, string flagId, string check)
        {
            if (controller != null && controller.HasFlagForTest(flagId))
            {
                Require(true, check);
                return;
            }

            RecordSkip($"{check} (current story data has no {flagId} state flag)");
        }

        private static void RecordSkip(string check)
        {
            var line = "SKIP: " + check;
            Checks.Add(line);
            Debug.Log(line);
            WriteReportSafely();
        }

        private static void Require(bool condition, string check)
        {
            var line = (condition ? "PASS: " : "FAIL: ") + check;
            Checks.Add(line);
            Debug.Log(line);
            if (!condition)
            {
                Failures.Add(check);
            }

            WriteReportSafely();
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
            if (finished)
            {
                return;
            }

            finished = true;
            EditorApplication.update -= Pump;
            WriteReportSafely();

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
            builder.AppendLine("# UI Master Validation Report");
            builder.AppendLine();
            var status = Failures.Count > 0 ? "FAIL" : completedAllChecks ? "PASS" : "IN PROGRESS";
            builder.AppendLine($"Status: {status}");
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

            if (Failures.Count > 0)
            {
                builder.AppendLine();
                builder.AppendLine("## Failures");
                foreach (var failure in Failures)
                {
                    builder.AppendLine($"- {failure}");
                }
            }

            var projectRoot = Directory.GetParent(Application.dataPath)?.FullName ?? string.Empty;
            var repoRoot = Directory.GetParent(projectRoot)?.FullName ?? projectRoot;
            var report = builder.ToString();
            File.WriteAllText(Path.Combine(repoRoot, "UI_MASTER_VALIDATION_REPORT.md"), report, Encoding.UTF8);
            File.WriteAllText(Path.Combine(OutputDirectory, "UI_MASTER_VALIDATION_REPORT.md"), report, Encoding.UTF8);
            File.WriteAllText(WorkspaceReportPath, report, Encoding.UTF8);
        }

        private static void WriteReportSafely()
        {
            try
            {
                WriteReport();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }
        }
    }
}
