using UnityEngine;
using UnityEngine.UI;

public class SurvivalGameManager : GameManager
{
    [SerializeField] private GameObject gameCompletePanel;
    [SerializeField] private Text timerText;

    [SerializeField] public float currentTime = 0f;
    [SerializeField] private float startingTime = 40f;

    private void Start()
    {
        base.StartGame();

        GameDataManager.IncrementSurvivalGamesPlayed();
        currentTime = startingTime;
        coinToAdd = 75;
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            currentTime -= Time.deltaTime;
            timerText.text = currentTime.ToString("00");
        }
        
        if (currentTime <= 0 && !isGameOver)
        {
            GameComplete();
        }
    }

    //Calls if the player had completed the game session
    public void GameComplete()
    {
        isGameOver = true;

        pauseButton.SetActive(false);
        gameCompletePanel.SetActive(true);

        rewardText.text = "+" + coinToAdd;
        GameDataManager.AddCoins(coinToAdd);
        GameSharedUI.Instance.UpdateCoinsUIText();

        ShowInterstitialAd();
    }

    public override void GameOver()
    {
        isGameOver = true;

        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);

        ShowInterstitialAd();
    }

    //Shows an interstitial ad after every fourth game in Survival mode
    private void ShowInterstitialAd()
    {
        if (!GameDataManager.IsRemovedAds())
        {
            if (GameDataManager.GetSurvivalGamesPlayed() % 4 == 0)
            {
                adManager.ShowInterstitialAd();
            }
        }
    }

}
