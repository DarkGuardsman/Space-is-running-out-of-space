using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : UIDisplay  
{	
    public Canvas gameUI;
    public Canvas respawnUI;
    public Canvas menuUI;
    public Canvas optionsUI;
    public Canvas helpUI;
    public Canvas failedUI;
    
    public bool showMenu = false;    
    public bool showInfo = false;
    public bool showOptions = false;
    public bool disableEscape = false;
    
    private float currentTimeScale;
    
	// Update is called once per frame
	void Update () 
    {
        //disable all UIs
        gameUI.enabled = false;
        respawnUI.enabled = false;
        menuUI.enabled = false;
        //optionsUI.enabled = false;
        //helpUI.enabled = false;
        //failedUI.enabled = false;
        
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0;
        
        if(!disableEscape && Input.GetButtonDown("Cancel"))
        {
            showMenu = !showMenu;
        }
        
        //enable desired UI
		if(showMenu)
        {
            menuUI.enabled = true;
        }
        else if(showOptions)
        {
            
        }
        else if(showInfo)
        {
            
        }
        else if(gameController.gameOver)
        {
            
        }
        else if(gameController.respawnPlayer)
        {
            respawnUI.enabled = true;
            Time.timeScale = currentTimeScale;
        }
        else
        {
            gameUI.enabled = true;
            Time.timeScale = currentTimeScale;
        }
	}
    
    public void ButtonShowMenu()
    {
        showMenu = true;
    }
}
