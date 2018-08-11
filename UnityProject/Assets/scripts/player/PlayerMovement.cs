using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles movement, works without care for direction of ship to simulate thrusters
public class PlayerMovement : MonoBehaviour 
{	
    public float movePower = 10f;
    public float breakingPower = 0.98f;
    
    private Rigidbody2D rigidBody2D; 
    
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
	// Update is called once per frame
	void Update () 
    {
        //Get user input
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        
        //Convert to vector 2
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        //Apply force with scale
        rigidBody2D.AddForce (movement * movePower * Time.deltaTime);
        
        if(Input.GetButton("Breaks"))
        {
            rigidBody2D.velocity = rigidBody2D.velocity * breakingPower;
        }
    }
}
