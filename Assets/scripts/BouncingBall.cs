using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour {

  float leftBound;
  float rightBound;
  float topBound;
  float bottomBound;
  Vector2 velUnit;
  public float speed = 10;
  public Vector2 position2d {
    get { return new Vector2(transform.position.x, transform.position.y); }
    set { transform.position = new Vector3(value.x, value.y, transform.position.z); }
  }

	// Use this for initialization
	void Start () {
    float viewHeight = Camera.main.orthographicSize;
    float viewWidth = Camera.main.aspect * viewHeight;
    leftBound = -viewWidth;
    rightBound = viewWidth;
    bottomBound = -viewHeight;
    topBound = viewHeight;
    velUnit = new Vector2(Random.value, Random.value);
	}
	
	// Update is called once per frame
	void Update () {
    position2d += velUnit * speed;
    if (transform.position.x < leftBound) {
      transform.position = new Vector2(leftBound, transform.position.y);
      velUnit.x *= -1;
      velUnit.y += Random.Range(-1f, 1f);
      velUnit.Normalize();
    } else if (transform.position.x > rightBound) {
      transform.position = new Vector2(rightBound, transform.position.y);
      velUnit.x *= -1;
      velUnit.y += Random.Range(-1f, 1f);
      velUnit.Normalize();
    } if (transform.position.y < bottomBound) {
      transform.position = new Vector2(transform.position.x, bottomBound);
      velUnit.y *= -1;
      velUnit.x += Random.Range(-1f, 1f);
      velUnit.Normalize();
    } else if (transform.position.y > topBound) {
      transform.position = new Vector2(transform.position.x, topBound);
      velUnit.y *= -1;
      velUnit.x += Random.Range(-1f, 1f);
      velUnit.Normalize();
    }
	}
}
