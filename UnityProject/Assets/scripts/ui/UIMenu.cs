using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : UIDisplay 
{
	public UISwitcher uiSwitcher;
    
    public UITabSelector menuSelector;
    
    public Canvas canvus;
    
    public GameObject mainUIPanel;
    public GameObject confirmRestartPanel;
    public GameObject confirmExitPanel;
    
    public GameObject playButton;
    public GameObject restartButton;
    public GameObject resumeButton;
    public GameObject worldObject;
    
    public override void Start()
    {
        base.Start();
        canvus = gameObject.GetComponent<Canvas>();
        menuSelector = gameObject.GetComponent<UITabSelector>();
        
        worldObject.SetActive(false);
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        
        ButtonReturnMain();        
    }
    
    IEnumerator WatchForMenuCancel()
    {
        while(!mainUIPanel.active)
        {
            if(Input.GetButtonDown("Cancel"))
            {
                ButtonReturnMain();
            }
            yield return null;
        }
    }    
    
    public void ButtonPlayGame()
    {
        playButton.SetActive(false);
        restartButton.SetActive(true);
        resumeButton.SetActive(true);
        worldObject.SetActive(true);
        uiSwitcher.ButtonResumeGame();
    }
    
    public void ButtonShowInfo()
    {
        uiSwitcher.showInfo = true;
    }
    
    public void ButtonShowOptions()
    {
        uiSwitcher.showOptions = true;
        uiSwitcher.optionsUI.LoadOptions();
    }
    
    public void ButtonRestart()
    {
        mainUIPanel.SetActive(false);
        confirmRestartPanel.SetActive(true); 
        StartCoroutine(WatchForMenuCancel());
    }
    
    public void ButtonExit()
    {
        mainUIPanel.SetActive(false);
        confirmExitPanel.SetActive(true);
        StartCoroutine(WatchForMenuCancel());
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
        menuSelector.SetAsPrimarySelector();
    }
}
