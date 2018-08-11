using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectJunk : MonoBehaviour 
{
    public float collectJunkCooldown = 1f;
    public float ropeConnectionDistance = 5;
    
    public GameObject junkConnectionPoint;
    public CollectionArea pickupArea;
    
    //Junk count (size, not individual objects)
    public int junkCarrySize = 0;
    
    public int maxJunkCarrySize = 100;
    
    //Junk collected
    public List<JunkObjectData> collectedJunk = new List<JunkObjectData>(); 
    
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
        
        foreach(JunkObjectData junkData in collectedJunk)
        {
            junkData.joint.attachedRigidbody.velocity *= 0.99f;
        }
	}
    
    void CollectJunk()
    {
        if(pickupArea.objectsInArea != null)
        {
            for(int i = pickupArea.objectsInArea.Count - 1; i >= 0; i--)
            {
                GameObject gameObject = pickupArea.objectsInArea[i];
                if(gameObject != null)
                {
                    JunkObjectData junkData = gameObject.GetComponent<JunkObjectData>();
                    if(junkData != null)
                    {
                        pickupArea.objectsInArea.Remove(gameObject);
                        CollectJunk(junkData);
                    }
                }
            }
        }
    }
    
    void CollectJunk(JunkObjectData junkData)
    {       
        junkData.Collect(junkConnectionPoint, ropeConnectionDistance);
        collectedJunk.Add(junkData);
    }
    
    void ReleaseJunk()
    {
        
    }
}
