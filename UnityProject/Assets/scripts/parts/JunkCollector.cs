using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkCollector : MonoBehaviour 
{
    public CollectionArea pickupArea;
    public GameController gameController;
    public int collectionTick = 5;
    
    void Start ()
    {
        gameController = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		if(pickupArea.objectsInArea != null)
        {
            for(int i = 0; i < pickupArea.objectsInArea.Count; i++)
            {
                GameObject gameObject = pickupArea.objectsInArea[i];
                if(gameObject != null)
                {
                    JunkObjectData junkData = gameObject.GetComponent<JunkObjectData>();
                    if(junkData != null && !junkData.collected)
                    {
                        if(junkData.collectTimer++ >= collectionTick)
                        {
                            gameController.AddPoints(junkData.pointValue);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
	}
}
