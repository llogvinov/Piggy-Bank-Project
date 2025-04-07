using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public interface IPlayerDataService : IService
    {
        PlayerData Load();
        void Save(PlayerData playerData);
        void RemoveAds();
        bool IsRemovedAds();
        Hat GetSelectedHat();
        Mask GetSelectedMask();
        Location GetSelectedLocation();
        void SetSelectedHat(Hat hat, int hatIndex);
        void SetSelectedMask(Mask mask, int maskIndex);
        void SetSelectedLocation(Location location, int locationIndex);
        int GetSelectedHatIndex();
        int GetSelectedMaskIndex();
        int GetSelectedLocationIndex();
        void SetSelectedHatIndex(int newHatIndex);
        void SetSelectedMaskIndex(int newMaskIndex);
        void SetSelectedLocationIndex(int newLocationIndex);
        int GetPlayerRecord();
        void SetNewRecord(int newRecord);
        long GetNormalGamesPlayed();
        long GetSurvivalGamesPlayed();
        void IncrementNormalGamesPlayed();
        void IncrementSurvivalGamesPlayed();
        int GetCoins();
        void AddCoins(int amount);
        bool CanSpendCoins(int amount);
        void SpendCoins(int amount);
        void AddPurchasedHat(int hatIndex);
        List<int> GetAllPurchasedHats();
        int GetPurchasedHat(int hatIndex);
        void AddPurchasedMask(int maskIndex);
        List<int> GetAllPurchasedMasks();
        int GetPurchasedMask(int maskIndex);
        void AddPurchasedLocation(int locationIndex);
        List<int> GetAllPurchasedLocations();
        int GetPurchasedLocation(int locationIndex);
    }
}