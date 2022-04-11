using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaData
{
    private long maxHP;
    private long hp;
    private float charSpeed;
    private float jumpHeight;

    public long MaxHP { get => maxHP; set
        {
            if (value < 0)
            {
                maxHP = 0;
            }
            else
            {
                maxHP = value;
            }
        }
    }
    public long HP { get => hp; set 
        { 
            if(value < 0)
            {
                hp = 0;
            }
            else if(value > MaxHP)
            {
                hp = MaxHP;
            }
            else
            {
                hp = value;
            }
        } 
    }
    public float CharSpeed { get => charSpeed; set
        {
            if (value < 0)
            {
                charSpeed = 0;
            }
            else
            {
                charSpeed = value;
            }
        }
    }
    public float JumpHeight { get => jumpHeight; set
        {
            if (value < 0)
            {
                jumpHeight = 0;
            }
            else
            {
                jumpHeight = value;
            }
        }
    }
}
