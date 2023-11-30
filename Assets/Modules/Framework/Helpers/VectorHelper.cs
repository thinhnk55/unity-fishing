using UnityEngine;

namespace Framework
{
    public static class VectorHelper
    {
        public static Vector3 RotateVectorByDegree(Vector3 v, float degree)
        {
            return RotateVectorByRadian(v, degree * Mathf.Deg2Rad);
        }

        public static Vector3 RotateVectorByRadian(Vector3 v, float radian)
        {
            float ca = Mathf.Cos(radian);
            float sa = Mathf.Sin(radian);
            return new Vector3(ca * v.x - sa * v.y, sa * v.x + ca * v.y);
        }

        public static Vector3 RadianToVector(float radian)
        {
            return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector3 DegreeToVector(float degree)
        {
            return RadianToVector(degree * Mathf.Deg2Rad);
        }

        public static bool IsTwoLinesIntersection(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, ref Vector2 intersectionPoint)
        {
            float s1_x, s1_y, s2_x, s2_y;
            s1_x = p1.x - p0.x;
            s1_y = p1.y - p0.y;
            s2_x = p3.x - p2.x;
            s2_y = p3.y - p2.y;

            float s, t;
            s = (-s1_y * (p0.x - p2.x) + s1_x * (p0.y - p2.y)) / (-s2_x * s1_y + s1_x * s2_y);
            t = (s2_x * (p0.y - p2.y) - s2_y * (p0.x - p2.x)) / (-s2_x * s1_y + s1_x * s2_y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                // Collision detected
                intersectionPoint.x = p0.x + (t * s1_x);
                intersectionPoint.y = p0.y + (t * s1_y);
                return true;
            }

            // No collision
            return false;
        }

        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 normal)
        {
            float angle = Vector3.Angle(from, to);
            float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(from, to)));
            return angle * sign;
        }

        public static float AngleBetween(Vector2 p1, Vector2 p2)
        {
            return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
        }

        public static Vector3 Snap(this Vector3 vector, float unit)
        {
            float x = vector.x - vector.x % unit;
            float y = vector.y - vector.y % unit;
            float z = vector.z - vector.z % unit;

            return new Vector3(x, y, z);
        }

        // Find the points where the two circles intersect.
        public static int FindCircleCircleIntersections(Vector2 c0, float radius0, Vector2 c1, float radius1, out Vector2 intersection1, out Vector2 intersection2)
        {
            // Find the distance between the centers.
            float dx = c0.x - c1.x;
            float dy = c0.y - c1.y;
            float dist = Mathf.Sqrt(dx * dx + dy * dy);

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new Vector2(float.NaN, float.NaN);
                intersection2 = new Vector2(float.NaN, float.NaN);
                return 0;
            }
            else if (dist < Mathf.Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Vector2(float.NaN, float.NaN);
                intersection2 = new Vector2(float.NaN, float.NaN);
                return 0;
            }
            else if ((dist == 0) && (radius0 == radius1))
            {
                // No solutions, the circles coincide.
                intersection1 = new Vector2(float.NaN, float.NaN);
                intersection2 = new Vector2(float.NaN, float.NaN);
                return 0;
            }
            else
            {
                // Find a and h.
                float a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                float h = Mathf.Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = c0.x + a * (c1.x - c0.x) / dist;
                double cy2 = c0.y + a * (c1.y - c0.y) / dist;

                // Get the points P3.
                intersection1 = new Vector2(
                    (float)(cx2 + h * (c1.y - c0.y) / dist),
                    (float)(cy2 - h * (c1.x - c0.x) / dist));
                intersection2 = new Vector2(
                    (float)(cx2 - h * (c1.y - c0.y) / dist),
                    (float)(cy2 + h * (c1.x - c0.x) / dist));

                // See if we have 1 or 2 solutions.
                if (dist == radius0 + radius1) return 1;
                return 2;
            }
        }

        public static float WrapAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f)
                return angle - 360f;

            return angle;
        }
    }
}