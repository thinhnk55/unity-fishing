using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
    public class MaterialFactory : SingletonScriptableObject<MaterialFactory>
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
