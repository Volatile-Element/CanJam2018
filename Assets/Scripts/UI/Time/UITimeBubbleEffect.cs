using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBubbleEffect : MonoBehaviour
{
    private void Awake()
    {
        var playerSpeedAdjuster = FindObjectOfType<PlayerSpeedAdjuster>();
        playerSpeedAdjuster.OnTimeNormal.AddListener(OnTimeNormal);
        playerSpeedAdjuster.OnTimeNotNormal.AddListener(OnTimeNotNormal);
    }

    public void OnTimeNormal()
    {
        UITool.Get<Image>(transform, "imgEffect").enabled = false;
    }

    public void OnTimeNotNormal(TimeBubble timeBubble)
    {
        var colour = timeBubble.bubbleColour;
        colour = new Color(colour.r, colour.g, colour.b, 0.5f);

        var image = UITool.Get<Image>(transform, "imgEffect");
        image.color = colour;
        image.enabled = true;
    }
}