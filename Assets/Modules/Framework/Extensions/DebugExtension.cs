using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugExtension
{
    static public void Log(this List<int> list) 
    {
        string s = "";
        foreach (int i in list)
        {
            s += "_" + i.ToString();
        }
        Debug.Log(s);
    }
}
