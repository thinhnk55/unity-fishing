#if FIREBASE
using DG.Tweening;
using UnityEngine;
namespace FirebaseIntegration
{
    public class FirebaseInitialization : MonoBehaviour
    {
        static public bool initialized = false;
        public delegate void Callback();
        public static Callback OnInitialized;
        void Start()
        {
            DOVirtual.DelayedCall(1, () =>
            {
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        FirebaseApp app = FirebaseApp.DefaultInstance;
                        Crashlytics.ReportUncaughtExceptionsAsFatal = true;
                        initialized = true;
                        OnInitialized?.Invoke();
                        Debug.Log("Firebase Initialize successfully");
                    }
                    else
                    {
                        Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                        initialized = false;
                    }
                });
            });
            // Initialize Firebase
        }
    }
}
#endif