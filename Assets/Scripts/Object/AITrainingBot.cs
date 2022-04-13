using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrainingBot : MonoBehaviour
{
    public CharacterController controller;
    private MechaBase main;
    private Vector3 targetPos;

    private void Awake()
    {
        targetPos = new Vector3(0, 0, 0);
        main = GetComponentInChildren<MechaBase>();
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Move

        var moveDirection = (targetPos - main.transform.position).normalized;
        
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        main.playerVelocity += Physics.gravity * (Time.deltaTime);

        var result = ((moveDirection * main.data.CharSpeed) + main.playerVelocity) * Time.deltaTime;

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
