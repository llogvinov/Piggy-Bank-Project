using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shop Data Holder
[System.Serializable] 
public class HatsShopData
{
    public List<int> purchasedHatsIndexes = new List<int>();
}

[System.Serializable]
public class MasksShopData
{
    public List<int> purchasedMasksIndexes = new List<int>();
}

[System.Serializable]
public class LocationShopData
{
    public List<int> purchasedLocatonsIndexes = new List<int>();
}

//Player Data Holder
[System.Serializable]
public class PlayerData
{
    public bool removedAds = false;
    public int coins = 0;
    public long normalGamesPlayed = 0;
    public long survivalGamesPlayed = 0;
    public int normalModeRecord = 0;
    public int selectedHatIndex = 0;
    public int selectedMaskIndex = 0;
    public int selectedLocationIndex = 0;
}

public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    
    static HatsShopData hatsShopData = new HatsShopData();
    static MasksShopData masksShopData = new MasksShopData();
    static LocationShopData locationsShopData = new LocationShopData();

    static Hat selectedHat = new Hat();
    static Mask selectedMask = new Mask();
    static Location selectedLocation = new Location();

    static GameDataManager()
    {
        LoadPlayerData();
        LoadHatsShopData();
        LoadMasksShopData();
        LoadLocationsShopData();
    }

    //Player Data Methods --------------------------------------------
    public static void RemoveAds()
    {
        playerData.removedAds = true;
    }

    public static bool IsRemovedAds()
    {
        return playerData.removedAds;
    }

    public static Hat GetSelectedHat()
    {
        return selectedHat;
    }

    public static Mask GetSelectedMask()
    {
        return selectedMask;
    }

    public static Location GetSelectedLocation()
    {
        return selectedLocation;
    }

    public static void SetSelectedHat(Hat hat, int hatInex)
    {
        selectedHat = hat;
        playerData.selectedHatIndex = hatInex;
        SavePlayerData();
    }

    public static void SetSelectedMask(Mask mask, int maskIndex)
    {
        selectedMask = mask;
        playerData.selectedMaskIndex = maskIndex;
        SavePlayerData();
    }

    public static void SetSelectedLocation(Location location, int locationIndex)
    {
        selectedLocation = location;
        playerData.selectedLocationIndex = locationIndex;
        SavePlayerData();
    }

    public static int GetSelectedHatIndex()
    {
        return playerData.selectedHatIndex;
    }

    public static int GetSelectedMaskIndex()
    {
        return playerData.selectedMaskIndex;
    }

    public static int GetSelectedLocationIndex()
    {
        return playerData.selectedLocationIndex;
    }

    public static void SetSelectedHatIndex(int newHatIndex)
    {
        playerData.selectedHatIndex = newHatIndex;
    }

    public static void SetSelectedMaskIndex(int newMaskIndex)
    {
        playerData.selectedMaskIndex = newMaskIndex;
    }

    public static void SetSelectedLocationIndex(int newLocationIndex)
    {
        playerData.selectedLocationIndex = newLocationIndex;
    }

    public static int GetPlayerRecord()
    {
        return playerData.normalModeRecord;
    }

    public static void SetNewRecord(int newRecord)
    {
        if (newRecord > playerData.normalModeRecord)
        {
            playerData.normalModeRecord = newRecord;
        }
    }

    public static long GetNormalGamesPlayed()
    {
        return playerData.normalGamesPlayed;
    }

    public static long GetSurvivalGamesPlayed()
    {
        return playerData.survivalGamesPlayed;
    }

    public static void IncrementNormalGamesPlayed()
    {
        playerData.normalGamesPlayed++;
        SavePlayerData();
    }

    public static void IncrementSurvivalGamesPlayed()
    {
        playerData.survivalGamesPlayed++;
        SavePlayerData();
    }

    public static int GetCoins()
    {
        return playerData.coins;
    }

    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
        SavePlayerData();
    }

    public static bool CanSpendCoins(int amount)
    {
        return (playerData.coins >= amount);
    }

    public static void SpendCoins(int amount)
    {
        playerData.coins -= amount;
        SavePlayerData();
    }

    private static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
        Debug.Log("Saved.");
    }

    private static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        Debug.Log("Loaded.");
    }

    //Hats Shop Data Methods --------------------------------------------
    public static void AddPurchasedHat(int hatIndex)
    {
        hatsShopData.purchasedHatsIndexes.Add(hatIndex);
        SaveHatsShopData();
    }
    
    public static List<int> GetAllPurchasedHats()
    {
        return hatsShopData.purchasedHatsIndexes;
    }

    public static int GetPurchasedHat(int hatIndex)
    {
        return hatsShopData.purchasedHatsIndexes[hatIndex];
    }

    private static void SaveHatsShopData()
    {
        BinarySerializer.Save(hatsShopData, "hats-shop-data.txt");
        Debug.Log("Saved.");
    }

    private static void LoadHatsShopData()
    {
        hatsShopData = BinarySerializer.Load<HatsShopData>("hats-shop-data.txt");
        Debug.Log("Loaded.");
    }

    // Masks Shop Data Methods --------------------------------------------
    public static void AddPurchasedMask(int maskIndex)
    {
        masksShopData.purchasedMasksIndexes.Add(maskIndex);
        SaveMasksShopData();
    }

    public static List<int> GetAllPurchasedMasks()
    {
        return masksShopData.purchasedMasksIndexes;
    }

    public static int GetPurchasedMask(int maskIndex)
    {
        return masksShopData.purchasedMasksIndexes[maskIndex];
    }

    private static void SaveMasksShopData()
    {
        BinarySerializer.Save(masksShopData, "masks-shop-data.txt");
        Debug.Log("Saved.");
    }

    private static void LoadMasksShopData()
    {
        masksShopData = BinarySerializer.Load<MasksShopData>("masks-shop-data.txt");
        Debug.Log("Loaded.");
    }

    // Locations Shop Data Methods --------------------------------------------
    public static void AddPurchasedLocation(int locationIndex)
    {
        locationsShopData.purchasedLocatonsIndexes.Add(locationIndex);
        SaveLocationsShopData();
    }

    public static List<int> GetAllPurchasedLocations()
    {
        return locationsShopData.purchasedLocatonsIndexes;
    }

    public static int GetPurchasedLocation(int locationIndex)
    {
        return locationsShopData.purchasedLocatonsIndexes[locationIndex];
    }

    private static void SaveLocationsShopData()
    {
        BinarySerializer.Save(locationsShopData, "locations-shop-data.txt");
        Debug.Log("Saved.");
    }

    private static void LoadLocationsShopData()
    {
        locationsShopData = BinarySerializer.Load<LocationShopData>("locations-shop-data.txt");
        Debug.Log("Loaded.");
    }

}
