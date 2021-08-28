using UnityEngine;

[CreateAssetMenu(fileName ="HatShopDatabase", 
    menuName = "Shopping/Hats shop database")]
public class HatShopDatabase : ScriptableObject
{
    public Hat[] hats;

    public int HatsCount => hats.Length;

    public Hat GetHat(int index) => hats[index];

    public void PurchaseHat(int index) => hats[index].isPurchased = true;

}
