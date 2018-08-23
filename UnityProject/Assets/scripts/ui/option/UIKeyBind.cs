using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIKeyBind : MonoBehaviour 
{
    public Button primaryButton;
    public Button secondaryButton;
    
    public InputAction actionInput = new InputAction("test", KeyCode.W, KeyCode.S);
    
    public TMP_Text keybindLabel;
    public TMP_Text primaryLabel;
    public TMP_Text secondaryLabel;

    public void Update()
    {
        if(actionInput != null)
        {
            keybindLabel.text = actionInput.displayName;
            
            //TODO consider updating text in real time
            primaryLabel.text = actionInput.GetPrimaryText();
            secondaryLabel.text = actionInput.GetSecondaryText();
        }
    }    
    
    public void AssignActionInput(InputAction actionInput)
    {
        this.actionInput = actionInput;
        
        //TODO clear listeners
        //TODO assign listeners        
    }	
}
