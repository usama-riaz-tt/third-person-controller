using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Rigidbody characterRigidbody;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Vector2 inputValues;

    void OnEnable()
    {
        moveAction.action.performed += MovePlayer;
    }
    void OnDisable()
    {
        moveAction.action.performed -= MovePlayer;
    }
    private void MovePlayer(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
        LerpAnimation(context);
    }
    void FixedUpdate()
    {
        var pos = transform.position;
        pos = new Vector3(pos.x + (inputValues.x * speedMultiplier),0,pos.z + (inputValues.y * speedMultiplier));
        transform.position = pos;
    }
    private void LerpAnimation(InputAction.CallbackContext context)
    {
        var inputVal = context.action.ReadValue<Vector2>();
        if (inputVal != Vector2.zero)
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0f, 0.3f));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0.3f,0f));
        }
    }
    private IEnumerator LerpBlend(float from, float to)
    {
        float timeElapsed = 0;
        var totalTime = 0.3f;
        while (timeElapsed < totalTime)
        {
            characterAnimator.SetFloat("horizontalMovement", Mathf.Lerp(from, to, timeElapsed / totalTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        characterAnimator.SetFloat("horizontalMovement", to);
        yield return null;
    }
    private void Jump(InputAction.CallbackContext context)
    {
        characterRigidbody.AddForce(new Vector3(0f,25f,0f), ForceMode.Impulse);
    }
}