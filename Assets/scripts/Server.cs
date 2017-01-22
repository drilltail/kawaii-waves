using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerID
{
    public PlayerID()
    {
        this.id = -1;
        this.team = PlayerTeam.Undeclared;
    }

    public PlayerID(int playerID, PlayerTeam team)
    {
        this.id = playerID;
        this.team = team;
    }

    public int id = -1;
    public PlayerTeam team = PlayerTeam.Undeclared;

    public override string ToString()
    {
        return "[id=" + id + ", team=" + team.ToString() + "]";
    }
}

public enum PlayerTeam
{
	Left, Right, Undeclared
}

public class Server : MonoBehaviour {
    public PlayerUnit spawnedPlayerUnit;

    static public Server singleton;

    List<NetworkConnection> connections = new List<NetworkConnection>();
    public Dictionary<int, PlayerUnit> playerUnits = new Dictionary<int, PlayerUnit>();
    public Dictionary<int, PaddleLocalState> paddleLocalStates = new Dictionary<int, PaddleLocalState>();
    public List<int> leftTeamPlayerIDs = new List<int>();
    public List<int> rightTeamPlayerIDs = new List<int>();

	public NetworkManager manager;

	public GameObject LeftSpawn;
	public GameObject RightSpawn;

    public int nextPlayerIDToAssign = 0;
    public PlayerTeam nextTeamToAssign = PlayerTeam.Left;

    void Awake()
    {
        singleton = this;
    }

	void Start () {
		print("StartServer: " + manager.StartServer());
		NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
	}
	
	// Update is called once per frame
	void Update () {
        if(!LevelState.singleton.gameActive && Input.GetKey("left shift") && Input.GetKey("s"))
        {
            LevelState.singleton.gameActive = true;
        }
    }

	void OnClientConnected(NetworkMessage netMsg)
    {
        print("nyan~");

		connections.Add (netMsg.conn);
    }

    public void AlternateNextTeamToAssign()
    {
        if(nextTeamToAssign == PlayerTeam.Left)
        {
            nextTeamToAssign = PlayerTeam.Right;
        }
        else if(nextTeamToAssign == PlayerTeam.Right)
        {
            nextTeamToAssign = PlayerTeam.Left;
        }
    }
}
