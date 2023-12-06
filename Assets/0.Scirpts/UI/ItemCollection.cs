using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : SingletonMono<ItemCollection>
{
    [SerializeField] int itemNumber;
    public List<ItemCard> items = new List<ItemCard>();
    [SerializeField] ItemCard itemPrefab;
    [SerializeField] Transform itemsRoot;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitItem());
    }

    private IEnumerator InitItem()
    {
        for(int i=0; i<itemNumber;  i++)
        {
            ItemCard itemCard = Instantiate<ItemCard>(itemPrefab, transform.position, Quaternion.identity, itemsRoot);
            items.Add(itemCard);
            itemCard.PlayAnimPlaySound();
            yield return new WaitForSeconds(itemCard.TimeScale * 2);
        }

        yield return null;
    }
}
