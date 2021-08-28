using UnityEngine;

[CreateAssetMenu(fileName = "MaskShopDatabase",
    menuName = "Shopping/Mask shop database")]
public class MaskShopDatabase : ScriptableObject
{
    public Mask[] mask;

    public int MasksCount => mask.Length;

    public Mask GetMask(int index) => mask[index];

    public void PurchaseMask(int index) => mask[index].isPurchased = true;

}
