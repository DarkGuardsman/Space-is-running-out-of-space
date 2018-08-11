using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionArea : MonoBehaviour 
{
    public List<GameObject> objectsInArea = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == gameObject.layer)
        {
            objectsInArea.Add(other.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == gameObject.layer)
        {
            objectsInArea.Remove(other.gameObject);
        }
    }
}
