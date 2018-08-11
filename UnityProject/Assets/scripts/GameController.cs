using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Handles game logic
public class GameController : MonoBehaviour 
{
    //Object to spawn for the player
    public GameObject playerPrefab;
    public CinemachineVirtualCamera cinemachineCamera;
    public Transform respawnPoint;
    
    //Current player object
    public GameObject currentPlayerObject;
    
    public Transform centerOfWorld;
    
    //List of all spawned junk objects
    public List<GameObject> junkSpawnedList;
    
    //List of all objects to drop stuff off
    public List<GameObject> dropOffLocations;
    
    //Current player score
    public int points;
    
    //Current number of lives left for player
    public int lives;
    
    //Should we respawn the player
    public bool respawnPlayer = true;
    //Delay for respawn
    public float respawnDelay = 10f;
    
    //Internal timer for respawn
    private float respawnTimer = 0f;
    
    void Update ()
    {
        if(respawnPlayer)
        {
            if(respawnTimer >= respawnDelay)
            {
                SpawnPlayer();
            }
            else
            {
                respawnTimer += Time.deltaTime;
            }
        }
    }
    
    public void AddPoints(int p)
    {
        points += p;
    }
    
    public void KillPlayer()
    {
        Destroy(currentPlayerObject);
        OnPlayerDeath();
    }
    
    public void OnPlayerDeath()
    {
        lives--;
        //TODO show death UI
        //TODO count down to respawn
    }
    
    public void SpawnPlayer()
    {
        respawnPlayer = false;
        respawnTimer = 0f;
        
        //Destroy if already created (fix for janky teleportation)
        if(currentPlayerObject != null)
        {
            Destroy(currentPlayerObject);
        }
        
        //Create player
        currentPlayerObject = (GameObject)Instantiate(playerPrefab);
        
        //Set camera to follow
        cinemachineCamera.Follow = currentPlayerObject.transform;
        
        //Set location and rotation
        currentPlayerObject.transform.position = respawnPoint.position;
        currentPlayerObject.transform.rotation = respawnPoint.rotation;
    }
}
