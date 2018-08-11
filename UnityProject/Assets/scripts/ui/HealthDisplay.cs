using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : UIDisplay 
{
    public Text text;
    
	private DamageData damageData;
    
	void FixedUpdate () 
    {
        if(gameController.currentPlayerObject != null)
        {
            if(damageData == null)
            {
                damageData = gameController.currentPlayerObject.GetComponent<DamageData>();
            }
            else
            {
                text.text =  text.text = String.Format("Health: {0:0}", damageData.health);
            }
        }
	}
}
