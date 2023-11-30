using UnityEngine;
using Color = UnityEngine.Color;

namespace Framework
{
    public static class ColorExtensions
    {
        public static void MakeRandomColor(this ref Color color, float minClamp = 0.5f, float alpha = 1f)
        {
            var randCol = Random.onUnitSphere * 3;
            randCol.x = Mathf.Clamp(randCol.x, minClamp, 1f);
            randCol.y = Mathf.Clamp(randCol.y, minClamp, 1f);
            randCol.z = Mathf.Clamp(randCol.z, minClamp, 1f);

            color = new Color(randCol.x, randCol.y, randCol.z, alpha);
        }

        public static void SetAlpha(this ref Color color, float alpha)
        {
            color = new Color(color.r, color.g, color.b, alpha);
        }

        public static void SetBlue(this ref Color color, float blue)
        {
            color = new Color(color.r, color.g, blue, color.a);
        }

        public static void SetRed(this ref Color color, float red)
        {
            color = new Color(red, color.g, color.b, color.a);
        }

        public static void SetGreen(this ref Color color, float green)
        {
            color = new Color(color.r, green, color.b, color.a);
        }

        public static void SetGreen(this ref Color color, float r, float g, float b, float a)
        {
            color = new Color(r, g, b, a);
        }
    }
}