using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : SingletonMono<ItemCollection>
{
    public List<ItemCard> items = new List<ItemCard>();
    [SerializeField] ItemCard itemPrefab;
    [SerializeField] Transform itemsRoot;
    [SerializeField] Transform blurryScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitItem());
    }

    private IEnumerator InitItem()
    {
        for(int i=0; i<items.Count;  i++)
        {
            transform.GetChild(i).SetAsLastSibling();
            items[i].ScaleToOne();
            yield return new WaitForSeconds(items[i].TimeScaleToOne + items[i].TimeSpeech);
            transform.GetChild(transform.childCount-1).SetSiblingIndex(i);
        }

        blurryScreen.gameObject.SetActive(false);

        yield return null;
    }
}
