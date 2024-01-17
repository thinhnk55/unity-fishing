using UnityEngine;
using UnityEngine.Video;

public class OnlineVideoLoader : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public string videoUrl = "https://www.youtube.com/watch?v=1G8SCotE2yg";

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = videoUrl;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.Prepare();
    }
}