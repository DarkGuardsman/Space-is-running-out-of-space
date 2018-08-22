using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionViewSwitcher : MonoBehaviour 
{
	public GameObject generalPanel;
    public GameObject controlsPanel;
    public GameObject graphicsPanel;
    public GameObject soundPanel;
    
    public bool showGeneral = true;
    public bool showControl = false;
    public bool showGraphic = false;
    public bool showSound = false;
    
    void Update()
    {
        generalPanel.SetActiveStateIfNot(showGeneral);
        controlsPanel.SetActiveStateIfNot(showControl);
        graphicsPanel.SetActiveStateIfNot(showGraphic);
        soundPanel.SetActiveStateIfNot(showSound);
    }
    
    void HideAll()
    {
        showGeneral = false;
        showControl = false;
        showGraphic = false;
        showSound = false;
    }
    
    public void ShowGeneral()
    {
        HideAll();
        showGeneral = true;
        Debug.Log("OptionsUI: Switch to general view");
    }
    
    public void ShowControl()
    {
        HideAll();
        showControl = true;
        Debug.Log("OptionsUI: Switch to controls view");
    }
    
    public void ShowGraphic()
    {
        HideAll();
        showGraphic = true;
        Debug.Log("OptionsUI: Switch to graphics view");
    }
    
    public void ShowSound()
    {
        HideAll();
        showSound = true;
        Debug.Log("OptionsUI: Switch to sound view");
    }
}
