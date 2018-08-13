using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Updated UI text field with current speed data
public class JunkSpawnedDisplay : UIDisplay 
{
    public Text text;
    
	void FixedUpdate () 
    {
        if(gameController.junkSpawnedList != null)
        {
            text.text = String.Format("Scrap in Area: {0:0}", gameController.junkSpawnedList.Count);
        }		
	}
}
