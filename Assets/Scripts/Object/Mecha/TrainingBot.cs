using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBot : MechaBase
{
    protected override void Awake()
    {
        base.Awake();

        #region GameData
        data = new MechaData();
        data.MaxHP = 1000;
        data.HP = 1000;
        data.CharSpeed = 5.0f;
        data.JumpHeight = 10.0f;
        #endregion
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        #region Camera Rotate
        var cameraEulers = cameraTarget.transform.rotation.eulerAngles + new Vector3(-lookValue.y, lookValue.x, 0);

        if (cameraEulers.x > 80 && cameraEulers.x < 180)
        {
            cameraEulers.x = 80;
        }
        else if (cameraEulers.x > 180 && cameraEulers.x < 280)
        {
            cameraEulers.x = 280;
        }

        cameraTarget.transform.rotation = Quaternion.Euler(cameraEulers);
        #endregion

        #region Head Rotate
        var headEulers = cameraTarget.transform.rotation.eulerAngles;

        headEulers.x = 0;

        mechaHead.transform.rotation = Quaternion.Euler(headEulers);
        #endregion
    }
}
