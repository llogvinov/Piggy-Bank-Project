using UnityEngine;
using UnityEngine.UI;

public class SurvivalGameManager : GameManager
{
    [Space(15f)]
    [SerializeField] private GameObject gameCompletePanel;
    [SerializeField] private Text timerText;

    [SerializeField] private float startingTime = 40f;
    [HideInInspector] public float СurrentTime;

    private void Start()
    {
        StartGame();

        GameDataManager.IncrementSurvivalGamesPlayed();
        СurrentTime = startingTime;
        CoinToAdd = 75;
    }

    private void FixedUpdate()
    {
        if (!IsGameOver)
        {
            СurrentTime -= Time.deltaTime;
            timerText.text = СurrentTime.ToString("00");
        }
        
        if (СurrentTime <= 0 && !IsGameOver)
        {
            GameComplete();
        }
    }

    //Calls if the player had completed the game session
    private void GameComplete()
    {
        IsGameOver = true;

        pauseButton.SetActive(false);
        gameCompletePanel.SetActive(true);

        rewardText.text = "+" + CoinToAdd;
        GameDataManager.AddCoins(CoinToAdd);
        GameSharedUI.Instance.UpdateCoinsUIText();

        ShowInterstitialAd();
    }

    public override void GameOver()
    {
        IsGameOver = true;

        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);

        ShowInterstitialAd();
    }

    //Shows an interstitial ad after every game in Survival mode
    private void ShowInterstitialAd()
    {
        if (GameDataManager.IsRemovedAds()) 
            return;
        
        if (GameDataManager.GetSurvivalGamesPlayed() % 1 == 0)
        {
            adManager.ShowInterstitialAd();
        }
    }

}
