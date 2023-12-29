using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingManager : SingletonMono<FishingManager>
{
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
    [SerializeField] int itemCorrectNumber;
    public List<int> itemsCorrect; // 3 items dau tien la item dung, con lai la sai

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

    // Start is called before the first frame update
    void Start()
    {
        SetItemsCorrect();
    }

    public void AddScore(int value)
    {
        Score += value;
        if (Score < 0) Score = 0;
        OnChangeScore(value);
    }

    public int CheckMatch(int idItem)
    {
        for (int i = 0; i < itemCorrectNumber; i++)
        {
            if (itemsCorrect[i] == idItem)
            {
                AddScore(3);
                return i;
            } 
        }
        AddScore(-1);
        return -1;
    }

    public void ChangeTarget(int index)
    {
        int random = UnityEngine.Random.Range(3, 10);
        int tmp = itemsCorrect[index];
        itemsCorrect[index] = itemsCorrect[random];
        itemsCorrect[random] = tmp;
    }

    public void GameOver(bool isWin)
    {
        if(IsGameOver) { return; }

        Debug.Log("Player Win: " + isWin);
        isGameOver = true;
        if(isWin)
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
        List<int> index = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        for(int i=0; i<10; i++)
        {
            int randomIndex = index.GetRandom();
            itemsCorrect.Add(randomIndex);
            index.Remove(randomIndex);
        }
    }

    public Action OnStartFishing;
    public Action OnStopFishing;
    public Action<int> OnChangeScore;
    public Action<bool> OnGameOver;
}