using UnityEngine;

namespace Framework
{
    public class ButtonLoadPreviousScene : ButtonBase
    {
        [SerializeField] bool showLoadingScene;
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();
            SceneTransitionHelper.LoadPreviousScene(showLoadingScene);
        }
    }
}
