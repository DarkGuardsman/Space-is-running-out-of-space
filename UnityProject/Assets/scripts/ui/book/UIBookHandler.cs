using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBookHandler : MonoBehaviour 
{
    public int index = 0;
    public GameObject[] pages;
    
    void Awake()
    {
        foreach(GameObject gameObject in pages)
        {
            gameObject.SetActive(false);
        }        
        OnPageChanged(-1);
    }
    
    public void NextPage()
    {
        int prevPage = index;
        
        index++;
        if(index >= pages.Length)
        {
            index = 0;
        }
        
        OnPageChanged(prevPage);
    }
    
    public void PrevPage()
    {
        int prevPage = index;
        
        index--;
        if(index < 0)
        {
            index = pages.Length - 1;
        }
        
        OnPageChanged(prevPage);
    }
    
    public void OnPageChanged(int prevPage)
    {
        if(prevPage >= 0 && prevPage < pages.Length)
        {            
            pages[prevPage].SetActive(false);
        }
        pages[index].SetActive(true);
    }
}
