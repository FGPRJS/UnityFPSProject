using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaBase : MonoBehaviour, IDamagable
{
    private MechaData data;

    public void Damage(long damage)
    {
        data.HP -= damage;
        Debug.Log(data.HP);
    }

    private void Awake()
    {
        this.data = new MechaData();
        this.data.HP = 1000;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
