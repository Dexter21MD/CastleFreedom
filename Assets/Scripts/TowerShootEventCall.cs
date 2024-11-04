using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootEventCall : MonoBehaviour
{
    Tower tower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<Tower>();
    }

    public void CallShootEvent()
    {
        tower.Shoot();
    }
}
