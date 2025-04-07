using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _playerDataService = AllServices.Container.Single<IPlayerDataService>();
    }

    #endregion

    [SerializeField] private Text[] coinsUIText;

    private IPlayerDataService _playerDataService;
    
    private void Start()
    {
        UpdateCoinsUIText();
    }

    public void UpdateCoinsUIText()
    {
        for (int i = 0; i < coinsUIText.Length; i++)
        {
            SetCoinsText(coinsUIText[i], _playerDataService.GetCoins());
        }
    }

    private void SetCoinsText(Text text, int value) => text.text = value + "";

}
