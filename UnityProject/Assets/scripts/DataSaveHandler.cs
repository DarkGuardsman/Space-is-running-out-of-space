using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaveHandler : MonoBehaviour 
{
    //Trigger to save game data
    public bool doSaveGameData = false;
    //Trigger to load game data
    public bool doLoadGameData = true;
    
    //Path to save data to
    public string gameSavePath = "/saves/";
    
    //File to save the game data
    public string gameSaveName = "save";
    
    //File to save general settings
    public string playerSettingsName = "player_settings";
    
    //File to save keybinds
    public string playerControlsName = "player_controls";
    
	// Update is called once per frame
	void FixedUpdate()
    {
        if(doLoadGameData)
        {
            doLoadGameData = false;
            //loadGameData();
        }
		if(doSaveGameData)
        {
            doSaveGameData = false;
            //saveGameData();
        }
	}
    
    //Folder to save all data
    public string getSaveFolder()
    {
        return getMainFolder() + gameSavePath;
    }
    
     //Folder to save all data
    public string getMainFolder()
    {
        //saves to on windows -> Users/[Username]/AppData/LocalLow/[CompanyName]/[GameName]
        return Application.persistentDataPath;
    } 
    
    //File to save game data
    public string getSaveFile()
    {
        return getSaveFolder() + gameSaveName + ".json";
    }
    
    //File to save game data
    public string getPlayerSettingsFile()
    {
        return getMainFolder() + "/" + playerSettingsName + ".json";
    }
    
    //File to save game data
    public string getPlayerControlsFile()
    {
        return getMainFolder() + "/" + playerControlsName + ".json";
    }
}
