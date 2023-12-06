using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnFishing : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabFish;
    [SerializeField] Transform transformParent;
    float halfHeightOfCamera;
    float halfWidthOfCamera;
    [SerializeField] float offsetHorizontal;
    [SerializeField] float offsetBottom;
    [SerializeField] float duration;

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
            Debug.Log("Spawn");
            Vector2 positon = GetRandomPosition();
            MovingObject fishObj = ObjectPoolManager.SpawnObject<MovingObject>(prefabFish.GetRandom(), positon, transformParent);

            fishObj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            fishObj.transform.rotation = Quaternion.Euler(0, 0, 90);
            fishObj.isMovingRight = positon.x < 0 ? true : false;
            fishObj.transform.rotation = Quaternion.Euler(0, 0, positon.x < 0 ? -90 : 90);
            fishObj.gameObject.SetActive(true);

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

        position.y = Random.Range(this.transform.position.y, -halfHeightOfCamera + offsetBottom);
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
