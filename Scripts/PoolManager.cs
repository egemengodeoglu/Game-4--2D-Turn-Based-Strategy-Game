using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Dictionary<int, Queue<PoolObject>> poolDictionary = new Dictionary<int, Queue<PoolObject>>();

    #region Singleton 
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<PoolManager>();
            return _instance;
        }
    }
    #endregion
    


    public PoolObject CreatePoolObject(PoolObject prefab, int id)
    {
        PoolObject pooledObject = Instantiate(prefab) as PoolObject;
        pooledObject.poolid = id;
        pooledObject.gameObject.SetActive(false);
        pooledObject.OnHideObject += RecyclePoolObject;
        return pooledObject;
    }

    public void CreatePool(PoolObject prefab)
    {
        int id = prefab.GetInstanceID();
        poolDictionary.Add(id,new Queue<PoolObject>());
        poolDictionary[id].Enqueue(CreatePoolObject(prefab, id));
    }

    public PoolObject UsePoolObject(PoolObject poolobject, Vector3 transform,  Quaternion rotation)
    {
        int poolid = poolobject.GetInstanceID();
        if (!poolDictionary.ContainsKey(poolid))
        {
            CreatePool(poolobject);
        }
        if(poolDictionary[poolid].Count == 0)
        {
            poolDictionary[poolid].Enqueue(CreatePoolObject(poolobject, poolid));
        }
        PoolObject pooledObject = poolDictionary[poolid].Dequeue();
        pooledObject.transform.position = transform;
        pooledObject.transform.rotation = rotation;
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    public void RecyclePoolObject(PoolObject pooledObject)
    {
        int poolid = pooledObject.poolid;
        pooledObject.gameObject.SetActive(false);
        poolDictionary[poolid].Enqueue(pooledObject);
    }
}
