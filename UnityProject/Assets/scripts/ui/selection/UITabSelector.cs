using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITabSelector : MonoBehaviour 
{
    public static UITabSelector currentSelector;
    
    public int selectedRow = 0;
    public int selectedCol = -1;
    
    public SelectionRow[] rows;
    
    public bool shouldFunction = true;
    
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
    
    public void Update()
    {    
        if(shouldFunction && currentSelector == this)
        {                
            if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Tab))
            {                
                int i = 0;
                while(!PrevSelection() && i < 5)
                {
                    i++;
                }
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                int i = 0;
                while(!NextSelection() && i < 5)
                {
                    i++;
                }
            }
            else if(Input.GetButtonDown("Submit"))
            {
                if(GetSelected().OnActived(this))
                {
                    DisableSelector();
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
        
        return OnSelectionChanged(prev_row, prev_col);
    }
    
    public bool PrevSelection()
    {
        int prev_col = selectedCol;
        int prev_row = selectedRow;
        
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
        return OnSelectionChanged(prev_row, prev_col);
    }
    
    bool OnSelectionChanged(int prev_row, int prev_col)
    {
        Debug.Log("Selection changed to " + selectedRow + "x" + selectedCol);
        
        if(prev_row >= 0 && prev_row < rows.Length && prev_col >= 0 && prev_col < rows[prev_row].selectObjects.Length)
        {
            if(GetSelected(prev_row, prev_col).CanBeSelected(this))
            {
                GetSelected(prev_row, prev_col).OnUnSelected(this);
            }
        }
        
        bool canBeSelected = GetSelected().CanBeSelected(this);
        if(canBeSelected && GetSelected().OnSelected(this))
        {
            DisableSelector();
        }
        return canBeSelected;
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
        if(selectedRow >= rows.Length)
        {
            selectedRow = rows.Length - 1;
        }
        if(selectedCol < 0)
        {
            selectedCol = 0;
        }
        if(selectedCol >= rows[selectedRow].selectObjects.Length)
        {
            selectedCol = rows[selectedRow].selectObjects.Length - 1;
        }
        return GetSelected(selectedRow, selectedCol);
    }
    
    public void EnableSelector()
    {
        //Toggle
        shouldFunction = true;    
        
        //Reset state
        selectedRow = 0;
        selectedCol = -1;
        
        //Toggle all components
        foreach (SelectionRow row in rows)
        {
            foreach(UISelectionObject selection in row.selectObjects)
            {
                selection.OnSelectorEnabled(this, true);
            }
        }
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
}
