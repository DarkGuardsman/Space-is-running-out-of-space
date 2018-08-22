using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputAction 
{
    public string displayName;
    
    public KeyCode primary {get; set;}
    public KeyCode secondary {get; set;}
    
    public InputAction(string name, KeyCode primary, KeyCode secondary)
    {
        this.displayName = name;
        this.primary = primary;
        this.secondary = secondary;
    }
    
    public bool IsKeyDown()
    {
        return Input.GetKey(primary) || Input.GetKey(secondary);
    }
}
