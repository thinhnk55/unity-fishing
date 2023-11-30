using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Fishing : GrabableObject
{
    public TypeFishing TypeFishing;
    [SerializeField] float timeCountDownEnable;
    [SerializeField] Transform target;

    public override void OnCollectObject(Hook collector)
    {
        if (!FishingManager.instance.CheckMatch(TypeFishing))
        {
        }
        else
        {
                
        }

        this.gameObject.SetActive(false);
        collector.RemoveObject();
        // late: make anim fly to image require
    }

    private void Update()
    {
        timeCountDownEnable -= Time.deltaTime;   
        if(timeCountDownEnable < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    //private void FlyToTarget()
    //{
    //    this.transform.DOMove(target.localPosition, 2f).OnComplete(() =>
    //    {
    //        this.gameObject.SetActive(false);
    //    });
    //}
}
