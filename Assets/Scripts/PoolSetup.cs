using UnityEngine;
using System.Collections;

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : MonoBehaviour
{

    [SerializeField]
    private PoolManager.PoolPart[] pools;

    void OnValidate()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].name = pools[i].prefab.name;

        }
    }

    void Awake()
    {
        Initalize();
    }

    public  void Initalize()
    {
        PoolManager.Initialize(pools);    
    }
}
