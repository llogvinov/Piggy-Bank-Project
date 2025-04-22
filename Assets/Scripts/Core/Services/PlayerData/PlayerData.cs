using System.Collections.Generic;

namespace Core.Services.PlayerData
{
    [System.Serializable]
    public class PlayerData
    {
        public bool Music = true;
        public bool Sound = true;
        public bool RemovedAds = false;
        
        public int Coins = 0;
        public long NormalGamesPlayed = 0;
        public long SurvivalGamesPlayed = 0;
        public int NormalModeRecord = 0;
        public int SelectedHatId = 0;
        public int SelectedMaskId = 0;
        public int SelectedLocationId = 0;

        public List<int> PurchasedHatsIds = new List<int>();
        public List<int> PurchasedMasksIds = new List<int>();
        public List<int> PurchasedLocationsIds = new List<int>();

        public Hat SelectedHat = new Hat();
        public Mask SelectedMask = new Mask();
        public Location SelectedLocation = new Location();
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public bool Music = true;
        public bool Sound = true;
        public bool RemovedAds = false;
        
        public int Coins = 0;
        public long NormalGamesPlayed = 0;
        public long SurvivalGamesPlayed = 0;
        public int NormalModeRecord = 0;
        public int SelectedHatId = 0;
        public int SelectedMaskId = 0;
        public int SelectedLocationId = 0;

        public List<int> PurchasedHatsIds = new List<int>();
        public List<int> PurchasedMasksIds = new List<int>();
        public List<int> PurchasedLocationsIds = new List<int>();

        public Hat SelectedHat = new Hat();
        public Mask SelectedMask = new Mask();
        public Location SelectedLocation = new Location();
    }
}