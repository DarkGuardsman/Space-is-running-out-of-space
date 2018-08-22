using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaveHandler : MonoBehaviour 
{   
    //Path to save data to
    public const string gameSavePath = "/saves/";    
    
    //File to save general settings
    public const string playerSettingsName = "player_settings";
    
    //File to save keybinds
    public const string playerControlsName = "player_controls";
    
    ///-------------------------------------------
    
    //Trigger to save game data
    public bool doSaveGameData = false;
    //Trigger to load game data
    public bool doLoadGameData = true;    
    
    //File to save the game data
    public string gameSaveName = "save";
    
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
    
    //File to save game data
    public string getSaveFile()
    {
        return getSaveFolder() + gameSaveName + ".json";
    }
    
    ///-------------------------------------------
    
    //Folder to save all data
    public static string getSaveFolder()
    {
        return getMainFolder() + gameSavePath;
    }
    
     //Folder to save all data
    public static string getMainFolder()
    {
        //saves to on windows -> Users/[Username]/AppData/LocalLow/[CompanyName]/[GameName]
        return Application.persistentDataPath;
    } 
    
    //File to save game data
    public static string getPlayerSettingsFile()
    {
        return getMainFolder() + "/" + playerSettingsName + ".json";
    }
    
    //File to save game data
    public static string getPlayerControlsFile()
    {
        return getMainFolder() + "/" + playerControlsName + ".json";
    }
}
