using System;
using System.Collections.Generic;
using Core;
using Core.Services.PlayerData;
using Main;
using UnityEngine;

namespace UI
{
    public class UIHatShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;

        [Header("UI Elements")]
        [SerializeField] private Transform _container;
        [SerializeField] private HatItemUI _uiItemPrefab;
        [SerializeField] private CoinsPanel _coinsPanel;
        [Space(20f)]
        [SerializeField] private HatShopDatabase _hatDB;

        private HatItemUI _currentSelectedItem;
        private IPlayerDataService _playerDataService;
        private Dictionary<Hat, HatItemUI> _uiHatDict;

        private PlayerSkinCreator _skinCreator;
        private PlayerSkinCreator SkinCreator => _skinCreator ?? (_skinCreator = FindObjectOfType<PlayerSkinCreator>());

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
            _uiHatDict = new Dictionary<Hat, HatItemUI>();
            for (int i = 0; i < _hatDB.SortedHats.Count; i++)
            {
                var hat = _hatDB.SortedHats[i];
                var uiItem = Instantiate(_uiItemPrefab, _container);
                _uiHatDict.Add(hat, uiItem);
                uiItem.gameObject.name = $"Item {i} {hat.LocalizationId}";
                uiItem.Initialize(hat);

                if (_playerDataService.PlayerData.PurchasedHatsIds.Contains(hat.Id))
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(hat.Id, OnItemSelected);
                }
                else
                {
                    uiItem.SetItemAsNotPurchased();
                    uiItem.OnItemPurchase(hat.Id, OnItemPurchased);
                }

                if (_playerDataService.PlayerData.SelectedHatId == hat.Id)
                {
                    SelectItemUI(hat.Id);
                }
            }

            UIGenerated?.Invoke();
        }

        public void ChangeItemSkin() => 
            SkinCreator.SetHat();

        public void OnItemSelected(int hatId)
        {
            var hat = _hatDB.GetHatById(hatId);
            _playerDataService.SetSelectedHat(hat, hat.Id);
            SelectItemUI(hatId);
            ChangeItemSkin();
        }

        public void SelectItemUI(int hatId)
        {
            if (_currentSelectedItem != null)
            {
                _currentSelectedItem.DeselectItem();
            }

            var hatItemUI = GetHatItemUI(hatId);
            if (hatItemUI != null)
            {
                _currentSelectedItem = hatItemUI;
                hatItemUI.SelectItem();
            }
        }

        public void OnItemPurchased(int hatId)
        {
            var hat = _hatDB.GetHatById(hatId);
            var hatItemUI = GetHatItemUI(hatId);

            if (_playerDataService.CanSpendCoins(hat.Price))
            {
                _playerDataService.SpendCoins(hat.Price);
                _playerDataService.AddPurchasedHat(hatId);
                _coinsPanel.UpdateUI();
                hatItemUI.SetItemAsPurchased();
                hatItemUI.OnItemSelect(hatId, OnItemSelected);
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }

        public HatItemUI GetHatItemUI(int hatId)
        {
            var hat = _hatDB.GetHatById(hatId);
            var uiItem = _uiHatDict[hat];
            return uiItem;
        }

        public int GetElementChildIndex(int hatId)
        {
            var hat = _hatDB.GetHatById(hatId);
            var uiItem = _uiHatDict[hat];
            return uiItem.transform.GetSiblingIndex();
        }
    }
}