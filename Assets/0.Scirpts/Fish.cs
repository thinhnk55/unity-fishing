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

    public override void OnCollectObject(Hook collector)
    {
        this.gameObject.SetActive(false);
        collector.RemoveObject(this);
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
