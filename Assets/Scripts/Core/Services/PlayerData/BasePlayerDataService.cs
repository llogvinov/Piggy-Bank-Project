using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public abstract class BasePlayerDataService : IPlayerDataService
    {
        public abstract PlayerData Load();
        
        public abstract void Save(PlayerData playerData);

        public void RemoveAds() => Load().RemovedAds = true;

        public bool IsRemovedAds() => Load().RemovedAds;

        public Hat GetSelectedHat() => Load().SelectedHat;

        public Mask GetSelectedMask() => Load().SelectedMask;

        public Location GetSelectedLocation() => Load().SelectedLocation;

        public void SetSelectedHat(Hat hat, int hatIndex)
        {
            var playerData = Load();
            playerData.SelectedHat = hat;
            playerData.SelectedHatId = hatIndex;
            Save(playerData);
        }

        public void SetSelectedMask(Mask mask, int maskIndex)
        {
            var playerData = Load();
            playerData.SelectedMask = mask;
            playerData.SelectedMaskId = maskIndex;
            Save(playerData);
        }

        public void SetSelectedLocation(Location location, int locationIndex)
        {
            var playerData = Load();
            playerData.SelectedLocation = location;
            playerData.SelectedLocationId = locationIndex;
            Save(playerData);
        }

        public int GetSelectedHatIndex() => Load().SelectedHatId;

        public int GetSelectedMaskIndex() => Load().SelectedMaskId;

        public int GetSelectedLocationIndex() => Load().SelectedLocationId;

        public void SetSelectedHatIndex(int newHatIndex) => Load().SelectedHatId = newHatIndex;

        public void SetSelectedMaskIndex(int newMaskIndex) => Load().SelectedMaskId = newMaskIndex;

        public void SetSelectedLocationIndex(int newLocationIndex) => Load().SelectedLocationId = newLocationIndex;

        public int GetPlayerRecord() => Load().NormalModeRecord;

        public void SetNewRecord(int newRecord)
        {
            var playerData = Load();
            if (newRecord > playerData.NormalModeRecord)
                playerData.NormalModeRecord = newRecord;
        }

        public long GetNormalGamesPlayed() => Load().NormalGamesPlayed;

        public long GetSurvivalGamesPlayed() => Load().SurvivalGamesPlayed;

        public void IncrementNormalGamesPlayed()
        {
            var playerData = Load();
            playerData.NormalGamesPlayed++;
            Save(playerData);
        }

        public void IncrementSurvivalGamesPlayed()
        {
            var playerData = Load();
            playerData.SurvivalGamesPlayed++;
            Save(playerData);
        }

        public int GetCoins() => Load().Coins;

        public void AddCoins(int amount)
        {
            var playerData = Load();
            playerData.Coins += amount;
            Save(playerData);
        }

        public bool CanSpendCoins(int amount) => Load().Coins >= amount;

        public void SpendCoins(int amount)
        {
            var playerData = Load();
            playerData.Coins -= amount;
            Save(playerData);
        }

        public void AddPurchasedHat(int hatIndex)
        {
            var playerData = Load();
            playerData.PurchasedHatsIds.Add(hatIndex);
            Save(playerData);
        }

        public List<int> GetAllPurchasedHats() => Load().PurchasedHatsIds;

        public int GetPurchasedHat(int hatIndex) => Load().PurchasedHatsIds[hatIndex];

        public void AddPurchasedMask(int maskIndex)
        {
            var playerData = Load();
            playerData.PurchasedMasksIds.Add(maskIndex);
            Save(playerData);
        }

        public List<int> GetAllPurchasedMasks() => Load().PurchasedMasksIds;

        public int GetPurchasedMask(int maskIndex) => Load().PurchasedMasksIds[maskIndex];

        public void AddPurchasedLocation(int locationIndex)
        {
            var playerData = Load();
            playerData.PurchasedLocationsIds.Add(locationIndex);
            Save(playerData);
        }

        public List<int> GetAllPurchasedLocations() => Load().PurchasedLocationsIds;

        public int GetPurchasedLocation(int locationIndex) => Load().PurchasedLocationsIds[locationIndex];
    }
}