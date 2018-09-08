using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScreenMode : MonoBehaviour 
{
    private TMP_Dropdown dropDownMenu;
    
    public FullScreenMode selectedMode;
    
    void Start()
    {
        //Get drop down
        dropDownMenu = gameObject.GetComponent<TMP_Dropdown>(); 
    }
	
	public void LoadScreenModes()
    {
        if(dropDownMenu != null)
        {
            List<string> list = new List<string>();
            foreach(FullScreenMode mode in Enum.GetValues(typeof(FullScreenMode)))
            {
                list.Add(mode.ToString());
            }
            
             //Reset old data
            dropDownMenu.ClearOptions();
            
            //Add entries to list
            dropDownMenu.AddOptions(list);
            
            //Set current index
            selectedMode = Screen.fullScreenMode;
            dropDownMenu.value = (int)selectedMode;
        }
	}
    
    public void OnValueChanged(int index)
    {       
        selectedMode = (FullScreenMode)index;
    }
    
    public bool ApplyChanges()
    {
        if(selectedMode != Screen.fullScreenMode)
        {           
            return true;
        }
        return false;
    }
}
