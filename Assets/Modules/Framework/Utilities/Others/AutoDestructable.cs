using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Framework
{
    public class AutoDestructable : CacheMonoBehaviour
    {
        public enum DestructType
        {
            Deactive,
            Descript,
            Disable,
            Destroy
        }
        [Header("Config")]
        [SerializeField] protected float delay = 0f;
        [SerializeField] protected DestructType destructType;
        [SerializeField] UnityEvent OnDestruct;
        protected Tween _tween;

        #region MonoBehaviour

        virtual protected void OnEnable()
        {
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(delay, Destruct);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Public

        public void ResetDestruct()
        {
            _tween?.Restart();
        }

        public void Construct(float delay)
        {
            this.delay = delay;
            OnEnable();
        }

        #endregion

        void Destruct()
        {
            switch (destructType)
            {
                case DestructType.Deactive:
                    CacheGameObject.SetActive(false);
                    break;
                case DestructType.Descript:
                    Destroy(this);
                    break;
                case DestructType.Disable:
                    this.enabled = false;
                    break;
                case DestructType.Destroy:
                    Destroy(CacheGameObject);
                    break;
                default:
                    break;
            }
            OnDestruct?.Invoke();
        }
    }
}