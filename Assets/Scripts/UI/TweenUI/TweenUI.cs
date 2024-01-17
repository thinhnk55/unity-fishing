using DG.Tweening;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float TweenTime;
    protected float TweenTimeDelay = 0f;
    [SerializeField] protected TweenUI TweenUIBefore;
    [SerializeField] protected Ease _ease = Ease.OutBack;

    protected virtual void Start()
    {
        if (target == null) target = transform;
        if (TweenUIBefore!=null)
        {
            TweenTimeDelay += TweenUIBefore.TweenTime;
        }

        DOVirtual.DelayedCall(TweenTimeDelay, () => {
            if (TweenUIBefore != null)
            {
                TweenTimeDelay += TweenUIBefore.TweenTimeDelay;
                DOVirtual.DelayedCall(TweenUIBefore.TweenTimeDelay, () => { TweeningUI(); });
            }
            else TweeningUI();
        });
    }
    public virtual void TweeningUI()
    {
    }

    protected void OnDestroy()
    {
        if (DOTween.IsTweening(gameObject)) DOTween.KillAll(gameObject);
    }
}
