using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles movement, works without care for direction of ship to simulate thrusters
public class PlayerMovement : PlayerControls 
{	
    public float rotationSpeed = 20f;
    public float rotationKeyboardMultiplier = 3f;
    
    public float movePower = 10f;
    public float breakingPower = 0.98f;
    
    private Rigidbody2D rigidBody2D;
    private PlayerOptions playerOptions;    
    
    private float lastAngle;
    
    public override void Start()
    {
        base.Start();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerOptions = FindObjectOfType<PlayerOptions>();
    }
    
	// Update is called once per frame
	void Update () 
    {
        //Get user input
        float moveHorizontal = GetHorizontal();
        float moveVertical = GetVertical();
        
        //Get force scale for frame
        float moveForce = movePower * Time.deltaTime;

        //Switch movement mode based on settings
        if(playerOptions.currentSettings.enableShipBasedMovement)
        {
            //Forwards/backwards movement
            rigidBody2D.AddForce (transform.up * moveForce * moveVertical);
            
            //Left/Right movement
            rigidBody2D.AddForce (transform.right * moveForce * moveHorizontal);
        }
        else
        {
            //Convert to vector 2
            Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        
            //Apply speed over time
            movement *= moveForce;
            
            //Apply
            rigidBody2D.AddForce (movement);
        }
        
        if(ShouldSlow())
        {
            rigidBody2D.velocity = rigidBody2D.velocity * breakingPower;
        }
        
        if(!playerOptions.currentSettings.enableMouseAim)
        {
            float aimMove = GetAim() * rotationKeyboardMultiplier;
            
            //Convert vector to angle
            float angle = rigidBody2D.rotation + (aimMove * rotationSpeed);              
                    
            if(Mathf.Abs(Mathf.DeltaAngle(angle, lastAngle)) > 0.01)
            {
                //Smooth rotation
                angle = Mathf.LerpAngle(lastAngle, angle, Time.deltaTime);
                        
                //Store last rotation for smooth movement
                lastAngle = angle;
                        
                //Set rotation        
                rigidBody2D.MoveRotation(angle); 
            }
        }
    }
    
    bool ShouldSlow()
    {
        return inputManager.getInputActions().slow.IsKeyDown() || Input.GetButton("Slow");
    }
    
    float GetAim()
    {
        if(inputManager.getInputActions().rotateLeft.IsKeyDown())
        {
            return 1;
        }
        else if(inputManager.getInputActions().rotateRight.IsKeyDown())
        {
            return -1;
        }
        return -Input.GetAxis ("Aim");
    }
    
    float GetHorizontal()
    {
        if(inputManager.getInputActions().left.IsKeyDown())
        {
            return -1;
        }
        else if(inputManager.getInputActions().right.IsKeyDown())
        {
            return 1;
        }
        return Input.GetAxis ("Horizontal");
    }
    
     float GetVertical()
    {
        if(inputManager.getInputActions().down.IsKeyDown())
        {
            return -1;
        }
        else if(inputManager.getInputActions().up.IsKeyDown())
        {
            return 1;
        }
        return Input.GetAxis ("Vertical");
    }
}
