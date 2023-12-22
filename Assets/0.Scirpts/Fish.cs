using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fish : GrabableObject
{
    [SerializeField] float timeCountDownEnable;
    private float timer;

    protected override void OnEnable()
    {
        base.OnEnable();
        timer = timeCountDownEnable;
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
