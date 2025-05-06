using Core.StateMachine;
using UI;
using UnityEngine;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private UILoading _uiLoading;
        [Space]
        [SerializeField] private AllLocalizationData _localizationData;
        [SerializeField] private LocationShopDatabase _locationShopDatabase;
        [SerializeField] private HatShopDatabase _hatShopDatabase;
        [SerializeField] private MaskShopDatabase _maskShopDatabase;

        private Game _game;

        private static GameBootstrapper _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _game = new Game(this, _uiLoading, new GameSettings
                {
                    LocalizationData = _localizationData,
                    LocationShopDatabase = _locationShopDatabase,
                    HatShopDatabase = _hatShopDatabase,
                    MaskShopDatabase = _maskShopDatabase
                });
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}