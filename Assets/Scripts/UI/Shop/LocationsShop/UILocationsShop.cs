using System;
using System.Collections.Generic;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace UI.LocationsShop
{
    public class UILocationsShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;

        [Header("UI Elements")]
        [SerializeField] private Transform _container;
        [SerializeField] private LocationItemUI _uiItemPrefab;
        [Space(20f)]
        [SerializeField] private LocationShopDatabase _locationDB;

        private LocationItemUI _currentSelectedItem;
        private IPlayerDataService _playerDataService;
        private Dictionary<Location, LocationItemUI> _uiLocationDict;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            GenerateShopItemUI();
        }

        public void GenerateShopItemUI()
        {
            _uiLocationDict = new Dictionary<Location, LocationItemUI>();
            for (int i = 0; i < _locationDB.SortedLocations.Count; i++)
            {
                var location = _locationDB.SortedLocations[i];
                var uiItem = Instantiate(_uiItemPrefab, _container);
                _uiLocationDict.Add(location, uiItem);
                uiItem.gameObject.name = $"Item {i} {location.LocalizationId}";
                uiItem.Initialize(location);

                if (_playerDataService.PlayerData.PurchasedLocationsIds.Contains(location.Id))
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(location.Id, OnItemSelected);
                }
                else
                {
                    uiItem.SetItemAsNotPurchased();
                    uiItem.OnItemPurchase(location.Id, OnItemPurchased);
                }

                if (_playerDataService.PlayerData.SelectedLocationId == location.Id)
                {
                    SelectItemUI(location.Id);
                }
            }

            UIGenerated?.Invoke();
        }

        public void ChangeItemSkin() { }

        public void OnItemSelected(int locationId)
        {
            var location = _locationDB.GetLocationById(locationId);
            _playerDataService.SetSelectedLocation(location, location.Id);
            SelectItemUI(locationId);
            ChangeItemSkin();
        }

        public void SelectItemUI(int locationId)
        {
            if (_currentSelectedItem != null)
            {
                _currentSelectedItem.DeselectItem();
            }

            var locationItemUI = GetLocationItemUI(locationId);
            if (locationItemUI != null)
            {
                _currentSelectedItem = locationItemUI;
                locationItemUI.SelectItem();
            }
        }

        public void OnItemPurchased(int locationId)
        {
            var location = _locationDB.GetLocationById(locationId);
            var locationItemUI = GetLocationItemUI(locationId);

            if (_playerDataService.CanSpendCoins(location.Price))
            {
                _playerDataService.SpendCoins(location.Price);
                _playerDataService.AddPurchasedLocation(locationId);
                locationItemUI.SetItemAsPurchased();
                locationItemUI.OnItemSelect(locationId, OnItemSelected);
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }

        public LocationItemUI GetLocationItemUI(int locationId)
        {
            var location = _locationDB.GetLocationById(locationId);
            var uiItem = _uiLocationDict[location];
            return uiItem;
        }

        public int GetElementChildIndex(int locationId)
        {
            var location = _locationDB.GetLocationById(locationId);
            var uiItem = _uiLocationDict[location];
            return uiItem.transform.GetSiblingIndex();
        }
    }
}