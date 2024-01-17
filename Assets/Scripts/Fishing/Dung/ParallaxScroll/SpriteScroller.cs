using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxScrolling();
    }

    void ParallaxScrolling()
    {
        // tạo biến di chuyển từng frame
        offset = moveSpeed + offset;

        // gán cái offset mới đc tăng vào ảnh background
        material.mainTextureOffset = offset;
    }
}
