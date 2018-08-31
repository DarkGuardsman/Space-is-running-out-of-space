using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabSelectButton : UISelectionObject
{
    public bool disableOnEnter = false;
    
    public Button button;
    
    public Color selectionColor;
    
    private Color oldColor;
    private bool hasBeenSelected = false;
    
    protected Button GetButton()
    {
        if(button == null)
        {
            button = gameObject.GetComponent<Button>();
            oldColor = button.colors.normalColor;
        }
        return button;
    }
    
    public override void OnSelectorEnabled(UITabSelector selector, bool enabled)
    {
        ResetButtonColor();
    }
    
    public override bool OnSelected(UITabSelector selector)
    {
        hasBeenSelected = true;        
        GetButton().SetButtonNormalColor(selectionColor);
        return false;
    }
    
    public override void OnUnSelected(UITabSelector selector)
    {
        if(hasBeenSelected)
        {
            ResetButtonColor();
        }
        hasBeenSelected = false;
    }
    
    public override bool OnActived(UITabSelector selector)
    {      
        if(disableOnEnter)
        {           
            hasBeenSelected = false;
        }
        ResetButtonColor();
        GetButton().onClick.Invoke();
        return disableOnEnter;
    }
    
    void ResetButtonColor()
    {
        if(oldColor != null)
        {
            GetButton().SetButtonNormalColor(oldColor);
        }
    }
}
