using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : UIDisplay 
{
    public Slider minArrowSizeSlider;
    public Slider maxArrowSizeSlider;
    public Slider maxJunkCountSlider;    
    
    public InputField minArrowSizeInput;
    public InputField maxArrowSizeInput;
    public InputField maxJunkCountInput;  
    
    public float minAllowArrowMaxSize = 1f;
    public float maxAllowedArrowMaxSize = 3f;
    
    public float minAllowArrowMinSize = 0.3f;
    public float maxAllowedArrowMinSize = 1f;
    
    public int minJunkSpawn = 20;
    public int maxJunkSpawn = 1000;
    
    void FixedUpdate()
    {
        minArrowSizeSlider.value = Mathf.Min(1, (playerOptions.arrowMinScale - minAllowArrowMinSize) / (maxAllowedArrowMinSize - minAllowArrowMinSize));
        maxArrowSizeSlider.value = Mathf.Min(1, (playerOptions.arrowMaxScale - minAllowArrowMaxSize) / (maxAllowedArrowMaxSize - minAllowArrowMaxSize));
        maxJunkCountSlider.value = Mathf.Min(1, (((float)playerOptions.maxJunkSpawn) - minJunkSpawn) / (maxJunkSpawn - minJunkSpawn));
	}
    
    public void ButtonSave()
    {
        playerOptions.arrowMinScale = float.Parse(minArrowSizeInput.text);
        playerOptions.arrowMaxScale = float.Parse(maxArrowSizeInput.text);
        playerOptions.maxJunkSpawn = int.Parse(maxJunkCountInput.text);
        playerOptions.SaveOptions();
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
