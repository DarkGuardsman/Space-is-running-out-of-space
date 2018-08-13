using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : MonoBehaviour 
{
    public float health = -1;
    public float maxHealth = 10f;
    
    public bool dead = false;
    public bool destroyOnDeath = true;
    
    public GameObject prefabToSpawnOnDeath;
    
	// Use this for initialization
	public virtual void Start () 
    {
		if(health <= 0)
        {
            health = maxHealth;
        }
	}
    
    public void Attack(float damage, GameObject source)
    {
        Debug.Log("DamageData: Object hit with '" + damage + "' damage from source '" + source +"'");
        health -= damage;
        
        if(health <= 0)
        {
            SetDead();
        }
    }
    
    public void SetDead()
    {
        Debug.Log("DamageData: Setting host dead '" + gameObject + "'");
        dead = true;
         
        OnDeath();
        
        if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnDeath()
    {
        if(prefabToSpawnOnDeath != null)
        {
            Instantiate(prefabToSpawnOnDeath, transform.position, transform.rotation);
        }
    }
}
