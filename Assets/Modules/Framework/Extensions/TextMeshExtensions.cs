using UnityEngine;

namespace Framework
{
    public static class TextMeshExtensions
    {
        public static void SetAlpha(this TextMesh textMesh, float alpha)
        {
            Color newColor = textMesh.color;
            newColor.SetAlpha(alpha);

            textMesh.color = newColor;
        }
    }
}