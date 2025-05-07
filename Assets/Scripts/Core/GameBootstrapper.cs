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

        public AllLocalizationData LocalizationData => _localizationData;

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
                (_localizationData,
                _locationShopDatabase,
                _hatShopDatabase,
                _maskShopDatabase));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}