using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPlayer : MonoBehaviour
{
    [SerializeField] Image faceSpriteRendererSelf;
    [SerializeField] Sprite nomarlFaceSprite;
    [SerializeField] Sprite happyFaceSprite;
    [SerializeField] Sprite sadlFaceSprite;
    // Start is called before the first frame update
    void Start()
    {
        FishingManager.Instance.OnStartFishing += OnStartFishing;
        FishingManager.Instance.OnChangeScore += OnChangeScore;
    }

    private void OnChangeScore(int score)
    {
        if(score > 0)
        {
            faceSpriteRendererSelf.sprite = happyFaceSprite;
        }
        else
        {
            faceSpriteRendererSelf.sprite = sadlFaceSprite;
        }
    }

    private void OnStartFishing()
    {
        faceSpriteRendererSelf.sprite = nomarlFaceSprite;
    }
}
