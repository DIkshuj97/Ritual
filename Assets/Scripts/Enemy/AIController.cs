using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float attackDistance = 2f;
    [Range(0,360)]
    [SerializeField] float viewAngle = 100;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float agroCooldownTime = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float maxSpeed = 6f;

    public LayerMask obstaclesMask;
    public LayerMask targetMask;
    public List<Transform> visibleTargts = new List<Transform>();

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

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer>attackDistance)
        {
            MoveTo(player.transform.position,1f);
        }
        else
        {
            Debug.Log("attack");
        }
       
    }


    private void suspicionBehaviour()
    {
        navMeshAgent.isStopped = true;
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

   // private bool IsAggrevated()
    //{
        //float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        //return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
    //}



    private bool IsAggrevated()
    {
        visibleTargts.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, chaseDistance, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget)<viewAngle/2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position,dirToTarget,disToTarget, obstaclesMask))
                {
                   if(!target.GetComponent<Hide>().isHide)
                    {
                        visibleTargts.Add(target);
                        return true;
                    }      
                }
            }
        }

        return false;
    }

    public Vector3 DirFromAngle(float angleInDegree,bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 viewAngleA = DirFromAngle(-viewAngle/2 , false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * chaseDistance);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * chaseDistance);
    }
}