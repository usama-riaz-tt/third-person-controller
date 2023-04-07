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
    [SerializeField] private LayerMask GroundLayer;
    private bool _isGrounded;
    private float _inAirTimer;

    void OnEnable()
    {
        moveAction.action.performed += WalkPlayer;
        sprintAction.action.performed += SprintPlayer;
        sprintAction.action.canceled += ResetState;
        jumpAction.action.performed += Jump;
    }
    void OnDisable()
    {
        moveAction.action.performed -= WalkPlayer;
        sprintAction.action.performed -= SprintPlayer;
        sprintAction.action.canceled -= ResetState;
        jumpAction.action.performed -= Jump;
    }
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector3(inputValues.x,0,inputValues.y) * characterSpeed;
        // if (!_isGrounded)
        // {
        //     GroundCheck();
        // }
    }
    private void WalkPlayer(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
        LerpWalkAnimation(context);
    }
    private void SprintPlayer(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>() * 2f;
        //Lerp Sprint Animation
        StartCoroutine(LerpBlend(0.3f, 0.6f, 0.5f, "zMovement"));
    }
    private void ResetState(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        var inputVal = context.ReadValue<Vector2>();
        StartCoroutine(LerpBlend(0.6f, 0f, 1f, "zMovement"));
        if (inputVal.x > 0)
        {
            StartCoroutine(LerpBlend(0f, 0.1f, 0.5f, "xMovement"));
        }
        if (inputVal.x < 0)
        {
            StartCoroutine(LerpBlend(0f, -0.1f, 0.5f, "xMovement"));
        }
    }
    private void LerpWalkAnimation(InputAction.CallbackContext context)
    {
        var inputVal = context.action.ReadValue<Vector2>();
        Debug.Log($"<color=orange>Lerp Walk inputValues: {inputVal}</color>");
        if (inputVal != Vector2.zero)
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0f, 0.3f, 0.5f, "zMovement"));
            if (inputVal.x > 0)
            {
                StartCoroutine(LerpBlend(0f, 0.1f, 0.5f, "xMovement"));
            }
            if (inputVal.x < 0)
            {
                StartCoroutine(LerpBlend(0f, -0.1f, 0.5f, "xMovement"));
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(LerpBlend(0.3f,0f, 0.5f, "zMovement"));
            characterAnimator.SetFloat("xMovement", 0f);
        }
    }
    private IEnumerator LerpBlend(float from, float to, float time, string animationParameter)
    {
        float timeElapsed = 0;
        while (timeElapsed < time)
        {
            characterAnimator.SetFloat(animationParameter, Mathf.Lerp(from, to, timeElapsed / time));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        characterAnimator.SetFloat(animationParameter, to);
        yield return null;
    }
    private void Jump(InputAction.CallbackContext context)
    {
        characterRigidbody.AddForce(new Vector3(0f,100f,0f), ForceMode.Impulse);
        characterAnimator.SetTrigger("HasJumped");
        characterAnimator.SetBool("isFalling", true);
        _isGrounded = false;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        if (!_isGrounded)
        {
            _inAirTimer = _inAirTimer + Time.deltaTime;
            characterRigidbody.AddForce(-Vector3.up * 20f * _inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, 0.01f, -Vector3.up, out hit, GroundLayer))
        {
            if (!_isGrounded)
            {
                Debug.Log("TOUCED GROUND");
                characterAnimator.SetBool("isFalling", false);
            }

            _inAirTimer = 0;
            _isGrounded = true;
        }
    }
}