using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseIfOutOfMap : MonoBehaviour 
{
    public float checkDelay = 5f;
    private GameController gameController;
    
    private float checkTimer;
    
    private bool isOutOfMap = false;
    private bool isBouncing = false;
    
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
                float distanceX = Mathf.Abs(transform.position.x - center.x);
                float distanceY = Mathf.Abs(transform.position.y - center.y);
                
                //If outside map reverse motion
                isOutOfMap = distanceX > gameController.sizeOfWorld || distanceY > gameController.sizeOfWorld;
                if(isOutOfMap)
                {
                    //Only bounce once to prevent getting stuck on edge
                    if(!isBouncing)
                    {
                        isBouncing = true;
                        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                        if(rb != null)
                        {
                            rb.velocity = -rb.velocity;
                        }
                    }
                }
                //Once inside map again, reset bounce state
                else
                {
                    isBouncing = false;
                }
            }
            else
            {
                checkTimer -= Time.deltaTime;
            }
        }
	}
}
