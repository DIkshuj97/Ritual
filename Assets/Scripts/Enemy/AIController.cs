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
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 3f;
    [Range(0, 1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float maxSpeed = 6f;

    public LayerMask obstaclesMask;
    public LayerMask targetMask;
    public List<Transform> visibleTargts = new List<Transform>();

    public bool isCrawler = false;
    public bool isCrawlerChasing = false;

    NavMeshAgent navMeshAgent;
    GameObject player;
    Vector3 guardPosition;

    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    
    int currentWaypointIndex = 0;

    public bool canChase = true;

    private void Start()
    {
        player = GameManager.ins.player;
        navMeshAgent = GetComponent<NavMeshAgent>();
        guardPosition = GetGuardPosition();
    }

    private Vector3 GetGuardPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        updateAnimator();

        if (!PlayerDeath.isAlive || !canChase) return;

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

        if(isCrawler)
        {
            if (navMeshAgent.velocity.magnitude > 0)
            {
                isCrawlerChasing = true;
            }
        }
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void AttackBehaviour()
    { 
        timeSinceLastSawPlayer = 0;
        
        if(!GetIsInRange())
        {
            StopAttack();
            SoundManager.ins.PlayMusic("Chase");
            MoveTo(player.transform.position,1f);
        }
        else
        {
            navMeshAgent.isStopped = true;
            transform.LookAt(player.transform);
            GetComponentInChildren<Animator>().ResetTrigger("Attack");
            GetComponentInChildren<Animator>().SetTrigger("Attack");
            player.GetComponentInChildren<PlayerDeath>().TriggerDeath();
           
        }
    }

    private bool GetIsInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackDistance;
    }

    private void updateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localvelocity = transform.InverseTransformDirection(velocity);
        float speed = localvelocity.z;
        GetComponentInChildren<Animator>().SetFloat("ForwardSpeed", speed);
    }


    private void suspicionBehaviour()
    {
        SoundManager.ins.PlayMusic("BGM");
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

    private void StopAttack()
    {
        GetComponentInChildren<Animator>().ResetTrigger("Attack");
        GetComponentInChildren<Animator>().SetTrigger("StopAttack");
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

  //  private bool IsAggrevated()
   // {
    //    float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      //  return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
    //}

    private bool IsAggrevated()
    {
       
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
                   
                    if (!target.GetComponent<Hide>().isHide)
                    {
                       
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