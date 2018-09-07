using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour 
{
    public float time = 1f;
	
	// Update is called once per frame
	void Update () 
    {
		if(time <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            time -= Time.deltaTime;
        }
	}
}
