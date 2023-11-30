using Framework;
using Monetization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public struct AdsRewardConfig
{
    public List<int> reward;
    public string rewardAdUnitId;
}

public enum RewardType
{
    Get_Beri = 2,
    Get_Rocket = 3,
    Get_Quest = 4,
    Change_Quest = 5,
    Get_X2DailyGift = 6,
    Get_RevivalOnlyPVE = 7,
}

public class AdsData : PDataBlock<AdsData>
{
    [SerializeField] private Dictionary<RewardType, string> AdsUnitIdMap; public static Dictionary<RewardType, string> adsUnitIdMap { get { return Instance.AdsUnitIdMap; } set { Instance.AdsUnitIdMap = value;} }
    [SerializeField] private Dictionary<string, AdsRewardConfig> RewardTypeToConfigMap; public static Dictionary<string, AdsRewardConfig> rewardTypeToConfigMap { get { return Instance.RewardTypeToConfigMap; } set { Instance.RewardTypeToConfigMap = value; } }
    [SerializeField] private int VersionAds; public static int versionAds { get { return Instance.VersionAds; } set { Instance.VersionAds = value; } }

    protected override void Init()
    {
        base.Init();
        Instance.AdsUnitIdMap = Instance.AdsUnitIdMap ?? new Dictionary<RewardType, string>();
        Instance.RewardTypeToConfigMap = Instance.RewardTypeToConfigMap ?? new Dictionary<string, AdsRewardConfig>();
        //Instance.VersionAds = 0;
    }
}

