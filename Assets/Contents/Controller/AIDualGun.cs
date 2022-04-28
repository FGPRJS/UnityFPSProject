using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDualGun : MonoBehaviour
{
    public AMecha target;
    private Vector3 targetPos;
    
    protected void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }
        
        #region MoveToTargetPosition
        target.Move(Vector2.zero);
        #endregion
        
        //Infinite Ammo
        target.Ammo = 10000;
        
        target.Fire();
    }
}
