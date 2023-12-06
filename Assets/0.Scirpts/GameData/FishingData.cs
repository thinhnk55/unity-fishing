using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class FishingData : PDataBlock<FishingData>
{
    [SerializeField] public int starAmount; public static int StarAmount { get { return Instance.starAmount; } set { Instance.starAmount = value; } }
    [SerializeField] public List<Item> itemsRequire; public static List<Item> ItemsRequire { get { return Instance.itemsRequire; } set { Instance.itemsRequire = value; } }
    [SerializeField] public List<Item> itemsWrong; public static List<Item> ItemsWrong { get { return Instance.itemsWrong; } set { Instance.itemsWrong = value; } }


    protected override void Init()
    {
        base.Init();

    }

    [SerializeField]
    public class Item
    {
        public int Id;
        public Sprite Sprite;
        public AudioClip Sound;
        public string Word;
    }
}