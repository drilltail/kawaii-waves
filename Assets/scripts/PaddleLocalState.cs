using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PaddleLocalState : NetworkBehaviour
{
    private float upwardsMin   = 480 - 210;
    private float upwardsMax   = 480;
    private float downwardsMin = 0;
    private float downwardsMax = 210;

    [SyncVar]
    public float inputMagnitude;

    [SyncVar]
    public PlayerID playerID = new PlayerID();

    public bool initialized = false;

    public int framesExisting = 0;
    public bool sentFancyYet = false;

    [SyncVar]
    public int framesToVibrate = 0;

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
                Server.singleton.leftTeamPlayerIDs.Add(playerID.id);
            }
            else if(playerUnit.playerID.team == PlayerTeam.Right)
            {
                playerUnit.transform.position = new Vector3(Server.singleton.RightSpawn.transform.position.x - xAdjustment,
                                                            Server.singleton.RightSpawn.transform.position.y,
                                                            0);
                Server.singleton.rightTeamPlayerIDs.Add(playerID.id);
            }

            Server.singleton.playerUnits[playerID.id] = playerUnit;
        }

	    inputMagnitude = 0;
	}
	
	void Update ()
    {
        if(framesExisting >= 5 && initialized && !sentFancyYet)
        {
            CmdSendPlayerFancy(playerID.id, CharacterDesignScreen.top, CharacterDesignScreen.bottom, CharacterDesignScreen.color);
            sentFancyYet = true;
        }

        if(!isServer && initialized)
        {
            framesExisting++;
        }

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

    [Command]
    public void CmdSendPlayerFancy(int playerID, int occupant, int vessel, Color vesselColor)
    {
        print("Received player fancy: id=" + playerID + ", occ=" + occupant + ", ves=" + vessel + ", clr=" + vesselColor);
        Server.singleton.playerUnits[playerID].SetSprite(occupant, vessel, vesselColor);
    }
}
