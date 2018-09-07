using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDamageData : DamageData 
{
    public int junkToSpawn = 2;
    public GameObject junkPrefab;
    
    protected override void OnDeath()
    {
        base.OnDeath();
        if(junkPrefab != null)
        {
            GameController gameController = FindObjectOfType<GameController>();
            for(int i = 0; i < junkToSpawn; i++)
            {
                gameController.SpawnJunk(junkPrefab, transform.position);
            }
        }
    }
}
