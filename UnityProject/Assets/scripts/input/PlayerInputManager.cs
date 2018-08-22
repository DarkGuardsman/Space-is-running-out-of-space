using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour 
{
    //Singleton for input managment
    public static PlayerInputManager instance;  

    //Reference object for action keys
    public InputActionHolder inputActions = new InputActionHolder(); 
    
    //List of action keys (mainly used for UI)
    public List<InputAction> actionList = new List<InputAction>();	
    
    //Are we assigning a key?
    public bool assignKey = false;
    //Primary (true) or secondary (false) key
    public bool assignPrimaryKey = false;
    //Input action (key) being assigned
    public InputAction assignAction;
    
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
        //Build action key list
        actionList.Clear();
        actionList.Add(inputActions.up);
        actionList.Add(inputActions.down);
        actionList.Add(inputActions.left);
        actionList.Add(inputActions.right);	
        actionList.Add(inputActions.slow);    
        actionList.Add(inputActions.rotateLeft);
        actionList.Add(inputActions.rotateRight);    
        actionList.Add(inputActions.release);
        actionList.Add(inputActions.hook);    
        actionList.Add(inputActions.shoot);    
        actionList.Add(inputActions.zoomIn);
        actionList.Add(inputActions.zoomOut);
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
                        assignAction.primary = keyCode;
                    }
                    else
                    {
                        assignAction.secondary = keyCode;
                    }
                    
                    //TODO save change
                }
            }
        }
	}
}
