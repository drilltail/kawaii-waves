using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PaddleLocalState : NetworkBehaviour
{
    private float upwardsMin   = 480 - 180;
    private float upwardsMax   = 480;
    private float downwardsMin = 0;
    private float downwardsMax = 180;

    [SyncVar]
    public float inputMagnitude;

    [SyncVar]
    public PlayerID playerID = new PlayerID();

    public bool initialized = false;

    void Awake()
    {
    }

    void Start ()
    {
        if(isServer)
        {
            playerID = new PlayerID(Server.singleton.nextPlayerIDToAssign, Server.singleton.nextTeamToAssign);
            print("Created playerID: " + playerID.ToString());

            Server.singleton.nextPlayerIDToAssign++;
            Server.singleton.AlternateNextTeamToAssign();

            Server.singleton.paddleLocalStates[playerID.id] = this;

            PlayerUnit playerUnit = Instantiate(Server.singleton.spawnedPlayerUnit) as PlayerUnit;
            playerUnit.playerID = playerID;
            playerUnit.linkedLocalState = this;

            float xAdjustment = playerID.id * 0.05f;

            if(playerUnit.playerID.team == PlayerTeam.Left)
            {
                playerUnit.transform.position = new Vector3(Server.singleton.LeftSpawn.transform.position.x + xAdjustment,
                                                            Server.singleton.LeftSpawn.transform.position.y,
                                                            0);
            }
            else if(playerUnit.playerID.team == PlayerTeam.Right)
            {
                playerUnit.transform.position = new Vector3(Server.singleton.RightSpawn.transform.position.x - xAdjustment,
                                                            Server.singleton.RightSpawn.transform.position.y,
                                                            0);
            }

            Server.singleton.playerUnits[playerID.id] = playerUnit;
        }

	    inputMagnitude = 0;
	}
	
	void Update ()
    {
        // Initialize
        if(!isServer && !initialized)
        {
            if(playerID != null && playerID.id >= 0)
            {
                initialized = true;

                PlayerIDText.singleton.GetComponent<Text>().text = "id=" + playerID.id.ToString() + ", team=" + playerID.team.ToString();
            }
        }

	    if(!isServer && initialized)
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
                //newMagnitude = (touchPosition.y - upwardsMin) / (upwardsMax - upwardsMin);
                newMagnitude = 1;
            }
            else if(touchPosition.y < downwardsMax)
            {
                //newMagnitude = -1 * ((downwardsMax - touchPosition.y) / (downwardsMax - downwardsMin));
                newMagnitude = -1;
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
