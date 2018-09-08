using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabSelectToggle : UISelectionObject 
{
    public Toggle toggle;
    public Color selectionColor;
    
    private Color oldColor;
    private bool hasBeenSelected = false;
    
    protected Toggle GetToggle()
    {
        if(toggle == null)
        {
            toggle = gameObject.GetComponent<Toggle>();
            oldColor = toggle.colors.normalColor;
        }
        return toggle;
    }
    
    public override bool OnSelected(UITabSelector selector, bool forward)
    {
        hasBeenSelected = true;
        GetToggle().SetToggleNormalColor(selectionColor);
        return false;
    }
    
    public override void OnUnSelected(UITabSelector selector)
    {
        if(hasBeenSelected)
        {
            ResetColor();
        }
        hasBeenSelected = false;
    }
    
    public override void OnSelectorEnabled(UITabSelector selector, bool enabled)
    {
        ResetColor();
    }
    
	public override bool OnActived(UITabSelector selector)
    {
        toggle.isOn = !toggle.isOn;
        return false;
    }
    
    void ResetColor()
    {
        GetToggle().SetToggleNormalColor(oldColor);
    }
}
