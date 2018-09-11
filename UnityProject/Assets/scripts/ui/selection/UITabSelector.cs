using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabSelector : MonoBehaviour 
{
    public static UITabSelector currentSelector;
    
    public static float tabClickDelay = 0.1f;
    private static float tabClickCooldown = 0f;
    
    public static float actionClickDelay = 0.1f;
    private static float actionClickCooldown = 0f;
    
    public string selectorName = "tab-selector";
    
    public int selectedRow = 0;
    public int selectedCol = -1;
    
    public SelectionRow[] rows;
    
    public bool shouldFunction = true;
    public bool resetPositionWhenEnabled = true;
    
    [System.Serializable]
    public class SelectionRow
    {
        public UISelectionObject[] selectObjects;
        
        public int Size()
        {
            return selectObjects.Length;
        }
    }
    
    public void SetAsPrimarySelector()
    {
        Debug.Log("UITabSelector: Making '" + this + "' as primary selector");
        
        //Disable last selector
        if(currentSelector != null)
        {
            currentSelector.DisableSelector();
        }
        
        //Enable selft
        EnableSelector();
        
        //Set current
        currentSelector = this;
    }
    
    /** Called from objects that disable the selector   
    * but enable it again in a return style. Should mainly
    * be used by sub-selectors that contain selection groups.
    *
    * row & col - index of the sub-selector. Will index to next object.
    */
    public void ReturnFromSubSelector(int row, int col)
    {
        //Enable this selector
        SetAsPrimarySelector();
        
        //Restore previous selection
        selectedRow = row;
        selectedCol = col;
        
        //Move to next selection
        NextSelection();
    }
    
    public void Update()
    {    
        if(shouldFunction && currentSelector == this)
        {        
            //Cooldown on click to prevent double click
            if(tabClickCooldown > 0)
            {
                tabClickCooldown -= Time.unscaledDeltaTime;
            }
            
            if(actionClickCooldown > 0)
            {
                actionClickCooldown -= Time.unscaledDeltaTime;
            }
                
            if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Tab))
            {      
                //Cooldown on click to prevent double click
                if(tabClickCooldown <= 0)
                {
                    Debug.Log("UITabSelector: Reverse tab clicked");                 
                    
                    int i = 0;
                    while(!PrevSelection() && i < 5)
                    {
                        i++;
                    }
                }
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                //Cooldown on click to prevent double click
                if(tabClickCooldown <= 0)
                {
                    //Reset cooldown
                    tabClickCooldown = tabClickDelay;                 
                
                    int i = 0;
                    while(!NextSelection() && i < 5)
                    {
                        i++;
                    }
                }
            }
            else if(Input.GetButtonDown("Submit"))
            {
                //Delay between clicks to prevent double click actions
                if(actionClickCooldown <= 0)
                {
                    actionClickCooldown = actionClickDelay;
                    
                    //Call activate and disable self if return is true
                    if(GetSelected().OnActived(this))
                    {
                        DisableSelector();
                    }
                }
            }
        }
            
        //TODO on D-Pad or arrow keys move col & rows
        //left-right cycle col only
        //up-down cycle row only
    }

    public bool NextSelection()
    {
        int prev_col = selectedCol;
        int prev_row = selectedRow;
        
        //If set to -1, reset to zero
        if(prev_col < 0 || prev_row < 0)
        {
            selectedCol=0;
            selectedRow=0;
            return OnSelectionChanged(prev_row, prev_col, true);
        }
        
        selectedCol++;
        if(selectedCol >= rows[selectedRow].Size())
        {
            selectedCol = 0;
            selectedRow++;
            if(selectedRow >= rows.Length)
            {
                selectedRow = 0;
            }
        }
        
        return OnSelectionChanged(prev_row, prev_col, true);
    }
    
    public bool PrevSelection()
    {
        int prev_col = selectedCol;
        int prev_row = selectedRow;
        
        //If set to -1, reset to max limit
        if(prev_col < 0 || prev_row < 0)
        {
            selectedRow = MaxRows() - 1;
            selectedCol = MaxColumns() - 1;
            
            return OnSelectionChanged(prev_row, prev_col, true);
        }
        
        selectedCol--;
        if(selectedCol < 0)
        {
            selectedCol = rows[selectedRow].Size() - 1;
            selectedRow--;
            if(selectedRow < 0)
            {
                selectedRow = rows.Length - 1;
            }
        }
        return OnSelectionChanged(prev_row, prev_col, false);
    }
    
    public int MaxRows()
    {
        return rows.Length;
    }
    
    public int MaxColumns()
    {
        return MaxColumns(selectedRow);
    }
    
    public int MaxColumns(int row)
    {
        if(row >= 0 && row <= MaxRows())
        {
            return rows[row].selectObjects.Length;
        }
        return 0;
    }
    
    public void SelectElement(int row, int col, bool forward)
    {
        int prev_col = selectedCol;
        int prev_row = selectedRow;
        
        //Set
        selectedRow = row;
        selectedCol = col;
        
        if(IsValidSelection(row, col))
        {
            //Trigger
            OnSelectionChanged(prev_row, prev_col, forward);
        }
    }
    
    bool OnSelectionChanged(int prev_row, int prev_col, bool forward)
    {
        //Reset cooldown
        tabClickCooldown = tabClickDelay;
                    
        Debug.Log("Selection changed to " + selectedRow + "x" + selectedCol);
        
        if(IsValidSelection(prev_row, prev_col))
        {
            if(GetSelected(prev_row, prev_col).CanBeSelected(this))
            {
                GetSelected(prev_row, prev_col).OnUnSelected(this);
            }
        }
        
        bool canBeSelected = GetSelected().CanBeSelected(this);
        Debug.Log("Selection can be selected:" + canBeSelected);
        if(canBeSelected && GetSelected().OnSelected(this, forward))
        {
            DisableSelector();
        }
        return canBeSelected;
    }
    
    public bool IsValidSelection(int row, int col)
    {
        return row >= 0 && row < MaxRows() && col >= 0 && col < MaxColumns(row);
    }
    
    public UISelectionObject GetSelected(int row, int col)
    {
        return rows[row].selectObjects[col];
    }
    
    public UISelectionObject GetSelected()
    {
        if(selectedRow < 0)
        {
            selectedRow = 0;
        }
        if(selectedRow >= MaxRows())
        {
            selectedRow = MaxRows() - 1;
        }
        if(selectedCol < 0)
        {
            selectedCol = 0;
        }
        if(selectedCol >= MaxColumns(selectedRow))
        {
            selectedCol = MaxColumns(selectedRow) - 1;
        }
        return GetSelected(selectedRow, selectedCol);
    }
    
    public void EnableSelector()
    {
        //Toggle
        shouldFunction = true;    
        
        if(resetPositionWhenEnabled)
        {
            //Reset state
            ResetSelectionState();
        }
        
        //Toggle all components
        foreach (SelectionRow row in rows)
        {
            foreach(UISelectionObject selection in row.selectObjects)
            {
                selection.OnSelectorEnabled(this, true);
            }
        }
    }
    
    public void ResetSelectionState()
    {
        selectedRow = -1;
        selectedCol = -1;
    }
    
    public void DisableSelector()
    {
        //Toggle
        shouldFunction = false;
        
         //Toggle all components
        foreach (SelectionRow row in rows)
        {
            foreach(UISelectionObject selection in row.selectObjects)
            {
                selection.OnSelectorEnabled(this, false);
                selection.OnUnSelected(this);
            }
        }
    }
    
    public override string ToString()
    {
       return "UITabSelector[" + selectorName + "]";
    }
}
