using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPossessor
{
    public string lookInputName;
    public string moveInputName;
    public string cameraZoomInputName;

    public string interactionInputName;
    
    public string skillReloadInputName;
    public string skillFireInputName;
    public string skillAdvancedInputName;
    public string skillUltimateInputName;
    public string skillSpecialInputName;
    public string skillSpecialMoveInputName;

    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction reloadAction;
    
    public AMecha target;
    public Queue<string> playerMessage;
    public UnityEvent playerMessageUpdated;

    public Camera mainCamera;
    private CinemachineVirtualCamera[] cinemachines;
    
    
    
    private enum CameraMode
    {
        DefaultCinemachine,
        ZoomCinemachine
    }
    private CameraMode cameraMode;

    private void Awake()
    {
        cinemachines = GetComponentsInChildren<CinemachineVirtualCamera>();
        playerInput = GetComponent<PlayerInput>();

        cameraMode = CameraMode.DefaultCinemachine;

        playerMessage = new Queue<string>();
    }

    private void Start()
    {
        //possess if inspector assigned
        if (target)
        {
            target.Possess(this);   
        }
        
        //key input
        lookAction = playerInput.actions[lookInputName];
        moveAction = playerInput.actions[moveInputName];
        fireAction = playerInput.actions[skillFireInputName];
        reloadAction = playerInput.actions[skillReloadInputName];
        
        playerInput.actions[interactionInputName].performed += Interaction;
        playerInput.actions[skillSpecialMoveInputName].performed += JumpChar;
        playerInput.actions[cameraZoomInputName].performed += ChangeCamera;
        playerInput.actions[skillAdvancedInputName].performed += Advanced;
        playerInput.actions[skillUltimateInputName].performed += Ultimate;
    }

    private void Interaction(InputAction.CallbackContext obj)
    {
        //Interaction 1st -> Item
        if ((target) && (target.itemNearbyMechaInfo.Count > 0))
        {
            if (!target.PutItem(target.itemNearbyMechaInfo[0]))
            {
                playerMessage.Enqueue("NO EMPTY SPACES IN INVENTORY");
                playerMessageUpdated.Invoke();
            }
            return;
        }
        //Interaction 2nd -> other object(Door or something)
        else
        {
            
        }
            
    }


    private void Ultimate(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValue<bool>();
        if (isPressed)
        {
            target.Ultimate();
        }
    }

    private void Advanced(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            target.Advance();
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
        target.SpecialMove();
    }

    private void Update()
    {
        if (!target) return;
        
        #region Rotate

        var lookValue = lookAction.ReadValue<Vector2>();
        
        target.RotateHead(lookValue);

        #endregion
        
        #region Move
        
        var moveValue = moveAction.ReadValue<Vector2>();
        
        var moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        
        target.Move(moveDirection);

        #endregion
        
        #region Fire

        var fire = fireAction.ReadValue<float>();

        if(fire > 0)
        {
            target.Fire();
        }

        #endregion

        #region Reload

        var reload = reloadAction.ReadValue<float>();

        if(reload > 0)
        {
            target.Reload();
        }

        #endregion
    }

    public void Dispossess()
    {
        target = null;
    }
}
