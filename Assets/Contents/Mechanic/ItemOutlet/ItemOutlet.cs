using System.Collections;
using System.Collections.Generic;
using Contents.Mechanic;
using UnityEngine;

public class ItemOutlet : AMecha
{
    private Collider currentCollider = null;
    protected override void Awake()
    {
        base.Awake();

        isInvincible = true;
    }
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
            currentCollider = other;
    }

    void OnTriggerExit(Collider other)
    {
        currentCollider = null;
    }

    protected override void Update()
    {
        if(!currentCollider)
            skillFire.Activate();
    }
}
