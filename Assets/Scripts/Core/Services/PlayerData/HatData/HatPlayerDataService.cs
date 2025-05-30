using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public abstract partial class BasePlayerDataService : IPlayerDataService
    {
        public Hat GetSelectedHat() =>
            PlayerData.SelectedHat;

        public void SetSelectedHat(Hat hat, int hatId)
        {
            PlayerData.SelectedHat = hat;
            PlayerData.SelectedHatId = hatId;
            Save(PlayerData);
        }

        public int GetSelectedHatId() =>
            PlayerData.SelectedHatId;

        public void SetSelectedHatId(int hatId)
        {
            PlayerData.SelectedHatId = hatId;
            Save(PlayerData);
        }

        public void AddPurchasedHat(int hatId)
        {
            if (PlayerData.PurchasedHatsIds.Contains(hatId))
                return;

            PlayerData.PurchasedHatsIds.Add(hatId);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedHatIds() =>
            PlayerData.PurchasedHatsIds;

        public int GetPurchasedHat(int hatId) =>
            PlayerData.PurchasedHatsIds[hatId];
    }
}