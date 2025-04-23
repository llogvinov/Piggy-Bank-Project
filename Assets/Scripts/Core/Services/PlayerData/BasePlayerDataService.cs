using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public abstract class BasePlayerDataService : IPlayerDataService
    {
        public PlayerData PlayerData { get; protected set; }
        
        public abstract PlayerData Load();
        
        public abstract void Save(PlayerData playerData);
        
        public bool GetMusic() => PlayerData.Music;

        public void SetMusic(bool value)
        {
            PlayerData.Music = value;
            Save(PlayerData);
        }

        public bool GetSound() => PlayerData.Sound;

        public void SetSound(bool value)
        {
            PlayerData.Sound = value;
            Save(PlayerData);
        }

        public void RemoveAds() => PlayerData.RemovedAds = true;

        public bool IsRemovedAds() => PlayerData.RemovedAds;

        public Hat GetSelectedHat() => PlayerData.SelectedHat;

        public Mask GetSelectedMask() => PlayerData.SelectedMask;

        public Location GetSelectedLocation() => PlayerData.SelectedLocation;

        public void SetSelectedHat(Hat hat, int hatIndex)
        {
            PlayerData.SelectedHat = hat;
            PlayerData.SelectedHatId = hatIndex;
            Save(PlayerData);
        }

        public void SetSelectedMask(Mask mask, int maskIndex)
        {
            PlayerData.SelectedMask = mask;
            PlayerData.SelectedMaskId = maskIndex;
            Save(PlayerData);
        }

        public void SetSelectedLocation(Location location, int locationIndex)
        {
            PlayerData.SelectedLocation = location;
            PlayerData.SelectedLocationId = locationIndex;
            Save(PlayerData);
        }

        public int GetSelectedHatIndex() => PlayerData.SelectedHatId;

        public int GetSelectedMaskIndex() => PlayerData.SelectedMaskId;

        public int GetSelectedLocationIndex() => PlayerData.SelectedLocationId;

        public void SetSelectedHatIndex(int newHatIndex) => PlayerData.SelectedHatId = newHatIndex;

        public void SetSelectedMaskIndex(int newMaskIndex) => PlayerData.SelectedMaskId = newMaskIndex;

        public void SetSelectedLocationIndex(int newLocationIndex) => PlayerData.SelectedLocationId = newLocationIndex;

        public int GetPlayerRecord() => PlayerData.NormalModeRecord;

        public void SetNewRecord(int newRecord)
        {
            if (newRecord > GetPlayerRecord())
                PlayerData.NormalModeRecord = newRecord;
        }

        public long GetNormalGamesPlayed() => PlayerData.NormalGamesPlayed;

        public long GetSurvivalGamesPlayed() => PlayerData.SurvivalGamesPlayed;

        public void IncrementNormalGamesPlayed()
        {
            PlayerData.NormalGamesPlayed++;
            Save(PlayerData);
        }

        public void IncrementSurvivalGamesPlayed()
        {
            PlayerData.SurvivalGamesPlayed++;
            Save(PlayerData);
        }

        public int GetCoins() => PlayerData.Coins;

        public void AddCoins(int amount)
        {
            PlayerData.Coins += amount;
            Save(PlayerData);
        }

        public bool CanSpendCoins(int amount) => PlayerData.Coins >= amount;

        public void SpendCoins(int amount)
        {
            PlayerData.Coins -= amount;
            Save(PlayerData);
        }

        public void AddPurchasedHat(int hatIndex)
        {
            PlayerData.PurchasedHatsIds.Add(hatIndex);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedHats() => PlayerData.PurchasedHatsIds;

        public int GetPurchasedHat(int hatIndex) => PlayerData.PurchasedHatsIds[hatIndex];

        public void AddPurchasedMask(int maskIndex)
        {
            PlayerData.PurchasedMasksIds.Add(maskIndex);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedMasks() => PlayerData.PurchasedMasksIds;

        public int GetPurchasedMask(int maskIndex) => PlayerData.PurchasedMasksIds[maskIndex];

        public void AddPurchasedLocation(int locationIndex)
        {
            PlayerData.PurchasedLocationsIds.Add(locationIndex);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedLocations() => PlayerData.PurchasedLocationsIds;

        public int GetPurchasedLocation(int locationIndex) => PlayerData.PurchasedLocationsIds[locationIndex];
    }
}