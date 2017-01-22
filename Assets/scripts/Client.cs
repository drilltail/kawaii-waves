using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Client : NetworkBehaviour {
    static public Client singleton;

    NetworkClient client;
	public NetworkManager manager;

    void Awake()
    {
        singleton = this;
    }

	void Start () {
		client = manager.StartClient();
        //client.RegisterHandler((short)CustomMessages.ClientReceivePlayerID, OnMsgClientReceivePlayerID);
	}
	
	void Update () {
		print (client.isConnected);
	}

    void OnConnectedToServer() {
        print("just connected to server");
    }
}
