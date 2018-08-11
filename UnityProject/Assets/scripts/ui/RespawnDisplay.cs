using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnDisplay : UIDisplay   
{
    public Text respawnTimerText;
    
    void Update()
    {
        respawnTimerText.text = String.Format("{0:0}", gameController.respawnDelay - gameController.respawnTimer);
    }	
}
