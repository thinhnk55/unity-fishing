using DG.Tweening;
using Framework;
using UnityEngine;

public class ScaleTweenUI : TweenUI
{
    [SerializeField] float startScale = 0f;
    [SerializeField] float endScale = 1f;
    protected override void Start()
    {
        target.SetScaleXY(startScale);
        base.Start();
    }

    public override void TweeningUI()
    {
        base.TweeningUI();
        target.DOScale(Vector3.one * endScale, TweenTime).SetEase(_ease);
    }
}
