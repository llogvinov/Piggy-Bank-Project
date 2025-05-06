using System;
using System.Collections;
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
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private IPlayerDataService _playerDataService;

        private readonly AllLocalizationData _localizationData;
        private readonly LocationShopDatabase _locationShopDatabase;
        private readonly HatShopDatabase _hatShopDatabase;
        private readonly MaskShopDatabase _maskShopDatabase;

        public BootstrapState(GameStateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            SceneLoader sceneLoader,
            AllServices services,
            GameSettings settings)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
            _services = services;

            _localizationData = settings.LocalizationData;
            _locationShopDatabase = settings.LocationShopDatabase;
            _hatShopDatabase = settings.HatShopDatabase;
            _maskShopDatabase = settings.MaskShopDatabase;
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(WaitForInitialize());
        }

        private IEnumerator WaitForInitialize()
        {
            while (!YG2.isSDKEnabled)
            {
                yield return null;
            }

            YG2.SetDefaultSaves();
            YG2.SaveProgress();
            
            RegisterServices();
            _playerDataService = _services.Single<IPlayerDataService>();

            LoadPlayerData();
            SwitchLanguage();


            SetSelectedLocation();
            SetSelectedHat();
            SetSelectedMask();

            _sceneLoader.LoadScene(AssetPath.MenuScene, () =>
                _stateMachine.Enter<MenuState>());
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
            // RegisterLocalDataService();
            RegisterYandexDataService();

            _services.RegisterSingle<ILocalizationService>(new YandexLocalizationService(_localizationData));
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<IAdService>(new YandexAdService());
        }

        private void RegisterLocalDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new LocalPlayerDataService());

        private void RegisterYandexDataService() =>
            _services.RegisterSingle<IPlayerDataService>(new YandexPlayerDataService());

        private void LoadPlayerData() =>
            _playerDataService.Load();

        private void SwitchLanguage() =>
            _services.Single<ILocalizationService>().SwitchLanguage(YG2.lang);

        public void SetSelectedLocation()
        {
            int index = _playerDataService.GetSelectedLocationIndex();
            _playerDataService.SetSelectedLocation(_locationShopDatabase.GetLocation(index), index);
        }

        public void SetSelectedHat()
        {
            int index = _playerDataService.GetSelectedHatIndex();
            _playerDataService.SetSelectedHat(_hatShopDatabase.GetHat(index), index);
        }

        public void SetSelectedMask()
        {
            int index = _playerDataService.GetSelectedMaskIndex();
            _playerDataService.SetSelectedMask(_maskShopDatabase.GetMask(index), index);
        }
    }
}