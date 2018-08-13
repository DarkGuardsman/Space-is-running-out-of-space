using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomScale : MonoBehaviour 
{
    public float scale = 0.2f;
    
	// Use this for initialization
	void Start () 
    {
        gameObject.transform.localScale += new Vector3(Random.Range(-scale, scale), Random.Range(-scale, scale), 0);
        
		//Disable as this only runs once
        enabled = false;
	}
}
