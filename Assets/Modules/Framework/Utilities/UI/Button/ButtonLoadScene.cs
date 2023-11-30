using UnityEngine;

namespace Framework
{
    public class ButtonLoadScene : ButtonBase
    {
        [SerializeField] ESceneName eSceneValue;
        [SerializeField] bool loadingObject;

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            SceneTransitionHelper.Load(eSceneValue, loadingObject);
        }
    }
}