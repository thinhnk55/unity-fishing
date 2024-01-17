using Framework;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class SingletonMono<T> : CacheMonoBehaviour where T : CacheMonoBehaviour
{
    static object _lock = new object();
    static T _instance;
    [SerializeField] protected bool dontDestroyOnLoad;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance { get { return GetInstance(false); } }

    public static T SafeInstance { get { return GetInstance(true); } }

    public static T GetInstance(bool safeGet)
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                // Search for existing instance.
                _instance = (T)FindObjectOfType(typeof(T));
                // Create new instance if one doesn't already exist.
                if (safeGet && _instance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    #region MonoBehaviour

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            _instance = this as T;
        }
        else
        {
            PDebug.Log("Duplicate singleton {0} found: {1}", typeof(T).ToString(), this);
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    #endregion
}
