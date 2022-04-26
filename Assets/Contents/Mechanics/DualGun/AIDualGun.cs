using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDualGun : MonoBehaviour
{
    public CharacterController controller;
    public AMecha target;
    private Vector3 targetPos;
    
    void Start()
    {
        targetPos = target.transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        #region DestroyCheck
        if(!target)
        {
            Destroy(this.gameObject);
            return;
        }

        #endregion
        
        
        #region Move

        var moveDirection = (targetPos - target.transform.position).normalized;
        
        moveDirection = transform.TransformDirection(moveDirection);
        target.Velocity += Physics.gravity * (Time.deltaTime);

        var result = ((moveDirection * target.Speed) + target.Velocity) * Time.deltaTime;

        controller.Move(result);
        #endregion
        
        //Infinite Ammo
        target.Ammo = 10000;
        
        target.Fire();
    }
}
