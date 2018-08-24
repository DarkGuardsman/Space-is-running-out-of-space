using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectJunk : PlayerControls
{
    public float ropeConnectionDistance = 5;
    
    public GameObject junkConnectionPoint;
    public CollectionArea pickupArea;
    
    //Junk count (size, not individual objects)
    public int currentLoad = 0;
    
    public int maxLoad = 10;
    
    //Junk collected
    public List<JunkObjectData> collectedJunk = new List<JunkObjectData>(); 
    
	// Update is called once per frame
	void Update () 
    {
        if(ShouldHook() && currentLoad <= maxLoad)
        {
            CollectJunk();
        }
        
        if(ShouldRelease())
        {
            ReleaseJunk();
        }
        
        currentLoad = 0;
        for(int i = collectedJunk.Count - 1; i >= 0; i--)
        {
            JunkObjectData junkData = collectedJunk[i];
            if(junkData != null)
            {
                junkData.joint.attachedRigidbody.velocity *= 0.99f;
                currentLoad += junkData.currentSize;
            }
            else
            {
                collectedJunk.Remove(junkData);
            }
        }
	}
    
    bool ShouldHook()
    {
        return inputManager.getInputActions().hook.IsKeyDown() || Input.GetButton("Fire2");
    }
    
    bool ShouldRelease()
    {
        return inputManager.getInputActions().release.IsKeyDown() || Input.GetButton("Release");
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
        //Tell junk to attach
        junkData.AttachRope(junkConnectionPoint, ropeConnectionDistance);
        
        //Add to list
        collectedJunk.Add(junkData);
        
        //Count up load
        currentLoad += junkData.currentSize;
    }
    
    void ReleaseJunk()
    {
        //Release each junk entry
        foreach (JunkObjectData junkData in collectedJunk)
        {
            ReleaseJunk(junkData);
        }
        
        //Clear list
        collectedJunk.Clear();
        
        //Reset value, include seperate from decrementer in ReleaseJunk in case size changes
        currentLoad = 0;
    }
    
    void ReleaseJunk(JunkObjectData junkData)
    { 
        //Tell junk to release
        junkData.ReleaseRope();
        
        //Decrease load counter
        currentLoad -= junkData.currentSize;
    }
}
