using Core;
using Core.Services.Ad;
using Core.Services.PlayerData;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameComplete : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _rewardButton;
        [Space]
        [SerializeField] private Text _coinToAddText;
        [SerializeField] private Text _totalCoinsText;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        private IPlayerDataService _playerDataService;
        private IAdService _adService;

        private void Awake()
        {
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
            
            var coinsToAdd = GameConstants.SURVIVAL_MODE_REWARD;
            _playerDataService.AddCoins(coinsToAdd);

            UpdateUI(coinsToAdd * 2, _playerDataService.GetCoins());
        }

        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Show(int coinsToAdd, int totalCoins)
        {
            UpdateUI(coinsToAdd, totalCoins);
            Show();
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }

        public void UpdateUI(int coinsToAdd, int totalCoins)
        {
            _coinToAddText.text = "+" + coinsToAdd.ToString();
            _totalCoinsText.text = totalCoins.ToString();
        }
    }
}