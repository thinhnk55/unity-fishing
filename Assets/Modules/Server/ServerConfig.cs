using Framework;
using System;
using UnityEngine;
namespace Server
{
    [Serializable]
    public class ServerConfig : SingletonScriptableObjectModulized<ServerConfig>
    {
        [SerializeField] private string webSocketURL; public static string WebSocketURL { get { return Instance.webSocketURL; } }
        [SerializeField] private string httpURL; public static string HttpURL { get { return Instance.httpURL; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
    }
}
