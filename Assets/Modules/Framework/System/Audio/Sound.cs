using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Framework
{
    public enum SoundType
    {
        ARROW_BUTTON,
        BONUS_LETTER_FLIGHT,
        BONUS_LETTER_LANDING,
        BUTTON_PLAY,
        BUTTON,
        CLICKED_ON_LETTER,
        COMPLETED_CROSSWORD,
        COMPLETED_PHRASE,
        CORRECT_WORD,
        FALLING_COIN,
        LOCKED,
        SKIP_BUTTON,
        KEYBOARD_POLISH_APPEAR,
        KEYBOARD_PRESSED_LETTER,
        KEYBOARD_REMOVE_KEY,
        OPEN_NEW_ITEM,
        NO_COINS,
        USE_HINT,
        LETTER_DISSAPEAR,
        WRONG_WORD
    }
    [Serializable]
    public struct ClipConfig
    {
        [HorizontalGroup("ClipConfig"), LabelWidth(50)]
        public AudioClip clip;
        [HorizontalGroup("ClipConfig"), LabelWidth(50), Range(0f, 1f)]
        public float volumn;
    }
    [Serializable]
    public struct SoundConfig
    {
        public SoundType type;
        public ClipConfig[] clipConfigs;
        [HorizontalGroup("SoundConfig"), LabelWidth(50), Tooltip("0 is 2D sound and 1 is 3D sound"), Range(0f, 1f)]
        public float spatial;
        [HorizontalGroup("SoundConfig"), LabelWidth(50)]
        public bool isFollow;
        [HorizontalGroup("SoundConfig"), LabelWidth(110), Tooltip("Value <= 0 mean no limit active sound")]
        public int maxActiveSound;
    }
}