using Framework;
using Framework.SimpleJSON;
using System;
using UnityEngine;
using WebSocketSharp;
namespace Server
{
    public class WSClient : Singleton<WSClient>
    {
        public event Callback OnConnect;
        public event Callback OnDisconnect;
        public event Callback OnLostConnection;
        public event Callback OnSystemError;
        public event Callback OnTokenInvalid;
        public event Callback OnLoginInOtherDevice;
        public event Callback OnAdminKick;
        public WebSocket ws;
        public void Connect(int userId, string token)
        {
            Messenger<ServerResponse>.AddListener<JSONNode>(ServerResponse.CheckLoginConnection, CheckLoginConnection);
            Messenger<GameEvent>.AddListener(GameEvent.LostConnection, OnLostConnection);
            ws = new WebSocket(ServerConfig.WebSocketURL + "?id=" + userId + "&token=" + token);
            //ws = new WebSocket(ServerConfig.WebSocketURL + "?id="+ 12 + "&token=" + "7lnyeclvtjlk49en9b63dsx8e6q5tqyi");
            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
            ws.Connect();
            WSPingPong.Create();
        }
        public void Disconnect(bool unlistening)
        {
            Messenger<ServerResponse>.RemoveListener<JSONNode>(ServerResponse.CheckLoginConnection, CheckLoginConnection);
            Messenger<GameEvent>.RemoveListener(GameEvent.LostConnection, OnLostConnection);
            Debug.Log("Disconnect");
            if (unlistening)
            {
                OnDisconnect?.Invoke();
            }
            ws.OnOpen -= OnOpen;
            ws.OnMessage -= OnMessage;
            ws.OnError -= OnError;
            ws.Close();
            WSPingPong.Destroy();
        }
        public void Unlisten()
        {
            OnDisconnect?.Invoke();
        }
        public void Ping()
        {
            ws.Send("{\"id\":2}");
        }

        public void Send(JSONNode json)
        {
            try
            {
                ws.Send(json.ToString());
            }
            catch (Exception e)
            {
                if (e != null)
                {
                    Debug.LogError(e.ToString());
                }
                Messenger<GameEvent>.Broadcast(GameEvent.LostConnection);
                throw;
            }
            Debug.Log($"<color=#FFA500>{json}</color>");
        }
        public void OnOpen(object sender, EventArgs e)
        {
            Debug.Log("Open " + ((WebSocket)sender).Url);
        }
        public void OnMessage(object sender, MessageEventArgs e)
        {
            Debug.Log($"<color=yellow>{e.Data}</color>");
            MainThreadDispatcher.ExecuteOnMainThread(() =>
            {
                JSONNode idJson = JSON.Parse(e.Data)["id"];
                if (idJson != null)
                {
                    ServerResponse id = (ServerResponse)int.Parse(idJson);
                    if (Messenger<ServerResponse>.eventTable.ContainsKey(id))
                    {
                        Messenger<ServerResponse>.Broadcast(id, JSON.Parse(e.Data));
                    }
                }
            });
        }
        public void OnError(object sender, ErrorEventArgs e)
        {
            MainThreadDispatcher.ExecuteOnMainThread(() =>
            {
                Debug.Log("Error : " + e.Exception);
            });
        }
        /// <summary>
        /// Handle the login connection status
        /// </summary>
        /// <param name="data">
        /// 0: login successfully
        /// 1: system error
        /// 2: token invalid
        /// 3: login in other device
        /// 4: admin kick
        /// </param>
        public void CheckLoginConnection(JSONNode data)
        {
            switch (data["e"].AsInt)
            {
                case 0:
                    OnConnect?.Invoke();
                    break;
                case 1:
                    OnSystemError?.Invoke();
                    Disconnect(true);
                    break;
                case 2:
                    ws.Close();
                    OnTokenInvalid?.Invoke();
                    Disconnect(false);
                    break;
                case 3:
                    OnLoginInOtherDevice?.Invoke();
                    Disconnect(true);
                    break;
                case 4:
                    OnAdminKick?.Invoke();
                    Disconnect(false);
                    break;
                default:
                    break;
            }
        }
    }
}