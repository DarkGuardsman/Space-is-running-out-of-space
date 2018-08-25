using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScreenSize : MonoBehaviour {

    private TMP_Dropdown dropDownMenu;
    
    public Resolution currentResolution;
    public Resolution selectedResolution;
    
    void Start()
    {
        //Get drop down
        dropDownMenu = gameObject.GetComponent<TMP_Dropdown>(); 
    }
    
    public void LoadScreenSize()
    {     
        //Get current screen size
        selectedResolution = Screen.currentResolution;
        currentResolution = Screen.currentResolution;
        
        //Collect selectedResolutions
        List<string> list = new List<string>();
        int currentResolutionIndex = 0;        
        
        Resolution[] resolutions = Screen.resolutions;       
        for(int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            list.Add(res.width + "x" + res.height + " @" + res.refreshRate + "Hz");
            
            //Check if is current selectedResolution so we can get index
            if(res.width == selectedResolution.width && res.height == selectedResolution.height)
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
        selectedResolution = Screen.resolutions[index];
    }
    
    public bool ApplyChanges()
    {
        return currentResolution.width != selectedResolution.width 
            || currentResolution.height != selectedResolution.height 
            || currentResolution.refreshRate != selectedResolution.refreshRate;
    }
}
