using Framework;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private GameObject attackedAnim; public static GameObject AttackedAnim { get { return Instance.attackedAnim; } }
        [SerializeField] private GameObject explosion; public static GameObject Explosion { get { return Instance.explosion; } }
        [SerializeField] private GameObject smoke; public static GameObject Smoke { get { return Instance.smoke; } }
        [SerializeField] private GameObject splashWater; public static GameObject SplashWater { get { return Instance.splashWater; } }
        [SerializeField] private GameObject coin; public static GameObject Coin { get { return Instance.coin; } }
        [SerializeField] private GameObject coin2; public static GameObject Coin2 { get { return Instance.coin2; } }
    }
}

