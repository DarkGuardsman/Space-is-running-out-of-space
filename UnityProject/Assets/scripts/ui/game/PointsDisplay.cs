using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Updated UI text field with current speed data
public class PointsDisplay : UIDisplay 
{
    public Text text;
    
	void FixedUpdate () 
    {
        text.text = String.Format("Credits: ${0:0}", gameController.points);
	}
}
