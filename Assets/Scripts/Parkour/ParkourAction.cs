using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public float MinimumHeight;
    public float MaximumHeight;
    public string AnimationName;
    private bool _checkIfAvailable;

    public bool CheckIfAvailableAction(EnvironmentChecking.ObstacleInfo obstacleInfo, Transform player)
    {
        float checkHeight = obstacleInfo.HeightInfo.point.y - player.position.y;
        if (checkHeight < MinimumHeight || checkHeight > MaximumHeight)
        {
            return false;
        }
        return true;
    }
}
