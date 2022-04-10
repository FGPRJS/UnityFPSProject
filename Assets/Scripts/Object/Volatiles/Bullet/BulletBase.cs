using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private BulletData data;
    public BulletData Data { get => data; set => data = value; }

    void Awake()
    {
        this.AfterAwakeFunction();
    }

    protected virtual void AfterAwakeFunction()
    {

    }

    void Start()
    {
        this.AfterStartFunction();
    }

    protected virtual void AfterStartFunction()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        BeforeOnTriggerEnterFunction(other);

        Destroy(this.gameObject);
    }

    protected virtual void BeforeOnTriggerEnterFunction(Collider other)
    {
        
    }

    protected virtual void AfterOnTriggerEnterFunction()
    {

    }

    void Update()
    {
        this.AfterUpdateFunction();
    }

    protected virtual void AfterUpdateFunction()
    {

    }
}
