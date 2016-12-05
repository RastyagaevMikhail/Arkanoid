using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : NetworkBehaviour
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

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("PoolSetup.isSetver = " + isServer);
      
        if (isServer)
            Initalize();
        
    }

    public  void Initalize()
    {
        PoolManager.Initialize(pools);    
    }
}
