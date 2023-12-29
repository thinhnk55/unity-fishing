using Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PopupHint : PopupBehaviour
{
    public int idWord;
    [SerializeField] Button buttonShowHint;
    [SerializeField] Image itemImg;
    [SerializeField] TextMeshProUGUI word;

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
        if (FishingManager.Instance.Score <= 0)
            return;

        FishingManager.Instance.AddScore(-1);
        itemImg.sprite = SpriteFactory.Items[idWord];
        itemImg.SetNativeSize();
        buttonShowHint.gameObject.SetActive(false);
        word.SetText(SpriteFactory.Items[idWord].name);
        word.gameObject.SetActive(true);
    }

}
