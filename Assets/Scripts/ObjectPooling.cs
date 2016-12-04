using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


public class ObjectPooling   {

    List<PoolObject> objects;
    Transform objectsParent;

    public void Initialaze(int count, PoolObject sample, Transform objects_parent)
    {
        objects = new List<PoolObject>();
        objectsParent = objects_parent;
        for (int i = 0; i < count; i++)
        {
            AddObject(sample, objects_parent);
        }

    }
    public PoolObject GetObject()
    {
        
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeInHierarchy ==  false)
            {
                return objects[i];
            }
        }
       // Debug.Log(" ObjectPooling.objects = " + objects.Count);
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1];
    }

    void AddObject(PoolObject sample, Transform objects_parent)
    {
        GameObject temp;
        
        temp = GameObject.Instantiate<GameObject>(sample.gameObject);
//        Debug.Log(temp);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        objects.Add(temp.GetComponent<PoolObject>());
        temp.SetActive(false);
    }

    public void PrintPool()
    {
        Debug.LogFormat("Objects = {0} ", objects);
        foreach (var obj in objects)
        {
            Debug.LogFormat(" Object {0}, Active = {1}",obj, (obj != null ? obj.ToString() : "NO EXISIST"));
        }
    }
    public void ReturnAllPollsObject()
    {
        foreach (var obj in objects)
        {
            obj.ReturnToPool();
        }
    }
}
