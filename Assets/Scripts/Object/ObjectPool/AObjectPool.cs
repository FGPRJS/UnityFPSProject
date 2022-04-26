using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AObjectPool<T> : MonoBehaviour
{
    public T target;
    protected Queue<T> pool;
    public long defaultPoolSize;

    protected virtual void Awake()
    {
        pool = new Queue<T>();
    }

    void Start()
    {
        StartCoroutine(FillPool());
    }

    public virtual T GetObject()
    {
        return default(T);
    }

    public virtual T GetObject(Vector3 position, Quaternion rotation)
    {
        return default(T);
    }

    public virtual void ReturnObject(T obj)
    {
        return;
    }


    IEnumerator FillPool()
    {
        while (pool.Count < defaultPoolSize)
        {
            var newInstance = CreateInstance();

            pool.Enqueue(newInstance);
            yield return null;
        }
    }

    protected virtual T CreateInstance()
    {
        return default(T);
    }
}
