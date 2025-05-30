using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public partial interface IPlayerDataService : IService
    {
        Location GetSelectedLocation();
        void SetSelectedLocation(Location location, int locationIndex);
        int GetSelectedLocationId();
        void SetSelectedLocationId(int newLocationIndex);
        void AddPurchasedLocation(int locationIndex);
        List<int> GetAllPurchasedLocationIds();
        int GetPurchasedLocation(int locationIndex);
    }
}