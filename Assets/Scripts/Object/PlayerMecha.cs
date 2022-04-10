using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMecha : MechaBase
{
    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction moveAction;
    private CharacterController charactercontroller;

    private GameObject mechaHead;

    public float sensibility = 2.0f;

    private Vector3 playerVelocity;
    private float charSpeed = 2.0f;
    private float jumpHeight = 3.0f;

    private CinemachineVirtualCamera[] cinemachines;



    private GameObject bullet;
    private AudioClip fireSoundClip;
    private AudioSource audioSource;

    private GameObject skill1Object;

    private bool Skill1Command = false;
    private float Skill1Cooltime = 0.25f;
    private float Skill1CurrentCooltime = 0.0f;

    private enum CameraMode
    {
        DefaultCinemachine,
        ZoomCinemachine
    }
    private CameraMode cameraMode;

    // Awake is called before Script Instance Load
    private void Awake()
    {
        this.bullet = Resources.Load<GameObject>("Prefabs/Volatiles/NormalBullet");
        this.fireSoundClip = Resources.Load<AudioClip>("Sound/SoundFX/FireArm/FireArm01");
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.fireSoundClip;
        this.playerInput = GetComponent<PlayerInput>();
        this.charactercontroller = GetComponent<CharacterController>();
        this.mechaHead = transform.Find("MechaTurret").gameObject;
        this.cinemachines = GetComponentsInChildren<CinemachineVirtualCamera>();

        cameraMode = CameraMode.DefaultCinemachine;

        var item = this.mechaHead.transform.Find("Skill1");
        this.skill1Object = item.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        moveAction = playerInput.actions["Move"];
        playerInput.actions["Jump"].performed += JumpChar;
        lookAction = playerInput.actions["Look"];
        playerInput.actions["Zoom"].performed += ChangeMode;
        playerInput.actions["Fire"].performed += Fire;
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        var isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            Skill1Command = true;
        }
        else
        {
            Skill1Command = false;
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
            if(cameraMode.ToString() == machine.Name)
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
        if (this.charactercontroller.isGrounded)
        {
            playerVelocity.y = jumpHeight;
        }
    }

    private void Skill1()
    {
        foreach (Transform skill1Muzzle in this.skill1Object.transform)
        {
            var currentMuzzlePosition = skill1Muzzle.transform.position;
            var shot = Instantiate(this.bullet, currentMuzzlePosition, this.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0));
            var bulletBase = shot.GetComponent<BulletBase>();
            bulletBase.Data = new BulletData();
            bulletBase.Data.Damage = 100;
            
            shot.GetComponent<Rigidbody>().AddForce(this.mechaHead.transform.forward * 20.0f, ForceMode.Impulse);
            this.audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Skill Action

        if (Skill1Command && Skill1CurrentCooltime == 0)
        {
            Skill1();
            Skill1CurrentCooltime = Skill1Cooltime;
        }

        Skill1CurrentCooltime -= Time.deltaTime;
        if (Skill1CurrentCooltime < 0) Skill1CurrentCooltime = 0;

        #endregion


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
        playerVelocity += Physics.gravity * (Time.deltaTime);

        var result = ((moveDirection * charSpeed) + playerVelocity) * Time.deltaTime;

        charactercontroller.Move(result);
        #endregion
    }
}
