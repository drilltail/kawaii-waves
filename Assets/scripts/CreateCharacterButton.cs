using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacterButton : MonoBehaviour {
    public PlayerSprite sprite;

	void Start()
    {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}
	
	void Update()
    {
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

    void TaskOnClick()
    {
        CharacterDesignScreen.top = sprite.occupant;
        CharacterDesignScreen.bottom = sprite.vessel;
        CharacterDesignScreen.color = sprite.GetComponentInChildren<Vessel>().GetComponent<SpriteRenderer>().color;

        SceneManager.LoadScene("networkStuff");
    }
}
