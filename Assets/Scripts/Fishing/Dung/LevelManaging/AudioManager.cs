using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Buttons Sound")]
    [SerializeField] AudioClip playButtonSFX;
    [SerializeField][Range(0, 1)] float playButtonVolume;

    [Header("Beach Wave Sound")]
    [SerializeField] AudioClip waterSFX;
    [SerializeField][Range(0, 1)] float waterVolume;

    [Header("Right Answer Audio")]
    [SerializeField] AudioClip rightSFX;
    [SerializeField][Range(0, 1)] float rightVolume;

    [Header("Wrong Answer Audio")]
    [SerializeField] AudioClip wrongSFX;
    [SerializeField][Range(0, 1)] float wrongVolume;

    [Header("Hint Answer Audio")]
    [SerializeField] AudioClip hintSFX;
    [SerializeField][Range(0, 1)] float hintVolume;

    AudioSource gameAudio;


    private void Awake()
    {
        gameAudio = GetComponent<AudioSource>();

        ManageSingleton();
    }

    private void ManageSingleton()
    {
        int instancesNum = FindObjectsOfType<AudioManager>().Length;

        if (instancesNum > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // sử dụng hàm dùng chung để phát audio clip
    private void PlayAudio(AudioClip SFXclip, float volume)
    {
        // lấy ra vị trí của camera chính để phát audio
        Vector3 cameraPosition = Camera.main.transform.position;

        //AudioSource.PlayClipAtPoint(SFXclip, cameraPosition, volume);
        gameAudio.PlayOneShot(SFXclip, volume);
    }

    // âm thanh khi ấn nút play
    public void PlayButtonAudio()
    {
        PlayAudio(playButtonSFX, playButtonVolume);
    }

    public void PlayBeachWaveAudio()
    {
        PlayAudio(waterSFX, waterVolume);
    }

    public void PlayRightAnswerAudio()
    {
        PlayAudio(rightSFX, rightVolume);
    }

    public void PLayWrongAnswerAudio()
    {
        PlayAudio(wrongSFX, wrongVolume);
    }

    public void PlayHintAudio()
    {
        PlayAudio(hintSFX, hintVolume);
    }
}
