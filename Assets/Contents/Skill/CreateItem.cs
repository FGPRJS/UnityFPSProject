using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : ASkill
{
    public GameObject instantiateTarget;

    protected override void Action()
    {
        Instantiate(instantiateTarget, transform.position, transform.rotation);
    }
}
