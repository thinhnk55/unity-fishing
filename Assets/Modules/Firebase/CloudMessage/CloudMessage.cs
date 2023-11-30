#if FIREBASE
using UnityEngine;

namespace FirebaseIntegration
{
    public class CloudMessage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            FirebaseInitialization.OnInitialized += () =>
            {
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            };
        }

        public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        }

        public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            UnityEngine.Debug.Log("From: " + e.Message.From);
            UnityEngine.Debug.Log("Message ID: " + e.Message.MessageId);
        }
    }
}
#endif