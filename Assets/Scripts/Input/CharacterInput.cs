using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private ThirdPersonControllerActions movementControls;
    [SerializeField] private Vector2 inputValues;

    void OnEnable()
    {
        movementControls = new ThirdPersonControllerActions();
        movementControls.Player.Movement.performed += i => inputValues = i.ReadValue<Vector2>();
        movementControls.Enable();
    }
}
