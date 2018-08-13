using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomMotion : MonoBehaviour 
{
    public float randomMovementEnergy = 100f;
    public float randomRotationEnergy = 5f;
    
	// Use this for initialization
	void Start () 
    {
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        
        if(rb != null)
        {
            Vector2 force = new Vector2(Random.Range(-randomMovementEnergy, randomMovementEnergy), Random.Range(-randomMovementEnergy, randomMovementEnergy));
            rb.AddForce(force, ForceMode2D.Impulse);
            
            float torque = Random.Range(-randomRotationEnergy, randomRotationEnergy);
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("GenerateRandomMotion script requires a rigidbody2D to work");
        }
         
        
        //Disable as this only runs once
        enabled = false;
	}
}
