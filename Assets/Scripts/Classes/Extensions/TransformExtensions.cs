using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform[] GetAllChildren(this Transform transform)
    {
        var children = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }

        return children.ToArray();
    }

    public static Transform FindChildBreadthFirst(this Transform parent, string objectName)
    {
        var result = parent.Find(objectName);
        if (result != null)
        {
            return result;
        }

        foreach (Transform child in parent)
        {
            result = FindChildBreadthFirst(child, objectName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public static Transform FindChildDepthFirst(this Transform parent, string objectName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == objectName)
            {
                return child;
            }

            var result = FindChildDepthFirst(child, objectName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}