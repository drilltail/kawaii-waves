using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Server : MonoBehaviour {

	public enum Team {
		Left, Right, Undeclared
	}

	public NetworkManager manager;

	public GameObject LeftSpawn;
	public GameObject RightSpawn;

	List<NetworkConnection> connections = new List<NetworkConnection>();
	public List<Team> teams = new List<Team> ();

	// Use this for initialization
	void Start () {
		print("StartServer: " + manager.StartServer());
		NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
		NetworkServer.RegisterHandler (MsgType.Highest + 1, OnLeftTeamDecide);
		NetworkServer.RegisterHandler (MsgType.Highest + 2, OnRightTeamDecide);
	}
	
	// Update is called once per frame
	void Update () {
        if(!LevelState.singleton.gameActive && Input.GetKey("left shift") && Input.GetKey("s"))
        {
            LevelState.singleton.gameActive = true;
        }
    }

	NetworkConnection checkingEquality;

	bool dumbEquals (NetworkConnection dumb){
		if (checkingEquality == null) {
			return false;
		} else {
			return dumb.Equals (checkingEquality);
		}
	}

	void OnClientConnected(NetworkMessage netMsg)
    {
        print("meow~");

		connections.Add (netMsg.conn);
    }

	void OnLeftTeamDecide(NetworkMessage netMsg) {
		checkingEquality = netMsg.conn;
		int connIndex = connections.FindIndex (dumbEquals);
		if (connIndex == -1)
			return;
		teams [connIndex] = Team.Left;
		//foreach( NetworkInstanceId id in netMsg.conn.clientOwnedObjects) {
		//	NetworkServer.FindLocalObject (id).GetComponent<Paddle> ().SetPosition (LeftSpawn.transform.position);
		//}
	}

	void OnRightTeamDecide(NetworkMessage netMsg) {
		checkingEquality = netMsg.conn;
		int connIndex = connections.FindIndex (dumbEquals);
		if (connIndex == -1)
			return;
		teams [connIndex] = Team.Right;
		//foreach( NetworkInstanceId id in netMsg.conn.clientOwnedObjects) {
		//	NetworkServer.FindLocalObject (id).GetComponent<PlayerUnit> ().SetPosition (RightSpawn.transform.position);
		//}
	}
}
