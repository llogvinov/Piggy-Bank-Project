using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

public class NormalGameManager : GameManager
{
    [Space(15f)]
    [SerializeField] private Text coinText;
    [SerializeField] private Text recordText;

    public Image PowerupIcon;
    public TimerOld Timer;

    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        _playerDataService = AllServices.Container.Single<IPlayerDataService>();
    }

    private void Start()
    {
        StartGame();

        _playerDataService.IncrementNormalGamesPlayed();
        CoinToAdd = 0;
    }

    private void FixedUpdate()
    {
        coinText.text = "+" + CoinToAdd;
    }

    public override void GameOver()
    {
        IsGameOver = true;

        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);

        _playerDataService.SetBestScore(CoinToAdd);
        recordText.text = "record: " + _playerDataService.GetBestScore();
        rewardText.text = "+" + CoinToAdd;
        _playerDataService.AddCoins(CoinToAdd);
        GameSharedUI.Instance.UpdateCoinsUIText();

        ShowInterstitialAd();
        Debug.Log(_playerDataService.GetNormalGamesPlayed());
    }

    //Shows an interstitial ad after every game in Normal mode
    private void ShowInterstitialAd()
    {
        if (_playerDataService.IsRemovedAds())
            return;

        if (_playerDataService.GetNormalGamesPlayed() % 1 == 0)
        {
            //adManager.ShowInterstitialAd();
        }
    }

}
