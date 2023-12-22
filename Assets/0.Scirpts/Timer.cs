using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    void Update()
    {
        if (FishingManager.Instance.IsGameOver) return;

        remainingTime -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        if (seconds < 0)
        {
            seconds = 0;
            FishingManager.Instance.GameOver(false);
        }

        timerText.SetText(seconds.ToString());
    }
}
