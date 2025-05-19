using System.Collections;
using Core;
using Core.Factory;
using Core.Services.Ad;
using Core.Services.PlayerData;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameOver : UIBase
    {
        [SerializeField] private GameObject _revivePanel;
        [SerializeField] private GameObject _buttonsPanel;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Button _reviveButton;
        [Space]
        [SerializeField] private Text _recordText;
        [SerializeField] private Text _coinToAddText;
        [Space]
        [SerializeField] private GameObject _uiPowerup;

        public Button ReviveButton => _reviveButton;
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

            var player = _gameFactory.Player;
            var coinsToAdd = player.CoinCollector.CoinsToAdd;
            _playerDataService.AddCoins(coinsToAdd);

            UpdateUI(_playerDataService.GetBestScore(),
                coinsToAdd * 2);
        }

        public void Show(bool showRevive, int record, int coinsToAdd)
        {
            SwitchPanels(showRevive);
            Show();
            _uiPowerup.SetActive(false);
            UpdateUI(record, coinsToAdd);
            if (showRevive)
            {
                StartCoroutine(RevivePanelCoroutine());
            }
        }

        public void UpdateUI(int record, int coinsToAdd)
        {
            var localized = _recordText.GetComponent<LocalizedText>();
            _recordText.text = $"{localized.GetValue()}: {record}";
            _coinToAddText.text = "+" + coinsToAdd.ToString();
        }

        public void ToggleRewardButton(bool enable) =>
            _rewardButton.gameObject.SetActive(enable);

        private void SwitchPanels(bool showRevive)
        {
            _revivePanel.SetActive(showRevive);
            _buttonsPanel.SetActive(!showRevive);
        }

        private IEnumerator RevivePanelCoroutine()
        {
            yield return new WaitForSeconds(GameConstants.REVIVE_PANEL_DURATION);
            SwitchPanels(false);
        }
    }
}