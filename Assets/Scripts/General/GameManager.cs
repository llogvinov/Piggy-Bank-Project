using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject gameOverPanel;
    [SerializeField] protected GameObject pauseButton;

    [SerializeField] protected Text rewardText;
    [Space]
    [SerializeField] protected AdManager adManager;

    public int CoinToAdd;
    public bool IsGameOver;

    public void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    //Updates UI - doubles coins to add
    public void DoubleCoins()
    {
        rewardText.text = "+" + (CoinToAdd * 2);
    }

    //Calls when the player dies
    //Overrides in subsidiary classes
    public abstract void GameOver();

    protected void StartGame()
    {
        IsGameOver = false;
        
        PowerUp.DeactivateAllPowerUps();
    }

}
