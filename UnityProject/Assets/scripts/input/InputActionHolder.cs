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
}
