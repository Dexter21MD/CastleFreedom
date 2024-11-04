using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitWarrior : MonoBehaviour
{
    [SerializeField] Warrior warriorPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform parentTransform;
    [SerializeField] PlayerEnemyMarks identityMark;

    [SerializeField] int warriorPrice = 15;
    [SerializeField] int bonusDamage  = 0;
    [SerializeField] int bonusArmor  = 0;
    
    CastleStats castleInfo;
    GameUnitsInformation gameUnitsInformation;

    List<Warrior> playerWarriors = new List<Warrior>();

    private void Start()
    {
        gameUnitsInformation = FindObjectOfType<GameUnitsInformation>();
        FindCastleStats();
    }

    private void FindCastleStats()
    {
        CastleStats[] castleStats = FindObjectsOfType<CastleStats>();
        foreach (CastleStats castle in castleStats)
        {
            if (identityMark == castle.GetMark)
            {
                castleInfo = castle;
            }
        }
    }

    public void Recruit()
    {
        if (castleInfo.Gold >= warriorPrice)
        {
            Warrior newWarrior = Instantiate(warriorPrefab, spawnPoint.position, Quaternion.identity);
            newWarrior.UpgradeArmor(bonusArmor);
            newWarrior.UpgradeDamage(bonusDamage);
            if (identityMark == PlayerEnemyMarks.Player)
            {
                playerWarriors.Add(newWarrior);
                newWarrior.listIndex = playerWarriors.Count - 1;
            } 
            gameUnitsInformation.CalculateAndStorePower(newWarrior, identityMark);
            newWarrior.transform.parent = parentTransform;
            castleInfo.Gold -= warriorPrice;
        }
    }

    public void DelWarriorFromList(int index)
    {
        playerWarriors.RemoveAt(index);
    }

    public int BonusDamage
    {
        get
        {
            return bonusDamage;
        }
        set
        {
            bonusDamage = value;
        }
    }

    public int BonusArmor
    {
        get
        {
            return bonusArmor;
        }
        set
        {
            bonusArmor = value;
        }
    }

    public PlayerEnemyMarks GetMark
    {
        get
        {
            return identityMark;
        }
    }

    public List<Warrior> GetPlayerWarriors() { return playerWarriors; }
}
