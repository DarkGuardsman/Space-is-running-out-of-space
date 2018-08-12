using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplay : MonoBehaviour {

	public GameController gameController;
    
	// Use this for initialization
	public virtual void Start () 
    {
		gameController = FindObjectOfType<GameController>();
	} 
}
