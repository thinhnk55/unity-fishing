using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : SingletonMono<ItemCollection>
{
    public List<ItemCard> items = new List<ItemCard>();
    [SerializeField] ItemCard itemPrefab;
    [SerializeField] Transform itemsRoot;
    public Transform blurryScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitItem());
    }

    private IEnumerator InitItem()
    {
        blurryScreen.gameObject.SetActive(true);
        for (int i=0; i<items.Count;  i++)
        {
            transform.GetChild(i).SetAsLastSibling();
            items[i].ScaleToOne();
            yield return new WaitForSeconds(items[i].TimeScaleToOne + items[i].TimeSpeech + 0.1f);
        }

        blurryScreen.gameObject.SetActive(false);
        FishingManager.Instance.OnStartFishing();
        yield return null;
    }
}
