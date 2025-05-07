using System;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace UI.LocationsShop
{
    public class UILocationsShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;

        [Header("UI Elements")]
        [SerializeField] private Transform ShopItemsContainer;
        [SerializeField] private GameObject itemPrefab;
        [Space(20f)]
        [SerializeField] private LocationShopDatabase locationDB;

        private int newSelectedLocationIndex;
        private int previousSelectedLocationIndex;
        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            GenerateShopItemUI();
            SelectItemUI(_playerDataService.GetSelectedLocationIndex());
            ChangeItemSkin();
        }

        public void GenerateShopItemUI()
        {
            for (int i = 0; i < _playerDataService.GetAllPurchasedLocations().Count; i++)
            {
                int purchaseLocationIndex = _playerDataService.GetPurchasedLocation(i);
                locationDB.PurchaseLocation(purchaseLocationIndex);
            }

            for (int i = 0; i < locationDB.LocationsCount; i++)
            {
                Location location = locationDB.GetLocation(i);
                LocationItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<LocationItemUI>();

                uiItem.gameObject.name = "Item" + i + "-" + location.name;

                uiItem.SetLocationName(location.name);
                uiItem.SetLocationImages(location.sky, location.ground, location.trees, location.mountain);
                uiItem.SetLocationPrice(location.price);

                if (location.isPurchased)
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(i, OnItemSelected);
                }
                else
                {
                    uiItem.SetLocationPrice(location.price);
                    uiItem.OnItemPurchase(i, OnItemPurchased);
                }
            }

            UIGenerated?.Invoke();
        }

        public void OnItemSelected(int index)
        {
            SelectItemUI(index);
            _playerDataService.SetSelectedLocation(locationDB.GetLocation(index), index);
            ChangeItemSkin();
        }

        public void ChangeItemSkin()
        {
            Location location = _playerDataService.GetSelectedLocation();
        }

        public void SelectItemUI(int itemIndex)
        {
            previousSelectedLocationIndex = newSelectedLocationIndex;
            newSelectedLocationIndex = itemIndex;

            LocationItemUI previousUiItem = GetItemUI(previousSelectedLocationIndex);
            LocationItemUI newUiItem = GetItemUI(newSelectedLocationIndex);

            previousUiItem.DeselectItem();
            newUiItem.SelectItem();
        }

        private LocationItemUI GetItemUI(int index) => ShopItemsContainer.GetChild(index).GetComponent<LocationItemUI>();

        public void OnItemPurchased(int index)
        {
            Location location = locationDB.GetLocation(index);
            LocationItemUI locationUIItem = GetItemUI(index);

            if (_playerDataService.CanSpendCoins(location.price))
            {
                _playerDataService.SpendCoins(location.price);
                locationDB.PurchaseLocation(index);
                locationUIItem.SetItemAsPurchased();
                locationUIItem.OnItemSelect(index, OnItemSelected);

                _playerDataService.AddPurchasedLocation(index);
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }
    }
}