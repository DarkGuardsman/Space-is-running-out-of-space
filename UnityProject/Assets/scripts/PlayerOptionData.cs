using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All player settings
[System.Serializable]
public class PlayerOptionData
{
    public float arrowMinScale = 0.3f;
    public float arrowMaxScale = 3f;
    
    public float cameraZoom = 8f;
    public float zoomSpeed = 0.1f;
    
    public int maxJunkSpawn = 1000;
    
    public bool enableEffects = true;
    public bool enableShipTrail = true;
    public bool enableBulletTrail = true;
    public bool enableShipBasedMovement = true;	
    public bool enableMouseAim = true;	
    
    public void CopyInto(PlayerOptionData data)
    {
        data.arrowMinScale = arrowMinScale;
        data.arrowMaxScale = arrowMaxScale;
        
        data.cameraZoom = cameraZoom;
        data.zoomSpeed = zoomSpeed;
        
        data.maxJunkSpawn = maxJunkSpawn;
        
        data.enableEffects = enableEffects;
        data.enableShipTrail = enableShipTrail;
        data.enableBulletTrail = enableBulletTrail;
        data.enableShipBasedMovement = enableShipBasedMovement;
        data.enableMouseAim = enableMouseAim;
    }
}
