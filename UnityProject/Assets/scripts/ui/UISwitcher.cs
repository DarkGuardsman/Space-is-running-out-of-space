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
    public bool showHelp = false;
    public bool showOptions = false;
    
	// Update is called once per frame
	void Update () 
    {
        //disable all UIs
        gameUI.enabled = false;
        respawnUI.enabled = false;
        //menuUI.enabled = false;
        //optionsUI.enabled = false;
        //helpUI.enabled = false;
        //failedUI.enabled = false;
        
        //enable desired UI
		if(showMenu)
        {
            
        }
        else if(showOptions)
        {
            
        }
        else if(showHelp)
        {
            
        }
        else if(gameController.gameOver)
        {
            
        }
        else if(gameController.respawnPlayer)
        {
            respawnUI.enabled = true;
        }
        else
        {
            gameUI.enabled = true;
        }
	}
    
    public void ShowMenu()
    {
        showMenu = true;
    }
    
    public void ShowHelp()
    {
        showHelp = true;
    }
    
    public void ShowOptions()
    {
        showOptions = true;
    }
}
