using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UIHelpers 
{
	public static void SetActiveStateIfNot(this GameObject gameObject, bool desiredState)
    {
        if(gameObject.activeSelf != desiredState)
        {
            gameObject.SetActive(desiredState);           
        }
    }
    
    public static void SetButtonNormalColor(this Button button, Color newColor)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = newColor;
        button.colors = cb;
    }
    
    public static void SetToggleNormalColor(this Toggle button, Color newColor)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = newColor;
        button.colors = cb;
    }
    
    public static void SetButtonDisabledColor(this Button button, Color newColor)
    {
        ColorBlock cb = button.colors;
        cb.disabledColor = newColor;
        button.colors = cb;
    }
    
    public static void SetButtonPressedColor(this Button button, Color newColor)
    {
        ColorBlock cb = button.colors;
        cb.pressedColor = newColor;
        button.colors = cb;
    }
    
    public static void SetButtonHighlightedColor(this Button button, Color newColor)
    {
        ColorBlock cb = button.colors;
        cb.highlightedColor = newColor;
        button.colors = cb;
    }
}
