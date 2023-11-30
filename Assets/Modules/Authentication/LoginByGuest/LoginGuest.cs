using Framework.SimpleJSON;
using Server;
using System;
using System.Text;
using UnityEngine;

namespace Authentication
{
    public class LoginGuest : ISocialAuth
    {
        public const long TOKEN_EXPIRED_TIME = 1 * 24 * 60 * 60 * 1000;
        public const string SECRET_KEY = "WeDonate10000$HzieKL";
        private string GenerateTokenDeviceId(string deviceId)
        {
            long expiredTime = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + TOKEN_EXPIRED_TIME) / 1000;
            JSONNode data = new JSONClass()
            {
                { "sub", deviceId },
                { "exp", new JSONData(expiredTime) },
                { "kid", Signature(deviceId, expiredTime)}
            };
            string id_token = data.ToString();
            string endcode = Convert.ToBase64String(Encoding.UTF8.GetBytes(id_token));
            return endcode;
        }

        private string Signature(string device, long expiredTime)
        {
            string data = SECRET_KEY + device + expiredTime;
            string signature = SHA256Hash.GetSHA256Hash(data);
            return signature;
        }

        public void Initialize()
        {

        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void SignUp()
        {
            throw new NotImplementedException();
        }

        public void SignIn()
        {
            Debug.Log("Login By Guest");
            HTTPClientAuth.LoginByGuest(GenerateTokenDeviceId(SystemInfo.deviceUniqueIdentifier));
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

    }
}