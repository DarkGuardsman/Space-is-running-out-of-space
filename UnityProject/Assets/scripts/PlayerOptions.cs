using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;

public class PlayerOptions : MonoBehaviour 
{        
    public PlayerOptionData currentSettings = new PlayerOptionData();
    public PlayerOptionData defaultSettings = new PlayerOptionData();
    
    private GameController gameController;
    private CinemachineVirtualCamera cinemachineCamera;
    public DataSaveHandler dataSaveHandler;
    
    // Use this for initialization
	void Start () 
    {
		gameController = FindObjectOfType<GameController>();
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        dataSaveHandler = FindObjectOfType<DataSaveHandler>();

        ApplyOptions();
	}
    
    void Awake()
    {
        LoadOptions();        
    }
    
	public void LoadOptions () 
    {
        Debug.Log("PlayerOptions: Loading player settings");
        LoadFromDisc();
	}	
	
	public void SaveOptions () 
    {
        Debug.Log("PlayerOptions: Saving player settings");
        SaveToDisc();
        
        //Update options, needed for option menu save button
        ApplyOptions();
	}
    
    //Loads game data
    public void LoadFromDisc()
    {
        //Create folder
        string saveFolder = dataSaveHandler.getMainFolder();
        if(!File.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder); 
        }
        
        //Find save
        string filePath = dataSaveHandler.getPlayerSettingsFile();        
        if (File.Exists (filePath)) 
        {
            //Read JSON
            string dataAsJson = File.ReadAllText (filePath);
            
            //Convert JSON to data object
            currentSettings = JsonUtility.FromJson<PlayerOptionData> (dataAsJson);
        } 
        else 
        {
            currentSettings = new PlayerOptionData();
            defaultSettings.CopyInto(currentSettings);
        }
    }
    
    //Saves game data
    public void SaveToDisc()
    {
        //Create folder
        string saveFolder = dataSaveHandler.getMainFolder();
        if(!File.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder); 
        }
        
        //Convert to JSON
        string dataAsJson = JsonUtility.ToJson (currentSettings, true);
        
        //Save data
        File.WriteAllText (dataSaveHandler.getPlayerSettingsFile(), dataAsJson);
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
