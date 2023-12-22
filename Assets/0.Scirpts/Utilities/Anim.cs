using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float time;

    private void OnEnable()
    {
        playAnim = StartCoroutine(PlayeAnim());        
    }

    Coroutine playAnim;
    IEnumerator PlayeAnim()
    {
        int index = 0;
        while (true)
        {
            if(index >= sprites.Length)
            {
                index = 0;
            }
            spriteRenderer.sprite = sprites[index++];
            yield return new WaitForSeconds(time);
        }
    }

    public void StopAnim()
    {
        StopCoroutine(playAnim);
    }
}
