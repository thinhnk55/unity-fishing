using System.Collections;
using System.Collections.Generic;
using System.Net;
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

}
