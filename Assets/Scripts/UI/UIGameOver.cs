using Core;
using Core.Factory;
using Core.Services.Ad;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _rewardButton;
        [Space]
        [SerializeField] private Text _recordText;
        [SerializeField] private Text _coinToAddText;
        [SerializeField] private Text _totalCoinsText;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        public IGameFactory _gameFactory;
        private IPlayerDataService _playerDataService;
        private IAdService _adService;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            _adService = AllServices.Container.Single<IAdService>();
        }

        private void Start()
        {
            _rewardButton.onClick.AddListener(ShowRewardedAd);
        }

        private void OnDestroy()
        {
            _rewardButton.onClick.RemoveListener(ShowRewardedAd);
        }

        private void ShowRewardedAd() =>
            _adService.ShowRewardedAd("DoubleCoins", DoubleCoins);

        private void DoubleCoins()
        {
            _rewardButton.gameObject.SetActive(false);
            
            var player = _gameFactory.Player;
            var coinsToAdd = player.CoinCollector.CoinsToAdd;
            _playerDataService.AddCoins(coinsToAdd);

            UpdateUI(_playerDataService.GetPlayerRecord(),
                coinsToAdd * 2,
                _playerDataService.GetCoins());
        }

        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Show(int record, int coinsToAdd, int totalCoins)
        {
            UpdateUI(record, coinsToAdd, totalCoins);
            Show();
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }

        public void UpdateUI(int record, int coinsToAdd, int totalCoins)
        {
            _recordText.text = "record: " + record.ToString();
            _coinToAddText.text = "+" + coinsToAdd.ToString();
            _totalCoinsText.text = totalCoins.ToString();
        }
    }
}