using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles movement, works without care for direction of ship to simulate thrusters
public class PlayerMovement : MonoBehaviour 
{	
    public float rotationSpeed = 20f;
    public float rotationKeyboardMultiplier = 3f;
    
    public float movePower = 10f;
    public float breakingPower = 0.98f;
    
    private Rigidbody2D rigidBody2D;
    private PlayerOptions playerOptions;    
    
    private float lastAngle;
    
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        playerOptions = FindObjectOfType<PlayerOptions>();
    }
    
	// Update is called once per frame
	void Update () 
    {
        //Get user input
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        
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
        
        if(Input.GetButton("Breaks"))
        {
            rigidBody2D.velocity = rigidBody2D.velocity * breakingPower;
        }
        
        if(!playerOptions.currentSettings.enableMouseAim)
        {
            float aimMove = -Input.GetAxis ("Aim") * rotationKeyboardMultiplier;
            
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
}
