using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputAction 
{
    public string displayName;
    
    public KeyCode primary;
    
    public KeyCode secondary;
    
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
    
    public void CopyKeysInto(InputAction other)
    {
        other.primary = primary;
        other.secondary = secondary;
    }
    
    public bool DoKeysMatch(InputAction other)
    {
        return other.primary == primary && other.secondary == secondary;
    }
    
    public InputAction Copy()
    {
        return new InputAction(displayName, primary, secondary);
    }
    
    public string GetPrimaryText()
    {
        if(primary == null || primary == KeyCode.None)
        {
            return "";
        }
        return primary.ToString();
    }
    
    public string GetSecondaryText()
    {
        if(secondary == null || secondary == KeyCode.None)
        {
            return "";
        }
        return secondary.ToString();
    }
    
    public override string ToString()
    {
        return "InputAction[" + displayName + ", " + primary + ", " + secondary + "]";
    }
}
