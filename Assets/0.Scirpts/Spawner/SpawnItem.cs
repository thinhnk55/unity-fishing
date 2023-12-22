using System;
using System.Collections;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject prefabItem;

    [SerializeField] Transform transformParent;
    [SerializeField] float posYMax;
    [SerializeField] float posYMin;
    [SerializeField] float offsetHorizontal;
    [SerializeField] float duration;

    void Start()
    {
        FishingManager.Instance.OnGameOver += OnGameOver;

        spawnItem = StartCoroutine(SpawnItemCoroutine());
    }

    private void OnDestroy()
    {
        try
        {
            FishingManager.Instance.OnGameOver -= OnGameOver;
        }
        catch(System.Exception exeption)
        {
            Debug.Log(exeption.ToString());
        }

    }

    Coroutine spawnItem;
    IEnumerator SpawnItemCoroutine()
    {
        while (true)
        {
            Vector2 positon = GetRandomPosition();
            MovingObject fishObj = ObjectPoolManager.SpawnObject<MovingObject>(prefabItem, positon, transformParent);

            fishObj.isMovingRight = positon.x < 0 ? true : false;
            fishObj.transform.rotation = Quaternion.Euler(0, positon.x < 0 ? 180 : 0, 0);
            fishObj.gameObject.SetActive(true);

            yield return new WaitForSeconds(duration);
        }
    }

    private void OnGameOver(bool isWin)
    {
        StopCoroutine(spawnItem);
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 position = Vector2.zero;
        bool isLeft = UnityEngine.Random.Range(0, 2) == 0 ? true : false;

        position.y = UnityEngine.Random.Range(posYMax, posYMin);
        if (isLeft)
        {
            position.x = -FishingManager.Instance.halfWidthOfCamera - offsetHorizontal;
        }
        else
        {
            position.x = FishingManager.Instance.halfWidthOfCamera + offsetHorizontal;
        }

        return position;
    }
}
