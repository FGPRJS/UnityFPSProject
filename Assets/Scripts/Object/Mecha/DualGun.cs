using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualGun : MechaBase
{
    public float sensibility = 2.0f;

    public GameObject bullet;
    public AudioClip fireSoundClip;
    public AudioSource audioSource;

    public GameObject muzzle;

    public long LookUpLimit;
    public long LookDownLimit;
    

    // Awake is called before Script Instance Load
    protected override void Awake()
    {
        base.Awake();

        #region GameData
        data = new MechaData();
        data.MaxHP = 1000;
        data.HP = 1000;
        data.CharSpeed = 5.0f;
        data.JumpHeight = 10.0f;
        data.TotalAmmo = 3600;
        data.MaxAmmo = 36;
        data.Ammo = data.MaxAmmo;
        #endregion

        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.fireSoundClip;

        this.muzzle = this.mechaHead.transform.Find("Muzzle").gameObject;
    }
    // Start is called before the first frame update

    private void Skill1()
    {
        if(ammoStatus == AmmoStatus.Reloading)
        {
            return;
        }

        if(data.Ammo <= 0)
        {
            CurrentReloadCoolTime = ReloadCoolTime;
            ammoStatus = AmmoStatus.Reloading;
            return;
        }

        foreach (Transform skill1Muzzle in muzzle.transform)
        {
            data.Ammo -= 1;

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
    protected override void Update()
    {
        base.Update();

        #region Skill Action

        if (Skill1Command && Skill1CurrentCooltime == 0)
        {
            Skill1();
            Skill1CurrentCooltime = Skill1Cooltime;
        }

        Skill1CurrentCooltime -= Time.deltaTime;
        if (Skill1CurrentCooltime < 0) Skill1CurrentCooltime = 0;

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
}
