using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerZoom : MonoBehaviour 
{    
    public float minZoom = 5;
    public float maxZoom = 20;
    
    private GameController gameController;
    private PlayerOptions playerOptions;
    private CinemachineVirtualCamera cinemachineCamera;
    
    // Use this for initialization
	void Start () 
    {
		gameController = FindObjectOfType<GameController>();
        playerOptions = FindObjectOfType<PlayerOptions>();
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Get input
        float zoom = Input.GetAxis("Zoom");
        
        //Do zoom
        cinemachineCamera.m_Lens.OrthographicSize -= playerOptions.zoomSpeed * zoom;
        
        //Clamp
        if(cinemachineCamera.m_Lens.OrthographicSize > maxZoom)
        {
            cinemachineCamera.m_Lens.OrthographicSize = maxZoom;
        }
        else if(cinemachineCamera.m_Lens.OrthographicSize < minZoom)
        {
            cinemachineCamera.m_Lens.OrthographicSize = minZoom;
        }
	}
}
