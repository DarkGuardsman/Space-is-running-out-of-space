using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectJunk : MonoBehaviour 
{
    public float ropeConnectionDistance = 5;
    
    public GameObject junkConnectionPoint;
    public CollectionArea pickupArea;
    
    //Junk count (size, not individual objects)
    public int ropeConnections = 0;
    
    public int maxRopeConnections = 10;
    
    //Junk collected
    public List<JunkObjectData> collectedJunk = new List<JunkObjectData>(); 
    
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetButton("Fire2") && ropeConnections <= maxRopeConnections)
        {
            CollectJunk();
        }
        
        if(Input.GetButton("Release"))
        {
            ReleaseJunk();
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
        junkData.AttachRope(junkConnectionPoint, ropeConnectionDistance);
        collectedJunk.Add(junkData);
        ropeConnections++;
    }
    
    void ReleaseJunk()
    {
        foreach (JunkObjectData junkData in collectedJunk)
        {
            ReleaseJunk(junkData);
        }
        collectedJunk.Clear();
    }
    
    void ReleaseJunk(JunkObjectData junkData)
    { 
        junkData.ReleaseRope();
        ropeConnections--;
    }
}
