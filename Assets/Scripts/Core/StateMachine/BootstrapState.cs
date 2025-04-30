using Core.Factory;
using Core.Services.Ad;
using Core.Services.Localization;
using Core.Services.PlayerData;
using PiggyBank;
using UnityEngine;
using YG;

namespace Core.StateMachine
{
    public class BootstrapState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly LocalizationData _localizationData;

        public BootstrapState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            _localizationData = Resources.Load<LocalizationData>("Localization Data");

            RegisterServices();
            LoadPlayerData();
            SwitchLanguage();
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
// #if UNITY_EDITOR
//             RegisterLocalDataService();
// #else
            RegisterYandexDataService();
// #endif
            _services.RegisterSingle<ILocalizationService>(new YandexLocalizationService(_localizationData));
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<IAdService>(new YandexAdService());
        }

        private void RegisterLocalDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new LocalPlayerDataService());

        private void RegisterYandexDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new YandexPlayerDataService());

        private void LoadPlayerData() =>
            _services.Single<IPlayerDataService>().Load();

        private void SwitchLanguage() => 
            _services.Single<ILocalizationService>().SwitchLanguage(YG2.lang);
    }
}