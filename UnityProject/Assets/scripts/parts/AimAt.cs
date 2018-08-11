using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimAt : MonoBehaviour {

	public float rotationSpeed = 5f;
    
    private Rigidbody2D rigidBody2D;
      
    void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 target = getTarget();
        if(target != null)
        {
            //Get direction vector
            Vector2 direction = target - (Vector2)transform.position;
            direction.Normalize();
            
            //Convert vector to angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            //Set rotation        
            rigidBody2D.MoveRotation(angle - 90); 
        }
	}
    
    float ClampAngle(float angle) 
    {
        angle = angle % 360;
        return angle;
    }
    
    public abstract Vector2 getTarget();
}
