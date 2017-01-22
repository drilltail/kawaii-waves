using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpritePiece : MonoBehaviour
{
    public Vector2 deathVelocity;
    public int deathBouncesLeft = -1;

    float leftBound;
      float rightBound;
      float topBound;
      float bottomBound;

    public Quaternion originalRotation;
    public Vector3 originalLocalPosition;

    bool firstFrame = true;

	protected virtual void Start()
    {
		float viewHeight = Camera.main.orthographicSize;
        float viewWidth = Camera.main.aspect * viewHeight;
        leftBound = -viewWidth;
        rightBound = viewWidth;
        bottomBound = -viewHeight;
        topBound = viewHeight;
	}

    public void Revive()
    {
        transform.rotation = originalRotation;
        transform.localPosition = originalLocalPosition;
        deathVelocity = Vector2.zero;
        deathBouncesLeft = -1;
    }
	
	protected virtual void Update()
    {
        if(firstFrame)
        {
            originalRotation = transform.rotation;
            originalLocalPosition = transform.localPosition;
            firstFrame = false;
        }

		if(deathBouncesLeft >= 0)
        {
            transform.Translate(deathVelocity * Time.smoothDeltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180 * Time.smoothDeltaTime);
        }

        if(deathBouncesLeft > 0)
        {
            if(transform.position.x <= leftBound && deathVelocity.x < 0)
            {
                print("deathbounce: left");
                deathBouncesLeft--;
                deathVelocity.x *= -1;
            }

            if(transform.position.x >= rightBound && deathVelocity.x > 0)
            {
                print("deathbounce: right");
                deathBouncesLeft--;
                deathVelocity.x *= -1;
            }

            if(transform.position.y <= bottomBound && deathVelocity.y < 0)
            {
                print("deathbounce: bottom");
                deathBouncesLeft--;
                deathVelocity.y *= -1;
            }

            if(transform.position.y >= topBound && deathVelocity.y > 0)
            {
                print("deathbounce: top");
                deathBouncesLeft--;
                deathVelocity.y *= -1;
            }
        }
	}
}
