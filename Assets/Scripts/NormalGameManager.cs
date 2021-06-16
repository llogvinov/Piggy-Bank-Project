using UnityEngine;
using UnityEngine.UI;

public class NormalGameManager : GameManager
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text recordText;

    [SerializeField] public Image powerupIcon;
    [SerializeField] public Timer timer;

    private void Start()
    {
        base.StartGame();

        GameDataManager.IncrementNormalGamesPlayed();
        coinToAdd = 0;
    }

    private void FixedUpdate()
    {
        coinText.text = "+" + coinToAdd;
    }

    public override void GameOver()
    {
        isGameOver = true;

        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);

        GameDataManager.SetNewRecord(coinToAdd);
        recordText.text = "record: " + GameDataManager.GetPlayerRecord();
        rewardText.text = "+" + coinToAdd;
        GameDataManager.AddCoins(coinToAdd);
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
