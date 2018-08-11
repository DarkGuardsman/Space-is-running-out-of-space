using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireWeapon : MonoBehaviour 
{
    public float weaponCooldown = 0.5f;
    public float destroyBulletTimer = 2f;
    public float firingForce = 100f;
    
    public GameObject ship;
    
    public Transform bulletExit;
    
    public GameObject bulletPrefab;
    
    private float weaponTimer = 0f;
	
	// Update is called once per frame
	void Update () 
    {
        if(weaponTimer <= 0)
        {
            if(Input.GetButton("Fire1"))
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
    }
}
