using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Label : MonoBehaviour
{
    [SerializeField] LabelType labelType;
    TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateStats(100);
    }

    public void UpdateStats(int resourceAmount)
    {
        textMesh.text = resourceAmount.ToString();
    }


    public LabelType LabelType
    {
        get
        {
            return labelType;
        }
    }

}
