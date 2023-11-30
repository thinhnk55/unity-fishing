using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtension
{
    public static List<T> ToList<U,T>(this Dictionary<U,T> dict)
    {
        List<T> list = new List<T>();
        foreach (var item in dict)
        {
            list.Add(item.Value);
        }
        return list;
    }
}
