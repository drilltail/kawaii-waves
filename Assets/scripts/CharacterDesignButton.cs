using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDesignButton : MonoBehaviour
{
    public PlayerSprite sprite;

    public enum CharacterDesignButtonAction
    {
        IncrementTop,
        DecrementTop,
        IncrementBottom,
        DecrementBottom,
    }

    public CharacterDesignButtonAction action;

	void Start()
    {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
    {
        int newOccupant = sprite.occupant;
        int newVessel = sprite.vessel;

		switch(action)
        {
            case CharacterDesignButtonAction.IncrementTop:                      newOccupant++;  break;
            case CharacterDesignButtonAction.DecrementTop:                      newOccupant--;  break;
            case CharacterDesignButtonAction.IncrementBottom:                   newVessel++;    break;
            case CharacterDesignButtonAction.DecrementBottom:                   newVessel--;    break;

            default:                                                            break;
        }

        if(newOccupant >= sprite.occupants.Count)
        {
            newOccupant = 0;
        }
        if(newOccupant < 0)
        {
            newOccupant = sprite.occupants.Count - 1;
        }
        if(newVessel >= sprite.vessels.Count)
        {
            newVessel = 0;
        }
        if(newVessel < 0)
        {
            newVessel = sprite.vessels.Count - 1;
        }

        if(newOccupant != sprite.occupant)
        {
            sprite.occupant = newOccupant;
        }

        if(newVessel != sprite.vessel)
        {
            sprite.vessel = newVessel;
        }
	}
}
