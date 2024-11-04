using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    Dictionary<Transform, MineralType> mineralLocations = new Dictionary<Transform, MineralType>();
    [SerializeField] PlayerEnemyMarks indentityMark;
    Mineral[] minerals;

    private void Awake()
    {
        AddMineralToDictionary();
    }

    private void SearchForMinerals()
    {
        minerals = GetComponentsInChildren<Mineral>();
    }

    private void AddMineralToDictionary()
    {
        SearchForMinerals();
        foreach (Mineral mineral in minerals)
        {
            mineralLocations.Add(mineral.transform, mineral.MineralLabel);
        }
    }

    public PlayerEnemyMarks IdentityMark
    {
        get
        {
            return indentityMark;
        }
    }



    public Dictionary<Transform, MineralType> GetMineralsLocations() { return mineralLocations; }
}
