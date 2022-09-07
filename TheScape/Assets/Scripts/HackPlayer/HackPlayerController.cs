using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackPlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector2 currentMovementInput;
    Vector2 currentAimInput;
    Vector3 currentMovement;
    public float speed = 2.0f;
    public bool canMove= true;
    public bool isHack5Animation = false;



    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        currentAimInput = context.ReadValue<Vector2>();
    }

    public void EnableMove()
    {
        canMove = true;
    }

    public void DisableMove()
    {
        canMove = false;
    }

    void HandleMovement()
    {
        currentMovement = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);

        characterController.Move(currentMovement * speed * Time.deltaTime);

        if(currentAimInput.y != 0 || currentAimInput.x != 0)
        {
            Vector3 lookDirection = new Vector3(currentAimInput.x, 0, currentAimInput.y);

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    void HandleMovementHack5()
    {
        currentMovement = new Vector3(0, 0, 1);

        characterController.Move(currentMovement * speed * Time.deltaTime);

        Vector3 lookDirection = new Vector3(0, 0, 1);

        transform.rotation = Quaternion.LookRotation(lookDirection);

    }

    private void FixedUpdate()
    {
        if(canMove)
            HandleMovement();

        if (isHack5Animation)
            HandleMovementHack5();
        
        

    }
}
