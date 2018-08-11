using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles movement, works without care for direction of ship to simulate thrusters
public class PlayerMovement : MonoBehaviour 
{	
    public float movePower = 10f;
    public Vector2 speedLimit;
    
    private Rigidbody2D rigidBody2D; 
    
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
	// Update is called once per frame
	void Update () 
    {
		HandleMovement();
        LimitVelocity();
	}
    
    //Handles user input and force application for movement
    void HandleMovement()
    {
        //Get user input
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        
        //Convert to vector 2
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        //Apply force with scale
        rigidBody2D.AddForce (movement * movePower);
    }
    
    //Limits max movement speed
    void LimitVelocity()
    {        
        Vector3 velocity = rigidBody2D.velocity;
        
        if(velocity.x > speedLimit.x)
        {
            velocity.x = speedLimit.x;
        }
        else if(velocity.x < -speedLimit.x)
        {
            velocity.x = -speedLimit.x;
        }
        
        if(velocity.y > speedLimit.y)
        {
            velocity.y = speedLimit.y;
        }
        else if(velocity.y < -speedLimit.y)
        {
            velocity.y = -speedLimit.y;
        }
        
        rigidBody2D.velocity = velocity;
    }
}
