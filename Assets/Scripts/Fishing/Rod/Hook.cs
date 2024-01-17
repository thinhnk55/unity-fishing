using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Cache lại GameObject GameManager để track vào script FishingManager
    [SerializeField] GameObject GameManager;
    FishingManager fishingManager;

    // Get ra rigidbody của hook 
    Rigidbody2D hookRigidbody;

    [SerializeField] GameObject headLine;
    [SerializeField] Rod rod;
    [SerializeField] GrabableObject currentHoldObject;
    public bool canCatch;

    void Start()
    {
        // lấy ra script FishingManager
        fishingManager = GameManager.GetComponent<FishingManager>();

        hookRigidbody = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        transform.position = new Vector3(headLine.transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.y < -FishingManager.Instance.halfHeightOfCamera)
        {
            Pull();
        }

        CountFishScore();
    }

    void CountFishScore()
    {
        if (hookRigidbody.IsTouchingLayers(LayerMask.GetMask("Fish")))
        {
            Debug.Log("Đã nhận biết được cá con");
            // tăng điểm
        }
        else if (hookRigidbody.IsTouchingLayers(LayerMask.GetMask("Shark")))
        {
            Debug.Log("Đã nhận biết được cá mập");
            // trừ điểm
        }
        else
        {
            Debug.Log("Không bắt được con cá nào cả");
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
        if (currentHoldObject == null) return;

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
