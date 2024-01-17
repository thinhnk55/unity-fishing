using System;

namespace Framework
{
    public enum MusicType
    {
        GAME_ROOM,
        MAIN_MENU,
    }
    [Serializable]
    public struct MusicConfig
    {
        public MusicType type;
        public ClipConfig clipConfig;
    }
}