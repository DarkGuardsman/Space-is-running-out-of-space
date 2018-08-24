using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : UIDisplay 
{
    public Canvas canvus;
    
    public UIKeyBindTable keybindTable;
    
    public Slider minArrowSizeSlider;
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
    
    void Awake ()
    {       
        canvus = gameObject.GetComponent<Canvas>();
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
    }
    
    public void ButtonApply()
    {
        SaveSettings();
    }
    
    public void ButtonSave()
    {
        SaveSettings();
    }
    
    public void SaveSettings()
    {
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
    }
    
    public void ButtonResetDefaults()
    {
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
