using UnityEngine;

[CreateAssetMenu(fileName = "LocationsShopDatabase",
    menuName = "Shopping/Locations shop database")]
public class LocationShopDatabase : ScriptableObject
{
    public Location[] location;

    public int LocationsCount
    {
        get { return location.Length; }
    }

    public Location GetLocation(int index)
    {
        return location[index];
    }

    public void PurchaseLocation(int index)
    {
        location[index].isPurchased = true;
    }

}