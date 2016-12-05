using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Ball : Unit2DNetwok
{	


    public PoolObject pObj = null;


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bottom"))
        {

            RpcReset();
            GameManager.Instatnce.RpcGameOver();
        }
        else
        {
            MyRigidbody2D.angularVelocity = -MyRigidbody2D.angularVelocity; 
        }
    }
    [RPC]
    public void RpcReset()
    {

        MyRigidbody2D.Sleep();
        pObj.ReturnToPool();

    }
}
