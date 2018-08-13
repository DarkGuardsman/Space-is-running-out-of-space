using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimAt : MonoBehaviour {

	public float rotationSpeed = 5f;
    public float aimDistance = 2f;
    
    private Rigidbody2D rigidBody2D;
    
    private float lastAngle;
      
    void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 target = getTarget();
        Vector2 position = transform.position;        
        if(target != null)
        {
            float distance = Mathf.Abs(Vector2.Distance(target, position));
            if(distance >= aimDistance)
            {
                //Get direction vector
                Vector2 direction = target - position; 
                direction.Normalize();
                
                //Convert vector to angle
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; //90 is offset to point up                
                
                if(Mathf.Abs(Mathf.DeltaAngle(angle, lastAngle)) > 0.01)
                {
                    //Smooth rotation
                    angle = Mathf.LerpAngle(lastAngle, angle, Time.deltaTime * rotationSpeed);
                    
                    //Store last rotation for smooth movement
                    lastAngle = angle;
                    
                    //Set rotation        
                    rigidBody2D.MoveRotation(angle); 
                }
            }
        }
	}
    
    float ClampAngle(float angle) 
    {
        angle = angle % 360;
        return angle;
    }
    
    public abstract Vector2 getTarget();
}
