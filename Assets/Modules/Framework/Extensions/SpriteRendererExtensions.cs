using UnityEngine;

namespace Framework
{
    public static class SpriteRendererExtensions
    {
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            Color newColor = spriteRenderer.color;
            newColor.SetAlpha(alpha);

            spriteRenderer.color = newColor;
        }

        public static void SetNativeRatioFixedWidth(this SpriteRenderer spriteRenderer)
        {
            float width = spriteRenderer.sprite.GetWidth();
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.sprite.GetFixedHeight(width) / width);
        }

        public static void SetNativeRatioFixedHeight(this SpriteRenderer spriteRenderer)
        {
            float height = spriteRenderer.sprite.GetWidth();
            spriteRenderer.transform.localScale = new Vector2(1, spriteRenderer.sprite.GetFixedWidth(height) / height);
        }
    }
}