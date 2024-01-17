using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
    /// <summary>
    /// Spawn an object from a pool
    /// If first time spawn object, create a new pool
    /// </summary>
    public class ObjectPoolManager : SingletonMono<ObjectPoolManager>
    {

        static Dictionary<GameObject, BasePool> objectPoolDict = new Dictionary<GameObject, BasePool>();
        [SerializeField] List<GameObject> beforeLoadObject;
        [SerializeField] Canvas canvasRoot;
        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < beforeLoadObject.Count; i++)
            {
                SpawnObject<Component>(beforeLoadObject[i], Vector3.zero).gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// Spawn an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab">Object need to spawn</param> 
        /// <param name="pos">Position of object being spawned</param>
        /// <param name="root">Parent of object being spawned</param>
        /// <param name="isUI">Is object UI</param>
        /// <param name="quantity">Init quantity of pool</param>
        public static T SpawnObject<T>(GameObject prefab, Vector3 pos, Transform root = null, bool isUI = false, int? quantity = null) where T : Component
        {
            if (isUI)
            {
                if (!Instance.canvasRoot)
                {
                    SetCanvas();
                }
                if (!root)
                {
                    root = Instance.canvasRoot.transform;
                }
            }
            BasePool pool;
            if (!objectPoolDict.ContainsKey(prefab))
            {
                pool = CreatePool<T>(prefab, root, isUI, quantity);
                objectPoolDict.Add(prefab, pool);
            }
            else
            {
                pool = objectPoolDict[prefab];
            }
            T obj = pool.GetItem<T>(root);
            obj.transform.position = pos;
            return obj;
        }

        static void SetCanvas()
        {
            if (!Instance.canvasRoot || !Instance.canvasRoot.gameObject)
            {
                Instance.canvasRoot = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
                Instance.canvasRoot.planeDistance = 10;
                Instance.canvasRoot.worldCamera = Camera.main;
            }
        }

        static BasePool CreatePool<T>(GameObject prefab, Transform root = null, bool isUI = false, int? quatity = null)
        {
            BasePool pool;
            if (root == null)
            {
                GameObject @object = new GameObject(typeof(T).ToString() + " Pool");
                root = @object.transform;
                if (!isUI)
                {
                    root.SetParent(Instance.transform);
                }
                else
                {
                    root = root.gameObject.AddComponent<RectTransform>();
                    root.SetParent(Instance.canvasRoot.transform);
                    root.transform.localScale = Vector3.one;
                    root.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                }

            }

            // init quantity of pool object
            if (quatity != null)
            {
                pool = new BasePool(prefab, quatity.Value, root);
            }
            else if (PoolConfig.InitPool.ContainsKey(prefab))
            {
                pool = new BasePool(prefab, PoolConfig.InitPool[prefab], root);
            }
            else
            {
                pool = new BasePool(prefab, PoolConfig.DefaultInitPoolGO, root);
            }
            return pool;
        }
    }
}