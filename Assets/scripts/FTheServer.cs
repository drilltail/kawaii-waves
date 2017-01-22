using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FTheServer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if(isServer)
        {
            // f the server
            Object.Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
