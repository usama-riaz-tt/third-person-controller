using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ParkourAction : ScriptableObject
{
    public float MinimumHeight;
    public float MaximumHeight;
    public string AnimationTriggerName;
    protected float _verticalVelocity;
    protected EnvironmentChecking.ObstacleInfo _obstacleInfo;
    public bool CheckIfAvailableAction(EnvironmentChecking.ObstacleInfo obstacleInfo, Transform player)
    {
        _obstacleInfo = obstacleInfo;
        float checkHeight = obstacleInfo.HeightInfo.point.y - player.position.y;
        if (checkHeight < MinimumHeight || checkHeight > MaximumHeight)
        {
            return false;
        }
        return true;
    }
    public abstract IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation, PlayerInput input);
    public abstract float SetVerticalVelocity(float jumpHeight, float gravity);
}
