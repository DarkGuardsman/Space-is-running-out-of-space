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
        if(!isPlayerInside && gameController.currentPlayerObject != null)
        {
            outOfWorldTimer += Time.deltaTime;
            
            if(outOfWorldTimer >= outOfWorldDelay)
            {
                gameController.KillPlayer();
                outOfWorldTimer = -2;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == gameController.currentPlayerObject)
        {
            isPlayerInside = true;
            outOfWorldTimer = 0;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == gameController.currentPlayerObject)
        {
            isPlayerInside = false;
        }
    }
}
