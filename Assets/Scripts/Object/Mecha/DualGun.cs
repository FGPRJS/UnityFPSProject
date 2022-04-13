using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualGun : MechaBase
{
    public float sensibility = 2.0f;

    private GameObject bullet;
    private AudioClip fireSoundClip;
    private AudioSource audioSource;

    private GameObject muzzle;
    

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
        #endregion



        this.bullet = Resources.Load<GameObject>("Prefabs/Volatiles/NormalBullet");
        this.fireSoundClip = Resources.Load<AudioClip>("Sound/SoundFX/FireArm/FireArm01");
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.fireSoundClip;

        this.muzzle = this.mechaHead.transform.Find("Muzzle").gameObject;
    }
    // Start is called before the first frame update

    private void Skill1()
    {
        foreach (Transform skill1Muzzle in muzzle.transform)
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

        #region Camera Rotate
        var cameraEulers = cameraTarget.transform.rotation.eulerAngles + new Vector3(-lookValue.y, lookValue.x, 0);

        if (cameraEulers.x > 80 && cameraEulers.x < 180)
        {
            cameraEulers.x = 80;
        }
        else if (cameraEulers.x > 180 && cameraEulers.x < 280)
        {
            cameraEulers.x = 280;
        }

        cameraTarget.transform.rotation = Quaternion.Euler(cameraEulers);
        #endregion

        #region Head Rotate
        var headEulers = cameraTarget.transform.rotation.eulerAngles;

        mechaHead.transform.rotation = Quaternion.Euler(headEulers);
        #endregion
    }
}
