using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelpers 
{
	public static void SetActiveStateIfNot(this GameObject gameObject, bool desiredState)
    {
        if(gameObject.activeSelf != desiredState)
        {
            gameObject.SetActive(desiredState);           
        }
    }
}
