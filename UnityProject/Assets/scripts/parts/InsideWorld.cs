using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideWorld : MonoBehaviour 
{    
    public GameController gameController;
    public float outOfWorldDelay = 5f;
    
    public bool isPlayerInside = true;   
   
    
    private float outOfWorldTimer;
    
    void Start ()
    {
        gameController = FindObjectOfType<GameController>();
    } 
    
    void Update()
    {
        //TODO show danger UI
        if(!isPlayerInside)
        {
            outOfWorldTimer += Time.deltaTime;
            
            if(outOfWorldTimer >= outOfWorldDelay)
            {
                gameController.KillPlayer();
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        isPlayerInside = true;
        outOfWorldTimer = 0;
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        isPlayerInside = false;
    }
}
