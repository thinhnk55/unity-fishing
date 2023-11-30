using UnityEngine;

namespace Framework
{
    public class ButtonRate : ButtonBase
    {
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

#if UNITY_ANDROID
            Application.OpenURL(string.Format("market://details?id={0}", Application.identifier));
#elif UNITY_IOS
            //Application.OpenURL(string.Format("itms-apps://itunes.apple.com/app/id{0}", PConfig.AppID));
#endif
        }
    }
}