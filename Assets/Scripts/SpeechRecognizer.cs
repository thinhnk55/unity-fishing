using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpeechRecognizer : SpeechRecognizerBase
{
    [SerializeField] Image[] redCircles;
    [SerializeField] float animDuration;
    Sequence[] sequences;
    Tween[] tweenDelay;
    private void Start()
    {
        sequences = new Sequence[redCircles.Length];
        tweenDelay = new Tween[redCircles.Length];
        for (int i = 0; i < redCircles.Length; i++)
        {
            int _i = i;
            redCircles[i].transform.localScale = Vector3.one / 3;
            redCircles[i].color = new Color(1, 0, 0, 0.5f);
            sequences[_i] = DOTween.Sequence();
            sequences[_i].Insert(0, redCircles[_i].transform.DOScale(1, animDuration))
                .Insert(0, redCircles[_i].DOFade(0.1f / redCircles.Length / redCircles.Length, animDuration))
                .AppendInterval(0.1f * animDuration / redCircles.Length)
                .SetEase(Ease.OutQuad).SetLoops(-1);
            sequences[_i].Pause();
        }
        IsSpeaking.OnDataChanged += OnToggle;
    }
    void OnToggle(bool isSpeaking)
    {
        if (isSpeaking)
        {
            for (int i = 0; i < redCircles.Length; i++)
            {
                int _i = i;
                float startTimeTween = animDuration * i / redCircles.Length;
                tweenDelay[i] = DOVirtual.DelayedCall(startTimeTween, () =>
                {
                    sequences[_i].Restart();
                });
            }
        }
        else
        {
            for (int i = 0; i < redCircles.Length; i++)
            {
                int _i = i;
                redCircles[_i].transform.localScale = Vector3.zero;
                redCircles[_i].color = Color.red;
                sequences[_i].Pause();
                tweenDelay[i].Kill();
            }
        }
    }
}
