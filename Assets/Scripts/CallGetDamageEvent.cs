using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGetDamageEvent : MonoBehaviour
{
    Warrior warrior;
    WarriorAI warriorAI;

    private void Start()
    {
        warrior = GetComponentInParent<Warrior>();
        warriorAI = GetComponentInParent<WarriorAI>();
    }

    public void DamageEvent()
    {
        Transform targetPos = warriorAI.getTarget();
        if (targetPos != null)
        {
            Warrior targetToAttack = targetPos.GetComponent<Warrior>();
            warrior.DealDamage(targetToAttack);
        }
    }
}
