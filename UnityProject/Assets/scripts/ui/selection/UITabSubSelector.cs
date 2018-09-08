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
                tabSelector.NextSelection();
            }
            else
            {
                //Do not select last, as sub selectors use last element to return to parent selector
                tabSelector.selectedRow = tabSelector.MaxRows() - 2;
                tabSelector.selectedCol = tabSelector.MaxColumns();
                //Trigger move next to get selection
                tabSelector.PrevSelection();
            }
        }
        
        //Return true to disable previous selector
        return true;
    }
}
