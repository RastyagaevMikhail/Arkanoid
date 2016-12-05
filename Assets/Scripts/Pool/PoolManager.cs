using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


public static class PoolManager
{

    private static PoolPart[] pools;
    private static GameObject objectsParent;

    [System.Serializable]
    public struct PoolPart
    {
        public string name;
        public PoolObject prefab;
        public int count;
        public ObjectPooling ferula;

        public PoolPart(string name, PoolObject prefab, int count, ObjectPooling ferula)
        {
            this.name = name;
            this.prefab = prefab;
            this.count = count;
            this.ferula = ferula;
        }
    }

    public static void Initialize(PoolPart[] newPools)
    {
        Debug.Log("Initialize Pool");
        pools = newPools;
        objectsParent = new GameObject();
        objectsParent.name = "Pool";
        objectsParent.AddComponent<NetworkIdentity>();

        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab != null)
            {
                pools[i].ferula = new ObjectPooling();
                pools[i].ferula.Initialaze(pools[i].count, pools[i].prefab, objectsParent.transform);
            }
        }
    }

    public static GameObject GetObject(string name, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        Debug.Log(" pools = " + pools );

        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (string.Compare(pools[i].name, name) == 0)
                {
                    
                    result = pools[i].ferula.GetObject().gameObject;
                    result.transform.position = position;
                    result.transform.rotation = rotation;
                    result.SetActive(true);
                    //print
                    //pools[i].ferula.PrintPool();
                    return result;
                }
            }
        }
        Debug.Log("result GetObject = " + result);
        return result;
    }

    public static void Print(string NameOfPool)
    {
        foreach (var pool in pools)
        {
            if (pool.name == NameOfPool)
            {
                pool.ferula.PrintPool();
                return;
            }
            
        }
    }

    public static void ReturnAllPollsObject()
    {
        foreach (var pool in pools)
        {
            pool.ferula.ReturnAllPollsObject();
        }
    }
}
