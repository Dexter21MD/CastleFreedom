using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollector : MonoBehaviour
{
    [SerializeField] PlayerEnemyMarks identityMark;
    public Waypoint[] GetWaypoints()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        return waypoints;
    }

    public PlayerEnemyMarks GetIdentity
    {
        get
        {
            return identityMark;
        }
    }
}
