using Framework;
using UnityEngine;
using UnityEngine.UI;
public class AnswerInfo : IDataUnit<AnswerInfo>
{
    public int Index { get; set; }
    public int indexIPA;
    public bool isSetSprite = false;
}
public class AnswerCard : CardBase<AnswerInfo>
{
    [SerializeField] private Image centerImage;
    [SerializeField] private Image ansBtnImage;
    public static Callback<int,int> AnswerChose;
    public override void BuildView(AnswerInfo info)
    {
        base.BuildView(info);
        if(Info.isSetSprite) centerImage.sprite = PhonicsConfig.PhonicSprite[info.indexIPA].sprites[0];
    }
    public void OnChoseAnswer()
    {

        AnswerChose?.Invoke(Info.indexIPA, Info.Index);
    }
    public void SetSprite(Sprite centerSprite = null, Sprite btnSprite = null)
    {
        if (centerSprite != null)
        {
            centerImage.sprite = centerSprite;
            centerImage.SetAlpha(1f);
        }
        if (btnSprite != null) ansBtnImage.sprite = btnSprite;
    }

    public void SetCenterImageAlpha(float alphaIndex)
    {
        centerImage.SetAlpha(alphaIndex);
    }
}