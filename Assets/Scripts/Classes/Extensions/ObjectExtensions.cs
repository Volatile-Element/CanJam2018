using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtensions
{
    public static void DestroyAll(this object obj, IEnumerable<Object> objects)
    {
        foreach (var objToDestroy in objects)
        {
            Object.Destroy(objToDestroy);
        }
    }
}