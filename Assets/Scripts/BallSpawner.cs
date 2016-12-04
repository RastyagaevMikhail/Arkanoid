using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BallSpawner : NetworkBehaviour
{

    public Rigidbody2D RigidBodyBallPrefab = null;

    public float spawnratedown = 0.1f;
    public float spawnRate = 1.5f;
    public float time = 0;

    public int speedSpeedUp = 10;
    public float rateSpeedUp = 30f;
    public float timeSpeedUp = 0;

    public int SpeedBall = 300;
   // public Platform platform;

    // Use this for initialization
    void Start()
    {
        Reset();
    }


    public void Reset()
    {
        spawnratedown = 0.1f;
        spawnRate = 1.5f;
        time = 0;

        speedSpeedUp = 10;
        rateSpeedUp = 30f;
        timeSpeedUp = 0;

        SpeedBall = 300;
    }

    Vector3 RandomPosition
    {
        get
        {
            return Vector3.up * 17 + Vector3.right * Random.Range(AppTools.Instatnce.CameraBounds.min.x, AppTools.Instatnce.CameraBounds.max.x);
        }
    }
    bool severReady = false;

    public override void OnStartServer()
    {
        severReady = true;
    }
  
  
    // Update is called once per frame
    void Update()
    {
        if (isServer && severReady)
            Spawn();     
    }

   // [Command]
    void Spawn()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
           
            Rigidbody2D rgb2DBall;
            GameObject tmp = PoolManager.GetObject("Ball", RandomPosition, Quaternion.identity);
            Debug.Log("Get Object" + tmp);
            rgb2DBall = tmp.GetComponent<Rigidbody2D>();

            rgb2DBall.AddForce(Vector2.down * SpeedBall);
            int randDir = Random.Range(-1, 2) > 0 ? 1 : -1;
            rgb2DBall.AddForce(Vector2.right * 1000 * randDir);
            rgb2DBall.angularVelocity = (50 * -randDir);
            time = spawnRate;

            NetworkServer.Spawn(tmp);

        }

        timeSpeedUp -= Time.deltaTime;
        if (timeSpeedUp <= 0)
        {
            SpeedBall += speedSpeedUp;
            timeSpeedUp = rateSpeedUp;
            spawnRate -= spawnratedown;
        }
    }
}
