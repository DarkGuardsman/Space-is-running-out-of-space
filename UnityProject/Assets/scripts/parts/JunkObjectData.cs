using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkObjectData : MonoBehaviour 
{
    public int size = 1;
    public bool collected = false;
    
    public SpringJoint2D joint;
    public LineRenderer ropeRenderer;
    public GameObject ropeConnectionPoint;
    
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
    }
    
    public void Collect(GameObject junkConnectionPoint, float ropeConnectionDistance)
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
    
    public void Release()
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
