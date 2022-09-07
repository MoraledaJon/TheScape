using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackPlayerControllerJump : MonoBehaviour
{
    CharacterController characterController;
    Vector2 currentMovementInput;
    Vector2 currentAimInput;
    Vector3 currentMovement;
    public float speed = 2.0f;
    public bool canMove = true;
    public bool isHack5Animation = false;
    Vector3 rotatedMovement;
    public Transform camera;
    float mDesiredRotation = 0f;
    float rotationSpeed = 15f;


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
        
        rotatedMovement = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * currentMovement;
        
        characterController.Move(rotatedMovement * speed * Time.deltaTime);

        if (currentAimInput.y != 0 || currentAimInput.x != 0)
        {
            Vector3 lookDirection = new Vector3(currentAimInput.x, 0, currentAimInput.y);

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        //if (rotatedMovement.magnitude > 0)
        //{
        //    mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        //}

        //Quaternion currentRotation = transform.rotation;
        //Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        //transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
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
        if (canMove)
            HandleMovement();

        if (isHack5Animation)
            HandleMovementHack5();



    }
}
