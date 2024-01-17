using Framework;
using UnityEngine;
using static SpeechRecognizerPlugin;

public class SpeechRecognizerBase : SingletonMono<SpeechRecognizerBase>, ISpeechRecognizerPlugin
{
    public Callback<string> OnResultCallback;
    public ObservableData<bool> IsSpeaking;
    SpeechRecognizerPlugin_Android plugin = null;

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern IntPtr getVc();

    [DllImport("__Internal")]
    private static extern void _TAG_startRecording();

    [DllImport("__Internal")]
    private static extern void _TAG_stopRecording();

    [DllImport("__Internal")]
    private static extern void _TAG_SettingSpeech(string language);

    private static IntPtr nativeInstance;

    // Singleton accessor
    private static IntPtr NativeInstance
    {
        get
        {
            if (nativeInstance == IntPtr.Zero)
            {
                nativeInstance = getVc();
            }
            return nativeInstance;
        }
    }
#elif UNITY_ANDROID
#endif

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
#elif UNITY_ANDROID
        plugin = (SpeechRecognizerPlugin_Android)SpeechRecognizerPlugin.GetPlatformPluginVersion(gameObject.name);
        plugin.SetContinuousListening(true);
        plugin.SetMaxResultsForNextRecognition(1);
#endif
        SetSpeechLanguage("en-US");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void StartSpeechRecognition()
    {
#if UNITY_EDITOR
#elif UNITY_IOS
        _TAG_startRecording();
#elif UNITY_ANDROID
        plugin.StartListening(true, "es-ES");
#endif
        Debug.Log("Start Speech");
        IsSpeaking.Data = true;
    }
    public void StopSpeechRecognition()
    {
        if (IsSpeaking.Data)
        {
#if UNITY_EDITOR
#elif UNITY_IOS
            _TAG_stopRecording();
#elif UNITY_ANDROID
            plugin.StopListening();
#endif
        }
        Debug.Log("Stop Speech");
        IsSpeaking.Data = false;
    }
    public void SetSpeechLanguage(string language)
    {
#if UNITY_EDITOR
#elif UNITY_IOS
        _TAG_SettingSpeech(language);
#elif UNITY_ANDROID
#endif
    }
    public void ToggleSpeechRecognition()
    {
        if (IsSpeaking.Data)
        {
            StopSpeechRecognition();
        }
        else
        {
            StartSpeechRecognition();
        }
    }
    public void onResults(string result)
    {
        // Update your UI with the recognized text
        OnResultCallback?.Invoke(result);
        Debug.Log("Result's onResults : " + result);
    }
    public void onMessage(string message)
    {
        Debug.Log("Received message from native code: " + message);

        if (message.Contains("CallStart"))
        {
            IsSpeaking.Data = true;
            Debug.Log("Speech recognition started");
        }
        else if (message.Contains("CallStop"))
        {
            IsSpeaking.Data = false;
            Debug.Log("Speech recognition stopped");
        }
    }
    public void onPartialResults(string result)
    {
        Debug.Log("Result's onPartialResults : " + result);
    }
    public void onError(string _value)
    {
        Debug.Log(_value);
    }

    public void OnResult(string result)
    {
        IsSpeaking.Data = false;
        OnResultCallback?.Invoke(result);
        Debug.Log("Result's OnResult : " + result);
    }

    public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError);
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                break;
            default:
                break;
        }
    }
}
