using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Game Scene Index - change this according to scene build settings")]
    [SerializeField] int GameMenu = 1;
    [SerializeField] int LoadingScreen = 2;
    [SerializeField] int CoreGameplay = 3;

    AudioManager gameAudio;

    void Awake()
    {
        gameAudio = FindObjectOfType<AudioManager>();    
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        // load ra scene menu
        SceneManager.LoadScene(GameMenu);

        gameAudio.PlayButtonAudio();

    }

    public void StartPlayButtonSound()
    {

    }

    public void LoadLoadingScreen()
    {
        SceneManager.LoadScene(LoadingScreen);

        gameAudio.PlayButtonAudio();

    }

    public void LoadCoreGame()
    {
        SceneManager.LoadScene(CoreGameplay);
    }

    
}
