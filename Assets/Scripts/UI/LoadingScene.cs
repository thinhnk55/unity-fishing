using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] float loadingDuration = 1;
    AudioListener[] aL;
    private void Awake()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Additive);
        GetComponent<Image>().DOFade(0, loadingDuration).OnComplete(() =>
        {
            SceneManager.UnloadSceneAsync("Loading");
        });
        InvokeRepeating("CheckMultipleAudioListener", 0, 0.1f);
    }

    public void CheckMultipleAudioListener()
    {
        aL = FindObjectsOfType<AudioListener>();
        if (aL.Length >= 2)
        {
            DestroyImmediate(aL[0]);
        }
    }

    
}
