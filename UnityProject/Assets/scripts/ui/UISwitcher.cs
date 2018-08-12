using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : UIDisplay  
{	
    public Canvas gameUI;
    public Canvas respawnUI;
    public UIMenu menuUI;
    public Canvas optionsUI;
    public Canvas helpUI;
    public Canvas failedUI;
    
    public bool showMenu = false;    
    public bool showInfo = false;
    public bool showOptions = false;
    
    // Update is called once per frame
	void Update () 
    {
        if(menuUI.mainUIPanel.active && Input.GetButtonDown("Cancel"))
        {
            showMenu = !showMenu;
        }
        
        //disable all UIs
        gameUI.enabled = false;
        respawnUI.enabled = false;
        menuUI.canvus.enabled = false;
        //optionsUI.enabled = false;
        //helpUI.enabled = false;
        //failedUI.enabled = false;
        
        //enable desired UI
		if(showMenu)
        {
            menuUI.canvus.enabled = true;
            Time.timeScale = 0;
        }
        else if(showOptions)
        {
            Time.timeScale = 0;
        }
        else if(showInfo)
        {
            Time.timeScale = 0;
        }
        else if(gameController.gameOver)
        {
            Time.timeScale = 0;
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
    
    public void ButtonShowMenu()
    {
        showMenu = true;
    }
}
