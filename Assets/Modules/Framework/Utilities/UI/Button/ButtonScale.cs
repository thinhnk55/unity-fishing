using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Framework
{
    public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Reference")]
        [SerializeField] Transform _tfTarget;

        [Header("Config")]
        [SerializeField] Vector2 _scaleValue = new Vector2(1f, 1.1f);
        [Min(0.1f)]
        [SerializeField] float _scaleSpeed = 1f;
        [SerializeField] bool _playSound = true;

        bool _isDown = false;
        Tween _tween;

        #region Monobehaviour

        void Awake()
        {
            if (_tfTarget == null)
                _tfTarget = transform;
            _tfTarget.SetScaleXY(_scaleValue.x);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Play Tween

        void ScaleUp()
        {
            _tween?.Kill();

            float duration = Mathf.Abs((_scaleValue.y - _tfTarget.localScale.x) / _scaleSpeed);

            _tween = _tfTarget.DOScale(_scaleValue.y, duration)
                .SetUpdate(true);
        }

        void ScaleDown()
        {
            _tween?.Kill();

            float duration = Mathf.Abs((_scaleValue.x - _tfTarget.localScale.x) / _scaleSpeed);

            _tween = _tfTarget.DOScale(_scaleValue.x, duration)
                .SetUpdate(true);
        }

        #endregion

        #region Pointer events

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;

            ScaleUp();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (_isDown)
                ScaleDown();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_isDown)
                ScaleUp();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _isDown = false;

            ScaleDown();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
        }

        #endregion
    }
}