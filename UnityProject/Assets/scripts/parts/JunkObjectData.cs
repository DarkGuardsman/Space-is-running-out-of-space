using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkObjectData : MonoBehaviour 
{
    public bool scaleWithSize = true;
    public bool scaleWithDamage = true;
     //Size of the junk
    public int minSize = 1;
    public int currentSize = 1;
    public int maxSize = 3;   
    
    public float scaleFactor = 1f;
    
    public float minScale = 0.5f;
    public float maxScale = 2f;
   
    //Point value of the junk
    public int pointValue = 1;
    
    //Is the junk currently collected by a player
    public bool collected = false;
    
    public SpringJoint2D joint;
    public LineRenderer ropeRenderer;
    public GameObject ropeConnectionPoint;
    public DamageData damageData;
    
    //Used by drop off point to prevent instant collection
    public int collectTimer = 0;
    
    private int prevSize;
    private float prevHealth;
    
    void Start ()
    {
        joint = gameObject.GetComponent<SpringJoint2D>();
        ropeRenderer = gameObject.GetComponent<LineRenderer>();
        damageData = gameObject.GetComponent<DamageData>();
        
        currentSize = minSize + Random.Range(0, maxSize - minSize);
    }
    
    void Update ()
    {
        ScaleJunk();
        UpdateConnections();        
    }
    
    void UpdateConnections()
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
    
    void ScaleJunk()
    {
        if((scaleWithSize || scaleWithDamage) && (prevSize != transform.localScale.x || prevHealth != damageData.health))
        {
            float hpPercentage = damageData.health / damageData.maxHealth;
            float sizeScale = (float)currentSize / (float) maxSize;
            
            if(scaleWithDamage)
            {
                //Update size
                currentSize = minSize + (int)Mathf.Floor(((maxSize - minSize) * hpPercentage));
                
                //Slightly increase size scale to match % missing hp
                sizeScale += (currentSize % (float)maxSize) * hpPercentage;
                
                //if size scale disabled but no hp scale, set scale to hp
                if(!scaleWithSize)
                {
                    sizeScale = hpPercentage;
                }
            }  
            
            //Clamp
            sizeScale = Mathf.Max(minScale, sizeScale);
            sizeScale = Mathf.Min(maxScale, sizeScale);
            
            sizeScale *= scaleFactor;
            
            //Set new scale
            transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
            
            //Save values
            prevSize = currentSize;
            prevHealth = damageData.health;
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
