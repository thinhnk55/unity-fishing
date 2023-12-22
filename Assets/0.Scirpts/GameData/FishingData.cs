using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using System;
using Framework.SimpleJSON;

public class FishingData : PDataBlock<FishingData>
{
    [SerializeField] public int starAmount; public static int StarAmount { get { return Instance.starAmount; } set { Instance.starAmount = value; } }
    [SerializeField] public List<Item> itemsRequire; public static List<Item> ItemsRequire { get { return Instance.itemsRequire; } set { Instance.itemsRequire = value; } }
    [SerializeField] public List<Item> itemsWrong; public static List<Item> ItemsWrong { get { return Instance.itemsWrong; } set { Instance.itemsWrong = value; } }



    [SerializeField] public List<JSONNode> nodes;


    protected override void Init()
    {
        base.Init();

    }

    [Serializable]
    public class Item
    {
        public int Id;
        public Sprite Sprite;
        public AudioClip Sound;
        public string Word;
    }


}
