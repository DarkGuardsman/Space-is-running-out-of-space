using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{    
    public float damage = 1; 
    
    void OnCollisionEnter2D(Collision2D col)
    {   
        DamageData damageData = col.gameObject.GetComponent<DamageData>();
        if (damageData != null) 
        {
            onHit(damageData);
        }            
        onHit(col.gameObject);
    }
    
    protected virtual void onHit(GameObject gameObject)
    {
        
    }
    
    protected virtual void onHit(DamageData damageData)
    {
         damageData.Attack(damage, gameObject);
    }
}
