using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Framework
{
    public static class DOTweenExtendExtensions
    {
        public static Tween DoDownAndUp(this Transform trans, Vector3 down, Vector3 up, float animateTime, TweenCallback onDownEnd = null, bool toNormal = false, Vector3 normal = default(Vector3))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DOScale(down, animateTime));
            if (onDownEnd != null) sequence.Join(DOVirtual.DelayedCall(animateTime, onDownEnd));
            sequence.Append(trans.DOScale(up, animateTime));
            if (toNormal) sequence.Append(trans.DOScale(normal, animateTime));
            return sequence;

        }
        public static Tween DoDownAndUp(this Transform trans, float down, float up, float animateTime, TweenCallback onDownEnd = null, bool toNormal = false, float normal = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DOScale(down, animateTime));
            if (onDownEnd != null) sequence.Join(DOVirtual.DelayedCall(animateTime, onDownEnd));
            sequence.Append(trans.DOScale(up, animateTime));
            if (toNormal) sequence.Append(trans.DOScale(normal, animateTime));
            return sequence;
        }

        public static Tween DoUpAndDown(this Transform trans, Vector3 up, Vector3 down, float animateTime, TweenCallback onUpEnd = null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DOScale(up, animateTime));
            if (onUpEnd != null) sequence.Join(DOVirtual.DelayedCall(animateTime, onUpEnd));
            sequence.Append(trans.DOScale(down, animateTime));
            return sequence;
        }
        public static Tween DoUpAndDown(this Transform trans, float up, float down, float animateTime, TweenCallback onUpEnd = null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DOScale(up, animateTime));
            if (onUpEnd != null) sequence.Join(DOVirtual.DelayedCall(animateTime, onUpEnd));
            sequence.Append(trans.DOScale(down, animateTime));
            return sequence;
        }


        public static Tween DoFactorUpAndDown(this Transform trans, float up, float down, float animateTime, TweenCallback onUpEnd = null)
        {
            var to1 = trans.localScale * up;
            var to2 = trans.localScale * down;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DOScale(to1, animateTime));
            if (onUpEnd != null) sequence.Join(DOVirtual.DelayedCall(animateTime, onUpEnd));
            sequence.Append(trans.DOScale(to2, animateTime));
            return sequence;
        }



        public static Tween DoRotateZ(this Transform trans, float z, float duration)
        {
            return trans.DORotate(new Vector3(0, 0, z), duration);
        }
        public static Tween DoShake(this Transform trans, float left, float right, float animateTime, float interval, bool toNormal = false, float normal = 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(trans.DORotate(new Vector3(0, 0, left), animateTime));
            sequence.Append(trans.DORotate(new Vector3(0, 0, right), animateTime));
            if (toNormal) sequence.Append(trans.DORotate(new Vector3(0, 0, normal), animateTime));
            sequence.AppendInterval(interval);
            return sequence;
        }

    }
}