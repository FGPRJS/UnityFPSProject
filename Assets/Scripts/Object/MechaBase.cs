using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    protected MechaData data;
    protected GameObject mechaHead;

    public void Damage(long damage)
    {
        data.HP -= damage;
        Debug.Log(data.HP);
    }

    protected virtual void Awake()
    {
        this.data = new MechaData();
        this.data.HP = 1000;
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
