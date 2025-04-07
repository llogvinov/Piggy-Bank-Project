using System;
using YG;

namespace Core.Services.Ad
{
    public class YandexAdService : IAdService
    {
        public void ShowInterstitialAd()
        {
            YG2.InterstitialAdvShow();
        }

        public void ShowRewardedAd(Action onShown)
        {
            YG2.RewardedAdvShow("0", onShown);
        }
    }
}