using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Updated UI text field with current speed data
public class PositionDisplay : UIDisplay 
{
    public Text text;
    
	void FixedUpdate () 
    {
        if(gameController.currentPlayerObject != null)
        {
            Vector3 pos = gameController.currentPlayerObject.transform.position;
            text.text = String.Format("Speed: {0:0}x {1:0}y", pos.x, pos.y);
        }		
	}
}
