using System;
using System.Collections.Generic;
using Core;
using Core.Services.PlayerData;
using Main;
using UnityEngine;

namespace UI
{
    public class UIMaskShop : MonoBehaviour, IItemShopUI
    {
        public event Action UIGenerated;

        [Header("UI Elements")]
        [SerializeField] private Transform _container;
        [SerializeField] private MaskItemUI _uiItemPrefab;
        [SerializeField] private CoinsPanel _coinsPanel;
        [Space(20f)]
        [SerializeField] private MaskShopDatabase _maskDB;

        private MaskItemUI _currentSelectedItem;
        private IPlayerDataService _playerDataService;
        private Dictionary<Mask, MaskItemUI> _uiMaskDict;

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
            _uiMaskDict = new Dictionary<Mask, MaskItemUI>();
            for (int i = 0; i < _maskDB.SortedMasks.Count; i++)
            {
                var mask = _maskDB.SortedMasks[i];
                var uiItem = Instantiate(_uiItemPrefab, _container);
                _uiMaskDict.Add(mask, uiItem);
                uiItem.gameObject.name = $"Item {i} {mask.LocalizationId}";
                uiItem.Initialize(mask);

                if (_playerDataService.PlayerData.PurchasedMasksIds.Contains(mask.Id))
                {
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(mask.Id, OnItemSelected);
                }
                else
                {
                    uiItem.SetItemAsNotPurchased();
                    uiItem.OnItemPurchase(mask.Id, OnItemPurchased);
                }

                if (_playerDataService.PlayerData.SelectedMaskId == mask.Id)
                {
                    SelectItemUI(mask.Id);
                }
            }

            UIGenerated?.Invoke();
        }

        public void ChangeItemSkin() => 
            SkinCreator.SetMask();

        public void OnItemSelected(int maskId)
        {
            var mask = _maskDB.GetMaskById(maskId);
            _playerDataService.SetSelectedMask(mask, mask.Id);
            SelectItemUI(maskId);
            ChangeItemSkin();
        }

        public void SelectItemUI(int maskId)
        {
            if (_currentSelectedItem != null)
            {
                _currentSelectedItem.DeselectItem();
            }

            var maskItemUI = GetMaskItemUI(maskId);
            if (maskItemUI != null)
            {
                _currentSelectedItem = maskItemUI;
                maskItemUI.SelectItem();
            }
        }

        public void OnItemPurchased(int maskId)
        {
            var mask = _maskDB.GetMaskById(maskId);
            var maskItemUI = GetMaskItemUI(maskId);

            if (_playerDataService.CanSpendCoins(mask.Price))
            {
                _playerDataService.SpendCoins(mask.Price);
                _playerDataService.AddPurchasedMask(maskId);
                _coinsPanel.UpdateUI();
                maskItemUI.SetItemAsPurchased();
                maskItemUI.OnItemSelect(maskId, OnItemSelected);
            }
            else
            {
                Debug.Log("Not Enough Coins!");
            }
        }

        public MaskItemUI GetMaskItemUI(int maskId)
        {
            var mask = _maskDB.GetMaskById(maskId);
            var uiItem = _uiMaskDict[mask];
            return uiItem;
        }

        public int GetElementChildIndex(int maskId)
        {
            var mask = _maskDB.GetMaskById(maskId);
            var uiItem = _uiMaskDict[mask];
            return uiItem.transform.GetSiblingIndex();
        }
    }
}