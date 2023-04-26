using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "New Parkour Action/LedgeHang", fileName = "LedgeHang")]
public class LedgeHang : ParkourAction
{
    private CharacterController _controller;
    public override IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation,
        PlayerInput input, FastIKFabric leftHand, FastIKFabric rightHand)
    {
        _controller = controller;
        input.enabled = false;
        var animationState = animator.GetNextAnimatorStateInfo(0);
        var totalTime = animationState.length;
        animator.SetBool(AnimationTriggerName, true);
        float timeElapsed = 0;
        Debug.Log($"TOTALTIME: {totalTime}");
        while (timeElapsed < totalTime)
        {
            if (controller.transform.position.y >= _obstacleInfo.HeightInfo.point.y - _controller.height)
            {
                controller.enabled = false;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        controller.enabled = true;
        animator.SetTrigger("LedgeClimb");
        var secondanimationState = animator.GetNextAnimatorStateInfo(0);
        totalTime = secondanimationState.length;
        timeElapsed = 0;
        while (timeElapsed < 3.8f)
        {
            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (_obstacleInfo.HitInfo.distance * Time.deltaTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        input.enabled = true;
        leftHand.Target = null;
        rightHand.Target = null;
        yield return null;
    }

    public override float SetVerticalVelocity(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt((_obstacleInfo.HeightInfo.point.y + 0.25f) * -2f * gravity);
    }
}
