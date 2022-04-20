using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMecha : MonoBehaviour, IDamagable, IAmmo
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


    //TempStatus
    public bool isHold = false;
    public bool isStun = false;
    public bool isDown = false;


    public long LookUpLimit;
    public long LookDownLimit;

    public GameObject mechaHead;
    public GameObject cameraTarget;
    public GameObject zoomCameraTarget;

    public AudioClip fireSoundClip;
    public AudioSource audioSource;
    public GameObject destroyEffect;

    public Vector2 lookValue;

    public ASkill Skill_Reload;
    public ASkill Skill_Fire;
    public ASkill Skill_Advanced;
    public ASkill Skill_Special;
    public ASkill Skill_Ultimate;


    //Temp
    public Vector3 Velocity = new Vector3(0, 0, 0);

    public void Damage(long damage)
    {
        HP -= damage;
        Debug.Log(HP);
    }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.fireSoundClip;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        #region HP Check
        if (HP <= 0)
        {
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        #endregion

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

    public void Reload()
    {
        Skill_Reload.Activate();
    }

    public void Fire()
    {
        Skill_Fire.Activate();
    }

    public void Advance()
    {
        Skill_Advanced.Activate();
    }

    public void Ultimate()
    {
        Skill_Ultimate.Activate();
    }

    public void Special()
    {
        Skill_Special.Activate();
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

    public long Ammo
    {
        get => ammo;
        set
        {
            if (value > MaxAmmo)
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
    public long MaxAmmo
    {
        get => maxAmmo;
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
    public long TotalAmmo
    {
        get => totalAmmo;
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
