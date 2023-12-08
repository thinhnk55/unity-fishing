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
    [SerializeField] Image img;

    [Header("Anim Scale")]
    [SerializeField] float timeScale;
    [SerializeField] float scaleDefault;
    [SerializeField] float scaleTo;
    public float TimeScale { get { return timeScale; } }
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
        transform.DOScale(scaleTo, timeScale)
        .OnComplete(() =>
        {
            PlaySound();
            transform.DOScale(scaleDefault, timeScale);
        });

    }
}
