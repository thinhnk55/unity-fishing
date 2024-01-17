using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : SingletonMono<FishingManager>
{
    [SerializeField] int rightAnswerScore = 10;
    [SerializeField] int wrongAnswerScore = -5;
    [SerializeField] int fishScore = 1;
    [SerializeField] int sharkScore = -10;


    AudioManager audioManager;

    private bool isGameOver;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
        }
    }

    public int Score = 5;
    [SerializeField] public int itemCorrectNumber;
    public List<int> itemsCorrect; // so items dau tien la item dung, con lai la sai

    [Header("Paramater Camera")]
    public float halfHeightOfCamera;
    public float halfWidthOfCamera;
    protected override void Awake()
    {
        base.Awake();
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            halfHeightOfCamera = mainCamera.orthographicSize;
            halfWidthOfCamera = halfHeightOfCamera * mainCamera.aspect;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

    protected override void OnDestroy()
    {
        try
        {
            base.OnDestroy();
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e);
        }
    }



    // Start is called before the first frame update
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        SetItemsCorrect();
    }

    public void AddScore(int value)
    {
        Score += value;
        if (Score < 0) Score = 0;
        OnChangeScore(value);
    }

    // Hàm kiểm tra xem đã câu đúng con mình cần chưa
    public int CheckMatch(int idItem)
    {
        for (int i = 0; i < itemCorrectNumber; i++)
        {
            if (itemsCorrect[i] == idItem)
            {
                // nếu câu trúng đáp án đúng thì mình sẽ cộng 10 sao 
                AddScore(rightAnswerScore);

                audioManager.PlayRightAnswerAudio();

                return i;
            }
        }
        AddScore(wrongAnswerScore);

        audioManager.PLayWrongAnswerAudio();

        return -1;



        //for (int i = 0; i < itemCorrectNumber; i++)
        //{
        //    if (itemsCorrect[i] == idItem)
        //    {
        //        if (idItem == smallFishId)
        //        {
        //            // Câu trả lời đúng là cá nhỏ, cộng 1 vào điểm số
        //            AddScore(fishScore);
        //        }
        //        else if (idItem == bigFishId)
        //        {
        //            // Câu trả lời đúng là cá mập, trừ 10 vào điểm số
        //            AddScore(wrongAnswerScore);
        //        }
        //        else
        //        {
        //            // Đáp án đúng (không phải cá nhỏ hoặc cá mập), cộng 10 vào điểm số
        //            AddScore(rightAnswerScore);
        //        }

        //        return i;
        //    }
        //}

        // Không tìm thấy đáp án, trừ 5 vào điểm số
        //AddScore(-5);
        //return -1;

    }

    public void ChangeTarget(int index)
    {
        int random = UnityEngine.Random.Range(itemCorrectNumber, SpriteFactory.Items.Count);
        int tmp = itemsCorrect[index];
        itemsCorrect[index] = itemsCorrect[random];
        itemsCorrect[random] = tmp;
    }
    public void GameOver(bool isWin)
    {
        if (IsGameOver) { return; }

        isGameOver = true;
        if (isWin)
        {
            PopupHelper.Create(PrefabFactory.WinPanel);
        }
        else
        {
            PopupHelper.Create(PrefabFactory.LosePanel);
        }
        OnGameOver?.Invoke(isWin);
    }

    private void SetItemsCorrect()
    {
        for (int i = 0; i < SpriteFactory.Items.Count; i++)
        {
            itemsCorrect.Add(i);
        }

        itemsCorrect.Shuffle();
    }

    public Action OnStartFishing;
    public Action OnStopFishing;
    public Action<int> OnChangeScore;
    public Action<bool> OnGameOver;
}