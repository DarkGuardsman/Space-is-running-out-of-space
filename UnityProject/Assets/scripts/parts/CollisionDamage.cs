using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{    
    public float minDamage = 1f;
    public float maxDamage = 5f;
    
    public float collisionMinSpeed = 1f;
    public float collisionMaxSpeed = 10f;
    
    void OnCollisionEnter2D(Collision2D collision)
    {   
        //Ignore low speed impacts
        float collisionSpeed = collision.relativeVelocity.magnitude;
        
        //Only harm if we hit a damage entity
        DamageData damageData = collision.gameObject.GetComponent<DamageData>();
        if (damageData != null) 
        {
            //Trigger damage impact
            onHit(damageData, collisionSpeed);
        }  

        //Trigger normal impact
        onHit(collision.gameObject, collisionSpeed);
    }
    
    protected virtual void onHit(GameObject gameObject, float collisionSpeed)
    {
        
    }
    
    protected virtual void onHit(DamageData damageData, float collisionSpeed)
    {
        collisionSpeed = Mathf.Abs(collisionSpeed);
        if(collisionSpeed > collisionMinSpeed)
        {            
            //Attack entity
            damageData.Attack(ScaleDamageForSpeed(collisionSpeed), gameObject);
        }
    }
    
    //Scale damage to speed
    protected float ScaleDamageForSpeed(float collisionSpeed)
    {
        return minDamage + (Mathf.Min(1, collisionSpeed / collisionMaxSpeed) * (maxDamage - minDamage));
    }
}
