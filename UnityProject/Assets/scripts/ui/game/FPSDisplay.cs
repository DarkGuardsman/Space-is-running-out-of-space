using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour 
{
    public Text text;
    float deltaTime = 0.0f;
    float fps = 60;
 
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        
        text.text = String.Format("FPS: {0:0}", fps);
	}
}
