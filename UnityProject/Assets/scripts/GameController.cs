using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

//Handles game logic
public class GameController : MonoBehaviour 
{
    public float sizeOfWorld = 250f;
    
    //Object to spawn for the player
    public GameObject playerPrefab;
    //Prefab for direction arrows
    public GameObject arrowPrefabDropLocation;
    public GameObject arrowPrefabJunk;
    
    public GameObject arrowParent;
    
    //Camera brain
    public CinemachineVirtualCamera cinemachineCamera;
    
    //Respawn point
    public Transform respawnPoint;
    
    //Current player object
    public GameObject currentPlayerObject;
    
    public Transform centerOfWorld;
    
    //List of all spawned junk objects
    public List<GameObject> junkSpawnedList = new List<GameObject>();
    
    //List of all objects to drop stuff off
    public List<GameObject> dropOffLocations = new List<GameObject>();
    
    Dictionary<GameObject, GameObject> objectToArrow = new Dictionary<GameObject, GameObject>();
    
    //Current player score
    public int points;
    
    //Current number of lives left for player
    public int lives;
    
    //Should we respawn the player
    public bool respawnPlayer = true;
    //Delay for respawn
    public float respawnDelay = 10f;
    
    //Internal timer for respawn
    public float respawnTimer = 0f;   
    
    public bool gameOver = false;
    
    public float gameTimeScale = 1;
    
    void Start ()
    {
        foreach (GameObject dropLocation in dropOffLocations)
        {
            GenerateArrow(dropLocation, arrowPrefabDropLocation);
        }
    }
    
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
        else if(currentPlayerObject == null)
        {
            respawnPlayer = true;
        }
    }
    
    public void SpawnJunk(GameObject prefab, float x, float y)
    {
        SpawnJunk(prefab, ToGamePosition(x, y));
    }
    
    public void SpawnJunk(GameObject prefab, Vector3 position)
    {
        //Create
        GameObject junkObject = (GameObject)Instantiate(prefab);
        
        //Set position
        junkObject.transform.position = position;
        
        //Track
        junkSpawnedList.Add(junkObject);
        
        //Generator arrow
        GenerateArrow(junkObject, arrowPrefabJunk);        
        
        //TODO scale random
        //TODO color random
        //TODO set into motion with random direction and rotation
        //TODO set to destroy or bounce if goes out of map
    }
    
    public Vector3 ToGamePosition(float x, float y)
    {
        return centerOfWorld.position + new Vector3(x, y, 0);
    }
    
    public bool IsInView(float x, float y)
    {
        //Convert target position to camera space
        Vector3 pos = Camera.main.WorldToViewportPoint(ToGamePosition(x, y));
        
        //Check if visible
        return pos.x >= 0.0f && pos.x <= 1.0f && pos.y >= 0.0f && pos.y <= 1.0f;
    }
    
    public void GenerateArrow(GameObject gameObject, GameObject arrowPrefab)
    {
        //Create arrow
        GameObject arrowObject = (GameObject)Instantiate(arrowPrefab);
        arrowObject.transform.parent = arrowParent.transform;
        arrowObject.transform.localPosition = Vector3.zero;
        
        //Assign target
        ArrowObjective arrowObjective = arrowObject.GetComponent<ArrowObjective>();
        arrowObjective.aimTarget = gameObject.transform;
        
        //Add to dictionary
        objectToArrow.Add(gameObject, arrowObject);
    }
    
    public void DestroyJunk(GameObject gameObject)
    {
        if (objectToArrow.ContainsKey(gameObject))
        {
            objectToArrow.Remove(gameObject);
        }
        junkSpawnedList.Remove(gameObject);
        Destroy(gameObject);
    }
    
    public void AddPoints(int p)
    {
        points += p;
    }
    
    public void KillPlayer()
    {
        Destroy(currentPlayerObject);
        currentPlayerObject = null;
        OnPlayerDeath();
    }
    
    public void OnPlayerDeath()
    {
        lives--;
        respawnPlayer = true;
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
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene ().name);
    }
}
