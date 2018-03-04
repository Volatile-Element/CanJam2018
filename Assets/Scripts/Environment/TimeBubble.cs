using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubble : MonoBehaviour
{
    public enum TimeBubbleType { Fast, Slow };
    public TimeBubbleType BubbleType;

    public Color bubbleColour;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = bubbleColour;
        GetComponentInChildren<Light>().color = bubbleColour;
    }

    private void OnTriggerEnter(Collider other)
    {
        var speedModifier = other.GetComponent<ISpeedModifier>();
        if (speedModifier == null)
        {
            return;
        }

        SetSpeed(speedModifier);
    }

    private void OnTriggerExit(Collider other)
    {
        var speedModifier = other.GetComponent<ISpeedModifier>();
        if (speedModifier == null)
        {
            return;
        }

        speedModifier.NormalSpeed(this);
    }

    private void SetSpeed(ISpeedModifier speedModifier)
    {
        switch (BubbleType)
        {
            case TimeBubbleType.Fast:
                speedModifier.FastSpeed(this);
                break;
            case TimeBubbleType.Slow:
                speedModifier.SlowSpeed(this);
                break;
            default:
                break;
        }
    }
}
