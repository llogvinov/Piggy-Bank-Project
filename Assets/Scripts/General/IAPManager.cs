using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private SettingsUI settingsUI;

    private const string coins1000 = "com.cringegamesls.piggybank.coins1000";
    private const string coins3000 = "com.cringegamesls.piggybank.coins3000";
    private const string coins10000 = "com.cringegamesls.piggybank.coins10000";
    private const string coins40000 = "com.cringegamesls.piggybank.coins40000";

    private const string removeAds = "com.cringegamesls.piggybank.remove_ads";

    public void OnPurchaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case coins1000:
                GameDataManager.AddCoins(1000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case coins3000:
                GameDataManager.AddCoins(3000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case coins10000:
                GameDataManager.AddCoins(10000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case coins40000:
                GameDataManager.AddCoins(40000);
                GameSharedUI.Instance.UpdateCoinsUIText();
                break;
            case removeAds:
                GameDataManager.RemoveAds();
                settingsUI.RemoveAdsComplete();
                break;
        }

    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailure)
    {
        Debug.Log(product.definition.id + " failed because " + purchaseFailure);
    }

}
