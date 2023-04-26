using System.Collections;
using DitzelGames.FastIK;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ParkourAction : ScriptableObject
{
    public float MinimumHeight;
    public float MaximumHeight;
    public string AnimationTriggerName;
    public LayerMask ObjectLayer;
    protected float _verticalVelocity;
    protected EnvironmentChecking.ObstacleInfo _obstacleInfo;
    public bool CheckIfAvailableAction(EnvironmentChecking.ObstacleInfo obstacleInfo, Transform player)
    {
        _obstacleInfo = obstacleInfo;
        float checkHeight = obstacleInfo.HeightInfo.point.y - player.position.y;
        Debug.Log(checkHeight);
        if (checkHeight < MinimumHeight || checkHeight > MaximumHeight ||  (1 << obstacleInfo.HitInfo.transform.gameObject.layer) != ObjectLayer)
        {
            return false;
        }
        return true;
    }
    public abstract IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation, PlayerInput input, FastIKFabric leftHand, FastIKFabric rightHand);
    public abstract float SetVerticalVelocity(float jumpHeight, float gravity);
}
