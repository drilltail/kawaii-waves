using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceArea : MonoBehaviour {

  public float angle = 30;
  public float acceleration = 1;
  public float desiredSpeed = 20;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}
	
  private void OnTriggerStay2D(Collider2D other) {
    if(other.GetComponent<BouncingBall>() != null)
    {
        other.gameObject.GetComponent<BouncingBall>().addVelocity((Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right * acceleration * Time.smoothDeltaTime), desiredSpeed);
    }
  }
}
