using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CollisionDamage 
{
    public GameObject shooter;
    public bool destroyOnHit = true;
    
	protected override void onHit(DamageData damageData, float collisionSpeed)
    {
        float damage = ScaleDamageForSpeed(collisionSpeed);
        Debug.Log("Bullet: Impacting '" + damageData.gameObject + "' for '" + damage +"' damage points");
        
        damageData.Attack(damage, shooter);
    }
    
    protected override void onHit(GameObject objectHit, float collisionSpeed)
    {
        if(destroyOnHit)
        {
            Destroy(this.gameObject);
        }
    }
}
