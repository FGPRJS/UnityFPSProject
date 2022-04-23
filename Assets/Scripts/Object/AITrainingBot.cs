using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AITrainingBot : MonoBehaviour
{
    public CharacterController controller;
    public AMecha target;
    private Vector3 targetPos;

    private void Awake()
    {
        targetPos = new Vector3(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region DestroyCheck
        if(target == null)
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
    }

    public void JustMoveForward(long distance)
    {
        
    }

    public void SetTargetLoc(Vector3 targetPosition)
    {
        targetPos = targetPosition;
    }
}
