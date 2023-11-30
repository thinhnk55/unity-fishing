using UnityEditor;
using UnityEngine;

namespace Framework
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string SOSFolderName = "Config";

        static protected T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    string resourcePath = string.Format("{0}", SOSFolderName);
                    if (typeof(T).Namespace != null)
                        resourcePath += "/" + typeof(T).Namespace;
                    resourcePath += "/" + typeof(T).Name;
                    _instance = Resources.Load<T>(resourcePath);
#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        string configPath = string.Format("Assets/Resources/{0}", SOSFolderName);
                        if (typeof(T).Namespace != null)
                            configPath += "/" + typeof(T).Namespace;
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