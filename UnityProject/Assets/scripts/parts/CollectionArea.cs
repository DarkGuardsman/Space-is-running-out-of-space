using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArea : MonoBehaviour 
{
    public int layerToCheck;
    public List<GameObject> objectsInArea = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == layerToCheck)
        {
            objectsInArea.Add(other.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == layerToCheck)
        {
            objectsInArea.Remove(other.gameObject);
        }
    }
}
