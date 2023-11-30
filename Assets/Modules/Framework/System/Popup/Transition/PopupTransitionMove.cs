using DG.Tweening;
using UnityEngine;

namespace Framework
{
    public class PopupTransitionMove : PopupTransition
    {
        [Header("Reference")]
        [SerializeField] RectTransform _target = null;

        [Header("Config")]
        [SerializeField] Vector2 _startPos = Vector2.zero;
        [SerializeField] Vector2 _endPos = Vector2.zero;
        [SerializeField] Ease _ease = Ease.OutBack;
        [Range(0.1f, 1f)]
        [SerializeField] float _durationRatio = 1f;

        public override Tween ConstructTransition(PopupBehaviour popup)
        {
            if (_target == null)
                _target = GetComponent<RectTransform>();

            _target.anchoredPosition = _startPos;

            return _target.DOAnchorPos(_endPos, popup.OpenDuration * _durationRatio).SetEase(_ease);
        }

        public override Tween ConstructTransition(float animTime)
        {
            _target = GetComponent<RectTransform>();
            _target.anchoredPosition = _startPos;
            return _target.DOAnchorPos(_endPos, animTime * _durationRatio).SetEase(_ease);
          
        }
    }
}