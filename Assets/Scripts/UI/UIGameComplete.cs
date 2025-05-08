using Core;
using Core.Services.Ad;
using Core.Services.PlayerData;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameComplete : UIBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _rewardButton;
        [Space]
        [SerializeField] private Text _coinToAddText;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        private IPlayerDataService _playerDataService;
        private IAdService _adService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            _adService = AllServices.Container.Single<IAdService>();
        }

        protected override void Start()
        {
            base.Start();
            _rewardButton.onClick.AddListener(ShowRewardedAd);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _rewardButton.onClick.RemoveListener(ShowRewardedAd);
        }

        private void ShowRewardedAd() =>
            _adService.ShowRewardedAd("DoubleCoins", DoubleCoins);

        private void DoubleCoins()
        {
            _rewardButton.gameObject.SetActive(false);
            
            var coinsToAdd = GameConstants.SURVIVAL_MODE_REWARD;
            _playerDataService.AddCoins(coinsToAdd);

            UpdateUI(coinsToAdd * 2);
        }

        public void Show(int coinsToAdd)
        {
            UpdateUI(coinsToAdd);
            Show();
        }

        public void UpdateUI(int coinsToAdd)
        {
            _coinToAddText.text = "+" + coinsToAdd.ToString();
        }

        public void ToggleRewardButton(bool enable) =>
            _rewardButton.gameObject.SetActive(enable);
    }
}