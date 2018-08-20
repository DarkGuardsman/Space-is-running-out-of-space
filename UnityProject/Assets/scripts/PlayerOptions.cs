using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerOptions : MonoBehaviour 
{    
    public PlayerOptionData currentSettings = new PlayerOptionData();
    public PlayerOptionData defaultSettings = new PlayerOptionData();
    
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
            currentSettings.arrowMinScale = PlayerPrefs.GetFloat("arrowMinScale");
            Debug.Log("PlayerOptions: Arrow Min -> " + currentSettings.arrowMinScale);
        }
        if(PlayerPrefs.HasKey("arrowMaxScale"))
        {
            currentSettings.arrowMaxScale = PlayerPrefs.GetFloat("arrowMaxScale");
            Debug.Log("PlayerOptions: Arrow Max -> " + currentSettings.arrowMaxScale);
        }
        if(PlayerPrefs.HasKey("maxJunkSpawn"))
        {
            currentSettings.maxJunkSpawn = PlayerPrefs.GetInt("maxJunkSpawn");
            Debug.Log("PlayerOptions: Junk Count -> " + currentSettings.maxJunkSpawn);
        }
        if(PlayerPrefs.HasKey("cameraZoom"))
        {
            //cameraZoom = PlayerPrefs.GetFloat("cameraZoom");
            //Debug.Log("PlayerOptions: Camera Zoom -> " + maxJunkSpawn);
        }
        
        currentSettings.enableEffects = GetBool("enableEffects", currentSettings.enableEffects);
        currentSettings.enableShipTrail = GetBool("enableShipTrail", currentSettings.enableShipTrail);
        currentSettings.enableBulletTrail = GetBool("enableBulletTrail", currentSettings.enableBulletTrail);
	}	
	
	public void SaveOptions () 
    {
        Debug.Log("PlayerOptions: Saving player prefs");
        PlayerPrefs.SetFloat("arrowMinScale", currentSettings.arrowMinScale);
        PlayerPrefs.SetFloat("arrowMaxScale", currentSettings.arrowMaxScale);
        PlayerPrefs.SetInt("maxJunkSpawn", currentSettings.maxJunkSpawn);
        //PlayerPrefs.SetFloat("cameraZoom", cameraZoom);
        
        SetBool("enableEffects", currentSettings.enableEffects);
        SetBool("enableShipTrail", currentSettings.enableShipTrail);
        SetBool("enableBulletTrail", currentSettings.enableBulletTrail);
        
        //Update options, needed for option menu save button
        ApplyOptions();
	}
    
    public void ApplyOptions()
    {
        //Update settings
        cinemachineCamera.m_Lens.OrthographicSize = currentSettings.cameraZoom;
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
