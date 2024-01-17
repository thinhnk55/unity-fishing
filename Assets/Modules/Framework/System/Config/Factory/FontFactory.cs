using TMPro;
using UnityEngine;

namespace Framework
{
    public class FontFactory : SingletonScriptableObject<FontFactory>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
        [SerializeField] private TMP_FontAsset fontDefault; public static TMP_FontAsset FontDefault { get { return Instance.fontDefault; } }

    }
}