using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollectionSprite : MonoBehaviour 
{
    public CollectionArea pickupArea;
    public SpriteRenderer[] spriteRenderers;
	
	// Update is called once per frame
	void Update () 
    {
		if(pickupArea.objectsInArea.Count > 0)
        {
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true; 
            }
        }
        else
        {
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = false; 
            }
        }
	}
}
