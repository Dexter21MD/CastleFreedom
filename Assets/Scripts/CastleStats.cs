using UnityEngine;

public class CastleStats : MonoBehaviour
{
    [SerializeField] int gold = 100;
    [SerializeField] int wood = 100;
    [SerializeField] int iron = 100;
    [SerializeField] int health = 100;
    [SerializeField] PlayerEnemyMarks identityMark;

    ResourceStatsUpdate resourceStatsUpdate;

    private void Start()
    {
        resourceStatsUpdate = FindObjectOfType<ResourceStatsUpdate>();
        Invoke("UpdateStatsAtStart", 0.1f);
        
    }

    

    private void UpdateStatsAtStart()
    {
        
        Gold = gold;
        Wood = wood;
        Iron = iron;
        Health = health;
    }

    public int Gold
    {
        
        get
        {
            return gold;
        }
        set
        {
            
            gold = value;
            if (identityMark == PlayerEnemyMarks.Player)
            {
                resourceStatsUpdate.SearchSpecificLabel(LabelType.GoldLabel, gold);
            }
            
        }
    }

    public int Wood
    {
        get
        {
            return wood;
        }
        set
        {
            wood = value;
            if (identityMark == PlayerEnemyMarks.Player)
            {
                resourceStatsUpdate.SearchSpecificLabel(LabelType.WoodLabel, wood);
            } 
        }
    }

    public int Iron
    {
        get
        {
            return iron;
        }
        set
        {
           iron = value;
            if (identityMark == PlayerEnemyMarks.Player)
            {
                resourceStatsUpdate.SearchSpecificLabel(LabelType.IronLabel, iron);

            }
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
            if (identityMark == PlayerEnemyMarks.Player)
            {
                resourceStatsUpdate.SearchSpecificLabel(LabelType.HealthLabel, health);
            } 
        }
    }

    public PlayerEnemyMarks GetMark
    {
        get
        {
            return identityMark;
        }
    }
}
