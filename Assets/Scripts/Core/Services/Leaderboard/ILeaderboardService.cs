using Core;

namespace Leaderboard
{
    public interface ILeaderboardService : IService
    {
        void SetLeaderboard(string name, int value);
    }
}