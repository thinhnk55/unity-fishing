using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;
namespace Framework
{
    public static class SpineExtensions
    {
        public static float GetDuration(this SkeletonAnimation skeletonAnimation, string animName)
        {
            return skeletonAnimation.skeleton.Data.FindAnimation(animName).Duration;
        }
        public static float GetDuration(this SkeletonGraphic skeletonAnimation, string animName)
        {
            return skeletonAnimation.SkeletonData.FindAnimation(animName).Duration;
        }
        public static void SetAnimation(this SkeletonAnimation skeletonAnimation, string animName, bool loop = false, bool isMix = false, int mixLine = 1)
        {
            var trackEntry = skeletonAnimation.AnimationState.SetAnimation(isMix ? 0 : mixLine, animName, loop);
            //skeletonAnimation.SkeletonDataAsset.dura = 5f;
            //Debug.Log("trackEntry " + trackEntry.MixDuration);
        }
        public static void SetAnimationWithScale(this SkeletonAnimation skeletonAnimation, string animName, bool loop ,float timeScale)
        {
            var trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, animName, loop);
            trackEntry.TimeScale = timeScale;
            //skeletonAnimation.SkeletonDataAsset.dura = 5f;
            //Debug.Log("trackEntry " + trackEntry.MixDuration);
        }
        public static void SetAnimation(this SkeletonGraphic skeletonAnimation, string animName, bool loop)
        {
            var trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, animName, loop);
        }
        public static void AddAnimation(this SkeletonAnimation skeletonAnimation, string animName, float delay, bool loop = false, bool isMix = false, int mixLine = 1)
        {
            var trackEntry = skeletonAnimation.AnimationState.AddAnimation(isMix ? 0 : mixLine, animName, loop, delay);
            //skeletonAnimation.SkeletonDataAsset.dura = 5f;
            //Debug.Log("trackEntry " + trackEntry.MixDuration);
        }
        public static void StopLine(this SkeletonAnimation skeletonAnimation, int mixLine, float mixTime = 0)
        {
            skeletonAnimation.AnimationState.SetEmptyAnimation(mixLine, mixTime);
        }
        public static void SetSkeletonDataAsset(this SkeletonAnimation skeletonAnimation, SkeletonDataAsset skeletonDataAsset)
        {
            skeletonAnimation.skeletonDataAsset.Clear();
            skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
            skeletonAnimation.Initialize(true);
        }
        public static void AddMixSkin(this SkeletonAnimation skeletonAnimation, string skinName)
        {
            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            var mixAndMatchSkin1 = skeletonData.Skins.Items[0];
            var mixAndMatchSkin2 = skeletonData.FindSkin(skinName);
            mixAndMatchSkin1.AddSkin(mixAndMatchSkin2);
            skeleton.SetSkin(mixAndMatchSkin1);
            skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }
        public static void MixSkin(this SkeletonAnimation skeletonAnimation, string skin1, string skin2)
        {

            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            var mixAndMatchSkin1 = skeletonData.FindSkin(skin1);
            var mixAndMatchSkin2 = skeletonData.FindSkin(skin2);
            mixAndMatchSkin1.AddSkin(mixAndMatchSkin2);
            skeleton.SetSkin(mixAndMatchSkin1);
            skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }
        public static void SetSkinRuntime(this SkeletonAnimation skeletonAnimation, string skinName)
        {
            skeletonAnimation.Skeleton.SetSkin(skinName);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }
        public static void SetSkinRuntime(this SkeletonGraphic skeletonAnimation, string skinName)
        {
            skeletonAnimation.Skeleton.SetSkin(skinName);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.LateUpdate();
        }
        public static void SetOrderLayer(this SkeletonAnimation skeletonAnimation, int sortingOrder)
        {

            skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        }
        public static int GetOrderLayer(this SkeletonAnimation skeletonAnimation)
        {
            return skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder;
        }
        public static Tween DOColor(this SkeletonAnimation skeletonAnimation, Color endValue, float duration)
        {
            return DOTween.To(() => skeletonAnimation.skeleton.GetColor(), co => { skeletonAnimation.skeleton.SetColor(co); }, endValue, duration);
        }
    }
}