using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDisplay : UIDisplay 
{
    public Text text;
    
    private PlayerCollectJunk junkCollector;
    
	void FixedUpdate () 
    {
        if(gameController.currentPlayerObject != null)
        {
            if(junkCollector == null)
            {
                junkCollector = FindObjectOfType<PlayerCollectJunk>();
            }
            else
            {
                text.text = String.Format("Load: {0}/{1}", junkCollector.currentLoad, junkCollector.maxLoad);
            }
        }		
	}
}

