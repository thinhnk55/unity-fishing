namespace Framework
{
    /// <summary>
    /// Also a singleton, but won't be destroyed when new scene loaded
    /// </summary>
    public class HardSingletonMono<T> : SingletonMono<T> where T : CacheMonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(CacheGameObject);
        }

        protected override void OnDestroy()
        {

        }
    }
}