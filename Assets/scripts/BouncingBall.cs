using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour {

  public float speed = 10f;
  public float angleBound = 70f;
  public float angleVariance = 1f;
  public float minSpeed = 3f;

  float leftBound;
  float rightBound;
  float topBound;
  float bottomBound;
  Vector2 velUnit;
  public Vector2 position2d {
    get { return new Vector2(transform.position.x, transform.position.y); }
    set { transform.position = new Vector3(value.x, value.y, transform.position.z); }
  }
  private SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
    float viewHeight = Camera.main.orthographicSize;
    float viewWidth = Camera.main.aspect * viewHeight;
    leftBound = -viewWidth;
    rightBound = viewWidth;
    bottomBound = -viewHeight;
    topBound = viewHeight;
    velUnit = new Vector2(0,1).normalized;
    myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(LevelState.singleton.gameActive)
        {
            transform.Translate(velUnit * speed  * Time.smoothDeltaTime);
            if (transform.position.x - myRenderer.bounds.extents.x < leftBound) {
              transform.position = new Vector2(leftBound + myRenderer.bounds.extents.x, transform.position.y);
              velUnit.x *= -1;
              velUnit = (Quaternion.Euler(0, 0, Random.Range(-angleVariance, angleVariance)) * velUnit).normalized;
              ClampAngle();
            } else if (transform.position.x + myRenderer.bounds.extents.x > rightBound) {
              transform.position = new Vector2(rightBound - myRenderer.bounds.extents.x, transform.position.y);
              velUnit.x *= -1;
              velUnit = (Quaternion.Euler(0, 0, Random.Range(-angleVariance, angleVariance)) * velUnit).normalized;
              ClampAngle();
            } if (transform.position.y - myRenderer.bounds.extents.y < bottomBound) {
              transform.position = new Vector2(transform.position.x, bottomBound + myRenderer.bounds.extents.y);
              velUnit.y *= -1;
              velUnit = (Quaternion.Euler(0, 0, Random.Range(-angleVariance, angleVariance)) * velUnit).normalized;
              ClampAngle();
            } else if (transform.position.y + myRenderer.bounds.extents.y > topBound) {
              transform.position = new Vector2(transform.position.x, topBound - myRenderer.bounds.extents.y);
              velUnit.y *= -1;
              velUnit = (Quaternion.Euler(0, 0, Random.Range(-angleVariance, angleVariance)) * velUnit).normalized;
              ClampAngle();
            }

            if (speed <= minSpeed) {
                speed = minSpeed;
            }
        }
	}

  public void ClampAngle() {
    float angle = Vector2.Angle(Vector2.right*Mathf.Sign(velUnit.x), velUnit);
    if(angle > angleBound) {
      angle = angle - angleBound;
      velUnit = (Quaternion.Euler(0, 0, -angle*Mathf.Sign(velUnit.x)*Mathf.Sign(velUnit.y)) * velUnit).normalized;
    }
  }

  public void addVelocity(Vector2 change, float speedLimit) {
      Vector2 newVel = change + (velUnit * speed);
      velUnit = newVel.normalized;
    if (speed <= speedLimit) {
      speed = newVel.magnitude;
    }
  }
}
