using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrderList : MonoBehaviour 
{
    public GameObject[] buttons;
    public Vector2 anchor;
    
    public float spacing;
    public float size;
	
	// Update is called once per frame
	void Update () 
    {
        int buttonCount = 0;
        foreach(GameObject gameObject in buttons)
        {
            if(gameObject != null && gameObject.activeInHierarchy && gameObject.activeSelf)
            {
                buttonCount++;
            }
        }
        
		float y = anchor.y + ((buttonCount * size) / 2f);
        foreach(GameObject gameObject in buttons)
        {
            if(gameObject != null && gameObject.activeInHierarchy && gameObject.activeSelf)
            {
                gameObject.transform.localPosition = new Vector3(anchor.x, y, gameObject.transform.localPosition.z);
                y += spacing;
            }
        }
	}
}
