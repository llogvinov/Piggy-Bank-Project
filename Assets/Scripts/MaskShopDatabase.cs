using UnityEngine;

[CreateAssetMenu(fileName = "MaskShopDatabase",
    menuName = "Shopping/Mask shop database")]
public class MaskShopDatabase : ScriptableObject
{
    public Mask[] mask;

    public int MasksCount
    {
        get { return mask.Length; }
    }

    public Mask GetMask(int index)
    {
        return mask[index];
    }

    public void PurchaseMask(int index)
    {
        mask[index].isPurchased = true;
    }

}
