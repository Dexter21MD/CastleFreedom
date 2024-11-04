using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBuildingCanvas : MonoBehaviour
{
    [SerializeField] Canvas buildingCanvas;
    Canvas[] canvasArray;
    // Start is called before the first frame update
    void Start()
    {
        canvasArray = FindObjectsOfType<Canvas>();
        buildingCanvas.enabled = false;
    }

    public void ActiveCanvas()
    {
        foreach (Canvas canvas in canvasArray)
        {
            if (canvas.enabled && canvas.tag != "StatCanvas")
            {
                canvas.enabled = false;
            }
        }
        buildingCanvas.enabled = true;
    }

    public void DeactivateCanvas()
    {
        buildingCanvas.enabled = false;
    }
}
