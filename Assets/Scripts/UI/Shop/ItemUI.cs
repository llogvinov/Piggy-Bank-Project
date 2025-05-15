using UnityEngine.Events;

public interface IItemUI
{
    void SetItemAsPurchased();
    void OnItemPurchase(int itemIndex, UnityAction<int> action);
    void OnItemSelect(int itemIndex, UnityAction<int> action);
    void SelectItem();
    void DeselectItem();
}
