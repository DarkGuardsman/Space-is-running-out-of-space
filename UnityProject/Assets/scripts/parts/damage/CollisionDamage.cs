using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : SceneObject
{    
    public float minDamage = 1f;
    public float maxDamage = 5f;
    
    public float collisionMinSpeed = 1f;
    public float collisionMaxSpeed = 10f;
    
    public GameObject collisionSpawnPrefab;
    
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
        
        //Spawn effects for collision
        if(collisionSpawnPrefab != null && AreEffectsEnabled())
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Vector3 point = new Vector3(hit.point.x, hit.point.y, 0);
                
                //Only spawn if the player will see it
                if(IsInView(point))
                {
                    GameObject spawnedObject = Instantiate(collisionSpawnPrefab, point, Quaternion.identity); //TODO see if we can get a rotation vector
                    OnCollisionObjectSpawned(spawnedObject, collision.gameObject, collisionSpeed);
                }
            }
        }
    }
    
    protected virtual void OnCollisionObjectSpawned(GameObject spawnedObject, GameObject collisionObject, float collisionSpeed)
    {
        SpawnedEffect[] effects = spawnedObject.GetComponents<SpawnedEffect>();
        if(effects != null)
        {
            foreach (SpawnedEffect effect in effects)
            {
                effect.OnSpawnedCollision(gameObject, collisionObject, this, collisionSpeed);
            }
        }
    }
    
    protected virtual void onHit(GameObject gameObject, float collisionSpeed)
    {
        
    }
    
    protected virtual void onHit(DamageData damageData, float collisionSpeed)
    {
        collisionSpeed = Mathf.Abs(collisionSpeed);
        if(collisionSpeed > collisionMinSpeed)
        {            
            float damage = ScaleDamageForSpeed(collisionSpeed);
            Debug.Log("CollisionDamage: Impacting '" + damageData.gameObject + "' for '" + damage +"' damage points with a speed of " + collisionSpeed);
            //Attack entity
            damageData.Attack(damage, gameObject);
        }
    }
    
    //Scale damage to speed
    protected float ScaleDamageForSpeed(float collisionSpeed)
    {
        return minDamage + (ScaleForSpeed(collisionSpeed) * (maxDamage - minDamage));
    }
    
    public float ScaleForSpeed(float collisionSpeed)
    {
        return Mathf.Min(1, collisionSpeed / collisionMaxSpeed);
    }
}
