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

        private readonly IPlayerDataService _playerDataService;

        private LocationShopDatabase _locationShopDatabase;
        private HatShopDatabase _hatShopDatabase;
        private MaskShopDatabase _maskShopDatabase;

        public BootstrapState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            _localizationData = Resources.Load<LocalizationData>("Localization Data");
            _locationShopDatabase = Resources.Load<LocationShopDatabase>("Location Shop Database");
            _hatShopDatabase = Resources.Load<HatShopDatabase>("Hat Shop Database");
            _maskShopDatabase = Resources.Load<MaskShopDatabase>("Mask Shop Database");

            RegisterServices();
            _playerDataService = _services.Single<IPlayerDataService>();
            LoadPlayerData();
            SwitchLanguage();

            SetSelectedLocation();
            SetSelectedHat();
            SetSelectedMask();
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