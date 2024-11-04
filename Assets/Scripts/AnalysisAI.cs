using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisAI : MonoBehaviour
{
    [SerializeField] float manageTroopsResponseTime = 1f;
    [SerializeField] float manageWorkersResponseTime = 4f;
    int dangerDistance = 61;

    RecruitWarrior addWarrior;
    List<Warrior> playerArmy;
    CastleStats castleData;
    GameUnitsInformation unitsInformation;
    RecruitWorkers addWorker;
    int armyDifference;


    // Start is called before the first frame update
    void Start()
    {
        unitsInformation = FindObjectOfType<GameUnitsInformation>();
        addWorker = GetComponent<RecruitWorkers>();
        castleData = GetComponent<CastleStats>();
        FindRecruitWarrior();
        StartCoroutine(ManageTroops());
        StartCoroutine(ManageWorkers());
    }

    private void FindRecruitWarrior()
    {
        RecruitWarrior[] recruitWarriors = FindObjectsOfType<RecruitWarrior>();
        foreach (RecruitWarrior recruitModule in recruitWarriors)
        {
            if (recruitModule.GetMark == castleData.GetMark)
            {
                addWarrior = recruitModule;
            }
            else
            {
                playerArmy = recruitModule.GetPlayerWarriors();
            }
        }
    }
    private void CheckArmyPowerDifference()
    {
        armyDifference = Mathf.RoundToInt(unitsInformation.GetPlayerPower - unitsInformation.GetBotPower);
    }

    private void SendTroops(int deployChance) 
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber < deployChance)
        {
            addWarrior.Recruit();
        }
       
    }
    private int InrceaseProbability()
    {
        int probabilityMultipler = 1;
        float distance = Vector3.Distance(castleData.transform.position, 
            playerArmy[0].transform.position);
        if (distance <= dangerDistance)
        {
            probabilityMultipler = 3;
        }
        else
        {
            probabilityMultipler = 1;
        }
        return probabilityMultipler;
    }

    IEnumerator ManageTroops()
    {
        while (true)
        {
            CheckArmyPowerDifference();
            yield return new WaitForSeconds(manageTroopsResponseTime);

            if (!(armyDifference <= 0))
            {
                Debug.Log(25 * InrceaseProbability());
                SendTroops(25 * InrceaseProbability());
            }
            SendTroops(0);
        }        
    }

    private void ChooseSpecForWorker()
    {
        MineralType[] specTypes = (MineralType[])Enum.GetValues(typeof(MineralType));
        int randomNumber = UnityEngine.Random.Range(0, 2);
        addWorker.SetSpecializationAI(specTypes[randomNumber]);
    }

    private void HireWorkers(int deployChance)
    {
        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber < deployChance)
        {
            addWorker.CreateMiner();
        }
    }


    IEnumerator ManageWorkers()
    {
        while (true)
        {
            ChooseSpecForWorker();
            yield return new WaitForSeconds(manageWorkersResponseTime);
            HireWorkers(25);
        }
    }
}
