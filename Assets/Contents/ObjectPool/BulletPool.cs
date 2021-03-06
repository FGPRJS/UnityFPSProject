using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : AObjectPool<ABullet>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override ABullet GetObject()
    {
        ABullet bullet;

        if(pool.Count > 0)
        {
            bullet = pool.Dequeue();
        }
        else
        {
            bullet = CreateInstance();
        }

        bullet.enabled = true;

        return bullet;
    }

    public override ABullet GetObject(Vector3 position, Quaternion rotation)
    {
        ABullet bullet;

        if (pool.Count > 0)
        {
            bullet = pool.Dequeue();
        }
        else
        {
            bullet = CreateInstance();
        }

        var bulletTransform = bullet.transform;
        
        bulletTransform.position = position;
        bulletTransform.rotation = rotation;

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    public override void ReturnObject(ABullet bullet)
    {
        var bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.velocity = Vector3.zero;
        bulletRigidBody.angularVelocity = Vector3.zero;

        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }

    protected override ABullet CreateInstance()
    {
        var newBullet = Instantiate(target);
        newBullet.pool = this;
        newBullet.gameObject.SetActive(false);

        return newBullet;
    }
}
