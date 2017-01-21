using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Client : NetworkBehaviour {
	NetworkClient client;
	public NetworkManager manager;

	// Use this for initialization
	void Start () {
		client = manager.StartClient ();
	}
	
	void Update () {
		print (client.isConnected);
	}

    void OnConnectedToServer() {
        print("just connected to server");
    }
}
