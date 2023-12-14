using DG.Tweening;
using Framework;
using Framework.SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image imgBG;
    [SerializeField] int index;

    [Header("Anim Scale")]
    [SerializeField] float timeScaleToOne;
    [SerializeField] float timeSpeech;
    [SerializeField] float scaleDefault;
    [SerializeField] float scaleTo;
    public float TimeScaleToOne { get { return timeScaleToOne; } }
    public float TimeSpeech { get { return timeSpeech; } }

    [Header("Sprite")]
    [SerializeField] Sprite nomarl;
    [SerializeField] Sprite Correct;
    [SerializeField] Sprite Picked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void PlaySound()
    {
        Debug.Log("Play Sound");
    }

    public void PlayAnimPlaySound()
    {
        imgBG.sprite = Picked;
        transform.DOScale(scaleTo, timeSpeech/2)
        .OnComplete(() =>
        {
            PlaySound();
            transform.DOScale(scaleDefault, timeSpeech/2)
            .OnComplete(() =>
            {
                imgBG.sprite = nomarl;
            });
        });
    }

    public void ScaleToOne()
    {
        transform.DOScale(scaleDefault, timeScaleToOne)
        .OnComplete(() =>
        {
            PlayAnimPlaySound();
        });
    }
}
