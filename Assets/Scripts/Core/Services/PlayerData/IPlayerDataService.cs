namespace Core.Services.PlayerData
{
    public partial interface IPlayerDataService : IService
    {
        PlayerData PlayerData { get; }
        PlayerData Load();
        void Save(PlayerData playerData);
        bool GetMusic();
        void SetMusic(bool value);
        bool GetSound();
        void SetSound(bool value);
        bool IsRemovedAds();
        void RemoveAds();
        int GetBestScore();
        void SetBestScore(int newRecord);
        long GetNormalGamesPlayed();
        void IncrementNormalGamesPlayed();
        long GetSurvivalGamesPlayed();
        void IncrementSurvivalGamesPlayed();
        int GetCoins();
        void AddCoins(int amount);
        bool CanSpendCoins(int amount);
        void SpendCoins(int amount);
    }
}