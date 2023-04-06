using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private Rigidbody characterRigidbody;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float characterSpeed;
    [SerializeField] private Vector2 inputValues;

    void OnEnable()
    {
        moveAction.action.performed += MovePlayer;
        sprintAction.action.performed += SprintPlayer;
        jumpAction.action.performed += Jump;
    }
    void OnDisable()
    {
        moveAction.action.performed -= MovePlayer;
        sprintAction.action.performed -= SprintPlayer;
        jumpAction.action.performed -= Jump;
    }
    private void MovePlayer(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
        LerpWalkAnimation(context);
    }
    private void SprintPlayer(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
        LerpSprintAnimation(context);
    }
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector3(inputValues.x,0,inputValues.y) * characterSpeed;
    }
    private void LerpWalkAnimation(InputAction.CallbackContext context)
    {
        var inputVal = context.action.ReadValue<Vector2>();
        if (inputVal != Vector2.zero)
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0f, 0.3f, 0.5f));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0.3f,0f, 0.5f));
        }
    }
    private void LerpSprintAnimation(InputAction.CallbackContext context)
    {
        var inputVal = context.action.ReadValue<Vector2>();
        if (inputVal != Vector2.zero)
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0.3f, 0.6f, 0.5f));
        }
        else
        {
            Debug.Log("Sprint to IDLE");
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0.6f,0f,1f));
        }
    }
    private IEnumerator LerpBlend(float from, float to, float time)
    {
        float timeElapsed = 0;
        while (timeElapsed < time)
        {
            characterAnimator.SetFloat("horizontalMovement", Mathf.Lerp(from, to, timeElapsed / time));
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