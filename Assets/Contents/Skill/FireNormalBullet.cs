using System.Collections;
using System.Collections.Generic;
using Contents.Skill;
using UnityEngine;

public class FireNormalBullet : ASkill
{
    public GameObject muzzle;
    private BulletPool pool;
    public float firePower = 20.0f;

    protected override void Awake()
    {
        base.Awake();
        
        pool = (BulletPool)GameObject.FindObjectOfType(typeof(BulletPool));
    }

    protected override void Action()
    {
        base.Action();

        foreach (Transform muzzle in muzzle.transform)
        {
            //Ammo Check
            if(skillOwner.Ammo <= 0)
            {
                skillOwner.Reload();
                return;
            }
            
            //Remove Ammo
            skillOwner.Ammo -= 1;

            var currentMuzzlePosition = muzzle.transform.position;
            var bulletRotation = skillOwner.mechaHead.transform.rotation * Quaternion.Euler(90, 0, 0);

            var shot = pool.GetObject(
                currentMuzzlePosition, 
                bulletRotation
                );

            var powerDirection = muzzle.transform.forward * firePower;

            shot.GetComponent<Rigidbody>().AddForce(
                powerDirection, 
                ForceMode.Impulse
                );
            skillOwner.PlayFireSound();
        }
    }
}
