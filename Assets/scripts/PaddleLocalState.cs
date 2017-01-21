using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PaddleLocalState : NetworkBehaviour {

    public int teamNumber;
    public int directionInput;

    void Start ()
    {
	    teamNumber = 0;
        position = new Vector2(50, 100);
	}
	
	void Update ()
    {
	    if(!isServer && hasAuthority)
        {
            UpdateInput();
        }
	}

    void UpdateInput()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).position.y > 100)
            {
                directionInput = 1;
            }
            else if(Input.GetTouch(0).position.y < 100)
            {
                directionInput = -1;
            }
            else
            {
                directionInput = 0;
            }
        }
        else
        {
            directionInput = 0;
        }
    }
}
