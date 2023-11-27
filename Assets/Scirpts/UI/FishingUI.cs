using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    void Start()
    {
        FishingManager.instance.OnChangeScore += OnChangeScore;
    }

    private void OnDestroy()
    {
        FishingManager.instance.OnChangeScore -= OnChangeScore;
    }

    private void OnChangeScore(int score)
    {
        this.score.SetText(score.ToString());
    } 

}
