using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : ASkill
{
    public float power;
    
    protected override void Action()
    {
        if(skillOwner.characterController.isGrounded) 
            skillOwner.MoveUpward(power);
    }
}
