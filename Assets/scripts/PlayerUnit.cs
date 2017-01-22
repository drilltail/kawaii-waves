using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only spawn these on the server.
public class PlayerUnit : MonoBehaviour
{
    public GameObject deathParticles;

    public PlayerID playerID = null;
    public PaddleLocalState linkedLocalState = null;

    private float baseMoveSpeed = 8.0f;

    public bool alive;

    public bool firstFrame = true;

    public Color vesselColor;
    public bool vesselColorKnown = false;
    public bool flipped = false;

	void Start ()
    {
        alive = true;
	}
	
	void Update ()
    {
        if(firstFrame)
        {
            HideSprite();
        }

		UpdateMovement();

        if(vesselColorKnown)
        {
            PlayerSprite sprite = GetComponentInChildren<PlayerSprite>();
            SpriteRenderer occupantRenderer = sprite.GetComponentInChildren<Occupant>().GetComponent<SpriteRenderer>();
            SpriteRenderer vesselRenderer = sprite.GetComponentInChildren<Vessel>().GetComponent<SpriteRenderer>();

            vesselRenderer.color = vesselColor;

            if(playerID.team == PlayerTeam.Right && !flipped)
            {
                occupantRenderer.flipX = !occupantRenderer.flipX;
                vesselRenderer.flipX = !vesselRenderer.flipX;
                flipped = true;
            }
        }

        firstFrame = false;
	}

    private void UpdateMovement()
    {
        if(alive)
        {
            if(linkedLocalState != null)
            {
                Vector2 netVelocity = Vector2.zero;
                netVelocity.y = GetNetMoveSpeed() * linkedLocalState.inputMagnitude;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x + (netVelocity.x * Time.smoothDeltaTime), -9 + 0.25f, 9 - 0.25f),
                                                 Mathf.Clamp(transform.position.y + (netVelocity.y * Time.smoothDeltaTime), -5 + 0.25f, 5 - 0.25f),
                                                 transform.position.z);
            }
        }
    }

    public float GetNetMoveSpeed()
    {
        return baseMoveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BouncingBall>() != null)
        {
            TakeHit();
        }
    }

    public void TakeHit()
    {
        if(alive)
        {
            Die();
        }
    }

    public void Die()
    {
        if(alive)
        {
            print("dead");
            alive = false;
            //GetComponent<AudioSource>().Play();
            Server.singleton.paddleLocalStates[playerID.id].framesToVibrate = 120;
            LevelState.singleton.playerDie.Play();

            ScreenShaker.singleton.AddShake(0.5f, 1.0f);
            Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);

            Occupant occupant = GetComponentInChildren<Occupant>();
            Vessel vessel = GetComponentInChildren<Vessel>();

            if(playerID.team == PlayerTeam.Left)
            {
                occupant.deathVelocity = Quaternion.Euler(0, 0, -60 + 120 * Random.value) * new Vector2(-50, 0);
                vessel.deathVelocity = Quaternion.Euler(0, 0, -60 + 120 * Random.value) * new Vector2(-50, 0);
            }
            else if(playerID.team == PlayerTeam.Right)
            {
                occupant.deathVelocity = Quaternion.Euler(0, 0, -60 + 120 * Random.value) * new Vector2(50, 0);
                vessel.deathVelocity = Quaternion.Euler(0, 0, -60 + 120 * Random.value) * new Vector2(50, 0);
            }

            occupant.deathBouncesLeft = 2;
            vessel.deathBouncesLeft = 2;
        }
    }

    public void Revive()
    {
        Occupant occupant = GetComponentInChildren<Occupant>();
        Vessel vessel = GetComponentInChildren<Vessel>();

        alive = true;
        occupant.Revive();
        vessel.Revive();
    }

    public void HideSprite()
    {
        PlayerSprite sprite = GetComponentInChildren<PlayerSprite>();
        SpriteRenderer occupantRenderer = sprite.GetComponentInChildren<Occupant>().GetComponent<SpriteRenderer>();
        SpriteRenderer vesselRenderer = sprite.GetComponentInChildren<Vessel>().GetComponent<SpriteRenderer>();

        occupantRenderer.color = Color.clear;
        vesselRenderer.color = Color.clear;
    }

    public void SetSprite(int occupant, int vessel, Color vesselColor)
    {
        PlayerSprite sprite = GetComponentInChildren<PlayerSprite>();

        sprite.occupant = occupant;
        sprite.vessel = vessel;

        this.vesselColor = vesselColor;
        vesselColorKnown = true;
    }
}
