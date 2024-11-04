using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractOreCallFunction : MonoBehaviour
{
    Worker worker;
    private void Start()
    {
        worker = GetComponentInParent<Worker>();
    }

    public void CallExtractFuction()
    {
        worker.ExtractOreEventAnimator();
    }
}
