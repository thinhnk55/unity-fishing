using System;
using UnityEngine;
namespace Framework
{
    [Serializable]
    public class SceneTransitionConfigSO : SingletonScriptableObject<SceneTransitionConfigSO>
    {
        [Header("Config")]
        [SerializeField] float _fadeInDuration = 0.2f;
        [SerializeField] float _loadDuration = 0.1f;
        [SerializeField] float _fadeOutDuration = 0.2f;

        [Header("Object")]
        [SerializeField] GameObject _objSceneTransition;

        public static GameObject ObjSceneTransition => Instance._objSceneTransition;

        public static float FadeInDuration => Instance._fadeInDuration;
        public static float LoadDuration => Instance._loadDuration;
        public static float FadeOutDuration => Instance._fadeOutDuration;
    }
}