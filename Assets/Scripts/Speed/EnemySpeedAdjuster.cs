using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpeedAdjuster : MonoBehaviour, ISpeedModifier
{
    public float StartWalkSpeed = 3.5f;
    public float SlowWalkSpeed = 1;
    public float FastWalkSpeed = 6;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        StartWalkSpeed = navMeshAgent.speed;
        SlowWalkSpeed = StartWalkSpeed * 0.2f;
        FastWalkSpeed = StartWalkSpeed * 2f;
    }

    public void FastSpeed(TimeBubble timeBubble)
    {
        navMeshAgent.speed = FastWalkSpeed;
    }

    public void NormalSpeed(TimeBubble timeBubble)
    {
        navMeshAgent.speed = StartWalkSpeed;
    }

    public void SlowSpeed(TimeBubble timeBubble)
    {
        navMeshAgent.speed = SlowWalkSpeed;
    }
}