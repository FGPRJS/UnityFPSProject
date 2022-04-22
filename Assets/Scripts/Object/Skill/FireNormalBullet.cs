using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNormalBullet : ASkill
{
    public GameObject Muzzle;
    public BulletObjectPool Pool;
    public float FirePower = 20.0f;

    protected override void Action()
    {
        base.Action();

        if(Target.Ammo <= 0)
        {
            Target.Reload();
            return;
        }

        foreach (Transform muzzle in Muzzle.transform)
        {
            //Remove Ammo
            Target.Ammo -= 1;

            var currentMuzzlePosition = muzzle.transform.position;
            var bulletRotation = Target.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0);

            var shot = Pool.GetBullet(
                currentMuzzlePosition, 
                bulletRotation
                );
            
            var bulletBase = shot.GetComponent<ABullet>();
            bulletBase.Damage = 1;

            var powerDirection = muzzle.transform.forward * FirePower;

            shot.GetComponent<Rigidbody>().AddForce(
                powerDirection, 
                ForceMode.Impulse
                );
            Target.audioSource.Play();
        }
    }
}
