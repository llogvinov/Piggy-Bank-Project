using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "4118859";
#elif UNITY_ANDROID
    private string gameId = "4118858";
#endif

#if UNITY_IOS
    string myInterstitialId = "Interstitial_iOS";
#elif UNITY_ANDROID
    string myInterstitialId = "Interstitial_Android";
#endif

#if UNITY_IOS
    string myRewardedId = "Rewarded_iOS";
#elif UNITY_ANDROID
    string myRewardedId = "Rewarded_Android";
#endif

    private bool testMode = false;

    [SerializeField] private Button rewardButton;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        if (!rewardButton.gameObject.activeSelf)
        {
            rewardButton.gameObject.SetActive(true);
        }
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myInterstitialId))
        {
            Advertisement.Show(myInterstitialId);
            // Replace yourPlacementID with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myRewardedId))
        {
            Advertisement.Show(myRewardedId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            GameDataManager.AddCoins(gameManager.coinToAdd);
            GameSharedUI.Instance.UpdateCoinsUIText();
            gameManager.DoubleCoins();
            rewardButton.gameObject.SetActive(false);
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == myRewardedId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

}
