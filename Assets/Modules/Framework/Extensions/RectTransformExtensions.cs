using UnityEngine;

namespace Framework
{
    public static class RectTransformExtensions
    {
        public static void SetWidth(this RectTransform rectTransform, float width)
        {
            Vector2 size = rectTransform.sizeDelta;
            size.x = width;
            rectTransform.sizeDelta = size;
        }

        public static void SetHeight(this RectTransform rectTransform, float height)
        {
            Vector2 size = rectTransform.sizeDelta;
            size.y = height;
            rectTransform.sizeDelta = size;
        }

        public static float GetLeft(this RectTransform rectTransform)
        {
            return GetPositionX(rectTransform, 0);
        }

        public static float GetCenter(this RectTransform rectTransform)
        {
            return GetPositionX(rectTransform, 0.5f);
        }

        public static float GetRight(this RectTransform rectTransform)
        {
            return GetPositionX(rectTransform, 1);
        }

        public static float GetBottom(this RectTransform rectTransform)
        {
            return GetPositionY(rectTransform, 0);
        }

        public static float GetMiddle(this RectTransform rectTransform)
        {
            return GetPositionY(rectTransform, 0.5f);
        }

        public static float GetTop(this RectTransform rectTransform)
        {
            return GetPositionY(rectTransform, 1);
        }

        public static void SetCenter(this RectTransform rectTransform, float x, float y)
        {
            SetPosition(rectTransform, x, y, 0.5f, 0.5f);
        }

        public static void SetBottomLeft(this RectTransform rectTransform, float x, float y)
        {
            SetPosition(rectTransform, x, y, 0, 0);
        }

        public static void SetBottomRight(this RectTransform rectTransform, float x, float y)
        {
            SetPosition(rectTransform, x, y, 1, 0);
        }

        public static void SetBottom(this RectTransform rectTransform, float x, float y)
        {
            SetPosition(rectTransform, x, y, 0.5f, 0);
        }

        public static void SetAnchoredPositionX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public static float GetPositionX(this RectTransform rectTransform, float anchor)
        {
            return rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x * (anchor - rectTransform.pivot.x);
        }

        public static float GetPositionY(this RectTransform rectTransform, float anchor)
        {
            return rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y * (anchor - rectTransform.pivot.y);
        }

        public static void SetPosition(this RectTransform rectTransform, float x, float y, float anchorX, float anchorY)
        {
            Vector2 size = rectTransform.sizeDelta;
            Vector2 pivot = rectTransform.pivot;

            rectTransform.anchoredPosition3D = new Vector3(x + size.x * (pivot.x - anchorX), y + size.y * (pivot.y - anchorY), 0);
        }

        public static void SetTouch(this RectTransform button, float paddingLeft, float paddingTop, float paddingRight, float paddingBottom)
        {
            Vector2 buttonSize = button.sizeDelta;

            var touch = button.GetChild(0).GetComponent<RectTransform>();
            touch.sizeDelta = new Vector2(buttonSize.x + paddingLeft + paddingRight, buttonSize.y + paddingBottom + paddingTop);
            touch.SetBottomLeft(-buttonSize.x * 0.5f - paddingLeft, -buttonSize.y * 0.5f - paddingBottom);
        }

        public static void TranslateX(this RectTransform rectTransform, float x)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + x, rectTransform.anchoredPosition.y);
        }

        public static void TranslateY(this RectTransform rectTransform, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + y);
        }

        public static void TranslateXY(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + x, rectTransform.anchoredPosition.y + y);
        }
    }
}