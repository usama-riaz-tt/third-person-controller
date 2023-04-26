using System.Collections;
using DitzelGames.FastIK;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "New Parkour Action/Climb", fileName = "Climb")]
public class Climb : ParkourAction
{
    public override IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation, PlayerInput input, FastIKFabric leftHand, FastIKFabric rightHand)
    {
        Debug.Log("CLIMB");
        leftHand.Target = _obstacleInfo.HeightInfo.transform;
        rightHand.Target = _obstacleInfo.HeightInfo.transform;
        input.enabled = false;
        var animationState = animator.GetNextAnimatorStateInfo(0);
        var totalTime = animationState.length;
        animator.SetBool(AnimationTriggerName, true);
        float timeElapsed = 0;
        while (timeElapsed < totalTime)
        {
            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (_obstacleInfo.HitInfo.distance * Time.deltaTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        input.enabled = true;
        leftHand.Target = null;
        rightHand.Target = null;
    }

    public override float SetVerticalVelocity(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt((_obstacleInfo.HeightInfo.point.y + 0.25f) * -2f * gravity);
    }
}
