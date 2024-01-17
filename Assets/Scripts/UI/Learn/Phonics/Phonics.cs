using Framework;
using UnityEngine;
using UnityEngine.UI;

public class Phonics : SingletonMono<Phonics>
{
    public AudioSource audioSource;
    [SerializeField] private GameObject mascotImage;
    [SerializeField] private CanvasGroup moonImage;
    [SerializeField] private AnimationImage animationImage;
    [SerializeField] private Image phonicImage;
    [SerializeField] private Image bgImage;
    [SerializeField] private CanvasScaler canvasScaler;

    private bool canClickBtn = true;
    private void Start()
    {
        InvokeRepeating(nameof(ReplayPhonicSound), 0.1f, PhonicsConfig.TimeReplaySound);
        phonicImage.sprite = PhonicsConfig.PhonicSprite[GameData.PhonicIndex].sprites[2];
    }

    private void ReplayPhonicSound()
    {
        if (audioSource.isPlaying) return;
        audioSource.PlayOneShot(PhonicsConfig.AudioClips[GameData.PhonicIndex]);
    }

    #region public 
    public void OnClickNextBtn()
    {
        if (canClickBtn)
        {
            canClickBtn = false;
            CancelInvoke(nameof(ReplayPhonicSound));
            audioSource.Stop();
            PopupHelper.Create(PhonicsConfig.QuestionAPI);
        }
    }
    public void OnClickSwitchBtn()
    {
        if (mascotImage.activeSelf)
        {
            mascotImage.SetActive(false);
            moonImage.alpha = 1f;
            animationImage.PlayAnimUI(PhonicsConfig.ListIndexAnimSprites[GameData.PhonicIndex].listIndexs);
            bgImage.sprite = PhonicsConfig.BgSprite[1];
        }
        else
        {
            moonImage.alpha = 0f;
            mascotImage.SetActive(true);
            bgImage.sprite = PhonicsConfig.BgSprite[0];
        }
    }
    public void ChangeCanvasScaler()
    {
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 0.5f;
    }
    #endregion
}
