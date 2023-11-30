using System;
using UnityEngine;

namespace Framework
{
    public enum SoundType
    {
        WIN,
        LOSE,
        SHIP_EXPLOSION,
        SHIP_HIT,
        SHIP_MISS,
        COIN_VFX,
        AUDIO,
        COUNT,
        CLICK,
        DOOR,
        LUCKYSHOT_FALSE,

    }
    [Serializable]
    public struct ClipConfig
    {
        public AudioClip clip;
        public float volumn;
    }
    [Serializable]
    public struct SoundConfig
    {
        public SoundType type;
        public ClipConfig[] clipConfigs;
        public float spatial;
        public bool isFollow;
        public int maxActiveSound;
    }
}