using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    public MechaData data;
    public GameObject mechaHead;
    public GameObject cameraTarget;
    public GameObject zoomCameraTarget;

    public GameObject destroyEffect;

    //Temp
    public bool Skill1Command = false;
    protected float Skill1Cooltime = 0.25f;
    protected float Skill1CurrentCooltime = 0.0f;
    public Vector3 playerVelocity = new Vector3(0,0,0);

    public Vector2 lookValue;

    public void Damage(long damage)
    {
        data.HP -= damage;
        Debug.Log(data.HP);
    }

    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (data.HP <= 0)
        {
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    
}
