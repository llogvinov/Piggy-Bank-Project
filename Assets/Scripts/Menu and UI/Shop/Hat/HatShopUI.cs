using UnityEngine;
using UnityEngine.UI;

public class HatShopUI : MonoBehaviour, IItemShopUI
{
    [Header("Layout Settings")]
    [SerializeField] private float itemSpacing = 5f;
    [SerializeField] private float itemHeight;

    [Header("UI Elements")]
    [SerializeField] private Transform ShopItemsContainer;
    [SerializeField] private GameObject itemPrefab;
    [Space(20f)]
    [SerializeField] private HatShopDatabase hatDB;

    [Header("Main Menu")]
    [SerializeField] private SpriteRenderer mainMenuHatImage;

    private int newSelectedHatIndex;
    private int previousSelectedHatIndex;

    private void Start()
    {
        GenerateShopItemUI();

        //Set selected hat in the playerDataManager
        SetSelectedItem();

        //Select UI item
        SelectItemUI(GameDataManager.GetSelectedHatIndex());

        //Update player skin
        ChangeItemSkin();
    }

    public void SetSelectedItem()
    {
        //Get Saved index
        int index = GameDataManager.GetSelectedHatIndex();

        //Set selected index
        GameDataManager.SetSelectedHat(hatDB.GetHat(index), index);
    }

    //Generate UI Shop Item
    public void GenerateShopItemUI()
    {
        //Loop through save purchased items and
        //make them purchased in the Database array
        for (int i = 0; i < GameDataManager.GetAllPurchasedHats().Count; i++)
        {
            int purchaseHatIndex = GameDataManager.GetPurchasedHat(i);
            hatDB.PurchaseHat(purchaseHatIndex);
        }

        //Delete item template after calculating item's height
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();

        //Generate Items
        for (int i = 0; i < hatDB.HatsCount; i++)
        {
            Hat hat = hatDB.GetHat(i);
            HatItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<HatItemUI>();

            //Move item to its position
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            //Set item name in Hierarchy
            uiItem.gameObject.name = "Item" + i + "-" + hat.name;

            //Add information to the UI (one item)
            uiItem.SetHatName(hat.name);
            uiItem.SetHatImage(hat.image);
            uiItem.SetHatPrice(hat.price);

            if (i == 0)
            {
                uiItem.SetHatImageOpacity();
            }
            
            if (hat.isPurchased)
            {
                //Hat is purchased
                uiItem.SetItemAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            } 
            else
            {
                //Hat is not purchased yet
                uiItem.SetHatPrice(hat.price);
                uiItem.OnItemPurchase(i, OnItemPurchased);
            }

            //ResizeItemsContainer
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((itemHeight + itemSpacing) * hatDB.HatsCount + itemSpacing);
        }
    }

    public void ChangeItemSkin()
    {
        Hat hat = GameDataManager.GetSelectedHat();
        mainMenuHatImage.sprite = hat.image;
    }

    public void OnItemSelected(int index)
    {
        //Select item in the UI
        SelectItemUI(index);

        //Save Data
        GameDataManager.SetSelectedHat(hatDB.GetHat(index), index);
        
        //Change hat skin
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

        if (GameDataManager.CanSpendCoins(hat.price))
        {
            //Proceed with the purchase operation
            GameDataManager.SpendCoins(hat.price);
            GameSharedUI.Instance.UpdateCoinsUIText();
            hatDB.PurchaseHat(index);
            hatUIItem.SetItemAsPurchased();
            hatUIItem.OnItemSelect(index, OnItemSelected);

            //Add purchased data on Shop Data
            GameDataManager.AddPurchasedHat(index);
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
