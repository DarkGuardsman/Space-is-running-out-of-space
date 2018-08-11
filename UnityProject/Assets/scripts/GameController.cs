using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public GameObject currentPlayerObject;
    public int points;
    
    public void AddPoints(int p)
    {
        points += p;
    }
}
