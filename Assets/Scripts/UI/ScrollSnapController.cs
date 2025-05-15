using Core;
using Core.Services.PlayerData;
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
            _uiShop.Opened += ScrollToSelected;
        }

        private void OnDestroy()
        {
            _itemShopUI.UIGenerated -= OnUIGenerated;
            _uiShop.Opened -= ScrollToSelected;

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

            ScrollToSelected();
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
            currentIndex = index;
            targetPosition = elementPositions[index];
            scrollRect.horizontalNormalizedPosition = targetPosition;
        }

        private void ScrollToSelected()
        {
            if (elementPositions == null) return;

            var _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            switch (_itemShopUI)
            {
                case UILocationsShop locationsShop:
                    SnapToElement(_playerDataService.GetSelectedLocationIndex());
                    break;
                case UIHatShop hatShop:
                    var elementChildIndex = hatShop.GetElementChildIndex(_playerDataService.GetSelectedHatIndex());
                    SnapToElement(elementChildIndex);
                    break;
                case UIMaskShop maskShop:
                    SnapToElement(_playerDataService.GetSelectedMaskIndex());
                    break;
                default:
                    SnapToElement(0);
                    break;
            }
        }

        private void UpdateButtons()
        {
            leftButton.gameObject.SetActive(currentIndex > 0);
            rightButton.gameObject.SetActive(currentIndex < totalElements - 1);
        }
    }
}