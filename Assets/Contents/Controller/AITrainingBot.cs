using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AITrainingBot : MonoBehaviour
{
    public AMecha target;
    private Vector3 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region DestroyCheck
        if(!target)
        {
            Destroy(this.gameObject);
            return;
        }

        #endregion

        #region Move
        target.Move(Vector2.zero);
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
