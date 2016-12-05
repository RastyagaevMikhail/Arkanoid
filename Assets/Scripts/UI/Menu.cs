using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public Button StartButton;
    public Button ConnectButton;
    public Button ExitButton;

    public InputField RemoteServerField;

	void Awake () {

        StartButton.onClick.AddListener(MyNetworkManager.Instance.StartServerGame  as UnityAction);
        ConnectButton.onClick.AddListener(MyNetworkManager.Instance.ConnectToServer as UnityAction);
        ExitButton.onClick.AddListener(MyNetworkManager.Instance.ApplicationExit as UnityAction);
        MyNetworkManager.Instance.inputRemoteServer = RemoteServerField;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
