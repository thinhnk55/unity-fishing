using Google;
using Server;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Authentication
{
    public class LoginGoogle : ISocialAuth
    {
        public string webClientId = "555442977696-0e53ilirr6l6hvu7u567bu4a4c0ekodg.apps.googleusercontent.com";


        private GoogleSignInConfiguration configuration;

        /*    private void Update()
            {
                Debug.Log("config");
                configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
                Debug.LogError(configuration);
            }*/

        public void SignInWithGoogle() { OnSignIn(); }
        public void SignOutFromGoogle() { OnSignOut(); }

        private void OnSignIn()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        private void OnSignOut()
        {
            GoogleSignIn.DefaultInstance.SignOut();
        }

        public void OnDisconnect()
        {
            GoogleSignIn.DefaultInstance.Disconnect();
        }

        internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted)
            {
                using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        Debug.Log(error);
                    }
                    else
                    {
                        Debug.Log("Error!!!!!!!!");
                    }
                }
            }
            else if (task.IsCanceled)
            {
                Debug.Log("isCanceled!!!");
            }
            else
            {
                Debug.Log("Login google Done");
                Debug.Log(task.Result.IdToken);
                HTTPClientAuth.LoginGoogle(task.Result.IdToken);
            }
        }

        public void OnSignInSilently()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;

            GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
        }

        public void OnGamesSignIn()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = true;
            GoogleSignIn.Configuration.RequestIdToken = false;

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        /*    private void AddToInformation(string str) { infoText.text += "\n" + str; }*/

        public void Initialize()
        {
            configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        }

        public void SignUp()
        {
            throw new NotImplementedException();
        }

        public void SignIn()
        {
            SignInWithGoogle();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
        }
    }
}
