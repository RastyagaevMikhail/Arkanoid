using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour
{

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
    [SyncVar]
    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = string.Format("Score {0}", score);
        }
    }

    public Text debug;
   
    public BallSpawner spawner;

 
    [RPC]
    public void RpcGameOver()
    {
        Score = 0;

        spawner.Reset();
        PoolManager.ReturnAllPollsObject();
    }
    public void CalculateScore()
    {
        Score++;
    }

    public  void DebugLog(string Log)
    {
        debug.text = Log;
    }
}