using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LevelState : NetworkBehaviour {
    public static LevelState singleton;

    [SyncVar]
    public bool gameActive = false;

    // Use this for initialization
    void Awake () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
    }
}
