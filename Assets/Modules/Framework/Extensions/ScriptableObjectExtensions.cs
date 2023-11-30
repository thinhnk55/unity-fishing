using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Framework
{
    public static class ScriptableObjectExtensions
    {
        public static void SetDirtySelf(this ScriptableObject scriptableObject)
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(scriptableObject);
#endif

        }
    }
}