using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public string LookInputName;
    public string MoveInputName;
    public string JumpInputName;
    public string CameraZoomInputName;
    public string ReloadInputName;
    public ASkill Reload;
    public string Skill1FireInputName;
    public string Skill2AdvancedInputName;
    public string Skill3UltimateInputName;
    public string Skill4SpecialInputName;

    private PlayerInput playerInput;
    private InputAction lookAction;
    public Vector2 lookValue;
    public InputAction moveAction;
    private CharacterController charactercontroller;
    private InputAction fireAction;
    private InputAction reloadAction;

    private AMecha Target;

    private CinemachineVirtualCamera[] cinemachines;
    private enum CameraMode
    {
        DefaultCinemachine,
        ZoomCinemachine
    }
    private CameraMode cameraMode;

    private void Awake()
    {
        Target = GetComponentInChildren<AMecha>();
        cinemachines = GetComponentsInChildren<CinemachineVirtualCamera>();
        charactercontroller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        cameraMode = CameraMode.DefaultCinemachine;
    }

    private void Start()
    {
        lookAction = playerInput.actions[LookInputName];
        moveAction = playerInput.actions[MoveInputName];
        fireAction = playerInput.actions[Skill1FireInputName];
        reloadAction = playerInput.actions[ReloadInputName];
        playerInput.actions[JumpInputName].performed += JumpChar;
        playerInput.actions[CameraZoomInputName].performed += ChangeCamera;
        
        playerInput.actions[Skill2AdvancedInputName].performed += Advanced;
        playerInput.actions[Skill3UltimateInputName].performed += Ultimate;
    }

    

    private void Ultimate(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValue<bool>();
        if (isPressed)
        {
            Target.Ultimate();
        }
    }

    private void Advanced(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            Target.Advance();
        }
    }


    private void ChangeCamera(InputAction.CallbackContext obj)
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
            Target.Velocity.y = Target.JumpHeight;
        }
    }

    private void Update()
    {
        #region Move

        lookValue = lookAction.ReadValue<Vector2>();

        Target.lookValue = lookValue;

        var readedMoveAction = moveAction.ReadValue<Vector2>();

        var moveDirection = new Vector3(readedMoveAction.x, 0, readedMoveAction.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        Target.Velocity += Physics.gravity * (Time.deltaTime);

        var result = ((moveDirection * Target.Speed) + Target.Velocity) * Time.deltaTime;
        
        //Check Movable
        if (Target.isHold || Target.isStun || Target.isDown)
        {
            //Cannot Move
        }
        else
        {
            charactercontroller.Move(result);
        }

        #endregion


        #region Fire

        var fire = fireAction.ReadValue<float>();

        if(fire > 0)
        {
            Target.Fire();
        }

        #endregion

        #region Reload

        var reload = reloadAction.ReadValue<float>();

        if(reload > 0)
        {
            Target.Reload();
        }

        #endregion
    }
}
