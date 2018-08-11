using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkGenerator : MonoBehaviour 
{
    public GameObject smallJunkPrefab;
    
    public int maxJunkCount = 100;
    public float spawnDelay = 1f;
    
    public float size = 20f;
    
	private GameController gameController;
    
    private float spawnTimer;
    
	// Use this for initialization
	void Start () 
    {
		gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(gameController.junkSpawnedList.Count <= maxJunkCount)
        {
            if(spawnTimer <= 0)
            {
                spawnTimer = spawnDelay;
                SpawnJunk();
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
	}
    
    void SpawnJunk()
    {
        float x = Random.Range(-size, size);
        float y = Random.Range(-size, size);
        
        gameController.SpawnJunk(smallJunkPrefab, x, y);
        //TODO select size random
        //TODO set into motion
    }
    
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        
        Vector3 center = transform.position;
        
        //Left side
        Gizmos.DrawLine(center + new Vector3(-size, -size, 0), transform.position + new Vector3(-size, size, 0)); 
        
        //Right side
        Gizmos.DrawLine(center + new Vector3(size, -size, 0), transform.position + new Vector3(size, size, 0)); 
        
        //Top side
        Gizmos.DrawLine(center + new Vector3(-size, size, 0), transform.position + new Vector3(size, size, 0));
        
         //Top side
        Gizmos.DrawLine(center + new Vector3(-size, -size, 0), transform.position + new Vector3(size, -size, 0));
    }
}
