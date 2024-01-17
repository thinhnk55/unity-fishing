using UnityEngine;

namespace Framework
{
    public class ButtonPlaySound : ButtonBase
    {
        [SerializeField] SoundType soundType;
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();
            soundType.PlaySound();
        }
        public void PlaySound()
        {
            soundType.PlaySound();
        }
    }
}
