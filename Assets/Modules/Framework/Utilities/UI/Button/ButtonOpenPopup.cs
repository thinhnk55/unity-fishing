using UnityEngine;

namespace Framework
{
    public class ButtonOpenPopup : ButtonBase
    {
        [SerializeField] GameObject _popup;

        public event Callback<PopupBehaviour> OnSpawnPopup;

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            PopupBehaviour popup = PopupHelper.Create(_popup);

            OnSpawnPopup?.Invoke(popup);

            HandleSpawnPopup(popup);
        }

        protected virtual void HandleSpawnPopup(PopupBehaviour popupBehaviour)
        {

        }
    }
}