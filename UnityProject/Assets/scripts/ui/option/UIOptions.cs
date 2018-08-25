using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : UIDisplay 
{
    public Canvas canvus;
    
    public UIKeyBindTable keybindTable; //TODO create array of options UIs driven by interface, to save space & reduce need for a field per UI
    public UIScreenSize screenSizeUI; //TODO create array of graphics UIs driven by interface, to save space & reduce need for a field per UI
    public UIScreenMode screenModeUI;  
    
    public Slider minArrowSizeSlider; //TODO consider breaking each option out into its own reusable script
    public Slider maxArrowSizeSlider;
    public Slider maxJunkCountSlider;    
    
    public InputField minArrowSizeInput;
    public InputField maxArrowSizeInput;
    public InputField maxJunkCountInput;  
    
    public Toggle enableEffectsToggle;
    public Toggle enableDroneTrailToggle;
    public Toggle enableBulletTrailToggle;
    public Toggle enableShipMovementToggle;
    public Toggle enableMouseAimToggle;
    
    public float minAllowArrowMaxSize = 1f;
    public float maxAllowedArrowMaxSize = 3f;
    
    public float minAllowArrowMinSize = 0.3f;
    public float maxAllowedArrowMinSize = 1f;
    
    public int minJunkSpawn = 20;
    public int maxJunkSpawn = 1000;
    
    private PlayerInputManager inputManager;
    
    void Awake ()
    {       
        canvus = gameObject.GetComponent<Canvas>();
    }
    
    public override void Start()
    {
        base.Start();
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
    
    //Load options from data into UI elements
    public void LoadOptions()
    {
        enableEffectsToggle.isOn = playerOptions.currentSettings.enableEffects;
        enableDroneTrailToggle.isOn = playerOptions.currentSettings.enableShipTrail;
        enableBulletTrailToggle.isOn = playerOptions.currentSettings.enableBulletTrail;
        enableShipMovementToggle.isOn = playerOptions.currentSettings.enableShipBasedMovement;
        enableMouseAimToggle.isOn = playerOptions.currentSettings.enableMouseAim;
        
        minArrowSizeSlider.value = Mathf.Min(1, (playerOptions.currentSettings.arrowMinScale - minAllowArrowMinSize) / (maxAllowedArrowMinSize - minAllowArrowMinSize));
        maxArrowSizeSlider.value = Mathf.Min(1, (playerOptions.currentSettings.arrowMaxScale - minAllowArrowMaxSize) / (maxAllowedArrowMaxSize - minAllowArrowMaxSize));
        maxJunkCountSlider.value = Mathf.Min(1, (((float)playerOptions.currentSettings.maxJunkSpawn) - minJunkSpawn) / (maxJunkSpawn - minJunkSpawn));    

        minArrowSizeInput.text = String.Format("{0}", playerOptions.currentSettings.arrowMinScale);
        maxArrowSizeInput.text = String.Format("{0}", playerOptions.currentSettings.arrowMaxScale);
        maxJunkCountInput.text = String.Format("{0:0}", playerOptions.currentSettings.maxJunkSpawn);

        screenSizeUI.LoadScreenSize(); 
        screenModeUI.LoadScreenModes();
    }
    
    public void ButtonApply()
    {
        Debug.Log("UIOptions: Apply Clicked");
        SaveSettings();
    }
    
    public void ButtonSave()
    {
        Debug.Log("UIOptions: Save clicked");
        SaveSettings();
    }
    
    public void SaveSettings()
    {
        Debug.Log("UIOptions: Saving settings");
        playerOptions.currentSettings.arrowMinScale = float.Parse(minArrowSizeInput.text);
        playerOptions.currentSettings.arrowMaxScale = float.Parse(maxArrowSizeInput.text);
        playerOptions.currentSettings.maxJunkSpawn = int.Parse(maxJunkCountInput.text);
        
        playerOptions.currentSettings.enableEffects = enableEffectsToggle.isOn;
        playerOptions.currentSettings.enableShipTrail = enableDroneTrailToggle.isOn;
        playerOptions.currentSettings.enableBulletTrail = enableBulletTrailToggle.isOn;      
        playerOptions.currentSettings.enableShipBasedMovement = enableShipMovementToggle.isOn;    
        playerOptions.currentSettings.enableMouseAim = enableMouseAimToggle.isOn;           
        
        playerOptions.SaveOptions();
        
        keybindTable.ApplyChanges();
        
        //TODO store previous screen settings, so we can reset in UI below
        if(screenSizeUI.ApplyChanges() || screenModeUI.ApplyChanges())
        {
            Debug.Log("UIOptions: Screen settings have changed. " + screenSizeUI.selectedResolution + "  " + screenModeUI.selectedMode);
             Screen.SetResolution(screenSizeUI.selectedResolution.width, screenSizeUI.selectedResolution.height, screenModeUI.selectedMode, screenSizeUI.selectedResolution.refreshRate);
            //TODO show timer UI to reset if something goes wrong
        }
    }
    
    public void ButtonResetDefaults()
    {
        Debug.Log("UIOptions: Resetting to defults");
        inputManager.ResetToDefaults();
        playerOptions.defaultSettings.CopyInto(playerOptions.currentSettings);
      
        LoadOptions();        
    }
    
    public void OnSliderChanged()
    {
        float minArrowSize = minAllowArrowMinSize + (maxAllowedArrowMinSize - minAllowArrowMinSize) * minArrowSizeSlider.value;
        float maxArrowSize = minAllowArrowMaxSize + (maxAllowedArrowMaxSize - minAllowArrowMaxSize) * maxArrowSizeSlider.value;
        float junkSpawn = minJunkSpawn + (maxJunkSpawn - minJunkSpawn) * maxJunkCountSlider.value;
        
        if(maxArrowSize <= minArrowSize)
        {
            maxArrowSize = minArrowSize + 0.1f;
        }
        
        minArrowSizeInput.text = String.Format("{0}", minArrowSize);
        maxArrowSizeInput.text = String.Format("{0}", maxArrowSize);
        maxJunkCountInput.text = String.Format("{0:0}", junkSpawn);
    }
}
