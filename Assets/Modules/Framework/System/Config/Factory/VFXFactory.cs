using UnityEngine;

namespace Framework
{
    public class VFXFactory : SingletonScriptableObject<VFXFactory>
    {
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

