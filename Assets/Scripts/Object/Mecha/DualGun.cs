using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualGun : MechaBase
{
    public GameObject bullet;
    public AudioClip fireSoundClip;
    public AudioSource audioSource;

    public GameObject muzzle;

    public enum DualGunMode
    {
        Normal,
        MachineGun
    }
    public DualGunMode mode = DualGunMode.Normal;

    // Awake is called before Script Instance Load
    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.fireSoundClip;
    }
    // Start is called before the first frame update

    private void Skill1_Fire()
    {
        if(ammoStatus == AmmoStatus.Reloading)
        {
            return;
        }

        if(Ammo <= 0)
        {
            CurrentReloadCoolTime = ReloadCoolTime * ReloadCoolTimeMultiplier;
            ammoStatus = AmmoStatus.Reloading;
            return;
        }

        foreach (Transform skill1Muzzle in muzzle.transform)
        {
            Ammo -= 1;

            var currentMuzzlePosition = skill1Muzzle.transform.position;
            var shot = Instantiate(this.bullet, currentMuzzlePosition, this.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0));
            var bulletBase = shot.GetComponent<BulletBase>();
            bulletBase.Data = new BulletData();
            bulletBase.Data.Damage = 100;

            shot.GetComponent<Rigidbody>().AddForce(this.mechaHead.transform.forward * 20.0f, ForceMode.Impulse);
            this.audioSource.Play();
        }
    }

    private void Skill2_Advanced()
    {
        switch (mode)
        {
            case DualGunMode.Normal:
                mode = DualGunMode.MachineGun;
                break;
            case DualGunMode.MachineGun:
                mode = DualGunMode.Normal;
                break;
        }

        Skill1CurrentCooltime = Skill1Cooltime;
    }

    private void Skill3_Ultimate()
    {

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        switch (mode)
        {
            case DualGunMode.Normal:
                isHold = false;
                break;
            case DualGunMode.MachineGun:
                isHold = true;
                break;
        }

        Skill1CurrentCooltime -= Time.deltaTime;
        if (Skill1CurrentCooltime < 0) Skill1CurrentCooltime = 0;

        Skill2CurrentCooltime -= Time.deltaTime;
        if (Skill2CurrentCooltime < 0) Skill2CurrentCooltime = 0;

        Skill3CurrentCooltime -= Time.deltaTime;
        if (Skill3CurrentCooltime < 0) Skill3CurrentCooltime = 0;

        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        #region Skill Action

        //Skill1
        if (Skill1Command && Skill1CurrentCooltime == 0)
        {
            Skill1_Fire();
            Skill1CurrentCooltime = Skill1Cooltime;
        }

        //Skill2
        if (Skill2Command && Skill2CurrentCooltime == 0)
        {
            this.Skill2_Advanced();
            Skill2CurrentCooltime = Skill2Cooltime;
        }

        //Skill3
        if (Skill3Command && Skill3CurrentCooltime == 0)
        {
            this.Skill3_Ultimate();
            Skill3CurrentCooltime = Skill3Cooltime;
        }


        #endregion
    }
}
