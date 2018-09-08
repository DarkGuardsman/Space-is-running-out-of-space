using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISelectionObject : MonoBehaviour 
{
    /**
    * Called when the selector is enabled/disabled to allow reseting state
    */
    public virtual void OnSelectorEnabled(UITabSelector selector, bool enabled)
    {
        
    }
    
    /** 
    * Called when selector moves to object.
    * Can disable if another selector needs to run
    *
    * @param forward - was the object selected in a forward motion
    *
    * @return true if should disable selector
    */
    public virtual bool OnSelected(UITabSelector selector, bool forward) //TODO replace forward with more detailed data once arrow keys are implemented
    {
        return false;
    }
    
    /** 
    * Called when selector moves to another object from this one.
    *
    */
    public virtual void OnUnSelected(UITabSelector selector)
    {
      
    }
    
    /** 
    * Called when selector activates the object
    * Can disable if selector should not run. Can be used
    * when entering a menu.
    *
    * @return true if should disable selector
    */
    public virtual bool OnActived(UITabSelector selector)
    {
        return false;
    }
    
    public virtual bool CanBeSelected(UITabSelector selector)
    {
        return gameObject.activeSelf && gameObject.activeInHierarchy; 
    }
}
