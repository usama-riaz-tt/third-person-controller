using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private ThirdPersonControllerActions thirdPersonControllerActions;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Vector2 inputValues;

    void OnEnable()
    {
        thirdPersonControllerActions = new ThirdPersonControllerActions();
        thirdPersonControllerActions.Player.Movement.performed += SetInputValues;
        thirdPersonControllerActions.Player.Movement.Enable();
    }

    void OnDisable()
    {
        thirdPersonControllerActions.Player.Movement.performed -= SetInputValues;
    }

    void SetInputValues(InputAction.CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
    }
    void Update()
    {
        var pos = transform.position;
        pos = new Vector3(pos.x + (inputValues.x * speedMultiplier),0,pos.z + (inputValues.y * speedMultiplier));
        transform.position = pos;
    }
}
