using Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupHint : PopupBehaviour
{
    [SerializeField] Button buttonShowHint;
    [SerializeField] Image itemImg;
    [SerializeField] TextMeshProUGUI word;
    [SerializeField] Sprite cheeseImg;
    private void Start()
    {
        FishingManager.Instance.OnStopFishing();
        buttonShowHint.onClick.AddListener(ButtonShowHintOnClick);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        FishingManager.Instance.OnStartFishing();
        buttonShowHint.onClick.RemoveListener(ButtonShowHintOnClick);
    }

    private void ButtonShowHintOnClick()
    {
        itemImg.sprite = cheeseImg;
        itemImg.SetNativeSize();
        buttonShowHint.gameObject.SetActive(false);
        word.SetText("Cheese");
        word.gameObject.SetActive(true);
    }

}
