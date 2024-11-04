using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownWorkerSpec : MonoBehaviour
{
    MineralType[] mineralSpecTypes = new MineralType[3];

    
    TMP_Dropdown dropdownWorker;

    private void Awake()
    {
        dropdownWorker = GetComponent<TMP_Dropdown>();
        dropdownWorker.options.Clear();
        SetMineralTypes();
        SetOptions();
    }


    private void SetMineralTypes()
    {
        for (int i = 0; i < mineralSpecTypes.Length; i++)
        {
            mineralSpecTypes.SetValue((MineralType)i,i);
        }
        
    }

    private void SetOptions()
    {
        foreach (MineralType specialization in mineralSpecTypes)
        {
            dropdownWorker.options.Add(new TMP_Dropdown.OptionData(specialization.ToString() +" Specialization")); 
        }
    }

    public TMP_Dropdown GetDropdown() { return dropdownWorker; }
}
