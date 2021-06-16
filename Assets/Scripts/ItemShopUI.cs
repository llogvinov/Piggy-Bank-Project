using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemShopUI
{
    void SetSelectedItem();
    void GenerateShopItemUI();
    void OnItemSelected(int index);
    void SelectItemUI(int itemIndex);
    void ChangeItemSkin();
    void OnItemPurchased(int index);
}
