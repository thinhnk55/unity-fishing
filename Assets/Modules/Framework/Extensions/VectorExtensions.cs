using UnityEngine;

namespace Framework
{
    public static class VectorExtensions
    {
        #region Vector2

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            return v.RotateRadians(degrees * Mathf.Deg2Rad);
        }

        public static Vector2 RotateRadians(this Vector2 v, float radians)
        {
            float ca = Mathf.Cos(radians);
            float sa = Mathf.Sin(radians);
            return new Vector2(ca * v.x - sa * v.y, sa * v.x + ca * v.y);
        }

        public static float ToAngle(this Vector2 v)
        {
            float angle = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
            if (angle < 0f)
                angle += 360f;

            return angle;
        }

        public static float RandomWithin(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        public static Vector3 ToXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0f, vector.y);
        }

        #endregion

        #region Vector2Int

        public static int RandomWithin(this Vector2Int vector)
        {
            return Random.Range(vector.x, vector.y + 1);
        }

        public static Vector3 ToVector3(this Vector2Int vector)
        {
            return new Vector3(vector.x, vector.y);
        }

        #endregion
    }
}