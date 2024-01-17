using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class FadeTweenUI : TweenUI
{
    CanvasGroup canvasGroup;
    protected override void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        base.Start();
    }
    public override void TweeningUI()
    {
        base.TweeningUI();
        if (canvasGroup != null )
        {
            canvasGroup.DOFade(1f, TweenTime).SetEase(_ease);
        }
    }
}
