using System;

public interface IItemShopUI
{
    event Action UIGenerated;
    void GenerateShopItemUI();
    void ChangeItemSkin();
    void OnItemSelected(int id);
    void SelectItemUI(int itemId);
    void OnItemPurchased(int id);
}
