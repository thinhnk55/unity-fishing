#if PLATFORM_IOS
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using UnityEngine;
using System;
using System.Text;
namespace Authentication
{
    public class LoginApple : ISocialAuth
    {
        private const string AppleUserIdKey = "AppleUserId";

        private IAppleAuthManager _appleAuthManager;

        public void SignInWithAppleButtonPressed()
        {
            //this.SetupLoginMenuForAppleSignIn();
            this.SignInWithApple();
        }

        private void InitializeLoginMenu()
        {
            // Check if the current platform supports Sign In With Apple
            if (this._appleAuthManager == null)
            {
                //this.SetupLoginMenuForUnsupportedPlatform();
                return;
            }

            // If at any point we receive a credentials revoked notification, we delete the stored User ID, and go back to login
            this._appleAuthManager.SetCredentialsRevokedCallback(result =>
            {
                Debug.Log("Received revoked callback " + result);
                PlayerPrefs.DeleteKey(AppleUserIdKey);
            });

            // If we have an Apple User Id available, get the credential status for it
            if (PlayerPrefs.HasKey(AppleUserIdKey))
            {
                var storedAppleUserId = PlayerPrefs.GetString(AppleUserIdKey);
                this.CheckCredentialStatusForUserId(storedAppleUserId);
            }
            // If we do not have an stored Apple User Id, attempt a quick login
            else
            {
                this.AttemptQuickLogin();
            }
        }

        private void CheckCredentialStatusForUserId(string appleUserId)
        {
            // If there is an apple ID available, we should check the credential state
            this._appleAuthManager.GetCredentialState(
                appleUserId,
                state =>
                {
                    switch (state)
                    {
                        // If it's authorized, login with that user id
                        case CredentialState.Authorized:
                            return;
                        // If it was revoked, or not found, we need a new sign in with apple attempt
                        // Discard previous apple user id
                        case CredentialState.Revoked:
                        case CredentialState.NotFound:
                            PlayerPrefs.DeleteKey(AppleUserIdKey);
                            return;
                    }
                },
                error =>
                {
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    Debug.LogWarning("Error while trying to get credential state " + authorizationErrorCode.ToString() + " " + error.ToString());
                });
        }

        private void AttemptQuickLogin()
        {
            var quickLoginArgs = new AppleAuthQuickLoginArgs();

            // Quick login should succeed if the credential was authorized before and not revoked
            this._appleAuthManager.QuickLogin(
                quickLoginArgs,
                credential =>
                {
                    // If it's an Apple credential, save the user ID, for later logins
                    var appleIdCredential = credential as IAppleIDCredential;
                    if (appleIdCredential != null)
                    {
                        PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                    }
                    Debug.Log("Quick login: " + Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode));
                    HTTPClient.Instance.LoginApple(Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode));
                },
                error =>
                {
                    // If Quick Login fails, we should show the normal sign in with apple menu, to allow for a normal Sign In with apple
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    Debug.LogWarning("Quick Login Failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                });
        }

        private void SignInWithApple()
        {
            var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

            this._appleAuthManager.LoginWithAppleId(
                loginArgs,
                credential =>
                {
                    // If a sign in with apple succeeds, we should have obtained the credential with the user id, name, and email, save it
                    PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                    var appleIdCredential = credential as IAppleIDCredential;
                    Debug.Log("Sign in with apple: " + Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode));
                    HTTPClient.Instance.LoginApple(Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode));
                },
                error =>
                {
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    Debug.LogWarning("Sign in with Apple failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                });
        }

        public void Initialize()
        {
            // If the current platform is supported
            if (AppleAuthManager.IsCurrentPlatformSupported)
            {
                // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
                var deserializer = new PayloadDeserializer();
                // Creates an Apple Authentication manager with the deserializer
                this._appleAuthManager = new AppleAuthManager(deserializer);
            }

            this.InitializeLoginMenu();
        }

        public void SignUp()
        {
            throw new NotImplementedException();
        }

        public void SignIn()
        {
            SignInWithApple();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        void ISocialAuth.Update()
        {
            if (this._appleAuthManager != null)
            {
                this._appleAuthManager.Update();
            }
        }
    }
}
#endif