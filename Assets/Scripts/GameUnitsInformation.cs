using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitsInformation : MonoBehaviour
{
    
    float playerUnitsPower { get; set; }

    float botUnitsPower { get; set; }

    const float DEFAULT_WARRIOR_POWER = 1f;

    public void CalculateAndStorePower(Warrior warrior, PlayerEnemyMarks mark)
    {
        warrior.power = DEFAULT_WARRIOR_POWER;

        switch (mark)
        {
            case PlayerEnemyMarks.Player:
                playerUnitsPower += DEFAULT_WARRIOR_POWER;
                break;
            case PlayerEnemyMarks.Enemy:
                botUnitsPower += DEFAULT_WARRIOR_POWER;
                break;
            default:
                break;
        }
    }

    public void DecreasePowerOnDeath(float storedPower, PlayerEnemyMarks mark)
    {
        switch (mark)
        {
            case PlayerEnemyMarks.Player:
                playerUnitsPower -= storedPower;
                break;
            case PlayerEnemyMarks.Enemy:
                botUnitsPower -= storedPower;
                break;
            default:
                break;
        }
    }

    public float GetPlayerPower
    {
        get
        {
            return playerUnitsPower;
        }
    }

    public float GetBotPower
    {
        get
        {
            return botUnitsPower;
        }
    }


}
