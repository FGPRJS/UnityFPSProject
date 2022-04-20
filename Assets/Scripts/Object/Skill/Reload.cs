using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : ASkill
{
    // Start is called before the first frame update
    protected override void Action()
    {
        long reloadAmount = 0;
        
        if(Target.TotalAmmo < Target.MaxAmmo)
        {
            reloadAmount = Target.TotalAmmo;
            Target.TotalAmmo = 0;
        }
        else
        {
            reloadAmount = Target.MaxAmmo;
            Target.TotalAmmo -= Target.MaxAmmo;
        }

        Target.Ammo = reloadAmount;
    }
}
