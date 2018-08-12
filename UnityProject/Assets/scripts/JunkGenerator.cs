﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkGenerator : MonoBehaviour 
{
    public GameObject[] junkPrefabs;
    
    public float spawnDelay = 1f;
    
    public float size = 20f;
    
	private GameController gameController;
    private PlayerOptions playerOptions;
    
    private float spawnTimer;
    
	// Use this for initialization
	void Start () 
    {
		gameController = FindObjectOfType<GameController>();
        playerOptions = FindObjectOfType<PlayerOptions>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(gameController.junkSpawnedList.Count < playerOptions.maxJunkSpawn)
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
        
        gameController.SpawnJunk(junkPrefabs[(int)Random.Range(0, junkPrefabs.Length - 1)], x, y);
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
