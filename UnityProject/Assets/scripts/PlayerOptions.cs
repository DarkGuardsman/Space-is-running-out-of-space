using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerOptions : MonoBehaviour 
{
	public float arrowMinScale = 0.3f;
    public float arrowMaxScale = 3f;
    
    public float cameraZoom = 8f;
    public float zoomSpeed = 0.1f;
    public int maxJunkSpawn = 1000;
    
    public bool enableEffects = true;
    public bool enableShipTrail = true;
    public bool enableBulletTrail = true;
    
    private GameController gameController;
    private CinemachineVirtualCamera cinemachineCamera;
    
    // Use this for initialization
	void Start () 
    {
		gameController = FindObjectOfType<GameController>();
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();

        ApplyOptions();
	}
    
    void Awake()
    {
        LoadOptions();        
    }
    
	public void LoadOptions () 
    {
        Debug.Log("PlayerOptions: Loading player prefs");
        if(PlayerPrefs.HasKey("arrowMinScale"))
        {
            arrowMinScale = PlayerPrefs.GetFloat("arrowMinScale");
            Debug.Log("PlayerOptions: Arrow Min -> " + arrowMinScale);
        }
        if(PlayerPrefs.HasKey("arrowMaxScale"))
        {
            arrowMaxScale = PlayerPrefs.GetFloat("arrowMaxScale");
            Debug.Log("PlayerOptions: Arrow Max -> " + arrowMaxScale);
        }
        if(PlayerPrefs.HasKey("maxJunkSpawn"))
        {
            maxJunkSpawn = PlayerPrefs.GetInt("maxJunkSpawn");
            Debug.Log("PlayerOptions: Junk Count -> " + maxJunkSpawn);
        }
        if(PlayerPrefs.HasKey("cameraZoom"))
        {
            cameraZoom = PlayerPrefs.GetFloat("cameraZoom");
            Debug.Log("PlayerOptions: Camera Zoom -> " + maxJunkSpawn);
        }
        
        enableEffects = GetBool("enableEffects", enableEffects);
        enableShipTrail = GetBool("enableShipTrail", enableShipTrail);
        enableBulletTrail = GetBool("enableBulletTrail", enableBulletTrail);
	}	
	
	public void SaveOptions () 
    {
        Debug.Log("PlayerOptions: Saving player prefs");
        PlayerPrefs.SetFloat("arrowMinScale", arrowMinScale);
        PlayerPrefs.SetFloat("arrowMaxScale", arrowMaxScale);
        PlayerPrefs.SetInt("maxJunkSpawn", maxJunkSpawn);
        PlayerPrefs.SetFloat("cameraZoom", cameraZoom);
        
        SetBool("enableEffects", enableEffects);
        SetBool("enableShipTrail", enableShipTrail);
        SetBool("enableBulletTrail", enableBulletTrail);
        
        //Update options, needed for option menu save button
        ApplyOptions();
	}
    
    public void ApplyOptions()
    {
        //Update settings
        cinemachineCamera.m_Lens.OrthographicSize = cameraZoom;
    }
    
    bool GetBool(string key, bool defaultValue)
    {
        if(PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key) == 1 ? true : false;
        }
        return defaultValue;
    }
    
    void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
}
