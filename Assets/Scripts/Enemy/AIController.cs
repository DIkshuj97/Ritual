using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float agroCooldownTime = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float maxSpeed = 6f;

    NavMeshAgent navMeshAgent;
    GameObject player;
    Vector3 guardPosition;

    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    float timeSinceAggrevated = Mathf.Infinity;
    int currentWaypointIndex = 0;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        guardPosition = GetGuardPosition();
    }

    private Vector3 GetGuardPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        if (IsAggrevated() )
        {
            AttackBehaviour();
        }
        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            suspicionBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }
        UpdateTimers();
    }

    public void Aggrevate()
    {
        timeSinceAggrevated = 0;
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        timeSinceAggrevated += Time.deltaTime;
    }

    private void AttackBehaviour()
    {
        timeSinceLastSawPlayer = 0;
        // move enemy to player 
        //if player is in attack range attack player
    }

    private void suspicionBehaviour()
    {
       
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWayPoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        if (timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
           MoveTo(nextPosition, patrolSpeedFraction);
        }
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        GetComponent<NavMeshAgent>().destination = destination;
        navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMeshAgent.isStopped = false;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWayPoint(currentWaypointIndex);
    }

    private void CycleWayPoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWayPoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private bool IsAggrevated()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}