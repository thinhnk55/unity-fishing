using DG.Tweening;
public class MoveTweenUI : TweenUI
{
    protected override void Start()
    {
        base.Start();
    }

    public override void TweeningUI()
    {
        base.TweeningUI();
        transform.DOMove(target.position, TweenTime).SetEase(_ease);
    }
}
