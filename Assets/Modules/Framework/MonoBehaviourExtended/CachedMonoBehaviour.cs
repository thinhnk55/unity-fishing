using UnityEngine;

namespace Framework
{
    public class CacheMonoBehaviour : MonoBehaviour
    {
        private GameObject _gameObject;
        private Transform _transform;

        public Transform CacheTransform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;

                return _transform;
            }
        }

        public GameObject CacheGameObject
        {
            get
            {
                if (_gameObject == null)
                    _gameObject = gameObject;

                return _gameObject;
            }
        }

        public Vector3 Position { get { return CacheTransform.position; } set { CacheTransform.position = value; } }
        public Vector3Int PositionInt => Vector3Int.CeilToInt(CacheTransform.position);

        public Vector3 EulerAngles { get { return CacheTransform.eulerAngles; } set { CacheTransform.eulerAngles = value; } }
    }
}