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

    public GrabableObject currentHoldObject { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnCollision");
        if (collision.CompareTag("DigableObject"))
        {
            if (currentHoldObject != null) return;
            var fishing = collision.GetComponent<Fishing>();
            if (fishing)
            {
                if (!FishingManager.instance.CheckMatch(fishing.TypeFishing))
                {
                    digger.StartPulling();
                    return;
                }
            }

            collision.GetComponent<ICatchable>().OnHookInteracted(this);
        }
        else if (collision.CompareTag("Wall"))
        {
            Pull();
        }
    }

    public void AttachObject(GrabableObject obj)
    {
        enableStolenTimer = StartCoroutine(EnableStolen(2.5f));
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
            obj?.OnCollectObject(digger);
        }
    }

    public void Pull()
    {
        digger.StartPulling();
    }

    Coroutine enableStolenTimer;

    public void RemoveObject(bool despawnObj = true)
    {
        if (currentHoldObject == null) return;

        digger.SetPullSpeedBasedOnObjectMass(1);
        if (despawnObj) currentHoldObject.gameObject.SetActive(false);
        currentHoldObject = null;
        if (enableStolenTimer != null) StopCoroutine(enableStolenTimer);
    }

    IEnumerator EnableStolen(float time)
    {
        yield return new WaitForSeconds(time);
        if (currentHoldObject != null)
            currentHoldObject.Attachable = true;
    }

    public void OnStolen()
    {
        RemoveObject(false);
    }
}
