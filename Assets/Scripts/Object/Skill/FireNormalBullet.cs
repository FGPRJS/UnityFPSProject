using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNormalBullet : ASkill
{
    public GameObject Muzzle;
    public BulletPool Pool;
    public float FirePower = 20.0f;

    protected override void Action()
    {
        base.Action();

        if(skillOwner.Ammo <= 0)
        {
            skillOwner.Reload();
            return;
        }

        foreach (Transform muzzle in Muzzle.transform)
        {
            //Remove Ammo
            skillOwner.Ammo -= 1;

            var currentMuzzlePosition = muzzle.transform.position;
            var bulletRotation = skillOwner.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0);

            var shot = Pool.GetObject(
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
            skillOwner.PlayFireSound();
        }
    }
}
