using Framework;
using Monetization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdsManager
{
    public Callback OnRewardShowed { get; set; }      
    public void ShowBannerAds();
    public void HideBannerAds();
    public void ShowInterstialAds();
    public void ShowRewardAds(Callback onRewardShowed, string id, string customData = null);
    public void Initialize();
    public void SetUserId(string id);
    public void LoadAds(string id, AdsType type);
}
