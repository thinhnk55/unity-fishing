using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class SpriteFactory : SingletonScriptableObject<SpriteFactory>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
        [SerializeField] private List<Sprite> items; public static List<Sprite> Items { get { return Instance.items; } }
        [SerializeField] private List<Sprite> itemSprites; public static List<Sprite> ItemSprites { get { return Instance.itemSprites; } }
    }
}