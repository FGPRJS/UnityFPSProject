using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : MonoBehaviour
{
    public AObjectPool<ABullet> pool;
    public long Damage;

    public float maxTimeLimit = 10.0f;
    public float timeLimit = 10.0f;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        pool.ReturnObject(this);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        pool.ReturnObject(this);
    }

    private void OnEnable()
    {
        timeLimit = maxTimeLimit;
    }

    protected virtual void Update()
    {
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            pool.ReturnObject(this);
        }
    }
}
