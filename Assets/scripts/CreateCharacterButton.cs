using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color newColor = Color.HSVToRGB(Mathf.Lerp(0, 1, Time.time * 0.15f % 1), 0.5f, 1.0f);
        newColor.a = 0.75f;

        ColorBlock newColors = new ColorBlock();
        newColors.normalColor = newColor;
        newColors.disabledColor = newColor;
        newColors.highlightedColor = newColor;
        newColors.pressedColor = newColor;
        newColors.colorMultiplier = 1.0f;

        GetComponent<Button>().colors = newColors;
	}
}
