using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMecha : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction moveAction;
    private CharacterController charactercontroller;


    public GameObject mechaHead;

    public float sensibility = 2.0f;

    private Vector3 playerVelocity;
    private float charSpeed = 2.0f;
    private float jumpHeight = 3.0f;
    private float gravity = 9.81f;

    // Awake is called before Script Instance Load
    private void Awake()
    {
        this.playerInput = GetComponent<PlayerInput>();
        this.charactercontroller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveAction = playerInput.actions["Move"];
        playerInput.actions["Jump"].performed += JumpChar;
        lookAction = playerInput.actions["Look"];
    }

    private void JumpChar(InputAction.CallbackContext obj)
    {
        if (this.charactercontroller.isGrounded)
        {
            playerVelocity.y = jumpHeight;
        }
    }


    // Update is called once per frame
    void Update()
    {
        #region Head Movement

        var readedLookAction = lookAction.ReadValue<Vector2>();

        var newEulers = mechaHead.transform.rotation.eulerAngles + new Vector3(-readedLookAction.y, readedLookAction.x, 0);
        
        if(newEulers.x > 80 && newEulers.x < 180)
        {
            newEulers.x = 80;
        }
        else if (newEulers.x > 180 && newEulers.x < 280)
        {
            newEulers.x = 280;
        }

        mechaHead.transform.rotation = Quaternion.Euler(newEulers);
        
        #endregion

        #region Body Movement

        var readedMoveAction = moveAction.ReadValue<Vector2>();

        var moveDirection = new Vector3(readedMoveAction.x, 0, readedMoveAction.y);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        playerVelocity.y -= gravity * Time.deltaTime;

        var result = ((moveDirection.normalized * charSpeed) + playerVelocity) * Time.deltaTime;

        charactercontroller.Move(result);
        #endregion
    }
}
