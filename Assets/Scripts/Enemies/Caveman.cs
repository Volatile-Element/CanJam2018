using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caveman : MonoBehaviour
{
    private enum CavemanState { Idle, Moving, ChasingPlayer, MovingToPlayerLastKnown, ScanningForPlayer };

    private NavMeshAgent navMeshAgent;
    private CavemanState state;

    private Vector3 destination;

    private bool playerInSight;
    private Vector3 playerLastKnown;

    private const float PLAYER_VISIBLE_IN = 20;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        PlanAGrunt();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckIfPlayerInRange();

        switch (state)
        {
            case CavemanState.Idle:
                if (Random.Range(0, 10000) > 9000)
                {
                    Wander();
                }
                break;
            case CavemanState.Moving:
                if (Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    navMeshAgent.isStopped = true;
                    state = CavemanState.Idle;
                }
                break;
            case CavemanState.ChasingPlayer:
                if (!playerInSight)
                {
                    StartMoving(playerLastKnown);
                    state = CavemanState.MovingToPlayerLastKnown;
                }
                break;
            case CavemanState.MovingToPlayerLastKnown:
                if (Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    navMeshAgent.isStopped = true;
                    state = CavemanState.ScanningForPlayer;
                }
                break;
            case CavemanState.ScanningForPlayer:
                if (Random.Range(0, 10000) > 9000)
                {
                    Wander();
                }
                break;
            default:
                break;
        }
    }

    private void StartMoving(Vector3 dest)
    {
        destination = dest;

        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;
    }

    private void Wander()
    {
        state = CavemanState.Moving;
        destination = GetRandomPointInWorld();

        StartMoving(destination);
    }

    private void PlanAGrunt()
    {
        DoActionIn.Create(() =>
        {
            SoundManager.Instance.PlaySingleFireRandom("Sounds/Grunts", 1, 0.5f, true);

            PlanAGrunt();
        }, Random.Range(5f, 20f));
    }

    private void CheckIfPlayerInRange()
    {
        Vector3 targetDir = Player.Instance.transform.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (angleToPlayer >= -90 && angleToPlayer <= 90)
        {
            Vector3 fromPosition = transform.position;
            Vector3 toPosition = Player.Instance.transform.position;
            Vector3 direction = toPosition - fromPosition;

            var ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction, Color.red, 0.1f);
            RaycastHit hit;

            var result = Physics.Raycast(ray, out hit, PLAYER_VISIBLE_IN);
            if (result)
            {
                if (hit.collider.GetComponent<Player>() != null)
                {
                    playerInSight = true;
                    playerLastKnown = new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z);
                    StartMoving(playerLastKnown);
                    state = CavemanState.ChasingPlayer;
                }
                else
                {
                    playerInSight = false;
                }
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }

    private Vector3 GetRandomPointInWorld()
    {
        var x = Random.Range(-(WorldManager.Instance.Width / 2), WorldManager.Instance.Width / 2);
        var y = 0;
        var z = Random.Range(-(WorldManager.Instance.Height / 2), WorldManager.Instance.Height / 2);

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        if (playerInSight)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position + new Vector3(0, 2, 0), 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        if (destination != null)
        {
            Gizmos.DrawSphere(destination, 0.25f);
        }
    }
}