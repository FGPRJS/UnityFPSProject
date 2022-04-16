using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    public float sensibility = 2.0f;

    [SerializeField]
    private long maxHP;
    [SerializeField]
    private long hp;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private long ammo;
    [SerializeField]
    private long maxAmmo;
    [SerializeField]
    private long totalAmmo;


    public bool isHold = false;
    public bool isStun = false;
    public bool isDown = false;


    public long LookUpLimit;
    public long LookDownLimit;

    public GameObject mechaHead;
    public GameObject cameraTarget;
    public GameObject zoomCameraTarget;

    public GameObject destroyEffect;

    public Vector2 lookValue;

    public float ReloadCoolTime = 2.0f;
    public float CurrentReloadCoolTime;
    public float ReloadCoolTimeMultiplier = 1.0f;


    //Temp
    public Vector3 Velocity = new Vector3(0, 0, 0);

    public bool Skill1Command = false;
    protected float Skill1Cooltime = 0.25f;
    protected float Skill1CurrentCooltime = 0.0f;

    public bool Skill2Command = false;
    protected float Skill2Cooltime = 8f;
    protected float Skill2CurrentCooltime = 0.0f;

    public bool Skill3Command = false;
    protected float Skill3Cooltime = 300.0f;
    protected float Skill3CurrentCooltime = 0.0f;


    public enum AmmoStatus
    {
        Normal,
        Reloading
    }
    public AmmoStatus ammoStatus = AmmoStatus.Normal;



    public void Damage(long damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }

    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        //Temp Reloading
        if (ammoStatus == AmmoStatus.Reloading)
        {
            if (CurrentReloadCoolTime > 0)
            {
                CurrentReloadCoolTime -= Time.deltaTime;
            }
            else
            {
                CurrentReloadCoolTime = 0;
                ammoStatus = AmmoStatus.Normal;
                long availableAmmo = 0;
                if(TotalAmmo < MaxAmmo)
                {
                    availableAmmo = TotalAmmo;
                    TotalAmmo = 0;
                }
                else
                {
                    availableAmmo = MaxAmmo;
                    TotalAmmo -= MaxAmmo;
                }
                Ammo = availableAmmo;
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (HP <= 0)
        {
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        #region Head Rotate
        var rotateVector = mechaHead.transform.eulerAngles + (new Vector3(-lookValue.y, lookValue.x, 0) * sensibility);

        if (rotateVector.x > LookDownLimit && rotateVector.x < 180)
        {
            rotateVector.x = LookDownLimit;
        }
        else if (rotateVector.x > 180 && rotateVector.x < LookUpLimit)
        {
            rotateVector.x = LookUpLimit;
        }

        mechaHead.transform.eulerAngles = rotateVector;
        #endregion
    }



    #region Encapsulate

    public long MaxHP { get => maxHP;
        set
        {
            if(value < 0)
            {
                maxHP = 0;
            }
            else
            {
                maxHP = value;
            }
        }
    }
    public long HP { get => hp; set 
        { 
            if(value > MaxHP)
            {
                hp = MaxHP;
            }
            else if(value < 0)
            {
                hp = 0;
            }
            else
            {
                hp = value;
            }
        }
    }
    public float Speed { get => speed; 
        set
        {
            if (value < 0)
            {
                speed = 0;
            }
            else
            {
                speed = value;
            }
        }
    }
    public float JumpHeight { get => jumpHeight;
        set
        {
            if (value < 0)
            {
                jumpHeight = 0;
            }
            else
            {
                jumpHeight = value;
            }
        }
    }
    public long Ammo { get => ammo;
        set
        {
            if(value > MaxAmmo)
            {
                ammo = MaxAmmo;
            }
            else if (value < 0)
            {
                ammo = 0;
            }
            else
            {
                ammo = value;
            }
        }
    }
    public long MaxAmmo { get => maxAmmo;
        set
        {
            if (value < 0)
            {
                maxAmmo = 0;
            }
            else
            {
                maxAmmo = value;
            }
        }
    }
    public long TotalAmmo { get => totalAmmo;
        set
        {
            if (value < 0)
            {
                totalAmmo = 0;
            }
            else
            {
                totalAmmo = value;
            }
        }
    }
    #endregion
}
