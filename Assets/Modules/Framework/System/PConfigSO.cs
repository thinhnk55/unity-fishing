using UnityEngine;

namespace Framework
{
    public class PConfigSO : SingletonScriptableObjectModulized<PConfigSO>
    {
        [Header("App")]
        [SerializeField] string _appID;

        [Header("Scene Transition")]
        //[SerializeField] float _sceneTransitionFadeInDuration = 0.2f;
        //[SerializeField] float _sceneTransitionLoadDuration = 0.5f;
        //[SerializeField] float _sceneTransitionFadeOutDuration = 0.2f;

        [Header("Prefab")]
        [SerializeField] GameObject _objSceneTransition = null;

        public static string AppID => Instance._appID;

        //public static float SceneTransitionLoadDuration => Instance._sceneTransitionLoadDuration;
        //public static float SceneTransitionFadeInDuration => Instance._sceneTransitionFadeInDuration;
        //public static float SceneTransitionFadeOutDuration => Instance._sceneTransitionFadeOutDuration;

        public static GameObject ObjSceneTransition => Instance._objSceneTransition;

    }
}