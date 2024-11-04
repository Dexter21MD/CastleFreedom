using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : MonoBehaviour
{

    [SerializeField] bool alive  = true;
    [SerializeField] float executionTime = 1f;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 50;
    [SerializeField] PlayerEnemyMarks mark;
    public float power { get; set; } = 0f;
    public int listIndex { get; set; }

    WarriorAI warriorAI;
    NavMeshAgent agent;
    Animator animator;
    RecruitWarrior recruitWarrior;
    GameUnitsInformation gameUnitsInformation;

    // Start is called before the first frame update
    void Start()
    {
        recruitWarrior = GetComponentInParent<RecruitWarrior>();
        gameUnitsInformation = FindObjectOfType<GameUnitsInformation>();
        warriorAI = GetComponent<WarriorAI>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(CheckLifeStatus());
    }

    IEnumerator CheckLifeStatus()
    {
        while (true)
        {
            alive = health > 0;
            if (!alive)
            {
                animator.SetTrigger("die");
                warriorAI.enabled = false;
                agent.isStopped = true;
                gameUnitsInformation.DecreasePowerOnDeath(power, mark);
                if (mark == PlayerEnemyMarks.Player)
                {
                    recruitWarrior.DelWarriorFromList(listIndex);
                }
                yield return new WaitForSeconds(executionTime);
                Destroy(gameObject);
            }
            yield return null;            
        }
    }

    public PlayerEnemyMarks GetMark
    {
        get
        {
            return mark;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public void DealDamage(Warrior targetHP)
    {
        targetHP.Health -= damage;
    }

    public bool Alive
    {
        get
        {
            return alive;
        }
        set
        {
            alive = value;
        }
    }

    public void UpgradeDamage(int upgradeValue)
    {
        damage += upgradeValue;
    }

    public void UpgradeArmor(int upgradeValue)
    {
        health += upgradeValue;
    }
}
