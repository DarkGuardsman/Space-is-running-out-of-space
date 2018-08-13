using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseIfOutOfMap : MonoBehaviour 
{
    public float checkDelay = 5f;
    private GameController gameController;
    
    private float checkTimer;
    
	// Use this for initialization
	void Awake () 
    {
		gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(gameController != null && gameController.centerOfWorld != null)
        {
            if(checkTimer <= 0)
            {
                //Reset timer
                checkTimer = checkDelay;
                
                //Get distance from center
                Vector3 center = gameController.centerOfWorld.position;
                float distance = Mathf.Abs(Vector3.Distance(center, transform.position));
                
                //If outside map reverse motion
                if(distance > gameController.sizeOfWorld)
                {
                    Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        rb.velocity = -rb.velocity;
                    }
                }
            }
            else
            {
                checkTimer -= Time.deltaTime;
            }
        }
	}
}
