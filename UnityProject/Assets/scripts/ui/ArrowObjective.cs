using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowObjective : UIDisplay 
{
    public Transform aimTarget;
    public SpriteRenderer spriteRenderer;
    
    public float circleSize = 0.5f;
    public float minScale = 0.3f;
    public float scaleGrowth = 1f;
    public float maxScale = 1f;
	
	// Update is called once per frame
	void Update () 
    {
        //Credit: https://answers.unity.com/questions/384074/how-to-make-a-2d-gui-arrow-point-at-a-3d-object.html
        
        //Disable sprite render
		spriteRenderer.enabled = false; 
        
        if(aimTarget != null && gameController.currentPlayerObject != null)
        {         
            //Get player and target position
            Vector3 targetPosition = aimTarget.transform.position;
            Vector3 playerPos = gameController.currentPlayerObject.transform.position;
            
            //Convert target position to camera space
            Vector3 pos = Camera.main.WorldToViewportPoint(targetPosition);
            
            //Get magnitude for scale
            float distance = Mathf.Abs(Vector3.Distance(targetPosition, playerPos));
             
            //Check if behind camera
            if (pos.z < Camera.main.nearClipPlane)
                return;
             
            //Check if visible
            if (pos.x >= 0.0f && pos.x <= 1.0f && pos.y >= 0.0f && pos.y <= 1.0f)
                return;
                     
            //Enable render
            spriteRenderer.enabled = true; 
            
            //Center
            pos.x -= 0.5f;
            pos.y -= 0.5f; 
            pos.z = 0;
            
            //Set rotation towards target
            float fAngle = Mathf.Atan2 (pos.x, pos.y);
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);
            
            //Set position near edge of screen
            pos.x = circleSize * Mathf.Sin (fAngle);  // Place on ellipse touching 
            pos.y = circleSize * Mathf.Cos (fAngle);  //   side of viewport
            pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
            
            //Get camera size
            float height = Camera.main.orthographicSize * 2f;
            float width = height * Camera.main.aspect;
            
            //Scale position to camera size
            pos.x *= width;
            pos.y *= height;
            
            //Set position
            transform.localPosition = pos;
            
            //Scale based on distance
            float scale = minScale + (maxScale - Mathf.Min(distance * scaleGrowth, maxScale));
            transform.localScale = new Vector3( scale, scale, 1);
        }
	}
}
