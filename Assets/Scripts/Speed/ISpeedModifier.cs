using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedModifier
{
    void SlowSpeed(TimeBubble timeBubble);
    void FastSpeed(TimeBubble timeBubble);
    void NormalSpeed(TimeBubble timeBubble);
}