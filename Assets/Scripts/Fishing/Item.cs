using DG.Tweening;
using Framework;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Item : GrabableObject
{
    public int idItem;
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
        rectSelf.SetScaleXY(1, 1);
        frame.rectTransform.SetScaleXY(1, 1);
        itemImg.rectTransform.SetScaleXY(1, 1);
        itemImg.rectTransform.SetParent(frame.rectTransform);
        itemImg.rectTransform.gameObject.SetActive(true);
    }

    public override void OnCollectObject(Hook collector)
    {
        FishingManager.Instance.OnStopFishing();
        int idCard = FishingManager.Instance.CheckMatch(idItem);
        if (idCard >= 0)
        {
            frame.sprite = frameCorrect;
            frame.rectTransform.DOScale(scaleFrame, timeScaleFrame);
            stars.Play();
            effect.SetActive(true);
            collector.RemoveObject();
            FlyToTarget(idCard);
        }
        else
        {
            frame.sprite = frameWrong;
            frame.rectTransform.DOScale(scaleFrame, timeScaleFrame);
            collector.RemoveObject();
            DOVirtual.DelayedCall(0.5f, () =>
            {
                this.gameObject.SetActive(false);
                FishingManager.Instance.OnStartFishing();
            });
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetImage(Sprite image)
    {
        itemImg.sprite = image;
    }

    CancellationTokenSource cancellationTokenSource;
    async void FlyToTarget(int id)
    {
        try
        {
            cancellationTokenSource = new CancellationTokenSource();
            ItemCard itemCardTarget = ItemCollection.Instance.items[id];
            await Task.Delay(750, cancellationTokenSource.Token);

            itemCardTarget.PlayEffectWin();
            var renderer = this.stars.GetComponent<ParticleSystemRenderer>().sortingOrder = 115;
            this.rectSelf.SetParent(uiCanvas);
            this.rectSelf.DOScale(scaleItem, timeFlyToCenter);
            this.rectSelf.DOAnchorPos(new Vector2(0, -200), timeFlyToCenter);
            await Task.Delay((int)((timeFlyToCenter)*1000), cancellationTokenSource.Token);

            Image itemFly = ObjectPoolManager.SpawnObject<ItemFly>(PrefabFactory.ItemFly, this.transform.position, this.transform).GetComponent<Image>();
            itemFly.sprite = itemImg.sprite;
            itemFly.rectTransform.SetParent(itemCardTarget.rect);
            itemFly.SetNativeSize();
            this.gameObject.SetActive(false);
            itemFly.rectTransform.DOScale(1, timeFlyToTarget);
            itemFly.rectTransform.DOAnchorPos(Vector2.zero, timeFlyToTarget);
            await Task.Delay((int)((timeFlyToTarget + 0.5f) * 1000), cancellationTokenSource.Token);

            itemCardTarget.ScaleToZero();
            FishingManager.Instance.ChangeTarget(id);
            await Task.Delay((int)(itemCardTarget.TimeScaleToOne * 1000), cancellationTokenSource.Token);

            itemFly.gameObject.SetActive(false);
            await Task.Delay((int)(itemCardTarget.TimeScaleToOne * 1000) + (int)(itemCardTarget.TimeSpeech * 1000), cancellationTokenSource.Token);

            ItemCollection.Instance.blurryScreen.gameObject.SetActive(false);
            FishingManager.Instance.OnStartFishing();
        }
        catch
        {
            Debug.Log("Task was cancelled!");
            return;
        }
        finally
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
}