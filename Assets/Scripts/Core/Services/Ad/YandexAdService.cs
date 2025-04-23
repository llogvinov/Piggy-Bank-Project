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

        public void ShowRewardedAd(string id, Action onShown)
        {
            YG2.RewardedAdvShow(id, onShown);
        }
    }
}