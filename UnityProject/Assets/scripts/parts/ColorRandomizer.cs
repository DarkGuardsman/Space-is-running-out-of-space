using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour 
{
    public SpriteRenderer[] spriteRenderers;
    
    public int red = 2;
    public int green = 2;
    public int blue = 2;
    public int alpha = 2;
    
	// Use this for initialization
	void Start () 
    {
		foreach (SpriteRenderer m_SpriteRenderer in spriteRenderers)
        {
            m_SpriteRenderer.color = m_SpriteRenderer.color + new Color(getRng(red), getRng(green), getRng(blue), getRng(alpha));
        }
	}
    
    float getRng(int randomNumber)
    {
        float r = randomNumber / 255f;
        if(r == 0)
        {
            return 0;
        }
        return Random.Range(0, r);
    }
}
