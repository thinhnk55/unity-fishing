using Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PopupHint : PopupBehaviour
{
    AudioManager audioManager;

    public int idWord;
    [SerializeField] Button buttonShowHint;
    [SerializeField] Image itemImg;
    [SerializeField] TextMeshProUGUI word;

    [Header("Popup hint star cost - negative number means subtract")]
    [SerializeField] int starCostPerHint = -8;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

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
        // do cost của một hint là 8 nên nếu chỉ có nhỏ hơn hoặc bằng 7 sao thì sẽ không được sử dụng hint
        if (FishingManager.Instance.Score <= 7 )
            return;

        // số sao sẽ trừ đi khi dùng hint là starCostPerHint
        FishingManager.Instance.AddScore(starCostPerHint);

        // khi đã dùng hint thì trừ sao và bật âm thanh hint
        audioManager.PlayHintAudio();

        itemImg.sprite = SpriteFactory.Items[idWord];
        itemImg.SetNativeSize();
        buttonShowHint.gameObject.SetActive(false);
        word.SetText(SpriteFactory.Items[idWord].name);
        word.gameObject.SetActive(true);
    }

}
