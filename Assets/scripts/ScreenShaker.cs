using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenShakeEvent
{
    public ScreenShakeEvent(float duration, float amplitude)
    {
        this.duration = duration;
        this.timeRemaining = duration;
        this.amplitude = amplitude;
    }

    public float duration;
    public float timeRemaining;
    public float amplitude;
}

public class ScreenShaker : MonoBehaviour {
	
    static public ScreenShaker singleton;

	public LinkedList<ScreenShakeEvent> shakeEvents = new LinkedList<ScreenShakeEvent>();
	
	void Start()
    {
		singleton = this;
	}
	
	void Update()
    {
        transform.position = Vector3.zero;

        LinkedList<ScreenShakeEvent> eventsToRemove = new LinkedList<ScreenShakeEvent>();
        foreach(ScreenShakeEvent shakeEvent in shakeEvents)
        {
            transform.Translate(Random.Range(-shakeEvent.amplitude, shakeEvent.amplitude), Random.Range(-shakeEvent.amplitude, shakeEvent.amplitude), 0);

            shakeEvent.amplitude /= 1.1f;
            shakeEvent.timeRemaining -= Time.smoothDeltaTime;

            if(shakeEvent.timeRemaining <= 0)
            {
                eventsToRemove.AddLast(shakeEvent);
            }
        }

        foreach(ScreenShakeEvent eventToRemove in eventsToRemove)
        {
            shakeEvents.Remove(eventToRemove);
        }
	}
	
	public void AddShake(float duration, float amplitude)
    {
		shakeEvents.AddLast(new ScreenShakeEvent(duration, amplitude));
	}
}
