using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    [SerializeField] int upgradeMultiplerDamage = 0;
    [SerializeField] int upgradMultiplerlArmor = 0;
    [SerializeField] int upgradeValue = 5;
    [SerializeField] int maxMultiplerUpgradeDamage = 3; //zmienić na const!
    [SerializeField] int maxMultiplerUpgradeArmor = 3; // I DAĆ JEDNO I DOSTĘP DO NIEGO
    [SerializeField] int costUpgradeWeapon = 50;
    [SerializeField] int costUpgradeArmor = 50;
    [SerializeField] PlayerEnemyMarks identityMark;

    RecruitWarrior recruitWarriorModule;
    CastleStats castleInfo;

    // Start is called before the first frame update
    void Start()
    {
        FindWarriorModule();
        FindCastleStats();
    }

    private void FindWarriorModule()
    {
        RecruitWarrior[] modules = FindObjectsOfType<RecruitWarrior>();

        foreach (RecruitWarrior module in modules)
        {
            if (identityMark == module.GetMark)
            {
                recruitWarriorModule = module;
            }
        }
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


    public void UpgradeWeapons()
    {
        if (upgradeMultiplerDamage < maxMultiplerUpgradeDamage)
        {
            if (costUpgradeWeapon <= castleInfo.Gold && costUpgradeWeapon <= castleInfo.Iron)
            {
                upgradeMultiplerDamage++;
                recruitWarriorModule.BonusDamage += upgradeValue * upgradeMultiplerDamage;
                castleInfo.Gold -= costUpgradeWeapon;
                castleInfo.Iron -= costUpgradeWeapon;
                costUpgradeWeapon *= upgradeMultiplerDamage;
            }
        }  
    }

    public void UpgradeArmor()
    {
        if (upgradMultiplerlArmor < maxMultiplerUpgradeArmor)
        {
            if (costUpgradeArmor <= castleInfo.Gold && costUpgradeArmor <= castleInfo.Iron)
            {
                upgradMultiplerlArmor++;
                recruitWarriorModule.BonusArmor += upgradeValue * upgradMultiplerlArmor;
                castleInfo.Gold -= costUpgradeArmor;
                castleInfo.Iron -= costUpgradeArmor;
                costUpgradeArmor *= upgradMultiplerlArmor;
            }
        }
    }
}
