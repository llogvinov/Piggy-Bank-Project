using System.Collections;
using Core.Factory;
using Core.Services.Ad;
using Core.Services.Localization;
using Core.Services.PlayerData;
using PiggyBank;
using YG;

namespace Core.StateMachine
{
    public class BootstrapState : ISimpleState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Game _game;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private IPlayerDataService _playerDataService;

        private readonly AllLocalizationData _localizationData;
        private readonly LocationShopDatabase _locationShopDatabase;
        private readonly HatShopDatabase _hatShopDatabase;
        private readonly MaskShopDatabase _maskShopDatabase;

        public BootstrapState(GameStateMachine stateMachine,
            Game game,
            ICoroutineRunner coroutineRunner,
            SceneLoader sceneLoader,
            AllServices services,
            GameSettings settings)
        {
            _stateMachine = stateMachine;
            _game = game;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
            _services = services;

            _localizationData = settings.LocalizationData;
            _locationShopDatabase = settings.LocationShopDatabase;
            _hatShopDatabase = settings.HatShopDatabase;
            _maskShopDatabase = settings.MaskShopDatabase;
            
            RegisterServices();
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
            RegisterLocalDataService();
            // RegisterYandexDataService();

            _services.RegisterSingle<ILocalizationService>(new YandexLocalizationService(_game));
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
            int locationId = _playerDataService.GetSelectedLocationIndex();
            var location = _locationShopDatabase.GetLocationById(locationId);
            _playerDataService.SetSelectedLocation(location, locationId);
        }

        public void SetSelectedHat()
        {
            int hatId = _playerDataService.GetSelectedHatIndex();
            var hat = _hatShopDatabase.GetHatById(hatId);
            _playerDataService.SetSelectedHat(hat, hatId);
        }

        public void SetSelectedMask()
        {
            int maskId = _playerDataService.GetSelectedMaskIndex();
            var mask = _maskShopDatabase.GetMaskById(maskId);
            _playerDataService.SetSelectedMask(mask, maskId);
        }
    }
}