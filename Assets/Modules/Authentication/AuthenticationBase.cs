using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

namespace Authentication
{
    public abstract class AuthenticationBase : SingletonMono<AuthenticationBase>
    {
        protected Dictionary<SocialAuthType, ISocialAuth> auths = new Dictionary<SocialAuthType, ISocialAuth>();
        protected async override void Awake()
        {
            base.Awake();
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            auths = new Dictionary<SocialAuthType, ISocialAuth>();
            for (int i = 0; i <= (int)SocialAuthType.Guest; i++)
            {
                ISocialAuth auth = null;
                switch ((SocialAuthType)i)
                {
#if UNITY_ANDROID || UNITY_IOS
                    case SocialAuthType.Google:
                        auth = new LoginGoogle();
                        break;
#endif
#if UNITY_ANDROID
                    case SocialAuthType.GooglePlay:
                        break;
#endif
#if UNITY_ANDROID || UNITY_IOS
                    case SocialAuthType.Facebook:
                        break;
#endif
#if UNITY_IOS
                    case SocialAuthType.Apple:
                        auth = new LoginApple();
                        break;
#endif
                    case SocialAuthType.Guest:
                        auth = new LoginGuest();
                        break;
                    default:
                        break;
                }
                if (auth != null)
                {
                    auth.Initialize();
                    auths.Add((SocialAuthType)i, auth);
                }
            }
        }
        public abstract void Authenticate();
        public abstract void Signup(int type);
        public abstract void Signin(int type);
        public abstract void Signout(int type);
        public abstract void Delete();
        public abstract void UpdatePlayerName();

    } 
}
