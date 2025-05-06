using System;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace UI
{
    public class UIHatShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;
        
        [Header("UI Elements")]
        [SerializeField] private Transform ShopItemsContainer;
        [SerializeField] private GameObject itemPrefab;
        [Space(20f)]
        [SerializeField] private HatShopDatabase hatDB;

        [Header("Main Menu")]
        [SerializeField] private SpriteRenderer mainMenuHatImage;

        private int newSelectedHatIndex;
        private int previousSelectedHatIndex;
        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            GenerateShopItemUI();
            SelectItemUI(_playerDataService.GetSelectedHatIndex());
            //ChangeItemSkin();
        }

        public void GenerateShopItemUI()
        {
            for (int i = 0; i < _playerDataService.GetAllPurchasedHats().Count; i++)
            {
                int purchaseHatId = _playerDataService.GetPurchasedHat(i);
                hatDB.PurchaseHat(purchaseHatId);
            }

            for (int i = 0; i < hatDB.HatsCount; i++)
            {
                Hat hat = hatDB.GetHat(i);
                HatItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<HatItemUI>();

                uiItem.gameObject.name = "Item" + i + "-" + hat.name;

                uiItem.SetHatName(hat.name);
                uiItem.SetHatImage(hat.image);
                uiItem.SetHatPrice(hat.price);

                if (i == 0)
                {
                    uiItem.SetHatImageOpacity();
                }

                if (hat.isPurchased)
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(i, OnItemSelected);
                }
                else
                {
                    uiItem.SetHatPrice(hat.price);
                    uiItem.OnItemPurchase(i, OnItemPurchased);
                }
            }

            UIGenerated?.Invoke();
        }

        public void ChangeItemSkin()
        {
            Hat hat = _playerDataService.GetSelectedHat();
            mainMenuHatImage.sprite = hat.image;
        }

        public void OnItemSelected(int index)
        {
            SelectItemUI(index);

            _playerDataService.SetSelectedHat(hatDB.GetHat(index), index);

            ChangeItemSkin();
        }

        public void SelectItemUI(int itemIndex)
        {
            previousSelectedHatIndex = newSelectedHatIndex;
            newSelectedHatIndex = itemIndex;

            HatItemUI previousUiItem = GetItemUI(previousSelectedHatIndex);
            HatItemUI newUiItem = GetItemUI(newSelectedHatIndex);

            previousUiItem.DeselectItem();
            newUiItem.SelectItem();
        }

        private HatItemUI GetItemUI(int index) => ShopItemsContainer.GetChild(index).GetComponent<HatItemUI>();

        public void OnItemPurchased(int index)
        {
            Hat hat = hatDB.GetHat(index);
            HatItemUI hatUIItem = GetItemUI(index);

            if (_playerDataService.CanSpendCoins(hat.price))
            {
                _playerDataService.SpendCoins(hat.price);
                hatDB.PurchaseHat(index);
                hatUIItem.SetItemAsPurchased();
                hatUIItem.OnItemSelect(index, OnItemSelected);

                _playerDataService.AddPurchasedHat(index);
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }
    }
}