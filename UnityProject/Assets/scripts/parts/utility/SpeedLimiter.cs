using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimiter : MonoBehaviour 
{
    public Vector2 speedLimit;
    private Rigidbody2D rigidBody2D; 
    
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	 //Limits max movement speed
    void FixedUpdate ()
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
    
    public void SetSpeed(float speed)
    {
        speedLimit.x = speed;
        speedLimit.y = speed;
    }
}
