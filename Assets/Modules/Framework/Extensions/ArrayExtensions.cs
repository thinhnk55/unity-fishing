using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Framework
{
    public static class ArrayExtensions
    {
        public static void Shuffle<T>(this T[] arr)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = arr.Length;
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
                T value = arr[k];
                arr[k] = arr[n];
                arr[n] = value;
            }
        }

        public static T First<T>(this T[] array)
        {
            return array[0];
        }

        public static T Last<T>(this T[] array)
        {
            return array[array.Length - 1];
        }

        public static T GetClamp<T>(this T[] array, int index)
        {
            return array[Mathf.Clamp(index, 0, array.Length - 1)];
        }

        public static T GetRandom<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T[] MaintainSize<T>(this T[] array, int size)
        {
            if (array.Length == size)
                return array;

            T[] newArray = new T[size];
            for (int i = 0; i < newArray.Length; i++)
            {
                if (i >= array.Length)
                    break;
                else
                    newArray[i] = array[i];
            }

            return newArray;
        }

        public static T[] GetUniue<T>(this T[] array, int count)
        {
            int[] picks = new int[array.Length];
            for (int i = 0; i < picks.Length; i++)
            {
                picks[i] = i;
            }

            int index1 = 0;
            int index2 = 0;
            T[] result = new T[count];

            while (index1 < count)
            {
                if (index2 >= picks.Length)
                {
                    picks.Shuffle();
                    index2 = 0;
                }

                result[index1] = array[picks[index2]];

                index1++;
                index2++;
            }

            return result;
        }

        public static T GetLoop<T>(this T[] array, int index)
        {
            return array[index % array.Length];
        }
    }
}
