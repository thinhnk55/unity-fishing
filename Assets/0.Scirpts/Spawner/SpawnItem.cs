using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

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
            List<int> indexList = new List<int>() { 3, 4, 5, 6, 7, 8, 9 };
            int randomIndexIncorrect = indexList.GetRandom();
            indexList.Remove(randomIndexIncorrect);
            CreateItem(randomIndexIncorrect);
            yield return new WaitForSeconds(duration / 2);

            int randomCorrectItem = UnityEngine.Random.Range(0, 3);
            CreateItem(randomCorrectItem);
            yield return new WaitForSeconds(duration / 2);

            randomIndexIncorrect = indexList.GetRandom();
            CreateItem(randomIndexIncorrect);
            yield return new WaitForSeconds(duration);
        }
    }

    private void CreateItem(int index)
    {
        Vector2 positon = GetRandomPosition();
        MovingObject itemObj = ObjectPoolManager.SpawnObject<MovingObject>(prefabItem, positon, transformParent);
        Item item = itemObj.GetComponent<Item>();
        item.SetImage(SpriteFactory.Items[FishingManager.Instance.itemsCorrect[index]]);
        item.idItem = FishingManager.Instance.itemsCorrect[index];

        itemObj.isMovingRight = positon.x < 0 ? true : false;
        itemObj.transform.rotation = Quaternion.Euler(0, positon.x < 0 ? 180 : 0, 0);
        itemObj.gameObject.SetActive(true);
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
