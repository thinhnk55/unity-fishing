using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public static class ImageExtensions
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            Color newColor = image.color;
            newColor.SetAlpha(alpha);

            image.color = newColor;
        }
        public static void SetSprite(this Image image, Sprite sprite)
        {
            image.sprite = sprite;
        }
        public static void SetNativeRatioFixedWidth(this Image image)
        {
            float width = image.sprite.GetWidth();
            image.transform.localScale = new Vector2(1, image.sprite.GetFixedHeight(width) / width);
        }

        public static void SetNativeRatioFixedHeight(this Image image)
        {
            float height = image.sprite.GetWidth();
            image.transform.localScale = new Vector2(image.sprite.GetFixedWidth(height)/ height, 1);
        }
    }
}