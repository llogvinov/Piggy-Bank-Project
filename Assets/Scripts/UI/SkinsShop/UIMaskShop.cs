using System;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace UI
{
    public class UIMaskShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;

        [Header("UI Elements")]
        [SerializeField] private Transform ShopItemsContainer;
        [SerializeField] private GameObject itemPrefab;
        [Space(20f)]
        [SerializeField] private MaskShopDatabase maskDB;

        [Header("Main Menu")]
        [SerializeField] private SpriteRenderer mainMenuMaskImage;

        private int newSelectedMaskIndex;
        private int previousSelectedMaskIndex;
        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            GenerateShopItemUI();
            SetSelectedItem();
            SelectItemUI(_playerDataService.GetSelectedMaskIndex());
            ChangeItemSkin();
        }

        public void SetSelectedItem()
        {
            int index = _playerDataService.GetSelectedMaskIndex();
            _playerDataService.SetSelectedMask(maskDB.GetMask(index), index);
        }

        public void GenerateShopItemUI()
        {
            for (int i = 0; i < _playerDataService.GetAllPurchasedMasks().Count; i++)
            {
                int purchaseCharacterIndex = _playerDataService.GetPurchasedMask(i);
                maskDB.PurchaseMask(purchaseCharacterIndex);
            }

            for (int i = 0; i < maskDB.MasksCount; i++)
            {
                Mask mask = maskDB.GetMask(i);
                MaskItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<MaskItemUI>();

                uiItem.gameObject.name = "Item" + i + "-" + mask.name;

                uiItem.SetMaskName(mask.name);
                uiItem.SetMaskImage(mask.image);
                uiItem.SetMaskPrice(mask.price);

                if (i == 0)
                {
                    uiItem.SetMaskImageOpacity();
                }

                if (mask.isPurchased)
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(i, OnItemSelected);
                }
                else
                {
                    uiItem.SetMaskPrice(mask.price);
                    uiItem.OnItemPurchase(i, OnItemPurchased);
                }
            }

            UIGenerated?.Invoke();
        }

        public void ChangeItemSkin()
        {
            Mask mask = _playerDataService.GetSelectedMask();
            mainMenuMaskImage.sprite = mask.image;
        }

        public void OnItemSelected(int index)
        {
            SelectItemUI(index);
            _playerDataService.SetSelectedMask(maskDB.GetMask(index), index);
            ChangeItemSkin();
        }

        public void SelectItemUI(int itemIndex)
        {
            previousSelectedMaskIndex = newSelectedMaskIndex;
            newSelectedMaskIndex = itemIndex;

            MaskItemUI previousUiItem = GetItemUI(previousSelectedMaskIndex);
            MaskItemUI newUiItem = GetItemUI(newSelectedMaskIndex);

            previousUiItem.DeselectItem();
            newUiItem.SelectItem();
        }

        private MaskItemUI GetItemUI(int index) => ShopItemsContainer.GetChild(index).GetComponent<MaskItemUI>();

        public void OnItemPurchased(int index)
        {
            Mask mask = maskDB.GetMask(index);
            MaskItemUI maskUIItem = GetItemUI(index);

            if (_playerDataService.CanSpendCoins(mask.price))
            {
                _playerDataService.SpendCoins(mask.price);
                GameSharedUI.Instance.UpdateCoinsUIText();
                maskDB.PurchaseMask(index);
                maskUIItem.SetItemAsPurchased();
                maskUIItem.OnItemSelect(index, OnItemSelected);
                _playerDataService.AddPurchasedMask(index);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Not Enough Coins!");
#endif
            }
        }
    }
}