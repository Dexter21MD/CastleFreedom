using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RecruitWorkers : MonoBehaviour
{
    [SerializeField] Worker minerPrefab;
    [SerializeField] int recruitmentPrice = 15;
    [SerializeField] Transform spawnPlace;
    [SerializeField] Transform newParentTransform;

    MineralType workerSpecializationAI;
    DropdownWorkerSpec dropdown;
    CastleStats castleStats;

    private void Start()
    {
        dropdown = FindObjectOfType<DropdownWorkerSpec>();
        castleStats = GetComponent<CastleStats>();
    }


    public void CreateMiner()
    {
        if (castleStats.Gold >= recruitmentPrice)
        {
            Worker newMiner = Instantiate(minerPrefab, spawnPlace.position, Quaternion.identity);
            newMiner.transform.parent = newParentTransform;
            if (castleStats.GetMark == PlayerEnemyMarks.Player)
            {
                newMiner.SetSpecialization(GetSpecialization());
            }
            else if (castleStats.GetMark == PlayerEnemyMarks.Enemy)
            {
                newMiner.SetSpecialization(workerSpecializationAI);
            }
            castleStats.Gold -= recruitmentPrice;
        }
    }

    public MineralType GetSpecialization()
    {
        string[] rawSpec;
        rawSpec = dropdown.GetDropdown().captionText.text.Split();
        MineralType newWorkSpecialization = (MineralType)Enum.Parse(typeof(MineralType), rawSpec[0]);
        return newWorkSpecialization;
    }

    public void SetSpecializationAI(MineralType newWorkerSpec)
    {
        workerSpecializationAI = newWorkerSpec;
    }
}
