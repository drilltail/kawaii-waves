using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

  public float minTime = 5f;
  public float maxTime = 15f;
  public float duration = 10;
  public float minSpeed = 3f;
  public float maxSpeed = 10f;
  public float minXPos = -5f;
  public float maxXPos = 5f;
  public float angleBound = 45f;
  public GameObject wavePrefab;

  private float timer;

	// Use this for initialization
	void Start () {
    timer = Random.Range(minTime, maxTime);
	}
	
	// Update is called once per frame
	void Update () {
    if(LevelState.singleton.gameActive) {
      timer -= Time.smoothDeltaTime;
      if (timer <= 0f) {
        timer = Random.Range(minTime, maxTime);
        GameObject wave = Instantiate(wavePrefab);
        float angle = Random.Range(-angleBound, angleBound);
        float xPos = Random.Range(minXPos, maxXPos);
        float yPos = Camera.main.orthographicSize * (Random.value > 0.5f ? 1f : -1f);
        float speed = Random.Range(minSpeed, maxSpeed);
        angle -= 90 * Mathf.Sign(yPos);
        wave.transform.position = new Vector3(xPos, yPos);
        wave.GetComponent<ForceArea>().desiredSpeed = speed * 0.8f;
        wave.GetComponent<ForceArea>().angle = angle;
        wave.GetComponent<ForceArea>().acceleration = 10f * (speed/maxSpeed);
        StartCoroutine(MoveWave(wave, duration, angle, speed));
      }
    }
	}

  IEnumerator MoveWave (GameObject g, float duration, float angle, float speed) {
    for(float f = duration; f >= 0; f -= Time.smoothDeltaTime) {
      Vector2 velocity = (Vector2) (Quaternion.Euler(0,0,angle) * Vector2.right * speed * Time.smoothDeltaTime);
      g.transform.position += (Vector3) velocity;
      yield return new WaitForFixedUpdate();
    }
    Destroy(g);
  }
}
