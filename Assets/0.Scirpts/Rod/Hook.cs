using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] GameObject headLine;
    [SerializeField] Rod rod;
    [SerializeField] GrabableObject currentHoldObject;
    public bool canCatch;

    private void Update()
    {
        this.transform.position = new Vector3(headLine.transform.position.x, this.transform.position.y, this.transform.position.z);
        if(this.transform.position.y < -FishingManager.Instance.halfHeightOfCamera)
        {
            Pull();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DigableObject"))
        {
            if (canCatch == false) return;
            if (currentHoldObject != null) return;
            collision.GetComponent<IGrabable>().OnHookInteracted(this);
        }
        //else if (collision.CompareTag("Wall"))
        //{
        //    Pull();
        //}
    }

    public void AttachObject(GrabableObject obj)
    {
        currentHoldObject = obj;
        //rod.SetPullSpeedBasedOnObjectMass(obj.Mass);
        Pull();
    }

    public void CollectObject()
    {
        if(currentHoldObject == null) return;

        var obj = currentHoldObject.GetComponent<GrabableObject>();
        if (obj != null)
        {
            obj?.OnCollectObject(this);
        }
    }

    public void Pull()
    {
        rod.StartPulling();
    }

    public void RemoveObject(bool despawnObj = true)
    {
        if (currentHoldObject == null) return;

        //rod.SetPullSpeedBasedOnObjectMass(1);
        //if (despawnObj) currentHoldObject.gameObject.SetActive(false);
        currentHoldObject = null;
    }
}
