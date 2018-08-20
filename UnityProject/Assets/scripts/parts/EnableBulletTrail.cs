﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBulletTrail : MonoBehaviour 
{
    public PlayerOptions playerOptions;
    public TrailRenderer trailRenderer;
    
	// Use this for initialization
	void Start () 
    {
		playerOptions = FindObjectOfType<PlayerOptions>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		trailRenderer.enabled = playerOptions.currentSettings.enableBulletTrail;
	}
}
