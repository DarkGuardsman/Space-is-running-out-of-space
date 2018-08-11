using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour 
{
    public float collectJunkCooldown = 1f;
    
    public CollectionArea pickupArea;
    
    //Junk count (size, not individual objects)
    public int junkCarrySize = 0;
    
    public int maxJunkCarrySize = 100;
    
    //Junk collected
    public List<GameObject> collectedJunk = new List<GameObject>(); 
    
    private float collectJunkTimer;
    
	// Update is called once per frame
	void Update () 
    {
        if(collectJunkTimer <= 0)
        {
            if(Input.GetButton("Fire2"))
            {
                collectJunkTimer = collectJunkCooldown;
                CollectJunk();
            }
        }
        else
        {
            collectJunkTimer -= Time.deltaTime;
        }
	}
    
    void CollectJunk()
    {
        
    }
    
    void ReleaseJunk()
    {
        
    }
}
