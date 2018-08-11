using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkObjectData : MonoBehaviour 
{
    //Size of the junk
    public int size = 1;
    //Point value of the junk
    public int pointValue = 1;
    
    //Is the junk currently collected by a player
    public bool collected = false;
    
    public SpringJoint2D joint;
    public LineRenderer ropeRenderer;
    public GameObject ropeConnectionPoint;
    
    //Used by drop off point to prevent instant collection
    public int collectTimer = 0;
    
    void Start ()
    {
        joint = gameObject.GetComponent<SpringJoint2D>();
        ropeRenderer = gameObject.GetComponent<LineRenderer>();
    }
    
    void Update ()
    {
        if(collected && ropeConnectionPoint != null)
        {            
            //Update rope render positions
            ropeRenderer.SetPosition(0, new Vector2(joint.connectedBody.position.x, joint.connectedBody.position.y) + (Vector2)joint.connectedAnchor);
            ropeRenderer.SetPosition(1, new Vector2(transform.position.x, transform.position.y) + (Vector2)joint.anchor);
        }
        else
        {
            joint.enabled = false;
            ropeRenderer.enabled = false;     
        }
    }
    
    public void AttachRope(GameObject junkConnectionPoint, float ropeConnectionDistance)
    {
        if(!collected)
        {
            collected = true;
            
            ropeConnectionPoint = junkConnectionPoint;
            
            //Connect to ship
            joint.enabled = true;
            joint.connectedBody = junkConnectionPoint.GetComponent<Rigidbody2D>();
            joint.distance = ropeConnectionDistance;
            
            //Render connection            
            ropeRenderer.enabled = true;
        }
    }
    
    public void ReleaseRope()
    {
        collected = false;
        
        //Reset join
        joint.connectedBody = null;
        joint.enabled = false;
        
        //Reset render
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropeRenderer.enabled = false;       
    }
}
