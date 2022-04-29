using Cinemachine;
using Contents.Mechanic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Contents.Controller.Player
{
    public class Player : MonoBehaviour, IPossessor
    {
        public enum PlayerInputMode
        {
            PlayMode,
            UIMode
        }

        public PlayerInputMode playerInputMode;
        public PlayerInput playerInput;

        public string lookInputName;
        public string moveInputName;
        public string cameraZoomInputName;
        public string inputModeInputName;
        
        
        public string interactionInputName;
        public string inventoryInputName;
    
        public string skillReloadInputName;
        public string skillFireInputName;
        public string skillAdvancedInputName;
        public string skillUltimateInputName;
        public string skillSpecialInputName;
        public string skillSpecialMoveInputName;


        private InputAction inputAction;
        private InputAction lookAction;
        private InputAction moveAction;
        private InputAction fireAction;
        private InputAction reloadAction;
    
        public AMecha target;
        public UnityEvent<PlayerEventArgs> playerChatMessageUpdatedEvent;

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
            playerInputMode = PlayerInputMode.PlayMode;
            #endregion
        
            #region PlayerEvent

            playerChatMessageUpdatedEvent = new UnityEvent<PlayerEventArgs>();

            #endregion
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
            inputAction = playerInput.actions[inputModeInputName];
            
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
                    var newMessage = new PlayerEventArgs
                    {
                        message = "INVENTORY FULL"
                    };
                    playerChatMessageUpdatedEvent.Invoke(newMessage);
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

            #region Input

            var inputValue = inputAction.ReadValue<float>();
            if (inputValue > 0)
            {
                playerInputMode = PlayerInputMode.UIMode;
            }
            else
            {
                playerInputMode = PlayerInputMode.PlayMode;
            }
            #endregion
            
            switch (playerInputMode)
            {
                case PlayerInputMode.PlayMode:
                    PlayModeUpdate();
                    break;
                
                case PlayerInputMode.UIMode:
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
}
