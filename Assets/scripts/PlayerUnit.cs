using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only spawn these on the server.
public class PlayerUnit : MonoBehaviour
{
    public PlayerID playerID = null;
    public PaddleLocalState linkedLocalState = null;

    private float baseMoveSpeed = 8.0f;

    public bool alive;

	void Start ()
    {
        alive = true;
	}
	
	void Update ()
    {
		UpdateMovement();
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
        print("dead");
        alive = false;
    }

    public void Revive()
    {
        alive = true;
    }
}
