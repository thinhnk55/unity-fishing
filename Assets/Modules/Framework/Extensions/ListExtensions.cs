using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Framework
{
    public static class ListExtensions
    {
        public static List<T> MaintainSize<T>(this List<T> list, int size)
        {
            if (list.Count == size)
                return list;

            if (list.Count > size)
                list.RemoveRange(size, list.Count - size);
            else
            {
                T[] arr = new T[size - list.Count];
                list.AddRange(arr);
            }

            return list;
        }

        public static void Shuffle<T>(this List<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];

                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (Byte.MaxValue / n)));

                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static bool IsOutOfBounds<T>(this IList<T> list, int index)
        {
            if (list == null)
                return true;

            if (index < 0 || index >= list.Count)
                return true;

            return false;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list == null)
                return true;

            if (list.Count == 0)
                return true;

            return false;
        }

        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            if (list == null)
                return null;

            List<T> newList = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add((T)list[i].Clone());
            }

            return newList;
        }

        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T First<T>(this List<T> list)
        {
            return list[0];
        }

        public static T GetClamp<T>(this List<T> list, int index)
        {
            return list[Mathf.Clamp(index, 0, list.Count - 1)];
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T GetLoop<T>(this List<T> list, int index)
        {
            return list[index % list.Count];
        }
    }
}