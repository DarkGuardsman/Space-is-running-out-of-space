using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    protected PlayerInputManager inputManager;
    
	// Use this for initialization
	public virtual void Start () 
    {
		inputManager = FindObjectOfType<PlayerInputManager>();
	}
}
