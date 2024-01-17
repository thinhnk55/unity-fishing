using Framework;
using UnityEngine;

public static class AudioHelper
{
    public static void PlaySound(this SoundType sound, Transform transform = null)
    {
        if (!PDataSettings.SoundEnabled) return;
        SoundConfig soundConfig = AudioConfig.SoundConfigs[sound];
        AudioManager.Instance.PlaySound(sound, soundConfig.clipConfigs[Random.Range(0, AudioConfig.SoundConfigs[sound].clipConfigs.Length)], transform, soundConfig.isFollow);
    }

    public static void StopAllSound()
    {
        AudioManager.Instance.StopAllSound();
    }
    public static void PlayMusic(this MusicType music)
    {
        if (!PDataSettings.MusicEnabled) return;
        AudioManager.Instance.PlayMusic(AudioConfig.MusicConfigs[music].clipConfig.clip, AudioConfig.MusicConfigs[music].clipConfig.volumn);
    }

    public static void StopMusic()
    {
        AudioManager.Instance.audioSource.Stop();
    }

    public static void PauseMusic()
    {
        AudioManager.Instance.audioSource.Pause();
    }
    public static void ResumeMusic()
    {
        if (!PDataSettings.MusicEnabled) return;
        AudioManager.Instance.audioSource.UnPause();
    }
}
