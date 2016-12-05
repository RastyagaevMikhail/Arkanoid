using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class MyNetworkManager : NetworkManager
{
    public InputField inputRemoteServer;

    private static MyNetworkManager instance = null;

    public static MyNetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MyNetworkManager>();
            }
            return instance;
        }
    }


    const int NETWORK_PORT = 4585;
    // сетевой порт
    const int MAX_CONNECTIONS = 20;
    // максимальное количество входящих подключений
    const bool USE_NAT = false;
    // использовать NAT?
    private string remoteServer = "127.0.0.1";
    // адрес сервера (также можно localhost)



    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartServerGame();
        
        if (Input.GetKeyDown(KeyCode.Space))
            ConnectToServer();

        if (Input.GetKeyDown(KeyCode.Backspace))
            ApplicationExit();
        
        
    }

    public void StartServerGame()
    {
        
        NetworkManager.singleton.StartHost();         
    }

    public void StopServerGame()
    {
        NetworkServer.Reset();
        NetworkManager.singleton.StopHost();
    }

    public void ConnectToServer()
    {
        remoteServer = inputRemoteServer.text;   

        NetworkManager.singleton.SetMatchHost(remoteServer, 7777, false);
        NetworkManager.singleton.StartClient();     
    }

    public void ApplicationExit()
    {
        Application.Quit();
    }
}