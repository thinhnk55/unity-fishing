using UnityEngine;

namespace Framework
{
    public static class PRandom
    {
        public static bool Bool(float probability)
        {
            return Random.value < probability;
        }

        public static float SignFloat()
        {
            return Random.Range(0, 2) == 0 ? -1f : 1f;
        }

        public static int SignInt()
        {
            return Random.Range(0, 2) == 0 ? -1 : 1;
        }

        public static T EnumValue<T>()
        {
            var v = System.Enum.GetValues(typeof(T));
            return (T)v.GetValue(Random.Range(0, v.Length));
        }
    }
}