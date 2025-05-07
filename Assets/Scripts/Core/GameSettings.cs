namespace Core
{
    public class GameSettings
    {
        public AllLocalizationData LocalizationData;
        public LocationShopDatabase LocationShopDatabase;
        public HatShopDatabase HatShopDatabase;
        public MaskShopDatabase MaskShopDatabase;

        public GameSettings(AllLocalizationData localizationData, 
            LocationShopDatabase locationShopDatabase, 
            HatShopDatabase hatShopDatabase,
            MaskShopDatabase maskShopDatabase)
        {
            LocalizationData = localizationData;
            LocationShopDatabase = locationShopDatabase;
            HatShopDatabase = hatShopDatabase;
            MaskShopDatabase = maskShopDatabase;
        }
    }
}