using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedParticleColorSet : SpawnedEffect 
{
	public override void OnSpawnedCollision(GameObject host, GameObject collisionObject, CollisionDamage collisionDamage,  float collisionSpeed)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if(particleSystem != null)
        {
            //TODO average all sprites
            //TODO maybe add a more exact way to get the color
            SpriteRenderer spriteRenderer = host.GetComponentInChildren<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.startColor = spriteRenderer.color;
            }
            else
            {
                Debug.Log("SpawnedParticleColorSet: Failed to get sprite render from " + host);
            }
        }
        else
        {
            Debug.Log("SpawnedParticleColorSet: Failed to find particle system on " + gameObject);
        }
    }
}
