using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : ASkill
{
    // Start is called before the first frame update
    protected override void Action()
    {
        long reloadAmount = skillOwner.MaxAmmo - skillOwner.Ammo;
        
        if(skillOwner.TotalAmmo < reloadAmount)
        {
            reloadAmount = skillOwner.TotalAmmo;
            skillOwner.TotalAmmo = 0;
        }
        else
        {
            reloadAmount = skillOwner.MaxAmmo;
            skillOwner.TotalAmmo -= skillOwner.MaxAmmo;
        }

        skillOwner.Ammo = reloadAmount;
    }
}
