using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkGenerator : MonoBehaviour 
{
    public GameObject[] junkPrefabs;
    
    public float spawnDelay = 1f;
    public float spawnDelayRandom = 2f;
    
    public float spawnCenterDeadZone = 20f;
    
    public float initSpawnAreaSize = 30f;
    public int initSpawnAreaCount = 20;
    
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
        if(gameController.junkSpawnedList.Count < playerOptions.currentSettings.maxJunkSpawn)
        {
            if(spawnTimer <= 0)
            {               
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
        float range = size;
        
        //Spawn some objects closer to start area
        if(initSpawnAreaCount > 0)
        {
            range = initSpawnAreaSize;
        }
        
        float x = Random.Range(-range, range);
        float y = Random.Range(-range, range);
        
        //Is spawn in dead zone area, if so prevent spawning
        if(x > -spawnCenterDeadZone && x < spawnCenterDeadZone && x > -spawnCenterDeadZone && x < spawnCenterDeadZone)
        {
            return;
        }
        
        //Check if player can see the spot, if so prevent spawning
        if(gameController.IsInView(x, y))
        {
            return;
        }
        
        gameController.SpawnJunk(junkPrefabs[(int)Random.Range(0, junkPrefabs.Length - 1)], x, y);        
        //TODO spawn in clusters of random size every so often
        
        //Decrease init spawns
        if(initSpawnAreaCount > 0)
        {
            initSpawnAreaCount--;
        }
        
        //Reset spawn timer
        spawnTimer = spawnDelay + Random.Range(0, spawnDelayRandom);
    }
    
    void OnDrawGizmosSelected() 
    {
        DrawBox(Color.red, size);
        DrawBox(Color.blue, spawnCenterDeadZone);
        DrawBox(Color.blue, initSpawnAreaSize);
    }
    
    void DrawBox(Color color, float size)
    {
        Gizmos.color = color;
        
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
