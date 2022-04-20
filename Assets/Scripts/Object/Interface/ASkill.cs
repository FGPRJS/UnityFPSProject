using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASkill : MonoBehaviour
{
    public AMecha Target;

    [SerializeField]
    private float casttime;
    [SerializeField]
    private float currentCasttime;

    [SerializeField]
    private float cooltime;
    [SerializeField]
    private float currentCooltime;

    private Queue CastQueue;

    public void Awake()
    {
        CastQueue = new Queue();
    }

    public void Activate() 
    {
        if (CurrentCooltime == 0) 
        {
            CurrentCooltime = Cooltime;

            //Skill Casting
            if(CurrentCasttime == 0)
            {
                CurrentCasttime = Casttime;
                if (CastQueue.Count <= 0) CastQueue.Enqueue(new object());
            }
        }
    }

    protected virtual void Action()
    {

    }


    public virtual void FixedUpdate()
    {
        CurrentCooltime -= Time.deltaTime;
        if(CurrentCooltime <= 0)
        {
            CurrentCooltime = 0;
        }
    }

    public virtual void Update()
    {
        CurrentCasttime -= Time.deltaTime;
        if((CurrentCasttime <= 0) && (CastQueue.Count > 0))
        {
            CastQueue.Dequeue();
            Action();
        }
    }




    #region Encapsulation
    public float Casttime { get => casttime;
        set 
        {
            if(value < 0)
            {
                casttime = 0;
            }
            else
            {
                casttime = value;
            }
        }
    }
    public float CurrentCasttime { get => currentCasttime; 
        set
        {
            if (value < 0)
            {
                currentCasttime = 0;
            }
            else
            {
                currentCasttime = value;
            }
        }
    }
    public float Cooltime { get => cooltime; 
        set
        {
            if (value < 0)
            {
                cooltime = 0;
            }
            else
            {
                cooltime = value;
            }
        }
    }
    public float CurrentCooltime { get => currentCooltime;
        set
        {
            if (value < 0)
            {
                currentCooltime = 0;
            }
            else
            {
                currentCooltime = value;
            }
        }
    }

    #endregion
}
