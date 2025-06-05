using YG;

namespace Core.Services.PlayerData
{
    public class YandexPlayerDataService : BasePlayerDataService, IPlayerDataService
    {        
        public override PlayerData Load()
        {
            PlayerData = new PlayerData();
            PlayerData.Music = YG2.saves.Music;
            PlayerData.Sound = YG2.saves.Sound;
            PlayerData.RemovedAds = YG2.saves.RemovedAds;
            PlayerData.Coins = YG2.saves.Coins;
            PlayerData.ReviewShown = YG2.saves.ReviewShown;
            PlayerData.NormalGamesPlayed = YG2.saves.NormalGamesPlayed;
            PlayerData.SurvivalGamesPlayed = YG2.saves.SurvivalGamesPlayed;
            PlayerData.BestScore = YG2.saves.BestScore;
            PlayerData.SelectedHatId = YG2.saves.SelectedHatId;
            PlayerData.SelectedMaskId = YG2.saves.SelectedMaskId;
            PlayerData.SelectedLocationId = YG2.saves.SelectedLocationId;
            PlayerData.PurchasedHatsIds = YG2.saves.PurchasedHatsIds;
            PlayerData.PurchasedMasksIds = YG2.saves.PurchasedMasksIds;
            PlayerData.PurchasedLocationsIds = YG2.saves.PurchasedLocationsIds;
            PlayerData.SelectedHat = YG2.saves.SelectedHat;
            PlayerData.SelectedMask = YG2.saves.SelectedMask;
            PlayerData.SelectedLocation = YG2.saves.SelectedLocation;
            return PlayerData;
        }

        public override void Save(PlayerData playerData)
        {
            YG2.saves.Music = playerData.Music;
            YG2.saves.Sound = playerData.Sound;
            YG2.saves.RemovedAds = playerData.RemovedAds;
            YG2.saves.Coins = playerData.Coins;
            YG2.saves.ReviewShown = playerData.ReviewShown;
            YG2.saves.NormalGamesPlayed = playerData.NormalGamesPlayed;
            YG2.saves.SurvivalGamesPlayed = playerData.SurvivalGamesPlayed;
            YG2.saves.BestScore = playerData.BestScore;
            YG2.saves.SelectedHatId = playerData.SelectedHatId;
            YG2.saves.SelectedMaskId = playerData.SelectedMaskId;
            YG2.saves.SelectedLocationId = playerData.SelectedLocationId;
            YG2.saves.PurchasedHatsIds = playerData.PurchasedHatsIds;
            YG2.saves.PurchasedMasksIds = playerData.PurchasedMasksIds;
            YG2.saves.PurchasedLocationsIds = playerData.PurchasedLocationsIds;
            YG2.saves.SelectedHat = playerData.SelectedHat;
            YG2.saves.SelectedMask = playerData.SelectedMask;
            YG2.saves.SelectedLocation = playerData.SelectedLocation;
            YG2.SaveProgress();
        }
    }
}