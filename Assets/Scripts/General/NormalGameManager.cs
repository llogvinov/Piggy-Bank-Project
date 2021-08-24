using UnityEngine;
using UnityEngine.UI;

public class NormalGameManager : GameManager
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text recordText;

    public Image PowerupIcon;
    public Timer Timer;

    private void Start()
    {
        base.StartGame();

        GameDataManager.IncrementNormalGamesPlayed();
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

        GameDataManager.SetNewRecord(CoinToAdd);
        recordText.text = "record: " + GameDataManager.GetPlayerRecord();
        rewardText.text = "+" + CoinToAdd;
        GameDataManager.AddCoins(CoinToAdd);
        GameSharedUI.Instance.UpdateCoinsUIText();

        ShowInterstitialAd();
        Debug.Log(GameDataManager.GetNormalGamesPlayed());
    }

    //Shows an interstitial ad after every fourth game in Normal mode
    private void ShowInterstitialAd()
    {
        if (!GameDataManager.IsRemovedAds())
        {
            if (GameDataManager.GetNormalGamesPlayed() % 4 == 0)
            {
                adManager.ShowInterstitialAd();
            }
        }
    }

}
