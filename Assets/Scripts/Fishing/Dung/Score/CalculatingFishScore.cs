using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatingFishScore : MonoBehaviour
{
    Rigidbody2D boatRigidbody;

    [SerializeField] GameObject GameManager;
    FishingManager fishingManager; 

    [SerializeField] bool isTouchingShark = false;
    [SerializeField] bool isTouchingFish = false;

    [SerializeField] int fishScore = 1;
    [SerializeField] int sharkScore = -10;

    void Awake()
    {
        boatRigidbody = GetComponent<Rigidbody2D>();

        fishingManager = GameManager.GetComponent<FishingManager>();

        if (fishingManager != null)
        {
            Debug.Log("Lấy ra fishing Manager script thành công");
        }
    }

    void Start()
    {
        isTouchingShark = false;
        isTouchingFish = false;
        if (fishingManager != null)
        {
            Debug.Log("Lấy ra fishing Manager script thành công");
        }
    }

    void Update()
    {
        CountingFishScore();
        CountingSharkScore();

        if (fishingManager != null)
        {
            Debug.Log("Lấy ra fishing Manager script thành công");
        }
    }

    void CountingSharkScore()
    {
        // nếu đang chạm vào cá mập và biến bool trạng thái là false thì bật bool lên và trừ sao
        if (boatRigidbody.IsTouchingLayers(LayerMask.GetMask("Shark")) )
        {
            isTouchingShark = true;
            fishingManager.AddScore(sharkScore);

            Debug.Log("Đã nhận biết được con cá mập");
        } 
        // nếu không còn chạm vào cá mập nữa và biến bool trạng thái vẫn là true thì tắt biến bool đi 
        else if (!boatRigidbody.IsTouchingLayers(LayerMask.GetMask("Shark")) )
        {
            isTouchingShark = false;

            Debug.Log("Đã tắt được con cá mập");

        }
    }

    void CountingFishScore()
    {
        // 
        if(boatRigidbody.IsTouchingLayers(LayerMask.GetMask("Fish")) )
        {
            isTouchingFish = true;
            fishingManager.AddScore(fishScore);


            Debug.Log("Đã nhận biết được con cá con");

        }
        else if (!boatRigidbody.IsTouchingLayers(LayerMask.GetMask("Fish")) )
        {
            isTouchingFish = false;

            Debug.Log("Đã tắt được con cá mập");

        }
    }
}
