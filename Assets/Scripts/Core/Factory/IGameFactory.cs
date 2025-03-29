using Main;

namespace Core.Factory
{
    public interface IGameFactory : IService
    {
        Player Player { get; }

        Player InstantiatePlayer();
    }
}