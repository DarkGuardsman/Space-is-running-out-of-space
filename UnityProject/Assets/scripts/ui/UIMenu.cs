using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : UIDisplay 
{
	public UISwitcher uiSwitcher;
    
    public GameObject mainUIPanel;
    public GameObject confirmRestartPanel;
    public GameObject confirmExitPanel;
    
    void Awake ()
    {
        ButtonReturnMain();
    }
    
    void Update ()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            ButtonReturnMain();
        }
    }
    
    public void ButtonShowInfo()
    {
        uiSwitcher.showInfo = true;
    }
    
    public void ButtonShowOptions()
    {
        uiSwitcher.showOptions = true;
    }
    
    public void ButtonRestart()
    {
        mainUIPanel.SetActive(false);
        confirmRestartPanel.SetActive(true);        
        uiSwitcher.disableEscape = true;
    }
    
    public void ButtonExit()
    {
        mainUIPanel.SetActive(false);
        confirmExitPanel.SetActive(true);        
        uiSwitcher.disableEscape = true;
    }
    
    public void ButtonConfirmExit()
    {
        Application.Quit();
    }
    
    public void ButtonConfirmRestart()
    {
        gameController.RestartLevel();
    }
    
    public void ButtonReturnMain()
    {
        mainUIPanel.SetActive(true);
        confirmRestartPanel.SetActive(false);
        confirmExitPanel.SetActive(false);
        uiSwitcher.disableEscape = false;
    }
}
