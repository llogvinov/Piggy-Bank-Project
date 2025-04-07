using YG;

namespace Core.Services.PlayerData
{
    public class YandexPlayerDataService : BasePlayerDataService, IPlayerDataService
    {
        public override PlayerData Load()
        {
            var playerData = new PlayerData();
            playerData.RemovedAds = YG2.saves.RemovedAds;
            playerData.Coins = YG2.saves.Coins;
            playerData.NormalGamesPlayed = YG2.saves.NormalGamesPlayed;
            playerData.SurvivalGamesPlayed = YG2.saves.SurvivalGamesPlayed;
            playerData.NormalModeRecord = YG2.saves.NormalModeRecord;
            playerData.SelectedHatId = YG2.saves.SelectedHatId;
            playerData.SelectedMaskId = YG2.saves.SelectedMaskId;
            playerData.SelectedLocationId = YG2.saves.SelectedLocationId;
            playerData.PurchasedHatsIds = YG2.saves.PurchasedHatsIds;
            playerData.PurchasedMasksIds = YG2.saves.PurchasedMasksIds;
            playerData.PurchasedLocationsIds = YG2.saves.PurchasedLocationsIds;
            playerData.SelectedHat = YG2.saves.SelectedHat;
            playerData.SelectedMask = YG2.saves.SelectedMask;
            playerData.SelectedLocation = YG2.saves.SelectedLocation;
            return playerData;
        }

        public override void Save(PlayerData playerData)
        {
            YG2.saves.RemovedAds = playerData.RemovedAds;
            YG2.saves.Coins = playerData.Coins;
            YG2.saves.NormalGamesPlayed = playerData.NormalGamesPlayed;
            YG2.saves.SurvivalGamesPlayed = playerData.SurvivalGamesPlayed;
            YG2.saves.NormalModeRecord = playerData.NormalModeRecord;
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