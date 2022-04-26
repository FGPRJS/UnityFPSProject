using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaOutletUnderGround : AMecha
{
    public GameObject head;
    public GameObject body;
    
    public Animator animator;
    public string openAnimationName;
    public string closeAnimationName;

    private Collider currentCollider = null;
    
    private Queue ActionQueue;

    protected override void Awake()
    {
        base.Awake();

        invincible = true;
        ActionQueue = new Queue();
        creationStatus = CreationStatus.Closed;
    }

    protected override void Start()
    {
        base.Start();
        
        
    }

    private enum CreationStatus
    {
        Opening,
        Opened,
        Closing,
        Closed
    }

    private CreationStatus creationStatus;
    
    void OnTriggerStay(Collider other)
    {
        currentCollider = other;
    }

    void OnTriggerExit(Collider other)
    {
        currentCollider = null;
    }

    protected override void Update()
    {
        switch (creationStatus)
        {
            case CreationStatus.Closed:

                if (currentCollider)
                {
                    break;
                }
                animator.Play(openAnimationName);
                creationStatus = CreationStatus.Opening;
                
                break;
            
            case CreationStatus.Opening:
                
                if (currentCollider)
                {
                    break;
                }
                
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1
                    && !animator.IsInTransition(0))
                {
                    creationStatus = CreationStatus.Opened;
                }
                
                break;
            
            case CreationStatus.Opened:
                
                Skill_Fire.Activate();
                animator.Play(closeAnimationName);
                creationStatus = CreationStatus.Closing;

                break;
            
            case CreationStatus.Closing:
                
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 
                    && !animator.IsInTransition(0))
                {
                    creationStatus = CreationStatus.Closed;
                }

                break;
        }
    }
}
