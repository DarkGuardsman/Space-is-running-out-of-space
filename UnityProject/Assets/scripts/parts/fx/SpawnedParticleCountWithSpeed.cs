using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedParticleCountWithSpeed : SpawnedEffect 
{
    public float minCount = 10;
    public float maxCount = 40;
    
	public override void OnSpawnedCollision(GameObject host, GameObject collisionObject, CollisionDamage collisionDamage,  float collisionSpeed)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if(particleSystem != null)
        {
            float count = Mathf.Min(minCount, collisionDamage.ScaleForSpeed(collisionSpeed) * maxCount);
            
            ParticleSystem.EmissionModule emission = particleSystem.emission;
            emission.rateOverTime = count;
            particleSystem.maxParticles = (int)count;
        }
        else
        {
            Debug.Log("SpawnedParticleColorSet: Failed to find particle system on " + gameObject);
        }
    }
}
