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

        this.data.CharSpeed = 5.0f;
        this.data.JumpHeight = 10.0f;

        this.bullet = Resources.Load<GameObject>("Prefabs/Volatiles/NormalBullet");
        this.fireSoundClip = Resources.Load<AudioClip>("Sound/SoundFX/FireArm/FireArm01");
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.fireSoundClip;

        this.mechaHead = transform.Find("Head").gameObject;
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
        var newEulers = mechaHead.transform.rotation.eulerAngles + new Vector3(-lookValue.y, lookValue.x, 0);

        if (newEulers.x > 80 && newEulers.x < 180)
        {
            newEulers.x = 80;
        }
        else if (newEulers.x > 180 && newEulers.x < 280)
        {
            newEulers.x = 280;
        }

        mechaHead.transform.rotation = Quaternion.Euler(newEulers);
        #endregion
    }
}
