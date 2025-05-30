using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    public abstract partial class BasePlayerDataService : IPlayerDataService
    {
        public Location GetSelectedLocation() =>
            PlayerData.SelectedLocation;

        public void SetSelectedLocation(Location location, int locationIndex)
        {
            PlayerData.SelectedLocation = location;
            PlayerData.SelectedLocationId = locationIndex;
            Save(PlayerData);
        }

        public int GetSelectedLocationId() =>
            PlayerData.SelectedLocationId;

        public void SetSelectedLocationId(int newLocationIndex)
        {
            PlayerData.SelectedLocationId = newLocationIndex;
            Save(PlayerData);
        }

        public void AddPurchasedLocation(int locationIndex)
        {
            PlayerData.PurchasedLocationsIds.Add(locationIndex);
            Save(PlayerData);
        }

        public List<int> GetAllPurchasedLocationIds() =>
            PlayerData.PurchasedLocationsIds;

        public int GetPurchasedLocation(int locationIndex) =>
            PlayerData.PurchasedLocationsIds[locationIndex];
    }
}