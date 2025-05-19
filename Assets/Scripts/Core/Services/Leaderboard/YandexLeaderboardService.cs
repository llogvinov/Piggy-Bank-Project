using YG;

namespace Leaderboard
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        public void SetLeaderboard(string name, int value)
        {
            YG2.SetLeaderboard(name, value);
        }
    }
}