using UnityEngine;

namespace Framework
{
    public class PDataSettings : PDataBlock<PDataSettings>
    {
        [SerializeField] ObservableData<bool> _soundEnabled;
        [SerializeField] ObservableData<bool> _musicEnabled;
        [SerializeField] ObservableData<bool> _vibrationEnabled;

        public static bool SoundEnabled { get { return Instance._soundEnabled.Data; } set { Instance._soundEnabled.Data = value; } }
        public static bool MusicEnabled { get { return Instance._musicEnabled.Data; } set { Instance._musicEnabled.Data = value; } }
        public static bool VibrationEnabled { get { return Instance._vibrationEnabled.Data; } set { Instance._vibrationEnabled.Data = value; } }

        public static ObservableData<bool> SoundEnabledData { get { return Instance._soundEnabled; } }
        public static ObservableData<bool> MusicEnabledData { get { return Instance._musicEnabled; } }
        public static ObservableData<bool> VibrationEnabledData { get { return Instance._vibrationEnabled; } }

        protected override void Init()
        {
            base.Init();

            _soundEnabled = _soundEnabled == null ? new ObservableData<bool>(true) : _soundEnabled;
            _musicEnabled = _musicEnabled == null ? new ObservableData<bool>(true) : _musicEnabled;
            _vibrationEnabled = _vibrationEnabled == null ? new ObservableData<bool>(true) : _vibrationEnabled;
        }
    }
}