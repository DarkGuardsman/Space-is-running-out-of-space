using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : MonoBehaviour 
{
    public float health = -1;
    public float maxHealth = 10f;
    
    public bool dead = false;
    
	// Use this for initialization
	void Start () 
    {
		if(health <= 0)
        {
            health = maxHealth;
        }
	}
    
    public void Attack(float damage, GameObject source)
    {
        health -= damage;
        
        if(health <= 0)
        {
            OnDeath();
        }
    }
    
    void OnDeath()
    {
        dead = true;
    }
}
