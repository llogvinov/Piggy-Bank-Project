using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public partial interface IPlayerDataService : IService
    {
        Hat GetSelectedHat();
        void SetSelectedHat(Hat hat, int hatIndex);
        int GetSelectedHatId();
        void SetSelectedHatId(int newHatIndex);
        void AddPurchasedHat(int hatIndex);
        List<int> GetAllPurchasedHatIds();
        int GetPurchasedHat(int hatIndex);
    }
}