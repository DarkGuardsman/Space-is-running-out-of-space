using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour 
{
    //Singleton for input managment
    public static PlayerInputManager instance;

    //Reference object for action keys
    [SerializeField]
    private InputActionHolder currentInputActions = new InputActionHolder(); 
    [SerializeField]
    private InputActionHolder defaultInputActions = new InputActionHolder(); 
    
    //List of action keys (mainly used for UI)
    public List<InputAction> actionList = new List<InputAction>();	
    
    //Are we assigning a key?
    public bool assignKey = false;
    //Primary (true) or secondary (false) key
    public bool assignPrimaryKey = false;
    //Input action (key) being assigned
    public InputAction assignAction;
    
    // Use this for initialization
	void Start () 
    {
        
	}
    
	void Awake () 
    {
        //Setup first time instance
		if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        //Destory if we make a duplicate
        else
        {
            Destroy(gameObject);
        }
        
        //Load keys
        LoadKeys();
	}
    
    //Called to load action inputs and player settings
    void LoadKeys()
    {
        //Load from disc
        LoadFromDisc();
    }
	
	// Update is called once per frame
	void Update () 
    {
        //start looking for a key
		if(assignKey)
        {
            //Loop key codes looking for match
            foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                //If key is active, its a match
                if (Input.GetKey(keyCode))
                {
                    //Log key change
                    Debug.Log("Assigning: KeyCode '" + keyCode + "' for input action '" + assignAction.displayName + "'");
                    
                    //Stop searching for key
                    assignKey = false;
                    
                    //Assign key
                    if(assignPrimaryKey)
                    {
                        if(keyCode == KeyCode.Escape)
                        {
                            assignAction.primary = KeyCode.None;
                        }
                        else
                        {
                            assignAction.primary = keyCode;
                        }
                    }
                    else
                    {
                       if(keyCode == KeyCode.Escape)
                        {
                            assignAction.secondary = KeyCode.None;
                        }
                        else
                        {
                            assignAction.secondary = keyCode;
                        }
                    }
                    
                    //TODO save change
                }
            }
        }
	}
    
    public void SetCurrentKeybindHolder(InputActionHolder inputActions)
    {
        Debug.Log("PlayerInputManager: Setting current input action holder '" + inputActions + "'");
        currentInputActions = inputActions;
        
        //Fix any issues
        Debug.Log("PlayerInputManager: Checking for issues");
        bool hadIssues = currentInputActions.CheckForIssues(defaultInputActions);
        if(!hadIssues)
        {
            Debug.Log("PlayerInputManager: No issues found with input action holder");
        }
        
        //Build action key list
        actionList.Clear();
        currentInputActions.CollectActionInputs(actionList);
        
        //Save a new copy to disk if we had issues that were fixed
        if(hadIssues)
        {
            Debug.Log("PlayerInputManager: Due to issues found saving new player_controls copy to disk");
            SaveToDisc();
        }
    }
    
    public InputActionHolder getInputActions()
    {
        if(currentInputActions == null)
        {
            currentInputActions = new InputActionHolder();
        }
        return currentInputActions;
    }
    
    public bool StartAssignKey(InputAction assignAction, bool assignPrimaryKey)
    {
        if(!assignKey && assignAction != null)
        {
            assignKey = true;
            this.assignAction = assignAction;
            this.assignPrimaryKey = assignPrimaryKey;
            return true;
        }
        return false;
    }    
    
    //Loads game data
    public void LoadFromDisc()
    {
        Debug.Log("PlayerInputManager: Loading data from disk, " + DataSaveHandler.getPlayerControlsFile());
        CheckIfMainFolderExists();           
            
        //Find save
        string filePath = DataSaveHandler.getPlayerControlsFile();        
        if (File.Exists (filePath)) 
        {
            Debug.Log("PlayerInputManager: Data file exists " + filePath);
                
            //Read JSON
            string dataAsJson = File.ReadAllText (filePath);
            
            Debug.Log(dataAsJson);
                
            //Convert JSON to data object
            SetCurrentKeybindHolder(JsonUtility.FromJson<InputActionHolder> (dataAsJson));
        } 
        else 
        {
            Debug.Log("PlayerInputManager: Data file not found, creating new and saving to: " + DataSaveHandler.getPlayerControlsFile());
            SetCurrentKeybindHolder(new InputActionHolder());
            SaveToDisc();
        }        
    }
    
    //Saves game data
    public void SaveToDisc()
    {
        Debug.Log("PlayerInputManager: Saving data from disk, " + DataSaveHandler.getPlayerControlsFile());
        CheckIfMainFolderExists();
        
        //Convert to JSON
        string dataAsJson = JsonUtility.ToJson (getInputActions(), true);
        
        //Save data
        File.WriteAllText (DataSaveHandler.getPlayerControlsFile(), dataAsJson);
    }
    
    void CheckIfMainFolderExists()
    {
        //Create folder
        string saveFolder = DataSaveHandler.getMainFolder();
        if(Directory.Exists(saveFolder))
        {            
            Debug.Log("PlayerInputManager: Main folder exists " + saveFolder);
        }
        else
        {
            Debug.Log("PlayerInputManager: Couldn't find main file '" + saveFolder + "' creating");
            Directory.CreateDirectory (saveFolder);
        }     
    }
}
