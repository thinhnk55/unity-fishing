
using Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Monetization
{

    public class ButtonShowAds : ButtonBase
    {
        [SerializeField] UnityEvent rewardCallback;
        [SerializeField] RewardType rewardType;

        public void ShowAds(string customData = null)
        {
            AdsManager.ShowRewardAds(() => rewardCallback?.Invoke(), AdsData.adsUnitIdMap[rewardType], customData);
        }

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();
            Debug.Log("ShowAds");
            ShowAds();
        }

    }

}
