using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] Image requireTarget;
    void Awake()
    {
        FishingManager.instance.OnChangeScore += OnChangeScore;
        FishingManager.instance.OnChangeTargetRequire += OnChangeTargetRequire;
    }

    private void OnDestroy()
    {
        FishingManager.instance.OnChangeScore -= OnChangeScore;
        FishingManager.instance.OnChangeTargetRequire -= OnChangeTargetRequire;
    }

    private void OnChangeScore(int score)
    {
        //this.score.SetText(score.ToString());
    } 

    private void OnChangeTargetRequire(Sprite targetRequire)
    {
        this.requireTarget.sprite = targetRequire;
    }
}
