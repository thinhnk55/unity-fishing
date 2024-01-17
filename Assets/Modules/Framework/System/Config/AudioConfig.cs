using System;
using UnityEngine;

namespace Framework
{
    public class AudioConfig : SingletonScriptableObject<AudioConfig>
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }



        [SerializeField] private MusicConfigDictionary musicConfigs; public static MusicConfigDictionary MusicConfigs { get { return Instance.musicConfigs; } }
        [SerializeField] private SoundConfigDictionary soundConfigs; public static SoundConfigDictionary SoundConfigs { get { return Instance.soundConfigs; } }
    }


    [Serializable]
    public class MusicConfigDictionary : SerializedDictionary<MusicType, MusicConfig> { }
    [Serializable]
    public class SoundConfigDictionary : SerializedDictionary<SoundType, SoundConfig> { }
}
