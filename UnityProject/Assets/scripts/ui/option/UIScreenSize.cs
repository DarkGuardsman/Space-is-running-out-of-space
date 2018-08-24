using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScreenSize : MonoBehaviour {

    private TMP_Dropdown dropDownMenu;
    
    private  Resolution resolution;
    
    void Start()
    {
        //Get drop down
        dropDownMenu = gameObject.GetComponent<TMP_Dropdown>(); 
    }
    
    public void LoadScreenSize()
    {     
        //Get current screen size
        resolution = Screen.currentResolution;
        
        //Collect resolutions
        List<string> list = new List<string>();
        int currentResolutionIndex = 0;        
        
        Resolution[] resolutions = Screen.resolutions;       
        for(int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            list.Add(res.width + "x" + res.height + " @" + res.refreshRate + "Hz");
            
            //Check if is current resolution so we can get index
            if(res.width == resolution.width && res.height == resolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        //Reset old data
        dropDownMenu.ClearOptions();
        
        //Add entries to list
        dropDownMenu.AddOptions(list);
        
        //Set current index
        dropDownMenu.value = currentResolutionIndex;
    }
    
    public void OnValueChanged(int index)
    {       
        resolution = Screen.resolutions[index];
    }
    
    public void ApplyChanges()
    {
        Resolution current = Screen.currentResolution;
        if(current.width != resolution.width || current.height != resolution.height)
        {
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
            //TODO show timer UI to reset if something goes wrong
        }
    }
}
