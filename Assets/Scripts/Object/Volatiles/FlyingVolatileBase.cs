using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingVolatileBase : MonoBehaviour
{
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
        BeforeOnTriggerEnterFunction();
        
        Destroy(this.gameObject);
    }

    protected virtual void BeforeOnTriggerEnterFunction()
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
