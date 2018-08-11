using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public GameObject currentPlayerObject;
    public List<GameObject> junkSpawnedList;
    
    public int points;
    
    public int lives;
    
    public void AddPoints(int p)
    {
        points += p;
    }
}
