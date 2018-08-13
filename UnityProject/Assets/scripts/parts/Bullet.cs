using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CollisionDamage 
{
    public GameObject shooter;
    public bool destroyOnHit = true;
    
	protected override void onHit(DamageData damageData, float collisionSpeed)
    {
        damageData.Attack(ScaleDamageForSpeed(collisionSpeed), shooter);
    }
    
    protected override void onHit(GameObject objectHit, float collisionSpeed)
    {
        if(destroyOnHit)
        {
            Destroy(this.gameObject);
        }
    }
}
