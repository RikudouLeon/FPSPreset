using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;

    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        movement = GetComponent<PlayerMovement>();

        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.Sprint.performed += ctx => movement.Sprint();

        look = GetComponent<PlayerLook>();
    }

    void FixedUpdate()
    {
        movement.ControlMove(onFoot.Movement.ReadValue<Vector2>()); // Moves the player using value from movement action
    }

    private void LateUpdate()
    {
        look.ControlLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
