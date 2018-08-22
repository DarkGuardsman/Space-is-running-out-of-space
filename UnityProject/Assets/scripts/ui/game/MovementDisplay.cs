﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Updated UI text field with current speed data
public class MovementDisplay : UIDisplay 
{
    public Text text;
    
    private Rigidbody2D rigidBody2D;
    
	void FixedUpdate () 
    {
        if(gameController.currentPlayerObject != null)
        {
            //Should go null if player goes null (in theory)
            if(rigidBody2D == null)
            {
                rigidBody2D = gameController.currentPlayerObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                Vector3 velocity = rigidBody2D.velocity;
                text.text = String.Format("Heading: {0:0}x {1:0}y", velocity.x, velocity.y);
            }
        }		
	}
}
