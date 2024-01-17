using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPClientBase
{

    static public IEnumerator Get(string url, Callback<string> callback)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string response = webRequest.downloadHandler.text;
            callback?.Invoke(response);
            Debug.Log("Response: " + response);
        }
        else
        {
            Debug.LogError("Error: " + webRequest.error);
        }
    }

    static public IEnumerator Post(string url, string data, Callback<string> callback)
    {
        /**/
        byte[] bodyRaw = UTF8Encoding.UTF8.GetBytes(data);
        using UnityWebRequest webRequest = new(url, "POST");

        Debug.Log(url + data);
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string response = webRequest.downloadHandler.text;
            Debug.Log("Response: " + response);
            callback?.Invoke(response);
        }
        else
        {
            Debug.LogError("Error: " + webRequest.error);
        }

    }
    static public IEnumerator LoadAudioFromURL(string url, AudioType audioType ,Callback<AudioClip> callback)
    {
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
            callback?.Invoke(audioClip);
        }
        else
        {
            Debug.LogError("Error downloading audio: " + www.error);
        }
    }

    static public IEnumerator LoadTextureFromURL(string url, Callback<Texture2D> callback)
    {
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                callback?.Invoke(texture);
            }
            else
            {
                Debug.Log("Error dowmloading sprite: " + www.error);
            }        
    }
}
