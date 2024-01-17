using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PhonicsConfig : SingletonScriptableObjectModulized<PhonicsConfig>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (_instance == null)
        {
            Instance.ToString();
        }
    }
    [SerializeField] private AudioClip[] audioClips; public static AudioClip[] AudioClips { get { return Instance.audioClips; } }
    [SerializeField] private float timeReplaySound; public static float TimeReplaySound { get { return Instance.timeReplaySound; } }
    [SerializeField] private float timeDelayPlaySound; public static float TimeDelayPlaySound { get { return Instance.timeDelayPlaySound; } }
    [SerializeField] private float timeSetSprite; public static float TimeSetSprite { get { return Instance.timeSetSprite; } }
    [SerializeField] private List<Sprite> phonicAnimSprites; public static List<Sprite> PhonicAnimSprites { get { return Instance.phonicAnimSprites; } }
    [SerializeField] private List<ListIndex> listIndexAnimSprites; public static List<ListIndex> ListIndexAnimSprites { get { return Instance.listIndexAnimSprites; } }
    [SerializeField] private Sprite[] bgSprite; public static Sprite[] BgSprite { get { return Instance.bgSprite; } }
    [SerializeField] private GameObject questionAPI; public static GameObject QuestionAPI { get { return Instance.questionAPI; } }
    [SerializeField] private GameObject collectionAPI; public static GameObject CollectionAPI { get { return Instance.collectionAPI; } }
    [SerializeField] private List<ListSprite> phonicSprite; public static List<ListSprite> PhonicSprite { get { return Instance.phonicSprite; } }
    [SerializeField] private Sprite[] sunQuestSprite; public static Sprite[] SunQuestSprite { get { return Instance.sunQuestSprite; } }
    [SerializeField] private Sprite[] ansQuestionBtnSprite; public static Sprite[] AnsQuestionBtnSprite { get { return Instance.ansQuestionBtnSprite; } }
    [SerializeField] private Sprite[] ansCollectBtnSprite; public static Sprite[] AnsCollectBtnSprite { get { return Instance.ansCollectBtnSprite; } }
    [SerializeField] private Sprite treeSprite; public static Sprite TreeSprite { get { return Instance.treeSprite; } }
}

[Serializable]
public class ListIndex
{
    public List<int> listIndexs;

}
[Serializable]
public class ListSprite
{
    public List<Sprite> sprites; 
}
