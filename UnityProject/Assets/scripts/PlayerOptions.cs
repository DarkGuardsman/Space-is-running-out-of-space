using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour 
{
	public float arrowMinScale;
    public float arrowMaxScale;
    public int maxJunkSpawn;
    
    void Awake()
    {
        LoadOptions();
    }
    
	public void LoadOptions () 
    {
        Debug.Log("PlayerOptions: Loading player prefs");
        if(PlayerPrefs.HasKey("arrowMinScale"))
        {
            arrowMinScale = PlayerPrefs.GetFloat("arrowMinScale");
            Debug.Log("PlayerOptions: Arrow Min -> " + arrowMinScale);
        }
        if(PlayerPrefs.HasKey("arrowMaxScale"))
        {
            arrowMaxScale = PlayerPrefs.GetFloat("arrowMaxScale");
            Debug.Log("PlayerOptions: Arrow Max -> " + arrowMaxScale);
        }
        if(PlayerPrefs.HasKey("maxJunkSpawn"))
        {
            maxJunkSpawn = PlayerPrefs.GetInt("maxJunkSpawn");
            Debug.Log("PlayerOptions: Junk Count -> " + maxJunkSpawn);
        }
	}	
	
	public void SaveOptions () 
    {
        Debug.Log("PlayerOptions: Saving player prefs");
        PlayerPrefs.SetFloat("arrowMinScale", arrowMinScale);
        PlayerPrefs.SetFloat("arrowMaxScale", arrowMaxScale);
        PlayerPrefs.SetInt("maxJunkSpawn", maxJunkSpawn);
	}
}
