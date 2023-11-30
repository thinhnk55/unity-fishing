#if APPLOVIN
using DG.Tweening;
using Monetization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaxAdsManagerBase : IAdsManager
{

    int retryAttemptInter;
    int retryAttemptReward;

    public Callback OnRewardShowed { get; set; }
    #region protected
    protected void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) { }

    protected void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    protected void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(AdsManager.InterAdUnitId);
    }

    protected void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttemptInter = 0;
    }

    protected void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttemptInter++;
        double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttemptInter));
        DOVirtual.DelayedCall((float)retryDelay, LoadInterstitial);
    }

    protected void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    protected void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
    }
    public void LoadRewardedAd(string adUnit)
    {
        MaxSdk.LoadRewardedAd(adUnit);
    }

    protected void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttemptReward = 0;
    }

    protected void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryAttemptReward++;
        double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttemptReward));
        DOVirtual.DelayedCall((float)retryDelay, ()=> LoadRewardedAd(adUnitId));
    }

    protected void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadAds(adUnitId, AdsType.Reward);
    }

    protected void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    protected void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadAds(adUnitId, AdsType.Reward);
    }

    protected void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        OnRewardShowed?.Invoke();
    }

    protected void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    #endregion

    #region public
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(AdsManager.BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerExtraParameter(AdsManager.BannerAdUnitId, "adaptive_banner", "false");

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(AdsManager.BannerAdUnitId, Color.white);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
    }
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
    }
    public virtual void Initialize()
    {
    }

    public virtual void ShowBannerAds()
    {
    }

    public virtual void HideBannerAds()
    {
    }

    public virtual void ShowInterstialAds()
    {
    }
    public virtual void ShowRewardAds(Callback onRewardShowed, string id, string customdata = null)
    {

    }
 
    public void SetUserId(string id)
    {
        MaxSdk.SetUserId(id);
    }

    public virtual void LoadAds(string id, AdsType type)
    {
        switch (type)
        {
            case AdsType.Banner:
                MaxSdk.LoadBanner(id);
                break;
            case AdsType.Inter:
                MaxSdk.LoadInterstitial(id);
                break;
            case AdsType.Reward:
                MaxSdk.LoadRewardedAd(id);
                break;
            case AdsType.Open:
                MaxSdk.LoadAppOpenAd(id);
                break;
            case AdsType.Native:

                break;
            default:
                break;
        }
    }


    #endregion

}
#endif