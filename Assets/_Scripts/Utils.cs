using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    
    public static void Print<T>(List<T> list)
    {
        foreach(T item in list)
        {
            Debug.Log($"{item.ToString()}");
        }
    }
}
