using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;

    private bool isGrounded;
    public float gravity = -9.8f;

    public float jumpHeight = 2f;

    public bool crouching = false;
    public bool lerpCrouch = false;
    public float crouchTimer = 1;

    public bool proning = false;
    public bool lerpProne = false;
    public float proneTimer = 1;

    public bool sprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if (lerpProne)
        {
            proneTimer += Time.deltaTime;
            float q = proneTimer / 1;
            q *= q;

            if (proning)
            {
                controller.height = Mathf.Lerp(controller.height, (float)0.25, q);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, q);
            }

            if (q > 1)
            {
                lerpProne = false;
                proneTimer = 0f;
            }
        }
    }

    // Receives inputs for InputManager.cs and applies them to character controller
    public void ControlMove (Vector2 input)
    {
        // On foot movement
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Prone()
    {
        proning = !proning;
        proneTimer = 0;
        lerpProne = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }
    }
}
