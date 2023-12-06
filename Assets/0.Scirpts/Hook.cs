using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] Digger digger;
    //[SerializeField] QuickEffectAnimController valueTextItem;
    [SerializeField] TextMeshPro valueText;

    [SerializeField] GrabableObject currentHoldObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DigableObject"))
        {
            if (currentHoldObject != null) return;
            collision.GetComponent<IGrabable>().OnHookInteracted(this);
        }
        else if (collision.CompareTag("Wall"))
        {
            Pull();
        }
    }

    public void AttachObject(GrabableObject obj)
    {
        currentHoldObject = obj;
        digger.SetPullSpeedBasedOnObjectMass(obj.Mass);
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
        digger.StartPulling();
    }

    public void RemoveObject(bool despawnObj = true)
    {
        if (currentHoldObject == null) return;

        digger.SetPullSpeedBasedOnObjectMass(1);
        //if (despawnObj) currentHoldObject.gameObject.SetActive(false);
        currentHoldObject = null;
    }
}
