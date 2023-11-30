using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class MonoScriptableObject : ScriptableObject
    {
        protected virtual void OnBegin() { }
        protected virtual void OnEnd() { }

#if UNITY_EDITOR
        void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayStateChange;
        }

        void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayStateChange;
        }

        void OnPlayStateChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                OnBegin();
            }
            else if (state == PlayModeStateChange.ExitingPlayMode)
            {
                OnEnd();
            }
        }
#else
        protected void OnEnable()
        {
            OnBegin();
        }

        protected void OnDisable()
        {
            OnEnd();
        }
#endif
    }
}
