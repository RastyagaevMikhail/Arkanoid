using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour
{
    [SyncVar(hook="OnCatch")]
    public int myScore = 0;

    void OnCatch(int newScore)
    {
        Score = newScore;
    }

    private static GameManager _instance = null;

    public static GameManager Instatnce
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    public bool Spawn = false;

    public Text ScoreText;

    public int Score
    {
        get { return myScore; }
        set
        {
            myScore = value;
            ScoreText.text = string.Format("Score {0}", myScore);
        }
    }

    public Text debug;
   
    public BallSpawner spawner;

 
    [RPC]
    public void RpcGameOver()
    {
        if (isServer)
        {
            Score = 0;
            spawner.Reset();
            PoolManager.ReturnAllPollsObject();    
            RpcOnClientGameOver();
            NetworkServer.DisconnectAll();
            MyNetworkManager.Instance.StopServerGame();
          //  SceneManager.LoadScene("Menu");
        }
    }

    
    [ClientRpc]
    void RpcOnClientGameOver()
    {
        Debug.Log("For Client in GameManager");
        foreach (var obj in GameObject.FindGameObjectsWithTag("Ball"))
        {
            obj.GetComponent<PoolObject>().ReturnToPool();   
        }        
    }

    public void CalculateScore()
    {
        Score++;
    }

    public  void DebugLog(string Log)
    {
        debug.text += Log + "\n";
        Debug.Log(Log);
    }
}