using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceStatsUpdate : MonoBehaviour
{
    Label[] labels;
    // Start is called before the first frame update
    void Awake()
    {
        GetAllLabels();
    }

   

    private void GetAllLabels()
    {
        labels = GetComponentsInChildren<Label>();
    }

    public void SearchSpecificLabel(LabelType type, int amountResource)
    {
        foreach (Label label in labels)
        {
            if (label.LabelType == type)
            {
                label.UpdateStats(amountResource);
                break;
            }
        }
    }
}
