using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public ABullet targetBullet;
    public long defaultPoolSize;
    private Queue<ABullet> pool;

    private void Awake()
    {
        pool = new Queue<ABullet>();
    }

    private void Start()
    {
        StartCoroutine(CreateBulletPool());
    }

    public ABullet GetBullet()
    {
        ABullet bullet;

        if(pool.Count > 0)
        {
            bullet = pool.Dequeue();
        }
        else
        {
            bullet = CreateBullet();
        }

        bullet.enabled = true;

        return bullet;
    }

    public ABullet GetBullet(Vector3 position, Quaternion rotation)
    {
        ABullet bullet;

        if (pool.Count > 0)
        {
            bullet = pool.Dequeue();
        }
        else
        {
            bullet = CreateBullet();
        }

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;

        bullet.gameObject.SetActive(true);

        return bullet;
    }

    public void ReturnBullet(ABullet bullet)
    {
        var bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.velocity = Vector3.zero;
        bulletRigidBody.angularVelocity = Vector3.zero;

        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }

    private ABullet CreateBullet()
    {
        var newBullet = Instantiate(targetBullet);
        newBullet.owner = this;
        newBullet.gameObject.SetActive(false);

        return newBullet;
    }

    IEnumerator CreateBulletPool()
    {
        while(pool.Count < defaultPoolSize)
        {
            var newBullet = CreateBullet();
            
            pool.Enqueue(newBullet);
            yield return null;
        }
    }
}
