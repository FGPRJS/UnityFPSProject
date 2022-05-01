using System.Collections;
using System.Collections.Generic;
using Contents.Skill;
using UnityEngine;

public class ChangeMechaMode : ASkill
{
    public enum MechaMode
    {
        Normal,
        Special
    }
    public MechaMode mode = MechaMode.Normal;

    public ASkill FireSkill;
    public ASkill ReloadSkill;
    public Animator Animation;

    protected override void Action()
    {
        switch (mode)
        {
            case MechaMode.Normal:

                mode = MechaMode.Special;
                Animation.Play("ToSpecial");

                //Change Target Status
                skillOwner.isHold = true;

                //Change Cooltime
                FireSkill.Cooltime = 0.1f;
                FireSkill.CurrentCooltime = FireSkill.Cooltime;

                ReloadSkill.Casttime = 0.1f;
                ReloadSkill.CurrentCasttime = ReloadSkill.Casttime;

                break;

            case MechaMode.Special:

                mode = MechaMode.Normal;
                Animation.Play("ToNormal");

                //Change Target Status
                skillOwner.isHold = false;

                //Change Cooltime
                FireSkill.Cooltime = 0.5f;
                FireSkill.CurrentCooltime = FireSkill.Cooltime;

                ReloadSkill.Casttime = 2.0f;
                ReloadSkill.CurrentCasttime = ReloadSkill.Casttime;

                break;
        }
    }
}
