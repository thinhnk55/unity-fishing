#if APPLOVIN
using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monetization
{
    public class MaxAdsManager : MaxAdsManagerBase
    {
        public override void Initialize()
        {
            MaxSdk.SetSdkKey(MonetizationConfig.SdkKey);
            MaxSdk.SetUserId("USER_ID");
            MaxSdk.InitializeSdk();
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                // AppLovin SDK is initialized, start loading ads
                //InitializeBannerAds();
                //InitializeInterstitialAds();
                InitializeRewardedAds();
            };

            // Load reward Ads
            foreach(KeyValuePair<RewardType, string> kvp in AdsData.adsUnitIdMap)
            {
                LoadAds(kvp.Value, AdsType.Reward);
            }
        }

        public override void ShowBannerAds()
        { 
            MaxSdk.ShowBanner(AdsManager.BannerAdUnitId);
        }

        public override void HideBannerAds()
        {
            MaxSdk.HideBanner(AdsManager.BannerAdUnitId);
        }
        public override void ShowInterstialAds()
        {
            if (MaxSdk.IsInterstitialReady(AdsManager.InterAdUnitId))
            {
                MaxSdk.ShowInterstitial(AdsManager.InterAdUnitId);
            }
        }
        public override void ShowRewardAds(Callback onRewardShowed, string rewardAdUnitId, string customdata = null)
        {
            OnRewardShowed = onRewardShowed;
            if (MaxSdk.IsRewardedAdReady(rewardAdUnitId))
            {
                MaxSdk.ShowRewardedAd(rewardAdUnitId, null, customdata);
            }
        }
    }
}
#endif