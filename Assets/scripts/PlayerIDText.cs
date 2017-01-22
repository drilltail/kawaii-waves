using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDText : MonoBehaviour {

    static public PlayerIDText singleton;

	void Awake () {
		singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
