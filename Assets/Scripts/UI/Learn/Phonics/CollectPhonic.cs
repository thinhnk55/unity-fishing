using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPhonic : MonoBehaviour
{
    //[SerializeField] private Button avatarBtn;
    [SerializeField] private Transform characterImage;
    [SerializeField] private AnswerCollection answerCollection;
    [SerializeField] private Vector3[,] mapPos;
    [SerializeField] private GridLayoutGroup mapParent;
    [SerializeField] private int numOfObstacle;
    [SerializeField] private int numOfPhonicCard;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private GameObject popupObject;
    [SerializeField] private Transform popupBg;
    [SerializeField] private Transform popupFlower;
    [SerializeField] private Image phonicImage;

    private Vector2Int curCell;

    private bool canMove = true;
    private HashSet<Vector2Int> usedCell = new ();

    private void Start()
    {
        popupObject.SetActive(false);
        Phonics.Instance.ChangeCanvasScaler();
        mapParent.constraintCount = columns;
        BuildAnsCard();
        StartCoroutine(nameof(UpdateMap));
        OnClickAvataBtn();
    }

    #region private
    private void BuildAnsCard()
    {
        List<AnswerInfo> answerList = new();

        for(int i=0; i < columns*rows; i++)
        {
            answerList.Add(new AnswerInfo {indexIPA = -1, isSetSprite = false});
        }

        answerCollection.BuildView(answerList);
    }
    private void PlayPhonicSound()
    {
        if (Phonics.Instance.audioSource.isPlaying) return;
        PlaySound(PhonicsConfig.AudioClips[GameData.PhonicIndex]);
    }
    private void MoveCharacterImage()
    {
        canMove = false;
        characterImage.DOMove(mapPos[curCell.x, curCell.y], 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            CheckPhonicAns();
            canMove = true;
        });
    }

    private IEnumerator UpdateMap()                 
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)mapParent.transform);
        curCell = new (0, Random.Range(0, columns));
        usedCell.Add(curCell);
        mapPos = new Vector3[rows, columns];

        for (int i = 0; i < rows;i++)
        {
            for (int j = 0; j < columns; j++)
            {
                mapPos[i,j] = mapParent.transform.GetChild((i*columns)+j).position;
                ((AnswerCard)answerCollection.Cards[(i * columns) + j])
                    .SetSprite(null, PhonicsConfig.AnsCollectBtnSprite[Random.Range(0, PhonicsConfig.AnsCollectBtnSprite.Length)]);
            }
        }

        characterImage.position = mapPos[curCell.x,curCell.y];
        SetObstaclePosition();
        SetPhonicPosition(numOfPhonicCard, GameData.PhonicIndex);
    }

    private void CheckPhonicAns()
    {
        int curAnsIndex = (curCell.x * columns) + curCell.y;
        AnswerCard curAnsCard = (AnswerCard)answerCollection.Cards[curAnsIndex];
        if (curAnsCard.Info.indexIPA < 0) return;
        if (curAnsCard.Info.indexIPA == GameData.PhonicIndex) PopupWin();
        else
        {
            curAnsCard.SetCenterImageAlpha(0);
            SoundType.WRONG_WORD.PlaySound();
        }
    }

    private void PopupWin()
    {
        popupObject.SetActive(true);
        Vector3 bgScale = popupBg.localScale;
        Vector3 flowerScale = popupFlower.localScale;
        popupBg.localScale = Vector3.zero;
        popupFlower.localScale = Vector3.zero;
        SoundType.COMPLETED_CROSSWORD.PlaySound();

        popupBg.DOScale(bgScale, 1f).OnComplete(() => 
        {
            CancelInvoke(nameof(PlayPhonicSound));
            PlaySound(PhonicsConfig.AudioClips[GameData.PhonicIndex]);
        });

        phonicImage.sprite = PhonicsConfig.PhonicSprite[GameData.PhonicIndex].sprites[2];
        popupFlower.DOScale(flowerScale, 2.5f).SetEase(Ease.OutBack).OnComplete(() => 
        {
            if (GameData.PhonicIndex >= PhonicsConfig.PhonicSprite.Count -1) GameData.PhonicIndex = 0;
            else GameData.PhonicIndex++;
            SceneTransitionHelper.Reload(true);
        });
    }

    private void PlaySound(AudioClip audioClip)
    {
        Phonics.Instance.audioSource.PlayOneShot(audioClip);
    }
    private void SetObstaclePosition()
    {
        for(int i = 1; i < rows-1; i++) 
        {
            int randomObstacle = Random.Range(0, numOfObstacle + 1);
            while (randomObstacle > 0)
            {
                Vector2Int obstaclePos = new(i, Random.Range(1, columns - 1));
                if (usedCell.Add(obstaclePos)) 
                {
                    ((AnswerCard)answerCollection.Cards[(obstaclePos.x * columns) + obstaclePos.y])
                        .SetSprite(PhonicsConfig.TreeSprite, PhonicsConfig.AnsCollectBtnSprite[Random.Range(0, PhonicsConfig.AnsCollectBtnSprite.Length)]);
                    answerCollection.Cards[(obstaclePos.x * columns) + obstaclePos.y].Info.indexIPA = -2;
                    randomObstacle--;
                } 
            }
        }
    }

    private void SetPhonicPosition(int numOfPhonic, int phonicAnsIndex)
    {
        List<AnswerInfo> answers = new();
        int numOfAnsPos = numOfPhonic;

        if (phonicAnsIndex < numOfPhonic)
        {
            for (int i = 0; i < numOfPhonic; i++)
            {
                answers.Add(new AnswerInfo { indexIPA = i, isSetSprite = false });
            }
        }
        else
        {
            HashSet<int> checkIndex = new();
            answers.Add(new AnswerInfo { indexIPA = phonicAnsIndex, isSetSprite = false });
            int randomIndex;
            int numAnsBuild = numOfPhonic;
            while (numAnsBuild > 0)
            {
                randomIndex = Random.Range(0, phonicAnsIndex);
                if (checkIndex.Add(randomIndex))
                {
                    answers.Add(new AnswerInfo { indexIPA = randomIndex, isSetSprite = false });
                    numAnsBuild--;
                }
            }
        }

        while (numOfAnsPos > 0)
        {
            Vector2Int randomCellPos = new(Random.Range(rows - 3, rows), Random.Range(0, columns));
            if (usedCell.Add(randomCellPos))
            {
                AnswerCard card = (AnswerCard)answerCollection.Cards[(randomCellPos.x * columns) + randomCellPos.y];
                answerCollection.ModifyCardAt((randomCellPos.x * columns) + randomCellPos.y, answers[numOfAnsPos - 1]);
                card.SetSprite(PhonicsConfig.PhonicSprite[card.Info.indexIPA].sprites[0], null);
                numOfAnsPos--;
            }
        }

        while (usedCell.Count <= numOfPhonic + 1)
        {
            Vector2Int obstaclePos = new(Random.Range(1, rows - 1), Random.Range(1, columns - 1));
            if (usedCell.Add(obstaclePos))
            {
                AnswerCard card = (AnswerCard)answerCollection.Cards[(obstaclePos.x * columns) + obstaclePos.y];
                card.SetSprite(PhonicsConfig.TreeSprite, null);
                answerCollection.Cards[(obstaclePos.x * columns) + obstaclePos.y].Info.indexIPA = -2;

            }
        }
    }
    #endregion

    #region public
    public void OnClickAvataBtn()
    {
        CancelInvoke(nameof(PlayPhonicSound));
        InvokeRepeating(nameof(PlayPhonicSound), 0.1f, PhonicsConfig.TimeDelayPlaySound);
    }
    public void OnClickMoveUpBtn()
    {
        if (curCell.x <= 0 || !canMove) return;
        if (answerCollection.Cards[((curCell.x -1) * columns) + curCell.y].Info.indexIPA != -2)
        {
            curCell = new Vector2Int(curCell.x - 1, curCell.y);
            MoveCharacterImage();
        }
    }
    public void OnClickMoveDownBtn()
    {
        if (curCell.x >= rows - 1 || !canMove) return;
        if (answerCollection.Cards[((curCell.x + 1) * columns) + curCell.y].Info.indexIPA != -2)
        {
            curCell = new Vector2Int(curCell.x + 1, curCell.y);
            MoveCharacterImage();
        }
    }
    public void OnClickMoveLeftBtn()
    {
        if (curCell.y == 0 || !canMove) return;
        if (answerCollection.Cards[(curCell.x * columns) + curCell.y - 1].Info.indexIPA != -2)
        {
            curCell = new Vector2Int(curCell.x, curCell.y - 1);
            MoveCharacterImage();
        }
    }
    public void OnClickMoveRightBtn()
    {
        if (curCell.y == columns - 1 || !canMove) return;
        if (answerCollection.Cards[(curCell.x * columns) + curCell.y +1].Info.indexIPA != -2)
        {
            curCell = new Vector2Int(curCell.x, curCell.y + 1);
            MoveCharacterImage();
        }
    }
    #endregion
}