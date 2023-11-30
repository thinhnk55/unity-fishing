using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class AudioConfig : SingletonScriptableObject<AudioConfig>
    {
        [SerializeField] private MusicConfigDictionary musicConfigs; public static MusicConfigDictionary MusicConfigs { get { return Instance.musicConfigs; } }
        [SerializeField] private SoundConfigDictionary soundConfigs; public static SoundConfigDictionary SoundConfigs { get { return Instance.soundConfigs; } }
    }


    [Serializable]
    public class MusicConfigDictionary : SerializedDictionary<MusicType, MusicConfig> { }
    [Serializable]
    public class SoundConfigDictionary : SerializedDictionary<SoundType, SoundConfig> { }
}
