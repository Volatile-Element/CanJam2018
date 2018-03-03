using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITool
{
    public static T Get<T>(Transform parent, string objectName) where T : UIBehaviour
    {
        var result = parent.FindChildBreadthFirst(objectName);
        if (result == null)
        {
            return null;
        }

        return result.GetComponent<T>();
    }
}