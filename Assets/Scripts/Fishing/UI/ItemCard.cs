using DG.Tweening;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    public RectTransform rect;
    [SerializeField] Image imgBG;
    [SerializeField] int index;
    [SerializeField] bool isShowedHint;
    public bool IsShowedHint => isShowedHint;

    [Header("Anim Scale")]
    [SerializeField] float timeScaleToOne;
    [SerializeField] float timeSpeech;
    [SerializeField] float scaleDefault;
    [SerializeField] float scaleTo;
    public float TimeScaleToOne { get { return timeScaleToOne; } }
    public float TimeSpeech { get { return timeSpeech; } }

    [Header("Sprite")]
    [SerializeField] Sprite nomarlSprite;
    [SerializeField] Sprite correctSprite;
    [SerializeField] Sprite pickedSprite;
    // Start is called before the first frame update

    private void PlaySound()
    {
        Debug.Log("Play Sound: " + SpriteFactory.Items[FishingManager.Instance.itemsCorrect[index]].name);
    }

    public void PlayAnimPlaySound()
    {
        imgBG.sprite = pickedSprite;
        transform.DOScale(scaleTo, timeSpeech / 2)
        .OnComplete(() =>
        {
            PlaySound();
            transform.DOScale(scaleDefault, timeSpeech / 2)
            .OnComplete(() =>
            {
                SetDefault();
            });
        });
    }

    public void ScaleToOne()
    {
        imgBG.sprite = nomarlSprite;
        transform.DOScale(scaleDefault, timeScaleToOne)
        .OnComplete(() =>
        {
            PlayAnimPlaySound();
        });
    }

    public void ScaleToZero()
    {
        transform.DOScale(0, timeScaleToOne)
        .OnComplete(() =>
        {
            ScaleToOne();
        });
    }

    public void PlayEffectWin()
    {
        ItemCollection.Instance.blurryScreen.gameObject.SetActive(true);
        imgBG.sprite = correctSprite;
        transform.SetAsLastSibling();
    }

    public void SetDefault()
    {
        imgBG.sprite = nomarlSprite;
        transform.SetSiblingIndex(index);
    }

    public void ChangeRequireItem()
    {
        PlaySound();
    }
}
