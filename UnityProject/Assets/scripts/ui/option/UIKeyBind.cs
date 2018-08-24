using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIKeyBind : MonoBehaviour 
{
    public Color buttonNormalColor;
    public Color buttonAssignColor;
    public Color buttonDisabledColor;
    public Color buttonDisabledAssignColor;
    
    public Button primaryButton;
    public Button secondaryButton;
    
    public InputAction actionInput = new InputAction("test", KeyCode.W, KeyCode.S);
    
    public InputAction actionInputCopy;
    
    public TMP_Text keybindLabel;
    public TMP_Text primaryLabel;
    public TMP_Text secondaryLabel;
    
    public PlayerInputManager inputManager;
    
    void Start()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
        ResetButtons();            
    }

    public void Update()
    {
        if(actionInput != null)
        {
            keybindLabel.text = actionInput.displayName;            
            primaryLabel.text = actionInput.GetPrimaryText();
            secondaryLabel.text = actionInput.GetSecondaryText();
        }

        if(!inputManager.assignKey)
        {
            ResetButtons(); 
        }
        else
        {
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
        }
    }    
    
    void ResetButtons()
    {
        primaryButton.SetButtonNormalColor(buttonNormalColor);
        secondaryButton.SetButtonNormalColor(buttonNormalColor);
        
        primaryButton.SetButtonDisabledColor(buttonDisabledColor);
        secondaryButton.SetButtonDisabledColor(buttonDisabledColor);
        
        primaryButton.interactable = true;
        secondaryButton.interactable = true;
    }
    
    public void AssignActionInput(InputAction actionInput)
    {
        this.actionInput = actionInput;
        if(actionInput == null)
        {
            this.actionInputCopy = null;
        }
        else
        {       
            this.actionInputCopy = actionInput.Copy();
        }
    }
    
    public bool ApplyChanges()
    {
        if(actionInput != null && !actionInput.DoKeysMatch(actionInputCopy))
        {
            actionInputCopy.CopyKeysInto(actionInput);
            return true;
        }
        return false;
    }

    public void StartAssignPrimaryKey()
    {
        if(actionInput != null)
        {
            if(inputManager.StartAssignKey(actionInput, true))
            {
                primaryButton.SetButtonNormalColor(buttonAssignColor);
                primaryButton.SetButtonDisabledColor(buttonDisabledAssignColor);
            }
        }
    }
    
    public void StartAssignSecondaryKey()
    {
        if(actionInput != null)
        {
            if(inputManager.StartAssignKey(actionInput, false))
            {
                secondaryButton.SetButtonNormalColor(buttonAssignColor);
                secondaryButton.SetButtonDisabledColor(buttonDisabledAssignColor);
            }
        }
    }
}
