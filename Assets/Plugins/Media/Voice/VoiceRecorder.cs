using Framework;
using System.Collections;
using UnityEngine;

public class VoiceRecorder : MonoBehaviour
{
    private AudioClip recordedClip; public AudioClip RecordedClip { get { return recordedClip; } }
    [HideInInspector] public ObservableData<bool> IsRecording = new(false);
    public Callback<float, int> OnVolumnDetect;
    float[] audioData;
    const int sample = 56;
    string microphoneName;
    [SerializeField] bool playback = false;
    void Start()
    {
        // Check if the microphone is available
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone found.");
            return;
        }
        microphoneName = Microphone.devices[0];
        Debug.Log(microphoneName);
    }
    public void StartRecording()
    {
        if (Microphone.devices.Length == 0) return;
        Debug.Log("Start Record");
        IsRecording.Data = true;
        recordedClip = Microphone.Start(microphoneName, false, sample, AudioSettings.outputSampleRate);
        StartCoroutine(Recording());
    }
    public IEnumerator Recording()
    {
        while (IsRecording.Data && Microphone.IsRecording(microphoneName))
        {
            OnVolumnDetect?.Invoke(GetLoudnessFromAudioClip(Microphone.GetPosition(microphoneName), recordedClip), Microphone.GetPosition(microphoneName));
            yield return null;
        }
        IsRecording.Data = false;
        yield return null;
    }

    public void StopRecording()
    {
        Debug.Log("Recorded");
        IsRecording.Data = false;
        Microphone.End(microphoneName);
        if (playback)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = recordedClip;
            audioSource.Play();
        }
    }

    public void ToggleRecording()
    {
        if (Microphone.devices.Length == 0) return;
        if (IsRecording.Data)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }
    }

    float GetLoudnessFromAudioClip(int clipPos, AudioClip clip)
    {
        int startPos = clipPos - sample;
        if (startPos < 0) return 0;
        audioData = new float[sample];
        clip.GetData(audioData, startPos);
        float totalLoudness = 0;
        for (int i = 0; i < audioData.Length; i++)
        {
            totalLoudness += audioData[i] * audioData[i];
        }
        return Mathf.Sqrt(totalLoudness / audioData.Length);
    }
}