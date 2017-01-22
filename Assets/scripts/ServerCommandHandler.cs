using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum CustomMessages
{
    ClientReceivePlayerID = MsgType.Highest + 1
}

//public class MsgClientReceivePlayerID : MessageBase
//{
//    public MsgClientReceivePlayerID()
//    {
//    }

//    public MsgClientReceivePlayerID(PlayerID playerID)
//    {
//        this.playerID = playerID;
//    }

//    public PlayerID playerID;
//}

public class ServerCommandHandler : NetworkBehaviour
{
    static public ServerCommandHandler singleton;

	void Start ()
    {
		singleton = this;
	}
	
	void Update ()
    {
	}
}
