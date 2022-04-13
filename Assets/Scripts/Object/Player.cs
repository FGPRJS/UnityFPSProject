using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction moveAction;
    private CharacterController charactercontroller;

    private MechaBase main;

    private CinemachineVirtualCamera[] cinemachines;
    private enum CameraMode
    {
        DefaultCinemachine,
        ZoomCinemachine
    }
    private CameraMode cameraMode;

    private void Awake()
    {
        main = GetComponentInChildren<MechaBase>();
        cinemachines = GetComponentsInChildren<CinemachineVirtualCamera>();
        foreach(var cinemachine in cinemachines)
        {
            cinemachine.Follow = main.cameraTarget.transform;
        }
        charactercontroller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        cameraMode = CameraMode.DefaultCinemachine;
    }

    private void Start()
    {
        lookAction = playerInput.actions["Look"];
        moveAction = playerInput.actions["Move"];
        playerInput.actions["Jump"].performed += JumpChar;
        playerInput.actions["Zoom"].performed += ChangeMode;
        playerInput.actions["Fire"].performed += Fire;
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            main.Skill1Command = true;
        }
        else
        {
            main.Skill1Command = false;
        }
    }


    private void ChangeMode(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            cameraMode = CameraMode.ZoomCinemachine;
        }
        else
        {
            cameraMode = CameraMode.DefaultCinemachine;
        }

        Debug.Log("Current : " + cameraMode.ToString());

        foreach (var machine in this.cinemachines)
        {
            if (cameraMode.ToString() == machine.Name)
            {
                machine.Priority = 99;
            }
            else
            {
                machine.Priority = 9;
            }
        }
    }

    private void JumpChar(InputAction.CallbackContext obj)
    {
        if (charactercontroller.isGrounded)
        {
            main.playerVelocity.y = main.data.JumpHeight;
        }
    }

    private void Update()
    {
        #region Move
        main.lookValue = lookAction.ReadValue<Vector2>();

        var readedMoveAction = moveAction.ReadValue<Vector2>();

        var moveDirection = new Vector3(readedMoveAction.x, 0, readedMoveAction.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        main.playerVelocity += Physics.gravity * (Time.deltaTime);

        var result = ((moveDirection * main.data.CharSpeed) + main.playerVelocity) * Time.deltaTime;

        charactercontroller.Move(result);
        #endregion
    }
}
