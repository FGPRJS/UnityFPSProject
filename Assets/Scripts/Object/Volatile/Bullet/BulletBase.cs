using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private BulletData data;
    public BulletData Data { get => data; set => data = value; }
    private GameObject prefab;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }


    protected virtual void Update()
    {
        
    }

    public void SetGameObject(GameObject prefab)
    {
        this.prefab = prefab;
    }
}
