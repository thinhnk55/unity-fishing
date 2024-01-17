using DG.Tweening;
using UnityEngine;

namespace Framework
{
    public class PopupTransitionScale : PopupTransition
    {
        [Header("References")]
        [SerializeField] Transform _target = null;
        [Header("Config")]
        [SerializeField] float _startScale = 0f;
        [SerializeField] float _endScale = 1f;
        [SerializeField] Ease _ease = Ease.OutBack;
        [Range(0.1f, 1f)]
        [SerializeField] float _scaleDurationRatio = 1f;

        public override Tween ConstructTransition(PopupBehaviour popup)
        {
            if(_target == null)
                _target = popup.transform;

            // Set target start scale
            _target.SetScaleXY(_startScale);

            return _target.DOScale(Vector3.one * _endScale, popup.OpenDuration * _scaleDurationRatio).SetEase(_ease);
        }

        public override Tween ConstructTransition(float animTime)
        {
            _target.SetScaleXY(_startScale);
            return _target.DOScale(Vector3.one * _endScale, animTime * _scaleDurationRatio).SetEase(_ease);
        }
    }
}