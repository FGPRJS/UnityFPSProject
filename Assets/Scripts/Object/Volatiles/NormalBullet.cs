using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : FlyingVolatileBase
{
    private GameObject afterBulletCollision;
    private float timeLimit = 10.0f;

    protected override void AfterAwakeFunction()
    {
        afterBulletCollision = Resources.Load<GameObject>("Prefabs/Volatiles/AfterBulletCollision");
    }

    protected override void BeforeOnTriggerEnterFunction()
    {
        var instance = Instantiate(this.afterBulletCollision, this.transform.position, Quaternion.identity);
    }

    protected override void AfterUpdateFunction()
    {
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
