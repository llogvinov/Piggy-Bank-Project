using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private SettingsUI settingsUI;

    private const string Coins1000 = "com.cringegamesls.piggybank.coins1000";
    private const string Coins3000 = "com.cringegamesls.piggybank.coins3000";
    private const string Coins10000 = "com.cringegamesls.piggybank.coins10000";
    private const string Coins40000 = "com.cringegamesls.piggybank.coins40000";

    private const string RemoveAds = "com.cringegamesls.piggybank.remove_ads";

    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        _playerDataService = AllServices.Container.Single<IPlayerDataService>();
    }

    public void OnPurchaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case Coins1000:
                _playerDataService.AddCoins(1000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case Coins3000:
                _playerDataService.AddCoins(3000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case Coins10000:
                _playerDataService.AddCoins(10000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case Coins40000:
                _playerDataService.AddCoins(40000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case RemoveAds:
                _playerDataService.RemoveAds();
                settingsUI.RemoveAdsComplete();
                break;
        }

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailure)
    {
#if UNITY_EDITOR
        Debug.Log(product.definition.id + " failed because " + purchaseFailure);
#endif
    }

}
