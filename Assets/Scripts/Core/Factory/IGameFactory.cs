using Main;

namespace Core.Factory
{
    public interface IGameFactory : IService
    {
        Player Player { get; }
        MusicManager MusicManager { get; }

        Player InstantiatePlayer();
        MusicManager CreateVOManager();
    }
}