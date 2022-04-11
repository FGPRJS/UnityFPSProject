using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    public MechaData data;
    public GameObject mechaHead;

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
        data = new MechaData();
        data.HP = 1000;

        mechaHead = transform.Find("Head").gameObject;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    
}
