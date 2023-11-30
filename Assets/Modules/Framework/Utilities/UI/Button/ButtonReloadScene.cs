using UnityEngine;

namespace Framework
{
    public class ButtonReloadScene : ButtonBase
    {
        [SerializeField] bool loadingObject;
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            SceneTransitionHelper.Reload(loadingObject);
        }
    }
}