using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenWaiter : MonoBehaviour
{
    // Script này là để tạo ra một khoảng thời gian chờ fixed - cho các scene
    [SerializeField] float delayTime = 2f;

    private void Awake()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        // chạy âm thanh sóng biển
        AudioManager gameAudio = FindObjectOfType<AudioManager>();
        gameAudio.PlayBeachWaveAudio();

        yield return new WaitForSecondsRealtime(delayTime);

        // lấy ra index của scene hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // lấy ra index của scene tiếp theo 
        int nextSceneIndex = currentSceneIndex + 1;


        // load ra scene CoreGame
        SceneManager.LoadScene(nextSceneIndex);


    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
