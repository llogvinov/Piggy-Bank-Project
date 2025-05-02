using System;
using UI.LocationsShop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScrollSnapController : MonoBehaviour
    {
        [SerializeField] private UIShop _uiShop;

        public ScrollRect scrollRect;
        public RectTransform content;
        public Button leftButton;
        public Button rightButton;
        public float snapSpeed = 10f;

        private IItemShopUI _itemShopUI;
        private int totalElements;
        private int currentIndex = 0;
        private float[] elementPositions;
        private bool isLerping = false;
        private float targetPosition;

        private void Awake()
        {
            _itemShopUI = GetComponent<IItemShopUI>();
            _itemShopUI.UIGenerated += OnUIGenerated;
            _uiShop.Opened += ScrollToFirst;
        }

        private void OnDestroy()
        {
            _itemShopUI.UIGenerated -= OnUIGenerated;
            _uiShop.Opened -= ScrollToFirst;

            leftButton.onClick.RemoveListener(ScrollLeft);
            rightButton.onClick.RemoveListener(ScrollRight);
        }

        private void OnUIGenerated()
        {
            totalElements = content.childCount;

            elementPositions = new float[totalElements];
            for (int i = 0; i < totalElements; i++)
            {
                elementPositions[i] = (float)i / (totalElements - 1);
            }

            leftButton.onClick.AddListener(ScrollLeft);
            rightButton.onClick.AddListener(ScrollRight);

            UpdateButtons();
        }

        private void Update()
        {
            if (isLerping)
            {
                scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                    scrollRect.horizontalNormalizedPosition,
                    targetPosition,
                    Time.deltaTime * snapSpeed
                );

                if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetPosition) < 0.001f)
                {
                    scrollRect.horizontalNormalizedPosition = targetPosition;
                    isLerping = false;
                }
            }
        }

        private void ScrollLeft()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                SnapToElement(currentIndex);
            }
            UpdateButtons();
        }

        private void ScrollRight()
        {
            if (currentIndex < totalElements - 1)
            {
                currentIndex++;
                SnapToElement(currentIndex);
            }
            UpdateButtons();
        }

        private void SnapToElement(int index)
        {
            targetPosition = elementPositions[index];
            isLerping = true;
        }

        private void ScrollToFirst()
        {
            currentIndex = 0;
            scrollRect.horizontalNormalizedPosition = elementPositions[0];
        }

        private void UpdateButtons()
        {
            leftButton.interactable = currentIndex > 0;
            rightButton.interactable = currentIndex < totalElements - 1;
        }
    }
}