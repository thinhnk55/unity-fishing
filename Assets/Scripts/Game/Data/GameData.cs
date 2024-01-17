using Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameData : PDataBlock<GameData>
{
    [SerializeField] public List<int> tutorial; public static List<int> Tutorial { get { return Instance.tutorial; } set { Instance.tutorial = value; } }
    [SerializeField] public int phonicIndex; public static int PhonicIndex{ get { return Instance.phonicIndex; } set { Instance.phonicIndex = value; } }

    protected override void Init()
    {
        base.Init();
        Instance.tutorial = Instance.tutorial??new List<int>();
    }
}
