using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour 
{
    private PlayerOptions playerOptions;
    private GameController gameController;
    
    protected GameController GetGameController()
    {
        if(gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
        return gameController;
    }
    
    protected PlayerOptions GetPlayerOptions()
    {
        if(playerOptions == null)
        {
            playerOptions = FindObjectOfType<PlayerOptions>();
        }
        return playerOptions;
    }
    
    protected bool IsInView(Vector3 point)
    {
        return GetGameController().IsInView(point);
    }
    
    protected bool AreEffectsEnabled()
    {
        return GetPlayerOptions().currentSettings.enableEffects;
    }
}
