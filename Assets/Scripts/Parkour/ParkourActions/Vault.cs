using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Parkour Action/Vault", fileName = "Vault")]
public class Vault : ParkourAction
{
    public override IEnumerator PerformParkourAction(Animator animator, CharacterController controller, float targetRotation)
    {
        animator.SetBool(AnimationTriggerName, true);
        var animationState = animator.GetNextAnimatorStateInfo(0);
        float timeElapsed = 0;
        var totalTime = animationState.length;
        var normal = _obstacleInfo.HitInfo.normal;
        normal = new Vector3(Mathf.Abs(normal.x),Mathf.Abs(normal.y),Mathf.Abs(normal.z));
        var addedDistance = Vector3.Dot(_obstacleInfo.HitInfo.collider.transform.localScale, normal);
        float distanceToJump = _obstacleInfo.HitInfo.point.x + addedDistance;;
        while (timeElapsed < totalTime)
        {
            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            controller.Move(targetDirection.normalized * (distanceToJump * Time.deltaTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    public override float SetVerticalVelocity(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt(1.2f * -2f * gravity);
    }
}
