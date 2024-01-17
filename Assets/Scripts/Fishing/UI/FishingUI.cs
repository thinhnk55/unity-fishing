using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] Image requireTarget;
    void Start()
    {
        FishingManager.Instance.OnChangeScore += OnChangeScore;
        //OnChangeScore(5); day la ban goc

        OnChangeScore(5);
    }
    private void OnDestroy()
    {
        try { FishingManager.Instance.OnChangeScore -= OnChangeScore; }
        catch (Exception e) { Debug.Log("Error: "+ e); }
    }
    private void OnChangeScore(int score)
    {
        this.score.SetText(FishingManager.Instance.Score.ToString());
    } 
}