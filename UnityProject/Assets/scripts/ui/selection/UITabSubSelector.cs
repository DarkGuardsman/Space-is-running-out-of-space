using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabSubSelector : UISelectionObject 
{
    public UITabSelector tabSelector;
    
    void Start()
    {
        if(tabSelector == null)
        {
            tabSelector = GetComponent<UITabSelector>();
        }
    }
    
    public override bool OnSelected(UITabSelector selector, bool forward)
    {
        //Enable selector
        tabSelector.SetAsPrimarySelector();
        
        //Have it select next element, if it defaults to no selection
        if(tabSelector.resetPositionWhenEnabled)
        {   
            if(forward)
            {
                tabSelector.SelectElement(0, 0, true);
            }
            else
            {                
                tabSelector.SelectElement(tabSelector.MaxRows() - 2, tabSelector.MaxColumns() - 1, false);
            }
        }
        
        //Return true to disable previous selector
        return true;
    }
}
