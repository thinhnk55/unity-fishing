using System.Collections;
using System.Collections.Generic;
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
        FishingManager.instance.OnGameOver += OnGameOver;

        spawnItem = StartCoroutine(SpawnItemCoroutine());
    }

    private void OnDestroy()
    {
        FishingManager.instance.OnGameOver -= OnGameOver;
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
        bool isLeft = Random.Range(0, 2) == 0 ? true : false;

        position.y = Random.Range(posYMax, posYMin);
        if (isLeft)
        {
            position.x = -SpawnFishing.halfWidthOfCamera - offsetHorizontal;
        }
        else
        {
            position.x = SpawnFishing.halfWidthOfCamera + offsetHorizontal;
        }

        return position;
    }
}
