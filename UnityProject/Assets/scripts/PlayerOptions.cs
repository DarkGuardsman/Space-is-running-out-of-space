using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerOptions : MonoBehaviour 
{
	public float arrowMinScale;
    public float arrowMaxScale;
    public float cameraZoom = 8f;
    public float zoomSpeed = 0.1f;
    public int maxJunkSpawn;
    
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
	}	
	
	public void SaveOptions () 
    {
        Debug.Log("PlayerOptions: Saving player prefs");
        PlayerPrefs.SetFloat("arrowMinScale", arrowMinScale);
        PlayerPrefs.SetFloat("arrowMaxScale", arrowMaxScale);
        PlayerPrefs.SetInt("maxJunkSpawn", maxJunkSpawn);
        PlayerPrefs.SetFloat("cameraZoom", cameraZoom);
        
        //Update options, needed for option menu save button
        ApplyOptions();
	}
    
    public void ApplyOptions()
    {
        //Update settings
        cinemachineCamera.m_Lens.OrthographicSize = cameraZoom;
    }
}
