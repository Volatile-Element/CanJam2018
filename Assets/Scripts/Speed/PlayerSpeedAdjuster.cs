using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSpeedAdjuster : Singleton<PlayerSpeedAdjuster>, ISpeedModifier
{
    public UnityEventFor<TimeBubble> OnTimeNotNormal = new UnityEventFor<TimeBubble>();
    public UnityEvent OnTimeNormal = new UnityEvent();

    public float StartWalkSpeed = 5;
    public float StartRunSpeed = 10;

    public float SlowWalkSpeed = 1;
    public float SlowRunSpeed = 5;

    public float FastWalkSpeed = 10;
    public float FastRunSpeed = 20;

    private void Awake()
    {
        var controller = GetComponent<FirstPersonController>();
        StartWalkSpeed = controller.GetWalkSpeed();
        StartRunSpeed = controller.GetRunSpeed();

        SlowWalkSpeed = StartWalkSpeed * 0.2f;
        SlowRunSpeed = StartRunSpeed * 0.2f;

        FastWalkSpeed = StartWalkSpeed * 2f;
        FastRunSpeed = StartRunSpeed * 2f;
    }

    public void SlowSpeed(TimeBubble timeBubble)
    {
        var controller = GetComponent<FirstPersonController>();
        controller.SetSpeeds(SlowWalkSpeed, SlowRunSpeed);

        OnTimeNotNormal.Invoke(timeBubble);
    }

    public void FastSpeed(TimeBubble timeBubble)
    {
        var controller = GetComponent<FirstPersonController>();
        controller.SetSpeeds(FastWalkSpeed, FastRunSpeed);

        OnTimeNotNormal.Invoke(timeBubble);
    }

    public void NormalSpeed(TimeBubble timeBubble)
    {
        var controller = GetComponent<FirstPersonController>();
        controller.SetSpeeds(StartWalkSpeed, StartRunSpeed);

        OnTimeNormal.Invoke();
    }
}