using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : GrabableObject
{
    public TypeFishing TypeFishing;
    [SerializeField] float timeCountDownEnable;
    private float timer;

    [Header("Anim Fly To Target")]
    [SerializeField] RectTransform root;
    [SerializeField] Transform target;
    [SerializeField] float timeFly;
    [SerializeField] float timeZoomOut;


    protected override void OnEnable()
    {
        base.OnEnable();
        timer = timeCountDownEnable;
    }

    public override void OnCollectObject(Hook collector)
    {
        //if (!FishingManager.instance.CheckMatch(TypeFishing))
        //{
        //}
        //else
        //{

        //}
        //target = ItemCollection.Instance.items[2].transform;
        this.gameObject.SetActive(false);
        collector.RemoveObject();
        //FlyToTarget();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            this.gameObject.SetActive(false);
        }
    }


    [SerializeField] Ease easeType;
    private void FlyToTarget()
    {
        Image itemFly = ObjectPoolManager.SpawnObject<Image>(PrefabFactory.ItemFly, this.transform.position, target, true);
        itemFly.rectTransform.localScale = Vector3.one;

        itemFly.rectTransform.DOAnchorPos(new Vector2(0, 0), timeFly).SetEase(easeType).OnComplete(() =>
        {
            itemFly.rectTransform.DOScale(0, timeZoomOut).OnComplete(() =>
            {
                itemFly.gameObject.SetActive(false);
            });
        });
    }
}
