using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : ABullet
{
    public GameObject afterBulletCollision;
    private float timeLimit = 10.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        var damaging = other.gameObject.GetComponentInParent<IDamagable>();
        if(damaging != null)
        {
            damaging.Damage(Damage);
        }

        var instance = Instantiate(this.afterBulletCollision, this.transform.position, Quaternion.identity);

        base.OnTriggerEnter(other);
    }

    protected override void Update()
    {
        base.Update();
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
