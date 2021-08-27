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
    public bool removedAds;
    public int coins;
    public long normalGamesPlayed;
    public long survivalGamesPlayed;
    public int normalModeRecord;
    public int selectedHatIndex;
    public int selectedMaskIndex;
    public int selectedLocationIndex;
}

public static class GameDataManager
{
    private static PlayerData playerData = new PlayerData();
    
    private static HatsShopData hatsShopData = new HatsShopData();
    private static MasksShopData masksShopData = new MasksShopData();
    private static LocationShopData locationsShopData = new LocationShopData();

    private static Hat selectedHat = new Hat();
    private static Mask selectedMask = new Mask();
    private static Location selectedLocation = new Location();

    static GameDataManager()
    {
        LoadPlayerData();
        LoadHatsShopData();
        LoadMasksShopData();
        LoadLocationsShopData();
    }

    //Player Data Methods --------------------------------------------

    #region Remove Ads
    public static void RemoveAds() => playerData.removedAds = true;

    public static bool IsRemovedAds() => playerData.removedAds;
    #endregion

    #region Shop Item getters/setters
    public static Hat GetSelectedHat() => selectedHat;

    public static Mask GetSelectedMask() => selectedMask;

    public static Location GetSelectedLocation() => selectedLocation;

    public static void SetSelectedHat(Hat hat, int hatIndex)
    {
        selectedHat = hat;
        playerData.selectedHatIndex = hatIndex;
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

    public static int GetSelectedHatIndex() => playerData.selectedHatIndex;

    public static int GetSelectedMaskIndex() => playerData.selectedMaskIndex;

    public static int GetSelectedLocationIndex() => playerData.selectedLocationIndex;

    public static void SetSelectedHatIndex(int newHatIndex) => playerData.selectedHatIndex = newHatIndex;

    public static void SetSelectedMaskIndex(int newMaskIndex) => playerData.selectedMaskIndex = newMaskIndex;

    public static void SetSelectedLocationIndex(int newLocationIndex) => playerData.selectedLocationIndex = newLocationIndex;
    #endregion

    #region Game
    public static int GetPlayerRecord() => playerData.normalModeRecord;

    public static void SetNewRecord(int newRecord)
    {
        if (newRecord > playerData.normalModeRecord)
            playerData.normalModeRecord = newRecord;
    }

    public static long GetNormalGamesPlayed() => playerData.normalGamesPlayed;

    public static long GetSurvivalGamesPlayed() => playerData.survivalGamesPlayed;

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
    #endregion

    #region Coins
    public static int GetCoins() => playerData.coins;

    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
        SavePlayerData();
    }

    public static bool CanSpendCoins(int amount) => (playerData.coins >= amount);

    public static void SpendCoins(int amount)
    {
        playerData.coins -= amount;
        SavePlayerData();
    }
    #endregion

    private static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }

    //Hats Shop Data Methods --------------------------------------------
    public static void AddPurchasedHat(int hatIndex)
    {
        hatsShopData.purchasedHatsIndexes.Add(hatIndex);
        SaveHatsShopData();
    }
    
    public static List<int> GetAllPurchasedHats() => hatsShopData.purchasedHatsIndexes;

    public static int GetPurchasedHat(int hatIndex) => hatsShopData.purchasedHatsIndexes[hatIndex];

    private static void SaveHatsShopData()
    {
        BinarySerializer.Save(hatsShopData, "hats-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadHatsShopData()
    {
        hatsShopData = BinarySerializer.Load<HatsShopData>("hats-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }

    // Masks Shop Data Methods --------------------------------------------
    public static void AddPurchasedMask(int maskIndex)
    {
        masksShopData.purchasedMasksIndexes.Add(maskIndex);
        SaveMasksShopData();
    }

    public static List<int> GetAllPurchasedMasks() => masksShopData.purchasedMasksIndexes;

    public static int GetPurchasedMask(int maskIndex) => masksShopData.purchasedMasksIndexes[maskIndex];

    private static void SaveMasksShopData()
    {
        BinarySerializer.Save(masksShopData, "masks-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadMasksShopData()
    {
        masksShopData = BinarySerializer.Load<MasksShopData>("masks-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }

    // Locations Shop Data Methods --------------------------------------------
    public static void AddPurchasedLocation(int locationIndex)
    {
        locationsShopData.purchasedLocatonsIndexes.Add(locationIndex);
        SaveLocationsShopData();
    }

    public static List<int> GetAllPurchasedLocations() => locationsShopData.purchasedLocatonsIndexes;

    public static int GetPurchasedLocation(int locationIndex) => locationsShopData.purchasedLocatonsIndexes[locationIndex];

    private static void SaveLocationsShopData()
    {
        BinarySerializer.Save(locationsShopData, "locations-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadLocationsShopData()
    {
        locationsShopData = BinarySerializer.Load<LocationShopData>("locations-shop-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }

}
