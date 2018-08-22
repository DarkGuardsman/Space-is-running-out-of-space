using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to store all input actions, only contains keys to make serialization easier
[System.Serializable]
public class InputActionHolder 
{
    //https://docs.unity3d.com/540/Documentation/ScriptReference/KeyCode.html
    
    public InputAction up = new InputAction("up", KeyCode.W, KeyCode.UpArrow);
    public InputAction down = new InputAction("down", KeyCode.S, KeyCode.DownArrow);
    public InputAction left = new InputAction("left", KeyCode.A, KeyCode.LeftArrow);
    public InputAction right = new InputAction("right", KeyCode.D, KeyCode.RightArrow);
	
    public InputAction slow = new InputAction("slow", KeyCode.Space, KeyCode.None);
    
    public InputAction rotateLeft = new InputAction("rotateLeft", KeyCode.Q, KeyCode.None);
    public InputAction rotateRight = new InputAction("rotateRight", KeyCode.E, KeyCode.None);
    
    public InputAction release = new InputAction("release", KeyCode.R, KeyCode.Mouse3);
    public InputAction hook = new InputAction("hook", KeyCode.F, KeyCode.Mouse1);
    
    public InputAction shoot = new InputAction("hook", KeyCode.C, KeyCode.Mouse2);
    
    public InputAction zoomIn = new InputAction("zoomIn", KeyCode.Plus, KeyCode.None);
    public InputAction zoomOut = new InputAction("zoomOut", KeyCode.Minus, KeyCode.None);
    
    //Make sure to add new actions to PlayerInputManager list
    
    //Copies keybinds into other holder
    public void CopyInto(InputActionHolder holder)
    {
        holder.up = up.Copy();
        holder.down = down.Copy();
        holder.left = left.Copy();
        holder.right = right.Copy();
	
        holder.slow = slow.Copy();
    
        holder.rotateLeft = rotateLeft.Copy();
        holder.rotateRight = rotateRight.Copy();
    
        holder.release = release.Copy();
        holder.hook = hook.Copy();
    
        holder.shoot = shoot.Copy();
    
        holder.zoomIn = zoomIn.Copy();
        holder.zoomOut = zoomOut.Copy();
    }
    
    public void CheckForIssues(InputActionHolder defaultSettings)
    {
        up = CheckForIssues(up, defaultSettings.up);
        down = CheckForIssues(down, defaultSettings.down);
        left = CheckForIssues(left, defaultSettings.left);
        right = CheckForIssues(right, defaultSettings.right);
	
        slow = CheckForIssues(slow, defaultSettings.slow);
    
        rotateLeft = CheckForIssues(rotateLeft, defaultSettings.rotateLeft);
        rotateRight = CheckForIssues(rotateRight, defaultSettings.rotateRight);
    
        release = CheckForIssues(release, defaultSettings.release);
        hook = CheckForIssues(hook, defaultSettings.hook);
    
        shoot = CheckForIssues(shoot, defaultSettings.shoot);
    
        zoomIn = CheckForIssues(zoomIn, defaultSettings.zoomIn);
        zoomOut = CheckForIssues(zoomOut, defaultSettings.zoomOut);
    }
    
    InputAction CheckForIssues(InputAction currentInput, InputAction defaultInput)
    {
        //Check for null
        if(currentInput == null)
        {
            return defaultInput.Copy();
        }
        
        //Force name
        currentInput.displayName = defaultInput.displayName;
        
        //TODO check keys?
        
        return currentInput;
    }
}
