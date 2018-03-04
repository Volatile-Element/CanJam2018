using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScanner : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Scanner>().OnDistanceChange.AddListener(OnDistanceChange);
    }

    public void OnDistanceChange(float distance)
    {
        UITool.Get<Text>(transform, "txtDistance").text = distance.ToString("#");
    }
}
