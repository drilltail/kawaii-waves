using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectSprite : MonoBehaviour
{
    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

	void Start()
    {
        rSlider.value = Random.value;
        gSlider.value = Random.value;
        bSlider.value = Random.value;
	}
	
	void Update()
    {
		Vessel vessel = GetComponentInChildren<Vessel>();
        SpriteRenderer renderer = vessel.GetComponent<SpriteRenderer>();

        renderer.color = new Color(rSlider.value, gSlider.value, bSlider.value);
	}
}
