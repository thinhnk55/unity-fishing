using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonicQuestion : MonoBehaviour
{
    [SerializeField] private PopupBehaviour phonicQuestionPopup;
    [SerializeField] private int numOfAnswer;
    [SerializeField] private AnswerCollection answerCollection;
    [SerializeField] private Image sunImage;
    [SerializeField] private Image showAnsImage;
    [SerializeField] private ParticleSystem[] sunParticleSystem;
    bool isAnimating = false;
    private void OnEnable()
    {
        PlayPhonicSound();
    }
    private void Start()
    {
        AnswerCard.AnswerChose += OnChoseAnswer;
        BuildAnswer(GameData.PhonicIndex);
        for (int i = 0;i<sunParticleSystem.Length;i++)
        {
            sunParticleSystem[i].gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        AnswerCard.AnswerChose -= OnChoseAnswer;
    }

    #region public
    public void OnClickBackBtn()
    {
        if (isAnimating) return;
        PlayPhonicSound();
    }
    public void PlayPhonicSound()
    {
        if (isAnimating) return;
        if (Phonics.Instance.audioSource.isPlaying) return;
        Phonics.Instance.audioSource.PlayOneShot(PhonicsConfig.AudioClips[GameData.PhonicIndex]);
    }

    #endregion


    #region private
    private void OnChoseAnswer(int phonicIndex, int ansIndex)
    {
        if (isAnimating) return;
        StartCoroutine(CheckAnswer(phonicIndex, ansIndex));
    }
    private void BuildAnswer(int curIndex)
    {
        List<AnswerInfo> answers = new();

        if (curIndex < numOfAnswer)
        {
            for (int i = 0; i < numOfAnswer; i++)
            {
                AnswerInfo answerInfo = new()
                {
                    indexIPA = i,
                    isSetSprite = true
                };
                answers.Add(answerInfo);
            }
        }
        else
        {
            HashSet<int> checkIndex = new();
            answers.Add(new AnswerInfo { indexIPA = curIndex, isSetSprite = true });
            int randomIndex;
            int numAnsBuild = numOfAnswer;
            while (numAnsBuild > 0)
            {
                randomIndex = Random.Range(0, curIndex);
                if (checkIndex.Add(randomIndex))
                {
                    answers.Add(new AnswerInfo { indexIPA = randomIndex, isSetSprite = true });
                    numAnsBuild--;
                }
            }
        }

        answers.Shuffle();
        answerCollection.BuildView(answers);
    }
    private IEnumerator CheckAnswer(int phonicIndex, int ansIndex)
    {
        isAnimating = true;
        float showAnsImageScaleX = showAnsImage.transform.localScale.x;
        AnswerCard choseCard = (AnswerCard)answerCollection.Cards[ansIndex];

        if (GameData.PhonicIndex == phonicIndex)
        {
            sunParticleSystem[1].gameObject.SetActive(true);
            choseCard.SetSprite(PhonicsConfig.PhonicSprite[phonicIndex].sprites[2], PhonicsConfig.AnsQuestionBtnSprite[1]);
            sunImage.sprite = PhonicsConfig.SunQuestSprite[1];
            SoundType.COMPLETED_PHRASE.PlaySound();
        }
        else
        {
            sunParticleSystem[0].gameObject.SetActive(true);
            choseCard.SetSprite(PhonicsConfig.PhonicSprite[phonicIndex].sprites[3], PhonicsConfig.AnsQuestionBtnSprite[2]);
            sunImage.sprite = PhonicsConfig.SunQuestSprite[0];
            SoundType.NO_COINS.PlaySound();
        }

        sunImage.transform.DOScale(Vector3.one, 0.5f);

        showAnsImage.transform.DOScaleX(0f, 0.5f).OnComplete(() =>
        {
            showAnsImage.sprite = PhonicsConfig.PhonicSprite[GameData.PhonicIndex].sprites[1];
            showAnsImage.transform.DOScaleX(showAnsImageScaleX, 0.5f);
        });

        yield return new WaitForSeconds(2.5f);
        AudioHelper.StopAllSound();
        PopupHelper.Create(PhonicsConfig.CollectionAPI);
        phonicQuestionPopup.Close();
        isAnimating = false;
    }
    #endregion

}
