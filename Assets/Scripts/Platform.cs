using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Platform : Unit2DNetwok
{

    public float HorisontalAxis;

    AppTools app;

    public float speed = 10;

    void Start()
    {
        app = AppTools.Instatnce;
    }

    void FixedUpdate()
    {
        //Debug.LogFormat("isServer = {0}", isServer);
        //Debug.LogFormat("isClient = {0}", isClient);
        if (!isServer)
            return;
        
        HorisontalAxis = Input.GetAxis("Horizontal");

        x += HorisontalAxis * speed * Time.deltaTime;


        if (app.CameraBounds.min.x + MyBounds.extents.x > x + HorisontalAxis)
        {
            x = app.CameraBounds.min.x + MyBounds.extents.x;
        }
        else if (app.CameraBounds.max.x - MyBounds.extents.x < x + HorisontalAxis)
        {
            x = app.CameraBounds.max.x - MyBounds.extents.x;
        }
    }
  
    public ParticleSystem PS = null;
    public void Emit()
    {                
        PS.Emit(5);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isServer)
            return;
       
        if (coll.gameObject.CompareTag("Ball"))
        {
            GameManager.Instatnce.CalculateScore();

            coll.gameObject.GetComponent<Ball>().RpcReset();
            Emit();
        }
    }
}
 