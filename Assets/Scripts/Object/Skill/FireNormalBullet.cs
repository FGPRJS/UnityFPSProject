using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNormalBullet : ASkill
{
    public GameObject Muzzle;
    public GameObject bullet;
    public float FirePower = 20.0f;

    protected override void Action()
    {
        base.Action();

        if(Target.Ammo <= 0)
        {
            Target.Reload();
            return;
        }

        foreach (Transform skill1Muzzle in Muzzle.transform)
        {
            //Remove Ammo
            Target.Ammo -= 1;

            var currentMuzzlePosition = skill1Muzzle.transform.position;
            var shot = Instantiate(this.bullet, currentMuzzlePosition, Target.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0));
            var bulletBase = shot.GetComponent<ABullet>();
            bulletBase.Damage = 100;

            shot.GetComponent<Rigidbody>().AddForce(Target.mechaHead.transform.forward * FirePower, ForceMode.Impulse);
            Target.audioSource.Play();
        }
    }
}
