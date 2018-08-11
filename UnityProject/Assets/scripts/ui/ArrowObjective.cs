using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowObjective : MonoBehaviour {

    public Transform aimTarget;
    public SpriteRenderer spriteRenderer;
    
    public float circleSize = 0.5f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        //https://answers.unity.com/questions/384074/how-to-make-a-2d-gui-arrow-point-at-a-3d-object.html
		spriteRenderer.enabled = false; 
         
        Vector3 v3Pos = Camera.main.WorldToViewportPoint(aimTarget.transform.position);
         
        if (v3Pos.z < Camera.main.nearClipPlane)
            return;  // Object is behind the camera
         
        if (v3Pos.x >= 0.0f && v3Pos.x <= 1.0f && v3Pos.y >= 0.0f && v3Pos.y <= 1.0f)
            return; // Object center is visible
                 
        spriteRenderer.enabled = true; 
        v3Pos.x -= 0.5f;  // Translate to use center of viewport
        v3Pos.y -= 0.5f; 
        v3Pos.z = 0;      // I think I can do this rather than do a 
                           //   a full projection onto the plane
         
        float fAngle = Mathf.Atan2 (v3Pos.x, v3Pos.y);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);
         
        v3Pos.x = circleSize * Mathf.Sin (fAngle);  // Place on ellipse touching 
        v3Pos.y = circleSize * Mathf.Cos (fAngle);  //   side of viewport
        v3Pos.z = Camera.main.nearClipPlane + 0.01f;  // Looking from neg to pos Z;
        
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        
        v3Pos.x *= width;
        v3Pos.y *= height;
        
        transform.localPosition = v3Pos;
	}
}
