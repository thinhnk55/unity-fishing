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
        FishingManager.Instance.OnChangeTargetRequire += OnChangeTargetRequire;
    }

    private void OnDestroy()
    {
        FishingManager.Instance.OnChangeScore -= OnChangeScore;
        FishingManager.Instance.OnChangeTargetRequire -= OnChangeTargetRequire;
    }

    private void OnChangeScore(int score)
    {
        //this.score.SetText(score.ToString());
    } 

    private void OnChangeTargetRequire(Sprite targetRequire)
    {
        //this.requireTarget.sprite = targetRequire;
    }


}
