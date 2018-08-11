using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollectionSprite : MonoBehaviour 
{
    public CollectionArea pickupArea;
    public SpriteRenderer spriteRenderer;
	
	// Update is called once per frame
	void Update () 
    {
		if(pickupArea.objectsInArea.Count > 0)
        {
            spriteRenderer.enabled = true; 
        }
        else
        {
            spriteRenderer.enabled = false; 
        }
	}
}
