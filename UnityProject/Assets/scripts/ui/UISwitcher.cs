using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : UIDisplay  
{	
    public Canvas gameUI;
    public Canvas respawnUI;
    public UIMenu menuUI;
    public UIOptions optionsUI;
    public Canvas helpUI;
    public Canvas failedUI;
    
    public bool showMenu = false;    
    public bool showInfo = false;
    public bool showOptions = false;
    
    public override void Start()
    {
        base.Start();
        StartCoroutine(WatchForMenuCancel());
    }
    
    void onEnable()
    {
        StartCoroutine(WatchForMenuCancel());
    }
    
    void onDisable()
    {
        StopCoroutine(WatchForMenuCancel());
    }
    
    void OnDestroy()
    {
        StopCoroutine(WatchForMenuCancel());
    }
    
    // Update is called once per frame
	void Update () 
    {        
        //disable all UIs
        gameUI.enabled = false;
        respawnUI.enabled = false;
        menuUI.canvus.enabled = false;
        optionsUI.canvus.enabled = false;
        helpUI.enabled = false;
        failedUI.enabled = false;
        
        //enable desired UI
		
        if(showOptions)
        {
            Time.timeScale = 0;
            optionsUI.canvus.enabled = true;
        }
        else if(showInfo)
        {
            helpUI.enabled = true;
            Time.timeScale = 0;
        }
        else if(showMenu)
        {
            menuUI.canvus.enabled = true;
            Time.timeScale = 0;
        }
        else if(gameController.gameOver)
        {
            failedUI.enabled = true;
            Time.timeScale = gameController.gameTimeScale;
        }
        else if(gameController.respawnPlayer)
        {
            respawnUI.enabled = true;
            Time.timeScale = gameController.gameTimeScale;
        }
        else
        {
            gameUI.enabled = true;
            Time.timeScale = gameController.gameTimeScale;
        }
	}
    
    IEnumerator WatchForMenuCancel()
    {
        while(true)
        {
            if(Input.GetButtonDown("Cancel"))
            {                
                if(showInfo)
                {
                    showInfo = false;
                    showMenu = true;
                }
                else if(showOptions)
                {
                    showOptions = false;
                    showMenu = true;
                }
                else if(menuUI.mainUIPanel.active)
                {
                    showMenu = !showMenu;
                }
            }
            yield return null;
        }
    }
    
    public void ButtonShowMenu()
    {
        showMenu = true;
        showInfo = false;
        showOptions = false;
    }
    
    public void ButtonResumeGame()
    {
        showMenu = false;
        showInfo = false;
        showOptions = false;
    }
}
