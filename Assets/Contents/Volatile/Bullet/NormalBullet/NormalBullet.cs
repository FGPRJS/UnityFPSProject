using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : ABullet
{
    public AudioClip metalCollisionClip;
    public AudioClip nullCollisionClip;
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        var damaging = other.gameObject.GetComponentInParent<IDamagable>();
        damaging?.Damage(Damage);

        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        var damaging = other.gameObject.GetComponentInParent<IDamagable>();
        damaging?.Damage(Damage);

        if(other.rigidbody)
            other.rigidbody.AddForce(transform.forward * Damage, ForceMode.Impulse);
        
        base.OnCollisionEnter(other);
    }
}
