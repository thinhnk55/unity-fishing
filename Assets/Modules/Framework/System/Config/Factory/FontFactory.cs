using System.Collections;
using System.Collections.Generic;
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
    }
}