using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Framework
{
    public abstract class SingletonScriptableObjectModulized<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string SOSFolderName = "Resources";

        static protected T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(string.Format("{0}", typeof(T).Name));
#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        string configPath = string.Format("{0}/{1}", GetScriptPath(typeof(T)), SOSFolderName);
                        if (!System.IO.Directory.Exists(configPath))
                            System.IO.Directory.CreateDirectory(configPath);

                        _instance = ScriptableObjectHelper.CreateAsset<T>(configPath, typeof(T).Name.ToString());
                    }
                    else
                    {
                        ScriptableObjectHelper.SaveAsset(_instance);
                    }
#endif
                }

                return _instance;
            }
        }
#if UNITY_EDITOR
        private static string GetScriptPath(Type classType)
        {
            string classTypeName = classType.Name;
            string[] guids = AssetDatabase.FindAssets("t:script " + classTypeName);

            foreach (string guid in guids)
            {
                string scriptPath = AssetDatabase.GUIDToAssetPath(guid);
                return System.IO.Path.GetDirectoryName(scriptPath);
            }

            return "Script not found in namespace.";
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
    }

}