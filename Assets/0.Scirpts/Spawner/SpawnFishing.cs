using Framework;
using System.Collections;
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
    void Start()
    {
        FishingManager.Instance.OnGameOver += OnGameOver;

        spawnFish = StartCoroutine(SpawnFish());
    }

    private void OnDestroy() 
    {
        FishingManager.Instance.OnGameOver -= OnGameOver;
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

        position.y = Random.Range(posYMax, posYMin);
        if(isLeft )
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
