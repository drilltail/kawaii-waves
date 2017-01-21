using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PaddleLocalState : NetworkBehaviour {
    private float upwardsMin   = 480 - 200;
    private float upwardsMax   = 480;
    private float downwardsMin = 0;
    private float downwardsMax = 200;

    [SyncVar]
    public float inputMagnitude;

    void Start ()
    {
	    inputMagnitude = 0;
	}
	
	void Update ()
    {
	    if(!isServer)
        {
            UpdateInput();
        }
	}

    void UpdateInput()
    {
        float newMagnitude = 0;

        if(Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if(touchPosition.y > upwardsMin)
            {
                newMagnitude = (touchPosition.y - upwardsMin) / (upwardsMax - upwardsMin);
            }
            else if(touchPosition.y < downwardsMax)
            {
                newMagnitude = (downwardsMax - touchPosition.y) / (downwardsMax - downwardsMin);
            }
        }

        // TODO: do this less frequently than 60 Hz? why?
        if(newMagnitude != inputMagnitude)
        {
            CmdSetInputMagnitude(newMagnitude);
        }

        InputMagnitudeText.singleton.GetComponent<Text>().text = newMagnitude.ToString();
    }

    [Command]
    public void CmdSetInputMagnitude(float magnitude)
    {
        inputMagnitude = magnitude;
    }
}
