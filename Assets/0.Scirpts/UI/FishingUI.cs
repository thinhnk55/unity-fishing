using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] Image requireTarget;
    void Awake()
    {
        FishingManager.Instance.OnChangeScore += OnChangeScore;
        OnChangeScore(5);
    }

    private void OnDestroy()
    {
        FishingManager.Instance.OnChangeScore -= OnChangeScore;
    }

    private void OnChangeScore(int score)
    {
        this.score.SetText(FishingManager.Instance.Score.ToString());
    } 
}
