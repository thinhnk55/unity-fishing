using Framework;
using UnityEngine;

namespace Server
{
    [SerializeField]
    public class AuthData
    {
        public int userId;
        public string username;
        public string token;
        public string refresh_token;
    }
    public class DataAuth : PDataBlock<DataAuth>
    {
        [SerializeField] private int userId; public static int UserId { get { return Instance.userId; } set { Instance.userId = value; } }
        [SerializeField] private string username; public static string Username { get { return Instance.username; } set { Instance.username = value; } }
        [SerializeField] private string token; public static string Token { get { return Instance.token; } set { Instance.token = value; } }
        [SerializeField] private string refresh_token; public static string Refresh_token { get { return Instance.refresh_token; } set { Instance.refresh_token = value; } }
    }
}

