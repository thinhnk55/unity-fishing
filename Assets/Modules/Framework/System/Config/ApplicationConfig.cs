using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ApplicationConfig : SingletonScriptableObject<ApplicationConfig>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
        [SerializeField] private string bundleId; public static string BundleId { get { return Instance.bundleId; } }

    }

}
