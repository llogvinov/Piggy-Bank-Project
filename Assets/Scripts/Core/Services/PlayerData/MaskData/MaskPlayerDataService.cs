using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public abstract partial class BasePlayerDataService : IPlayerDataService
    {
        public Mask GetSelectedMask() =>
            PlayerData.SelectedMask;

        public void SetSelectedMask(Mask mask, int maskIndex)
        {
            PlayerData.SelectedMask = mask;
            PlayerData.SelectedMaskId = maskIndex;
            Save(PlayerData);
        }

        public int GetSelectedMaskId() =>
            PlayerData.SelectedMaskId;

        public void SetSelectedMaskId(int newMaskIndex)
        {
            PlayerData.SelectedMaskId = newMaskIndex;
            Save(PlayerData);
        }

        public void AddPurchasedMask(int maskIndex)
        {
            PlayerData.PurchasedMasksIds.Add(maskIndex);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedMaskIds() =>
            PlayerData.PurchasedMasksIds;

        public int GetPurchasedMask(int maskIndex) =>
            PlayerData.PurchasedMasksIds[maskIndex];
    }
}