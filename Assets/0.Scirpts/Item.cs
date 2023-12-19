using DG.Tweening;
using Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Item : GrabableObject
{
    public TypeFishing TypeFishing;
    [SerializeField] RectTransform rectSelf;
    [SerializeField] RectTransform uiCanvas;
    [SerializeField] float timeCountDownEnable;
  
    private float timer;

    [Header("Anim Fly To Target")]
    [SerializeField] RectTransform root;
    [SerializeField] Image frame;
    [SerializeField] Image itemImg;
    [SerializeField] GameObject effect;
    [SerializeField] ParticleSystem stars;
    [SerializeField] float scaleFrame;
    [SerializeField] float scaleItem;
    [SerializeField] float timeScaleFrame;
    [SerializeField] float timeFlyToTarget;
    [SerializeField] float timeFlyToCenter;
    [SerializeField] float timeZoomOut;
    [Header("Sprite")]
    [SerializeField] Sprite frameDefault;
    [SerializeField] Sprite frameCorrect;
    [SerializeField] Sprite frameWrong;


    protected override void OnEnable()
    {
        base.OnEnable();
        effect.SetActive(false);
        stars.Stop();
        frame.sprite = frameDefault;
        timer = timeCountDownEnable;
        this.rectSelf.SetScaleXY(1, 1);
        frame.rectTransform.SetScaleXY(1, 1);
        itemImg.rectTransform.SetScaleXY(1, 1);
        itemImg.rectTransform.SetParent(frame.rectTransform);
        itemImg.rectTransform.gameObject.SetActive(true);
    }

    public override void OnCollectObject(Hook collector)
    {
        //if (!FishingManager.instance.CheckMatch(TypeFishing))
        //{
        //}
        //else
        //{

        //}
        frame.sprite = frameCorrect;
        frame.rectTransform.DOScale(scaleFrame, timeScaleFrame);
        stars.Play();
        effect.SetActive(true);
        collector.RemoveObject();
        StartCoroutine(FlyToTarget(0));
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            //this.gameObject.SetActive(false);
        }
    }


    [SerializeField] Ease easeType;
    IEnumerator FlyToTarget(int id)
    {
        yield return new WaitForSeconds(0.75f);
        ItemCollection.Instance.items[id].PlayEffectWin();
        var renderer = this.stars.GetComponent<ParticleSystemRenderer>().sortingOrder = 115;
        this.rectSelf.SetParent(uiCanvas);
        this.rectSelf.DOScale(scaleItem, 0.5f);
        this.rectSelf.DOAnchorPos(new Vector2(0, -200), timeFlyToCenter);
        yield return new WaitForSeconds(timeFlyToCenter + 0.5f);

        Image itemFly = Instantiate<Image>(PrefabFactory.ItemFly, this.transform);
        itemFly.sprite = itemImg.sprite;
        itemFly.rectTransform.SetParent(ItemCollection.Instance.items[id].rect);
        itemFly.SetNativeSize();
        this.gameObject.SetActive(false);
        itemFly.rectTransform.DOScale(1, timeFlyToTarget);
        itemFly.rectTransform.DOAnchorPos(Vector2.zero, timeFlyToTarget);
        yield return new WaitForSeconds(timeFlyToTarget + 0.75f);
        Debug.Log("34");
        ItemCollection.Instance.items[id].ScaleToZero();
        yield return new WaitForSeconds(ItemCollection.Instance.items[id].TimeScaleToOne);
        Destroy(itemFly);
        
    }
}
