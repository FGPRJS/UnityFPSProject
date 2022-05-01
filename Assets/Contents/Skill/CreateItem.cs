using System.Collections;
using System.Collections.Generic;
using Contents.Item;
using Contents.Skill;
using UnityEngine;

public class CreateItem : ASkill
{
    public AItem instance;

    protected override void Action()
    {
        var item = Instantiate<AItem>(instance, transform.position, transform.rotation);
    }
}
