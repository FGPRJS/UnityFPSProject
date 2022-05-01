using System;
using System.Collections;
using System.Collections.Generic;
using Contents.Mechanic;
using UnityEngine;

public class MechaOutletUnderGround : AMecha
{
    public Animator animator;
    public string openAnimationName;
    public string closeAnimationName;

    public Collider currentCollider = null;
    
    private Queue ActionQueue;

    protected override void Awake()
    {
        base.Awake();

        isInvincible = true;
        ActionQueue = new Queue();
        creationStatus = CreationStatus.Closed;
    }

    protected override void Start()
    {
        base.Start();
    }

    public enum CreationStatus
    {
        Opening,
        Opened,
        Closing,
        Closed
    }

    public CreationStatus creationStatus;
    
    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Mecha")) 
            //Except itself
            && (other.gameObject != mechaHead))
            currentCollider = other;
    }

    void OnTriggerExit(Collider other)
    {
        currentCollider = null;
    }

    protected override void Update()
    {
        //Empty
        if (!currentCollider)
        {
            switch (creationStatus)
            {
                case CreationStatus.Closed:
                    creationStatus = CreationStatus.Opening;
                    animator.Play(openAnimationName);
                    break;
                
                case CreationStatus.Closing:
                    if (isAnimationComplete())
                    {
                        creationStatus = CreationStatus.Closed;
                    }
                    break;
                
                case CreationStatus.Opened:
                    skillFire.Activate();
                    break;
                
                case CreationStatus.Opening:
                    if (isAnimationComplete())
                    {
                        creationStatus = CreationStatus.Opened;
                    }
                    break;
            }
        }
        //Not Empty(Something is in)
        else
        {
            switch (creationStatus)
            {
                case CreationStatus.Closed:
                    //Normal. do nothing
                    break;
                
                case CreationStatus.Closing:
                    if (isAnimationComplete())
                    {
                        creationStatus = CreationStatus.Closed;
                    }
                    break;
                
                case CreationStatus.Opened:
                    creationStatus = CreationStatus.Closing;
                    animator.Play(closeAnimationName);
                    break;
                
                case CreationStatus.Opening:
                    if (isAnimationComplete())
                    {
                        creationStatus = CreationStatus.Opened;
                    }
                    break;
            }
        }
    }

    private bool isAnimationComplete()
    {
        return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                && !animator.IsInTransition(0));
    }
}
