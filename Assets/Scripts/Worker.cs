using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] MineralType minerSpecialization;
    [SerializeField] int amountOfExtraction = 1;
    [SerializeField] int bagCapacity = 10;
    [SerializeField] int amountOfExtractedOre = 0;


    public void ExtractOreEventAnimator()
    {
        amountOfExtractedOre += amountOfExtraction;
    }

    public int GetBagCapacity() { return bagCapacity; }


    public int AmountOfExtractedOre
    {
        get
        {
            return amountOfExtractedOre;
        }
        set
        {
            amountOfExtractedOre = value;
        }
    }

    public MineralType GetMineralSpecialization() { return minerSpecialization; }

    public void SetSpecialization (MineralType specialization)
    {
        minerSpecialization = specialization;
    }
}
