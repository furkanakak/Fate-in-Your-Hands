using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace FateInYourHands.Story
{
    public enum SmmCursorState
    {
        Default,
        Hover,
        Pressed,
        Disabled
    }

    public sealed class SmmCursorManager : MonoBehaviour
    {
        private const string DefaultPath = "UI/Cursors/ui_cursor_default";
        private const string HoverPath = "UI/Cursors/ui_cursor_hover";
        private const string PressedPath = "UI/Cursors/ui_cursor_pressed";
        private const string DisabledPath = "UI/Cursors/ui_cursor_disabled";

        private static readonly Vector2 FingerHotspot = new Vector2(14f, 33f);

        private readonly HashSet<Object> hoverSources = new HashSet<Object>();
        private readonly HashSet<Object> pressedSources = new HashSet<Object>();
        private readonly HashSet<Object> disabledSources = new HashSet<Object>();

        private Texture2D defaultCursor;
        private Texture2D hoverCursor;
        private Texture2D pressedCursor;
        private Texture2D disabledCursor;
        private SmmCursorState appliedState = (SmmCursorState)(-1);
        private bool appliedVisible = true;
        private bool assetsLoaded;
        private bool inputLocked;
        private bool touchModeForTest;
        private bool gamepadPointerHidden;
        private bool hasPreviousMousePosition;
        private Vector2 previousMousePosition;

        public static SmmCursorManager Instance { get; private set; }
        public bool HasAllCursorAssetsForTest => assetsLoaded;
        public bool IsCursorVisibleForTest => appliedVisible;
        public bool IsUsingSystemCursorForTest => !assetsLoaded;
        public bool IsTouchModeForTest => ShouldHideForTouch();
        public Vector2 HotspotForTest => FingerHotspot;
        public SmmCursorState CurrentStateForTest => ComputeDesiredState();

        public static SmmCursorManager Ensure()
        {
            if (Instance != null)
            {
                return Instance;
            }

            var existing = FindAnyObjectByType<SmmCursorManager>();
            if (existing != null)
            {
                Instance = existing;
                existing.LoadCursorAssets();
                existing.ApplyCursor(true);
                return existing;
            }

            var cursorObject = new GameObject("Smm Cursor Manager");
            return cursorObject.AddComponent<SmmCursorManager>();
        }

        public static void SetTargetState(Object source, bool hovering, bool pressing, bool disabled)
        {
            if (Instance == null || source == null)
            {
                return;
            }

            Instance.SetSourceState(source, hovering, pressing, disabled);
        }

        public static void ClearSource(Object source)
        {
            if (Instance == null || source == null)
            {
                return;
            }

            Instance.ClearSourceState(source);
        }

        public static void SetGlobalInputLocked(bool locked)
        {
            if (Instance == null)
            {
                return;
            }

            Instance.inputLocked = locked;
            if (locked)
            {
                Instance.pressedSources.Clear();
            }

            Instance.ApplyCursor(false);
        }

        public static void SetTouchModeForTest(bool touchMode)
        {
            if (Instance == null)
            {
                return;
            }

            Instance.touchModeForTest = touchMode;
            Instance.ApplyCursor(false);
        }

        public static void ResetForStoryScreen()
        {
            if (Instance == null)
            {
                return;
            }

            Instance.hoverSources.Clear();
            Instance.pressedSources.Clear();
            Instance.disabledSources.Clear();
            Instance.inputLocked = false;
            Instance.gamepadPointerHidden = false;
            Instance.ApplyCursor(false);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCursorAssets();
            ApplyCursor(true);
        }

        private void OnEnable()
        {
            LoadCursorAssets();
            ApplyCursor(true);
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                Cursor.visible = true;
                Instance = null;
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                ApplyCursor(true);
            }
        }

        private void Update()
        {
            RefreshLastPointerDevice();
            ApplyCursor(false);
        }

        private void LoadCursorAssets()
        {
            if (assetsLoaded)
            {
                return;
            }

            defaultCursor = LoadCursorTexture(DefaultPath);
            hoverCursor = LoadCursorTexture(HoverPath);
            pressedCursor = LoadCursorTexture(PressedPath);
            disabledCursor = LoadCursorTexture(DisabledPath);
            assetsLoaded = defaultCursor != null && hoverCursor != null && pressedCursor != null && disabledCursor != null;

            ConfigureTexture(defaultCursor);
            ConfigureTexture(hoverCursor);
            ConfigureTexture(pressedCursor);
            ConfigureTexture(disabledCursor);
        }

        private static Texture2D LoadCursorTexture(string path)
        {
            var texture = Resources.Load<Texture2D>(path);
            if (texture != null)
            {
                return texture;
            }

            var sprite = Resources.Load<Sprite>(path);
            return sprite != null ? sprite.texture : null;
        }

        private static void ConfigureTexture(Texture2D texture)
        {
            if (texture == null)
            {
                return;
            }

            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;
        }

        private void SetSourceState(Object source, bool hovering, bool pressing, bool disabled)
        {
            SetMembership(hoverSources, source, hovering && !disabled);
            SetMembership(pressedSources, source, hovering && pressing && !disabled);
            SetMembership(disabledSources, source, hovering && disabled);
            ApplyCursor(false);
        }

        private void ClearSourceState(Object source)
        {
            hoverSources.Remove(source);
            pressedSources.Remove(source);
            disabledSources.Remove(source);
            ApplyCursor(false);
        }

        private static void SetMembership(HashSet<Object> set, Object value, bool enabled)
        {
            if (enabled)
            {
                set.Add(value);
            }
            else
            {
                set.Remove(value);
            }
        }

        private void RefreshLastPointerDevice()
        {
            var mouse = Mouse.current;
            if (mouse != null)
            {
                var position = mouse.position.ReadValue();
                if (!hasPreviousMousePosition)
                {
                    previousMousePosition = position;
                    hasPreviousMousePosition = true;
                }

                if ((position - previousMousePosition).sqrMagnitude > 0.01f
                    || mouse.leftButton.wasPressedThisFrame
                    || mouse.rightButton.wasPressedThisFrame
                    || mouse.middleButton.wasPressedThisFrame)
                {
                    gamepadPointerHidden = false;
                }

                previousMousePosition = position;
            }

            var gamepad = Gamepad.current;
            if (gamepad == null)
            {
                return;
            }

            var gamepadUsed = gamepad.buttonSouth.wasPressedThisFrame
                || gamepad.buttonEast.wasPressedThisFrame
                || gamepad.buttonWest.wasPressedThisFrame
                || gamepad.buttonNorth.wasPressedThisFrame
                || gamepad.dpad.ReadValue().sqrMagnitude > 0.25f
                || gamepad.leftStick.ReadValue().sqrMagnitude > 0.35f;

            if (gamepadUsed)
            {
                gamepadPointerHidden = true;
            }
        }

        private void ApplyCursor(bool force)
        {
            var state = ComputeDesiredState();
            var visible = assetsLoaded && !ShouldHideForTouch() && !gamepadPointerHidden;

            if (assetsLoaded && (force || state != appliedState))
            {
                Cursor.SetCursor(GetTextureForState(state), FingerHotspot, CursorMode.Auto);
                appliedState = state;
            }
            else if (!assetsLoaded && force)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }

            if (force || visible != appliedVisible)
            {
                Cursor.visible = visible;
                appliedVisible = visible;
            }
        }

        private SmmCursorState ComputeDesiredState()
        {
            if (inputLocked || disabledSources.Count > 0)
            {
                return SmmCursorState.Disabled;
            }

            if (pressedSources.Count > 0)
            {
                return SmmCursorState.Pressed;
            }

            return hoverSources.Count > 0 ? SmmCursorState.Hover : SmmCursorState.Default;
        }

        private Texture2D GetTextureForState(SmmCursorState state)
        {
            switch (state)
            {
                case SmmCursorState.Hover:
                    return hoverCursor;
                case SmmCursorState.Pressed:
                    return pressedCursor;
                case SmmCursorState.Disabled:
                    return disabledCursor;
                default:
                    return defaultCursor;
            }
        }

        private bool ShouldHideForTouch()
        {
#if UNITY_ANDROID || UNITY_IOS
            return true;
#else
            return touchModeForTest
                || Application.isMobilePlatform
                || (Touchscreen.current != null && Mouse.current == null);
#endif
        }
    }

    public sealed class SmmCursorHoverTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private Selectable selectable;
        private bool hovering;
        private bool pressing;

        private bool IsDisabled => selectable != null && !selectable.interactable;

        private void Awake()
        {
            selectable = GetComponent<Selectable>();
        }

        private void Update()
        {
            if (hovering)
            {
                PushState();
            }
        }

        private void OnDisable()
        {
            ClearState();
        }

        private void OnDestroy()
        {
            ClearState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovering = true;
            pressing = false;
            PushState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ClearState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData != null && eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            hovering = true;
            pressing = !IsDisabled;
            PushState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressing = false;
            PushState();
        }

        private void PushState()
        {
            SmmCursorManager.SetTargetState(this, hovering, pressing, IsDisabled);
        }

        private void ClearState()
        {
            hovering = false;
            pressing = false;
            SmmCursorManager.ClearSource(this);
        }
    }
}
