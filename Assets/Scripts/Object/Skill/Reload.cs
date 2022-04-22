using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : ASkill
{
    // Start is called before the first frame update
    protected override void Action()
    {
        long reloadAmount = Target.MaxAmmo - Target.Ammo;
        
        if(Target.TotalAmmo < reloadAmount)
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
