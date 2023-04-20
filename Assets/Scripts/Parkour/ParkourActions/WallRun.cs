using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "New Parkour Action/Wall Run", fileName = "Wall Run")]
public class WallRun : ParkourAction
{
    public float JumpHeight;
    public float DistanceCovered;
    public override IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation,
        PlayerInput input)
    {
        input.enabled = false;
        animator.SetBool(AnimationTriggerName, true);
        var animationState = animator.GetNextAnimatorStateInfo(0);
        float timeElapsed = 0;
        var totalTime = animationState.length;
        var normal = _obstacleInfo.HitInfo.normal;
        var wallForward = Vector3.Cross(normal, controller.transform.up);
        wallForward = new Vector3(Mathf.Abs(wallForward.x),Mathf.Abs(wallForward.y),Mathf.Abs(wallForward.z));
        while (timeElapsed < totalTime)
        {
            controller.Move(wallForward * (DistanceCovered * Time.deltaTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        input.enabled = true;
    }

    public override float SetVerticalVelocity(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt((JumpHeight) * -2f * gravity);

    }
}
