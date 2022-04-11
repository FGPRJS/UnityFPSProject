using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBot : MechaBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Awake();

        this.data.CharSpeed = 5.0f;
        this.data.JumpHeight = 10.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        #region Head Rotate
        var newEulers = mechaHead.transform.rotation.eulerAngles + new Vector3(-lookValue.y, lookValue.x, 0);

        if (newEulers.x > 80 && newEulers.x < 180)
        {
            newEulers.x = 80;
        }
        else if (newEulers.x > 180 && newEulers.x < 280)
        {
            newEulers.x = 280;
        }

        mechaHead.transform.rotation = Quaternion.Euler(newEulers);
        #endregion
    }
}
