using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFishing : MonoBehaviour
{
    [SerializeField] GameObject prefabFish;
    [SerializeField] Transform transformParent;

    [SerializeField] float posYMax;
    [SerializeField] float posYMin;
    [SerializeField] float offsetHorizontal;
    [SerializeField] float duration;

    [Header("Paramater Camera")]
    public static float halfHeightOfCamera;
    public static float halfWidthOfCamera;
    void Start()
    {
        FishingManager.instance.OnGameOver += OnGameOver;

        // Get the main camera
        Camera mainCamera = Camera.main;
        // Check if the main camera is not null
        if (mainCamera != null)
        {
            halfHeightOfCamera = mainCamera.orthographicSize;
            halfWidthOfCamera = halfHeightOfCamera * mainCamera.aspect;

            // Print the size of the camera
            Debug.Log($"Camera Size: {halfHeightOfCamera} x {halfWidthOfCamera}");
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }

        spawnFish = StartCoroutine(SpawnFish());
    }

    private void OnDestroy() 
    {
        FishingManager.instance.OnGameOver -= OnGameOver;
    }

    Coroutine spawnFish;
    IEnumerator SpawnFish()
    {
        while (true) 
        {
            Vector2 positon = GetRandomPosition();
            MovingObject itemObj = ObjectPoolManager.SpawnObject<MovingObject>(prefabFish, positon, transformParent);

            itemObj.isMovingRight = positon.x < 0 ? true : false;
            itemObj.transform.rotation = Quaternion.Euler(0, positon.x < 0 ? 180 : 0, 0);
            itemObj.gameObject.SetActive(true);

            yield return new WaitForSeconds(duration);
        }
    }

    private void OnGameOver(bool isWin)
    {
        StopCoroutine(spawnFish);
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 position = Vector2.zero;
        bool isLeft = Random.Range(0, 2) == 0 ? true : false;

        position.y = Random.Range(this.transform.position.y, -halfHeightOfCamera);
        if(isLeft )
        {
            position.x = -halfWidthOfCamera - offsetHorizontal;
        }
        else
        {
            position.x = halfWidthOfCamera + offsetHorizontal;
        }

        return position;
    }
}
