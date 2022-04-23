using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : MonoBehaviour
{
    public AObjectPool<ABullet> owner;
    public long Damage;

    private GameObject prefab;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        owner.ReturnObject(this);
    }


    protected virtual void Update()
    {
        
    }

    public void SetGameObject(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
