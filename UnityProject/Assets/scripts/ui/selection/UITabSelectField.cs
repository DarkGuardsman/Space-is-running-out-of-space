using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITabSelectField : UISelectionObject 
{
    public InputField inputField;
    
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

	public override bool OnSelected(UITabSelector selector, bool forward)
    {
        inputField.Select();
        inputField.ActivateInputField();
        return false;
    }
    
    public override void OnUnSelected(UITabSelector selector)
    {
        inputField.DeactivateInputField();
        inputField.OnDeselect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public override void OnSelectorEnabled(UITabSelector selector, bool enabled)
    {
        inputField.DeactivateInputField();
        inputField.OnDeselect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
    }
}
