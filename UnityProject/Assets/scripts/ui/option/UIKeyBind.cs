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
    
    public int actionInputIndex;
    
    public InputAction actionInputCopy;
    
    public TMP_Text keybindLabel;
    public TMP_Text primaryLabel;
    public TMP_Text secondaryLabel;
    
    public PlayerInputManager inputManager;
    
    void Start()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
        ResetButtons();
        
        if(inputManager != null && inputManager.getInputActions().Get(actionInputIndex) != null)
        {
            this.actionInputCopy = inputManager.getInputActions().Get(actionInputIndex).Copy();    
        }                
    }

    public void Update()
    {
        if(actionInputCopy != null)
        {
            keybindLabel.text = actionInputCopy.displayName;            
            primaryLabel.text = actionInputCopy.GetPrimaryText();
            secondaryLabel.text = actionInputCopy.GetSecondaryText();
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
    
    public void AssignActionInput(int index)
    {
        this.actionInputIndex = index;
    }
    
    public bool ApplyChanges()
    {
        InputAction inputAction = inputManager.getInputActions().Get(actionInputIndex);
        Debug.Log("UIKeyBind: Checking for changes " + actionInputCopy + " from " + inputAction);
        if(actionInputCopy != null && !actionInputCopy.DoKeysMatch(inputAction))
        {
            Debug.Log("UIKeyBind: Has changed");
            actionInputCopy.CopyKeysInto(inputAction);
            return true;
        }
        return false;
    }

    public void StartAssignPrimaryKey()
    {
        if(actionInputCopy != null)
        {
            if(inputManager.StartAssignKey(actionInputCopy, true))
            {
                primaryButton.SetButtonNormalColor(buttonAssignColor);
                primaryButton.SetButtonDisabledColor(buttonDisabledAssignColor);
            }
        }
    }
    
    public void StartAssignSecondaryKey()
    {
        if(actionInputCopy != null)
        {
            if(inputManager.StartAssignKey(actionInputCopy, false))
            {
                secondaryButton.SetButtonNormalColor(buttonAssignColor);
                secondaryButton.SetButtonDisabledColor(buttonDisabledAssignColor);
            }
        }
    }
}
