using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    private void Awake()
    {
        TimeManager.Instance.OnTimeChanged.AddListener(OnTimeChanged);
    }

    public void OnTimeChanged(int seconds)
    {
        UITool.Get<Text>(transform, "txtTime").text = GetTime(seconds);
    }

    private string GetTime(int seconds)
    {
        return $"{GetMinutes(seconds)}:{GetSeconds(seconds)}";
    }

    private string GetMinutes(int seconds)
    {
        var stringSeconds = Mathf.Floor(seconds / 60).ToString();
        if (stringSeconds.Length == 1)
        {
            stringSeconds = $"0{stringSeconds}";
        }

        return stringSeconds;
    }

    private string GetSeconds(int seconds)
    {
        var stringSeconds = (seconds % 60).ToString();
        if (stringSeconds.Length == 1)
        {
            stringSeconds = $"0{stringSeconds}";
        }

        return stringSeconds;
    }
}
