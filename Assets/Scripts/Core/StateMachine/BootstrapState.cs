using Core.Factory;
using Core.Services.PlayerData;
using PiggyBank;

namespace Core.StateMachine
{
    public class BootstrapState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(AssetPath.MenuScene, () =>
                _stateMachine.Enter<MenuState>());
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
#if UNITY_EDITOR
            RegisterLocalDataService();
#else
            RegisterYandexDataService();
#endif
            _services.RegisterSingle<IGameFactory>(new GameFactory());
        }

        private void RegisterLocalDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new LocalPlayerDataService());

        private void RegisterYandexDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new LocalPlayerDataService());
    }
}