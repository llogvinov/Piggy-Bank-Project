using Leaderboard;

namespace Core.Services.PlayerData
{
    public abstract partial class BasePlayerDataService : IPlayerDataService
    {
        public PlayerData PlayerData { get; protected set; }

        public abstract PlayerData Load();

        public abstract void Save(PlayerData playerData);

        public bool GetMusic() =>
            PlayerData.Music;

        public void SetMusic(bool value)
        {
            PlayerData.Music = value;
            Save(PlayerData);
        }

        public bool GetSound() =>
            PlayerData.Sound;

        public void SetSound(bool value)
        {
            PlayerData.Sound = value;
            Save(PlayerData);
        }

        public bool IsRemovedAds() =>
            PlayerData.RemovedAds;

        public void RemoveAds()
        {
            PlayerData.RemovedAds = true;
            Save(PlayerData);
        }

        public int GetBestScore() =>
            PlayerData.BestScore;

        public void SetBestScore(int score)
        {
            if (score > GetBestScore())
            {
                PlayerData.BestScore = score;
                Save(PlayerData);
                var leaderboardService = AllServices.Container.Single<ILeaderboardService>();
                if (leaderboardService != null)
                {
                    leaderboardService.SetLeaderboard("BestScore", score);
                }
            }
        }

        public long GetNormalGamesPlayed() =>
            PlayerData.NormalGamesPlayed;

        public void IncrementNormalGamesPlayed()
        {
            PlayerData.NormalGamesPlayed++;
            Save(PlayerData);
        }

        public long GetSurvivalGamesPlayed() =>
            PlayerData.SurvivalGamesPlayed;

        public void IncrementSurvivalGamesPlayed()
        {
            PlayerData.SurvivalGamesPlayed++;
            Save(PlayerData);
        }

        public int GetCoins() =>
            PlayerData.Coins;

        public void AddCoins(int amount)
        {
            PlayerData.Coins += amount;
            Save(PlayerData);
        }

        public bool CanSpendCoins(int amount) =>
            PlayerData.Coins >= amount;

        public void SpendCoins(int amount)
        {
            PlayerData.Coins -= amount;
            Save(PlayerData);
        }
    }
}