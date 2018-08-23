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
    public InputActionHolder currentInputActions = new InputActionHolder(); 
    public InputActionHolder defaultInputActions = new InputActionHolder(); 
    
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
        
        //Fix any issues with loaded key set
        currentInputActions.CheckForIssues(defaultInputActions);
        
        //Build action key list
        actionList.Clear();
        actionList.Add(currentInputActions.up);
        actionList.Add(currentInputActions.down);
        actionList.Add(currentInputActions.left);
        actionList.Add(currentInputActions.right);
        actionList.Add(currentInputActions.rotateLeft);
        actionList.Add(currentInputActions.rotateRight);    
        actionList.Add(currentInputActions.slow);    
        actionList.Add(currentInputActions.release);
        actionList.Add(currentInputActions.hook);    
        actionList.Add(currentInputActions.shoot);    
        actionList.Add(currentInputActions.zoomIn);
        actionList.Add(currentInputActions.zoomOut);
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
    
    //Loads game data
    public void LoadFromDisc()
    {
        //Create folder
        string saveFolder = DataSaveHandler.getMainFolder();
        if(File.Exists(saveFolder))
        {        
            //Find save
            string filePath = DataSaveHandler.getPlayerControlsFile();        
            if (File.Exists (filePath)) 
            {
                //Read JSON
                string dataAsJson = File.ReadAllText (filePath);
                
                //Convert JSON to data object
                currentInputActions = JsonUtility.FromJson<InputActionHolder> (dataAsJson);
            } 
            else 
            {
                currentInputActions = new InputActionHolder();
            }
        }
    }
    
    //Saves game data
    public void SaveToDisc()
    {
        //Create folder
        string saveFolder = DataSaveHandler.getMainFolder();
        if(!File.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder); 
        }
        
        //Convert to JSON
        string dataAsJson = JsonUtility.ToJson (currentInputActions, true);
        
        //Save data
        File.WriteAllText (DataSaveHandler.getPlayerControlsFile(), dataAsJson);
    }
}
