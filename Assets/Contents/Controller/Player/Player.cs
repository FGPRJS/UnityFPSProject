using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPossessor
{
    public PlayerModeManager playerModeManager;
    private PlayerModeDefiner inputPlayerModeDefiner;
    private PlayerModeDefiner inputUIModeDefiner;
    
    public string lookInputName;
    public string moveInputName;
    public string cameraZoomInputName;
    public string playerModeInputName;

    public string interactionInputName;
    public string inventoryInputName;
    
    public string skillReloadInputName;
    public string skillFireInputName;
    public string skillAdvancedInputName;
    public string skillUltimateInputName;
    public string skillSpecialInputName;
    public string skillSpecialMoveInputName;

    public PlayerInput playerInput;
    
    private InputAction playerModeAction;
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
        #region Cinemachine and CameraMode
        cinemachines = GetComponentsInChildren<CinemachineVirtualCamera>();

        cameraMode = CameraMode.DefaultCinemachine;
        #endregion
        
        #region PlayerMode
        playerModeManager = new PlayerModeManager
        {
            Mode = PlayerModeManager.PlayerMode.Play
        };
        inputPlayerModeDefiner = new PlayerModeDefiner();
        inputUIModeDefiner = new PlayerModeDefiner();
        playerModeManager.AddPlayModeDefiner(inputPlayerModeDefiner);
        playerModeManager.AddUIModeDefiner(inputUIModeDefiner);
        #endregion

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
        playerModeAction = playerInput.actions[playerModeInputName];
        lookAction = playerInput.actions[lookInputName];
        moveAction = playerInput.actions[moveInputName];
        fireAction = playerInput.actions[skillFireInputName];
        reloadAction = playerInput.actions[skillReloadInputName];
        
        playerInput.actions[inventoryInputName].performed += Inventory;
        playerInput.actions[interactionInputName].performed += Interaction;
        playerInput.actions[skillSpecialMoveInputName].performed += JumpChar;
        playerInput.actions[cameraZoomInputName].performed += ChangeCamera;
        playerInput.actions[skillAdvancedInputName].performed += Advanced;
        playerInput.actions[skillUltimateInputName].performed += Ultimate;
    }

    private void Inventory(InputAction.CallbackContext obj)
    {
        
    }

    private void Interaction(InputAction.CallbackContext obj)
    {
        //Interaction 1st -> Item
        if ((target) && (target.itemNearbyMechaInfo.Count > 0))
        {
            var itemsNearbyMechaInfo = target.itemNearbyMechaInfo.ToArray();
            
            if (!target.PutItem(itemsNearbyMechaInfo[0]))
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

        #region PlayerMode

        var modeValue = playerModeAction.ReadValue<float>();

        if (modeValue == 0)
        {
            inputPlayerModeDefiner.IsActive = true;
            inputUIModeDefiner.IsActive = false;
        }
        else
        {
            inputPlayerModeDefiner.IsActive = false;
            inputUIModeDefiner.IsActive = true;
        }
        
        #endregion
        
        switch (playerModeManager.Mode)
        {
            case PlayerModeManager.PlayerMode.Play:
                PlayModeUpdate();
                break;
            
            case PlayerModeManager.PlayerMode.UI:
                UIModeUpdate();
                break;
        }
    }

    private void PlayModeUpdate()
    {
        #region Rotate

        var lookValue = lookAction.ReadValue<Vector2>();
        
        target.RotateHead(lookValue);

        #endregion
        
        #region Move
        
        var moveValue = moveAction.ReadValue<Vector2>();
        
        var moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        
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

    private void UIModeUpdate()
    {
        
    }

    public void Dispossess()
    {
        target = null;
    }
}
