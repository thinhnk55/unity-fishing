using System;
using UnityEngine;

namespace Framework
{
    public class AudioManager : SingletonMono<AudioManager>
    {
        public AudioSource audioSource;
        AudioTrackerDictionary audioTrackers = new AudioTrackerDictionary();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            GameObject obj = new GameObject(typeof(AudioManager).ToString());
            AudioManager audio = obj.AddComponent<AudioManager>();
            audio.audioSource = obj.AddComponent<AudioSource>();
            audio.audioSource.loop = true;
            DontDestroyOnLoad(Instance);
        }

        protected override void Awake()
        {
            base.Awake();
            ObjectPoolManager.SpawnObject<AudioSource>(PrefabFactory.AudioSourcePrefab, Vector3.zero, Instance.transform, false, PoolConfig.DefaultInitPoolSound).gameObject.SetActive(false);
        }
        public void PlaySound(SoundType sound, ClipConfig clipConfig, Transform transform, bool isFollow)
        {
            // AudioTracker track the audio source
            AudioTracker audioTracker;
            if (audioTrackers.ContainsKey(sound))
            {
                audioTracker = audioTrackers[sound];
            }
            else
            {
                audioTracker = new GameObject("Tracker " + sound.ToString()).AddComponent<AudioTracker>();
                audioTracker.type = sound;
                if (!isFollow)
                {
                    audioTracker.transform.parent = this.transform;
                }
                else
                {
                    audioTracker.transform.parent = transform;
                }
                audioTrackers.Add(sound, audioTracker);
            }

            if (audioTracker.IsFullActiveSound())
                return;

            // Set audio source
            AudioSource audioSrc = ObjectPoolManager.SpawnObject<AudioSource>(PrefabFactory.AudioSourcePrefab, Vector3.zero, audioTracker.transform);
            audioSrc.transform.parent = audioTracker.transform;
            audioSrc.clip = clipConfig.clip;
            audioSrc.spatialBlend = AudioConfig.SoundConfigs[sound].spatial;
            audioSrc.volume = clipConfig.volumn;
            audioSrc.Play();
        }
        public void PlayMusic(AudioClip clip, float volume)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }

        public void StopAllSound() 
        {
            foreach(var kvp in audioTrackers)
            {
                for (int i = 0; i < kvp.Value.transform.childCount; i++)
                {
                    kvp.Value.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
    [Serializable]
    public class AudioTrackerDictionary : SerializedDictionary<SoundType, AudioTracker> {}
}
