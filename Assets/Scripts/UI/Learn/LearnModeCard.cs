using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LearnModeCard : MonoBehaviour
{
    public Button learnModeCard;
    [SerializeField] Image aroundCircle;
    [SerializeField] Sprite onSelectSprite;
    [SerializeField] Sprite onHiddenSprite;

    public void OnCardSelected(bool isSelect)
    {
        if (learnModeCard != null)
        {
            if (isSelect)
            {
                learnModeCard.image.sprite = onSelectSprite;
                learnModeCard.interactable = true;
            }
            else
            {
                learnModeCard.image.sprite = onHiddenSprite;
                learnModeCard.interactable = false;
            }
        }

        if (onHiddenSprite != null)
        {    
            if (isSelect) aroundCircle.gameObject.SetActive(true);
            else aroundCircle.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        DOTween.CompleteAll(gameObject);
    }

}