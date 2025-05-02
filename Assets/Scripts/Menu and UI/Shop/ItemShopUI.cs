using System;

public interface IItemShopUI
{
    event Action UIGenerated;
    void SetSelectedItem();
    void GenerateShopItemUI();
    void OnItemSelected(int index);
    void SelectItemUI(int itemIndex);
    void ChangeItemSkin();
    void OnItemPurchased(int index);
}
