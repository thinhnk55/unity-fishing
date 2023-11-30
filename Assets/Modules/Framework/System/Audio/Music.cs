using System;
using UnityEngine;

namespace Framework
{
    public enum MusicType
    {
        INGAME,
        MAINMENU,
        RADAR,
        END,
    }
    [Serializable]
    public struct MusicConfig
    {
        public MusicType type;
        public AudioClip clip;
        public float volume;
    }
}