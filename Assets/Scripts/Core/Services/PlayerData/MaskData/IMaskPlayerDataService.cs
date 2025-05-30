using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public partial interface IPlayerDataService : IService
    {
        Mask GetSelectedMask();
        void SetSelectedMask(Mask mask, int maskIndex);
        int GetSelectedMaskId();
        void SetSelectedMaskId(int newMaskIndex);
        void AddPurchasedMask(int maskIndex);
        List<int> GetAllPurchasedMaskIds();
        int GetPurchasedMask(int maskIndex);
    }
}