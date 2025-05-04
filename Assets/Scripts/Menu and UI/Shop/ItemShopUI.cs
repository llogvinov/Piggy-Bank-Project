using System;

public interface IItemShopUI
{
    event Action UIGenerated;
    void GenerateShopItemUI();
    void OnItemSelected(int index);
    void SelectItemUI(int itemIndex);
    void ChangeItemSkin();
    void OnItemPurchased(int index);
}
