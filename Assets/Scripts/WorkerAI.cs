using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] PlayerEnemyMarks identityMark;

    Transform extractionLocation;
    Vector3 mineralDestination;
    Worker worker;
    Minerals minerals;
    Animator animator;
    CastleStats castleStats;
    NavMeshAgent navMeshAgent;
    NavMeshObstacle navMeshObstacle;

    private void Awake()
    {
        FindExtractionLocation();
        FindMineralSpot();
    }
    void Start()
    {
        worker = GetComponent<Worker>();
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
        FindMatchingMaterialToWorkerSpec();
        StartCoroutine(RunAI());
    }

    private void FindExtractionLocation()
    {
        GameObject[] extractionLocations = GameObject.FindGameObjectsWithTag("ExtractionPoint");
        foreach (var extraction in extractionLocations)
        {
            if (identityMark == extraction.GetComponentInParent<CastleStats>().GetMark)
            {
                extractionLocation = extraction.transform;
                castleStats = extraction.transform.GetComponentInParent<CastleStats>();
                break;
            }
        }  
    }

    private void FindMineralSpot()
    {
        Minerals[] mineralsList = FindObjectsOfType<Minerals>();
        foreach (Minerals mineralGroup in mineralsList)
        {
            if (identityMark == mineralGroup.IdentityMark)
            {
                minerals = mineralGroup;
            }
        }
    }

    IEnumerator RunAI()
    {
        while (true)
        {
            ManageBehaviour();
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void FindMatchingMaterialToWorkerSpec()
    {
        var mineralsLocations = minerals.GetMineralsLocations();

        foreach (var mineral in mineralsLocations)
        {
            bool occupedState = mineral.Key.GetComponent<Mineral>().GetIsOccupied();
            if (mineral.Value == worker.GetMineralSpecialization() && !occupedState)
            {
                mineralDestination = mineral.Key.position;
                mineral.Key.GetComponent<Mineral>().WorkerNumber++;
                break;
            }
        }
    }

    private void ManageBehaviour()
    {
        int amountOfOre = worker.AmountOfExtractedOre;
        int bagCapacity = worker.GetBagCapacity();
        if (amountOfOre < bagCapacity)
        {
            GoToMineral();
            
        }
        else if (amountOfOre >= bagCapacity)
        {
            GoToExtractionPoint();
        }
    }

    private void GoToMineral()
    {
        float distance = Vector3.Distance(transform.position, mineralDestination);
        
        if (distance >= navMeshAgent.stoppingDistance)
        {
            ActivateAgent();
            navMeshAgent.SetDestination(mineralDestination);
            animator.SetBool("isRunning", true);;
        }
        else if (distance <= navMeshAgent.stoppingDistance)
        {
            ActivateObstacle();
            LookAtOre();
            animator.SetBool("isMining", true);
        }
        
    }

    private void GoToExtractionPoint()
    {
        animator.SetBool("isMining", false);
        float distance = Vector3.Distance(transform.position, extractionLocation.position);
        if (distance >= navMeshAgent.stoppingDistance)
        {
            ActivateAgent();
            navMeshAgent.SetDestination(extractionLocation.position);
            animator.SetBool("isRunning", true);
        }
        else if(distance < navMeshAgent.stoppingDistance)
        {
            ExtractOre();
        }
    }

    private void ActivateAgent()
    {
        navMeshAgent.enabled = true;
        navMeshObstacle.enabled = false;
    }

    private void ActivateObstacle()
    {
        navMeshAgent.enabled = false;
        navMeshObstacle.enabled = true;
    }

    private void LookAtOre()
    {
        Vector3 direction = (mineralDestination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    
    private void ExtractOre()
    {
        MineralType workerSpecialization = worker.GetMineralSpecialization();
        switch (workerSpecialization)
        {
            case MineralType.Iron:
                castleStats.Iron += worker.AmountOfExtractedOre;
                worker.AmountOfExtractedOre = 0;
                break;
            case MineralType.Gold:
                castleStats.Gold += worker.AmountOfExtractedOre;
                worker.AmountOfExtractedOre = 0;
                break;
            case MineralType.Wood:
                castleStats.Wood += worker.AmountOfExtractedOre;
                worker.AmountOfExtractedOre = 0;
                break;
            default:
                break;
        }
    }
}
