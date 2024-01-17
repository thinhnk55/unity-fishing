using System;
using UnityEngine;

namespace Framework
{
    [Serializable]
    public class PrefabFactory : SingletonScriptableObject<PrefabFactory>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
            //Instantiate(CorePrefab);
        }
        #region PrimitiveAsset
        [SerializeField] private GameObject corePrefab; public static GameObject CorePrefab { get { return Instance.corePrefab; } }
        [SerializeField] private GameObject textPrefab; public static GameObject TextPrefab { get { return Instance.textPrefab; } }
        [SerializeField] private GameObject audioSourcePrefab; public static GameObject AudioSourcePrefab { get { return Instance.audioSourcePrefab; } }
        #endregion

        [SerializeField] private GameObject winPanel; public static GameObject WinPanel { get { return Instance.winPanel; } }
        [SerializeField] private GameObject losePanel; public static GameObject LosePanel { get { return Instance.losePanel; } }
        [SerializeField] private GameObject itemFly; public static GameObject ItemFly { get { return Instance.itemFly; } }
    }
}

