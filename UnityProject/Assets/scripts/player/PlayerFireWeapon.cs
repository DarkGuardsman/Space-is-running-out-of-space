﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireWeapon : PlayerControls
{
    public float weaponCooldown = 0.5f;
    public float destroyBulletTimer = 2f;
    public float firingForce = 100f;
    
    public GameObject ship;
    
    public Transform bulletExit;
    
    public GameObject bulletPrefab;
    
    private AudioSource weaponAudio;
    
    private float weaponTimer = 0f;
    
    public override void Start () 
    {
        base.Start();
        weaponAudio = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(weaponTimer <= 0)
        {
            if(ShouldFire())
            {
                weaponTimer = weaponCooldown;
                FireWeapon();
            }
        }
        else
        {
            weaponTimer -= Time.deltaTime;
        }
	}
    
    bool ShouldFire()
    {
        return inputManager.getInputActions().shoot.IsKeyDown() || Input.GetButton("Fire1");
    }
    
    void FireWeapon ()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletExit.position,
            bulletExit.rotation);
        
        //Set shooter
        bullet.GetComponent<Bullet>().shooter = ship;
            
        // Add velocity to the bullet
        Rigidbody2D bulletPhysicsBody = bullet.GetComponent<Rigidbody2D>();
        bulletPhysicsBody.AddForce(bullet.transform.up * firingForce);            

        // Destroy the bullet after a set time
        Destroy(bullet, destroyBulletTimer);
        
        //Play audio
        weaponAudio.Play(0);
    }
}
