using Core;
using Core.Services.PlayerData;
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
            
        }
    }

    public override void GameOver()
    {
        throw new System.NotImplementedException();
    }
}
