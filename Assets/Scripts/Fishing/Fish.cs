using   DG.Tweening;
using UnityEngine;

public class Fish : GrabableObject
{
    [SerializeField] float TimePerSpawn;
    private float timer;

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = TimePerSpawn;
    }

    public override void OnHookInteracted(Hook hook)
    {
        base.OnHookInteracted(hook);
        if (attachable)
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);
        }
    }

    public override void OnCollectObject(Hook collector)
    {
        FishingManager.Instance.OnStopFishing();
        DOVirtual.DelayedCall(0.5f, () =>
        {
            this.gameObject.SetActive(false);
            collector.RemoveObject(this);
            FishingManager.Instance.OnStartFishing();
        });
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
