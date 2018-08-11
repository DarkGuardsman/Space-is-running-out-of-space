using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : CollisionDamage 
{
    public GameObject shooter;
    public bool destroyOnHit = true;
    
	protected override void onHit(DamageData damageData)
    {
        damageData.Attack(damage, shooter);
    }
    
    protected override void onHit(GameObject objectHit)
    {
        if(destroyOnHit)
        {
            Destroy(this.gameObject);
        }
    }
}
