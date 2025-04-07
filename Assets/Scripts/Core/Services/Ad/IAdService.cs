using System;

namespace Core.Services.Ad
{
    public interface IAdService : IService
    {
        void ShowInterstitialAd();
        void ShowRewardedAd(Action onShown);
    }
}