using UnityEngine;

namespace Framework
{
    public static partial class GameObjectExtensions
    {
        public static void RemoveComponent<T>(this GameObject gameObject) where T : Object
        {
            T component = gameObject.GetComponent<T>();
            if (component != null)
                MonoBehaviour.Destroy(component);
        }

        public static void DestroyChildren(this GameObject gameObject)
        {
            Transform transform = gameObject.transform;

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this GameObject gameObject)
        {
            Transform transform = gameObject.transform;

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public static void SetActiveChildren(this GameObject gameObject, bool isActive)
        {
            Transform transform = gameObject.transform;

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.SetActive(isActive);
            }
        }

        public static void SetLayerRecursively(this GameObject obj, int newLayer)
        {
            obj.layer = newLayer;

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                SetLayerRecursively(obj.transform.GetChild(i).gameObject, newLayer);
            }
        }
        public static void SetChildrenRecursively<T>(this GameObject obj, Callback<T> callback) where T : Object
        {
            T component = obj.GetComponent<T>();
            if (component)
            {
                callback?.Invoke(component);
            }
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                SetChildrenRecursively(obj.transform.GetChild(i).gameObject, callback);
            }
        }
        #region Create

        public static GameObject Create(this GameObject gameObject)
        {
            return Object.Instantiate(gameObject);
        }

        public static GameObject Create(this GameObject gameObject, Transform parent)
        {
            return Object.Instantiate(gameObject, parent);
        }

        public static GameObject Create(this GameObject gameObject, Transform parent = null, bool worldPositionStays = true)
        {
            return Object.Instantiate(gameObject, parent, worldPositionStays);
        }

        public static GameObject Create(this GameObject gameObject, Vector3 position)
        {
            return Object.Instantiate(gameObject, position, Quaternion.identity);
        }

        public static GameObject CreateRelative(this GameObject gameObject, Transform target = null)
        {
            GameObject obj = Object.Instantiate(gameObject);

            if (target != null)
            {
                obj.transform.position = target.position + gameObject.transform.position;
                obj.transform.eulerAngles = target.eulerAngles + gameObject.transform.eulerAngles;
            }
            else
            {
                obj.transform.position = gameObject.transform.position;
                obj.transform.eulerAngles = gameObject.transform.eulerAngles;
            }

            return obj;
        }

        public static GameObject CreateRelative(this GameObject gameObject, Vector3 position)
        {
            GameObject obj = Object.Instantiate(gameObject);

            obj.transform.position = gameObject.transform.position + position;
            obj.transform.eulerAngles = gameObject.transform.eulerAngles;

            return obj;
        }

        public static GameObject CreateRelativeLocal(this GameObject gameObject, Transform parent)
        {
            GameObject obj = Object.Instantiate(gameObject, parent);
            obj.transform.localPosition = gameObject.transform.position;
            obj.transform.localScale = gameObject.transform.localScale;
            return obj;
        }

        public static GameObject CreateFitUI(this GameObject prefab, RectTransform parent)
        {
            GameObject go = Object.Instantiate(prefab, parent, false);

            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            return go;
        }

        #endregion
    }
}