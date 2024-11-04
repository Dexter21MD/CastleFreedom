using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour
{
    [SerializeField] float executionTimeMove = 2f;
    [SerializeField] float executionTimeAttack = 2f;
    [SerializeField] PlayerEnemyMarks identityMark;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] bool move = true;
    [SerializeField] bool attack = false;
    float distance;
    bool targetAliveStatus = false;
    Transform target;
    Waypoint[] waypoints; 
    NavMeshAgent navMesh;
    Warrior warrior;
    Animator animator;
    int waypointIndex = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        warrior = GetComponent<Warrior>();
        animator = GetComponentInChildren<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        SearchWaypoints();
        StartCoroutine(Move());
        StartCoroutine(Attack());
    }

    private void SearchWaypoints()
    {
        WaypointCollector[] waypointCollectors = FindObjectsOfType<WaypointCollector>();

        foreach (WaypointCollector waypointCollection in waypointCollectors)
        {
            if (waypointCollection.GetIdentity == identityMark)
            {
                waypoints = waypointCollection.GetWaypoints();
            }
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (move && !attack)
            {
                navMesh.SetDestination(waypoints[waypointIndex].transform.position);
                yield return new WaitForSeconds(executionTimeMove);
                float distance = Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position);
                if (distance <= navMesh.stoppingDistance)
                {
                    if (!(waypointIndex == waypoints.Length - 1))
                    {
                        waypointIndex++;
                    }
                    else
                    {
                        move = false;
                    }
                }
            }
            yield return null;  
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (attack && !move)
            {
                if (target != null)
                {
                    SetDestinationDefensive();
                    yield return new WaitForSeconds(executionTimeAttack);
                    SetDistance();
                    if (distance <= navMesh.stoppingDistance)
                    {
                        LookAtEnemy();
                        SetTargetAliveStatus();
                        animator.SetBool("isRunning", !targetAliveStatus);
                        animator.SetBool("isAttacking", targetAliveStatus);
                    }
                    else if (target == null)
                    {
                        SetTargetAliveStatus();
                    }
                }
            }
            yield return null;
        } 
    }
    IEnumerator OnTriggerStay(Collider other)
    {
        
        yield return new WaitForSeconds(0.5f);
        if (other != null)
        {
            if (other.GetComponent<Warrior>() != null && other.GetComponent<Warrior>().Alive)
            {
                if (!attack && other != null)
                {
                    if (identityMark != other.GetComponent<WarriorAI>().identityMark)
                    {
                        target = other.transform;
                        move = false;
                        attack = true;
                    }
                }
            }
        }
    }

    private void SetDestinationDefensive()
    {
        if (target.GetComponent<NavMeshAgent>().enabled)
        {
            navMesh.SetDestination(target.position);
        }
    }
    private void SetTargetAliveStatus()
    {
        if (target != null)
        {
           targetAliveStatus = target.GetComponent<WarriorAI>().enabled;
           attack = targetAliveStatus;
           move = !targetAliveStatus;
        }
        else if (target == null)
        {
            targetAliveStatus = false;
            attack = false;
            move = true;
        }
    }
    private void SetDistance()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
        }
    }

    private void LookAtEnemy()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        } 
    }

    public Transform getTarget()
    {
        return target;
    }

    public PlayerEnemyMarks Mark
    {
        get
        {
            return identityMark;
        }
        set
        {
            identityMark = value;
        }
    }
}
