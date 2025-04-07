using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace UI
{
    public class UIMaskShop : MonoBehaviour, IItemShopUI
    {
        [Header("Layout Settings")]
        [SerializeField] private float itemSpacing = 5f;
        [SerializeField] private float itemHeight;

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

            //Set selected mask in the playerDataManager
            SetSelectedItem();

            //Select UI item
            SelectItemUI(_playerDataService.GetSelectedMaskIndex());

            //Update player skin
            ChangeItemSkin();
        }

        public void SetSelectedItem()
        {
            //Get Saved index
            int index = _playerDataService.GetSelectedMaskIndex();

            //Set selected index
            _playerDataService.SetSelectedMask(maskDB.GetMask(index), index);
        }

        //Generate UI Shop Item
        public void GenerateShopItemUI()
        {
            //Loop through save purchased items and
            //make them purchased in the Database aaray
            for (int i = 0; i < _playerDataService.GetAllPurchasedMasks().Count; i++)
            {
                int purchaseCharacterIndex = _playerDataService.GetPurchasedMask(i);
                maskDB.PurchaseMask(purchaseCharacterIndex);
            }

            //Delete item template after calculating item's height
            itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            Destroy(ShopItemsContainer.GetChild(0).gameObject);
            ShopItemsContainer.DetachChildren();

            //Generate Items
            for (int i = 0; i < maskDB.MasksCount; i++)
            {
                Mask mask = maskDB.GetMask(i);
                MaskItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<MaskItemUI>();

                //Move item to its position
                uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

                //Set item name in Hierarchy
                uiItem.gameObject.name = "Item" + i + "-" + mask.name;

                //Add information to the UI (one item)
                uiItem.SetMaskName(mask.name);
                uiItem.SetMaskImage(mask.image);
                uiItem.SetMaskPrice(mask.price);

                if (i == 0)
                {
                    uiItem.SetMaskImageOpacity();
                }

                if (mask.isPurchased)
                {
                    //Mask is purchased
                    uiItem.SetItemAsPurchased();
                    uiItem.OnItemSelect(i, OnItemSelected);
                }
                else
                {
                    //Mask is not purchased yet
                    uiItem.SetMaskPrice(mask.price);
                    uiItem.OnItemPurchase(i, OnItemPurchased);
                }

                //ResizeItemsContainer
                ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                    Vector2.up * ((itemHeight + itemSpacing) * maskDB.MasksCount + itemSpacing);
            }
        }

        public void ChangeItemSkin()
        {
            Mask mask = _playerDataService.GetSelectedMask();
            mainMenuMaskImage.sprite = mask.image;
        }

        public void OnItemSelected(int index)
        {
            //Select item in the UI
            SelectItemUI(index);

            //Save Data
            _playerDataService.SetSelectedMask(maskDB.GetMask(index), index);

            //Change mask skin
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
                //Proceed with the purchase operation
                _playerDataService.SpendCoins(mask.price);
                GameSharedUI.Instance.UpdateCoinsUIText();
                maskDB.PurchaseMask(index);
                maskUIItem.SetItemAsPurchased();
                maskUIItem.OnItemSelect(index, OnItemSelected);

                //Add purchased data on Shop Data
                _playerDataService.AddPurchasedMask(index);
            }
            else
            {
                //Not enough coins
#if UNITY_EDITOR
                Debug.Log("Not Enough Coins!");
#endif
            }
        }
    }
}