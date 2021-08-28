using UnityEngine;
using UnityEngine.UI;

public class LocationShopUI : MonoBehaviour, IItemShopUI
{
    [Header("Layout Settings")]
    [SerializeField] private float itemSpacing = 5f;
    [SerializeField] private float itemWidth;

    [Header("UI Elements")]
    [SerializeField] private Transform ShopItemsContainer;
    [SerializeField] private GameObject itemPrefab;
    [Space(20f)]
    [SerializeField] private LocationShopDatabase locationDB;

    private int newSelectedLocationIndex;
    private int previousSelectedLocationIndex;

    private void Start()
    {
        GenerateShopItemUI();

        //Set selected location in the playerDataManager
        SetSelectedItem();

        //Select UI item
        SelectItemUI(GameDataManager.GetSelectedLocationIndex());

        //Update player skin
        ChangeItemSkin();
    }

    //Generate UI Shop Item
    public void GenerateShopItemUI()
    {
        //Loop through save purchased items and
        //make them purchased in the Database array
        for (int i = 0; i < GameDataManager.GetAllPurchasedLocations().Count; i++)
        {
            int purchaseLocationIndex = GameDataManager.GetPurchasedLocation(i);
            locationDB.PurchaseLocation(purchaseLocationIndex);
        }

        //Delete item template after calculating item's width
        itemWidth = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();

        //Generate Items
        for (int i = 0; i < locationDB.LocationsCount; i++)
        {
            Location location = locationDB.GetLocation(i);
            LocationItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<LocationItemUI>();

            //Move item to its position
            uiItem.SetItemPosition(Vector2.right * i * (itemWidth + itemSpacing));

            //Set item name in Hierarchy
            uiItem.gameObject.name = "Item" + i + "-" + location.name;

            //Add information to the UI (one item)
            uiItem.SetLocationName(location.name);
            uiItem.SetLocationImages(location.sky, location.ground, location.trees, location.mountain);
            uiItem.SetLocationPrice(location.price);

            if (location.isPurchased)
            {
                //Location is purchased
                uiItem.SetItemAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //Location is not purchased yet
                uiItem.SetLocationPrice(location.price);
                uiItem.OnItemPurchase(i, OnItemPurchased);
            }

            //ResizeItemsContainer
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.right * (itemWidth + itemSpacing) * (locationDB.LocationsCount-1);
        }
    }

    public void SetSelectedItem()
    {
        //Get Saved index
        int index = GameDataManager.GetSelectedLocationIndex();

        //Set selected index
        GameDataManager.SetSelectedLocation(locationDB.GetLocation(index), index);
    }

    public void OnItemSelected(int index)
    {
        //Select item in the UI
        SelectItemUI(index);

        //Save Data
        GameDataManager.SetSelectedLocation(locationDB.GetLocation(index), index);

        //Change location
        ChangeItemSkin();
    }

    public void ChangeItemSkin()
    {
        Location location = GameDataManager.GetSelectedLocation();
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

        if (GameDataManager.CanSpendCoins(location.price))
        {
            //Proceed with the purchase operation
            GameDataManager.SpendCoins(location.price);
            GameSharedUI.Instance.UpdateCoinsUIText();
            locationDB.PurchaseLocation(index);
            locationUIItem.SetItemAsPurchased();
            locationUIItem.OnItemSelect(index, OnItemSelected);

            //Add purchased data on Shop Data
            GameDataManager.AddPurchasedLocation(index);
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
