using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    [SerializeField] MineralType mineralType;
    [SerializeField] int workerNumber = 0;
    [SerializeField] int maxWorkerNumber = 1;
    bool isOccupied = false;
    

    public MineralType MineralLabel
    {
        get
        {
            return mineralType;
        }
    }

    public int WorkerNumber
    {
        get
        {
            return workerNumber;
        }
        set
        {
            if (!isOccupied)
            {
                workerNumber = value;
                isOccupied = workerNumber == maxWorkerNumber;
            }
        }
    }
    public bool GetIsOccupied() { return isOccupied; }
}
