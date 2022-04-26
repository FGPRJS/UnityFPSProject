using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : ABullet
{
    private float maxTimeLimit = 10.0f;
    private float timeLimit = 10.0f;

    public AudioClip metalCollisionClip;
    public AudioClip nullCollisionClip;
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        switch (PhysicMaterialManager.ToMaterialType(other.name))
        {
            case PhysicMaterialManager.MaterialType.Metal:
                audioSource.PlayOneShot(metalCollisionClip);
                break;
            
            case PhysicMaterialManager.MaterialType.Null:
                audioSource.PlayOneShot(nullCollisionClip);
                break;
        }
        
        var damaging = other.gameObject.GetComponentInParent<IDamagable>();
        if(damaging != null)
        {
            damaging.Damage(Damage);
        }

        base.OnTriggerEnter(other);
    }

    private void OnEnable()
    {
        timeLimit = maxTimeLimit;
    }

    protected override void Update()
    {
        base.Update();
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            owner.ReturnObject(this);
        }
    }
}
