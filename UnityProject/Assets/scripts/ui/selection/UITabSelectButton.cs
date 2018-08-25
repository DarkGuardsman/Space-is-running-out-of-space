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
    
    void Start()
    {
        if(button == null)
        {
            button = gameObject.GetComponent<Button>();
        }
    }
    
    public override bool OnSelected(UITabSelector selector)
    {
        hasBeenSelected = true;
        oldColor = button.colors.normalColor;
        button.SetButtonNormalColor(selectionColor);
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
            ResetButtonColor();
            hasBeenSelected = false;
        }
        button.onClick.Invoke();
        return disableOnEnter;
    }
    
    void ResetButtonColor()
    {
        if(oldColor != null)
        {
            button.SetButtonNormalColor(oldColor);
        }
    }
}
