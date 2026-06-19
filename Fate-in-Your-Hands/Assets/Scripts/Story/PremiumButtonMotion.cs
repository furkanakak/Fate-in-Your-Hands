using UnityEngine;
using UnityEngine.EventSystems;

namespace FateInYourHands.Story
{
    public sealed class PremiumButtonMotion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float hoverLift = 6f;
        [SerializeField] private float pressDrop = -3f;
        [SerializeField] private float hoverScale = 1.035f;
        [SerializeField] private float pressScale = 0.965f;
        [SerializeField] private float followSpeed = 18f;

        private RectTransform rectTransform;
        private Vector2 basePosition;
        private Vector2 targetOffset;
        private Vector3 targetScale = Vector3.one;
        private bool initialized;
        private bool hovering;
        private bool pressing;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
            targetOffset = Vector2.zero;
            targetScale = Vector3.one;
            pressing = false;
            hovering = false;
        }

        private void Update()
        {
            Initialize();
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, basePosition + targetOffset, followSpeed * Time.unscaledDeltaTime);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, followSpeed * Time.unscaledDeltaTime);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovering = true;
            RefreshTargets();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovering = false;
            pressing = false;
            RefreshTargets();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData != null && eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            pressing = true;
            RefreshTargets();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressing = false;
            RefreshTargets();
        }

        private void Initialize()
        {
            if (initialized)
            {
                return;
            }

            rectTransform = GetComponent<RectTransform>();
            basePosition = rectTransform != null ? rectTransform.anchoredPosition : Vector2.zero;
            initialized = rectTransform != null;
        }

        private void RefreshTargets()
        {
            if (pressing)
            {
                targetOffset = new Vector2(0f, pressDrop);
                targetScale = Vector3.one * pressScale;
                return;
            }

            if (hovering)
            {
                targetOffset = new Vector2(0f, hoverLift);
                targetScale = Vector3.one * hoverScale;
                return;
            }

            targetOffset = Vector2.zero;
            targetScale = Vector3.one;
        }
    }
}
