using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationImage : MonoBehaviour
{
    [SerializeField] private Image animationImage;
    int indexOfSprite;

    public void PlayAnimUI(List<int> indexSprites)
    {
        indexOfSprite = 0;
        StartCoroutine(AnimationUI(indexSprites));
    }
    public IEnumerator AnimationUI(List<int> listIndexSprites)
    {
        if(indexOfSprite>= listIndexSprites.Count)
        {
            indexOfSprite = 0;
        }
        animationImage.sprite = PhonicsConfig.PhonicAnimSprites[listIndexSprites[indexOfSprite]];
        indexOfSprite++;
        yield return new WaitForSeconds(PhonicsConfig.TimeSetSprite);
        StartCoroutine(AnimationUI(listIndexSprites));
    }

}
