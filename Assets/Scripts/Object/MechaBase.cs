using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    public long MaxHP;
    public long HP;
    public float CharSpeed;
    public float JumpHeight;

    public long Ammo;
    public long MaxAmmo;
    public long TotalAmmo;

    public GameObject mechaHead;
    public GameObject cameraTarget;
    public GameObject zoomCameraTarget;

    public GameObject destroyEffect;

    //Temp
    public bool Skill1Command = false;
    protected float Skill1Cooltime = 0.25f;
    protected float Skill1CurrentCooltime = 0.0f;
    public Vector3 playerVelocity = new Vector3(0,0,0);

    public Vector2 lookValue;
    
    public float ReloadCoolTime = 2.0f;
    public float CurrentReloadCoolTime;
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
                TotalAmmo -= MaxAmmo;
                Ammo = MaxAmmo;
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
    }
}
