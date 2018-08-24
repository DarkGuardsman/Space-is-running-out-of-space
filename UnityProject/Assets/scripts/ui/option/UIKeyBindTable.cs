﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyBindTable : MonoBehaviour 
{
    public float[] columnPositions;
    public float rowSpacing = 50f;
    public float rowYStart = 50f;
    
    public GameObject prefabUI;
    public PlayerInputManager inputManager;
    
    private List<UIKeyBind> generatedObjects = new List<UIKeyBind>();
    
    void Start()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
    
    public void GenerateEntries()
    {        
        int entries = inputManager.actionList.Count;
        int entryPerCol = entries / columnPositions.Length;
        if(entries % 2 == 1)
        {
            entryPerCol += 1;
        }
        
        int index = 0;
        foreach(InputAction action in inputManager.actionList)
        {
            int col = index / entryPerCol;
            int row = index % entryPerCol;
            GameObject entry = GenerateEntry(col, row);
            
            UIKeyBind keyBind = entry.GetComponent<UIKeyBind>();
            generatedObjects.Add(keyBind);
            keyBind.AssignActionInput(action);
            
            index += 1;
        }
    } 
    
    public void ApplyChanges()
    {
        bool hasChanged = false;
        foreach(UIKeyBind keybind in generatedObjects)
        {
            if(keybind.ApplyChanges())
            {
                hasChanged = true;
            }
        }
        if(hasChanged)
        {
            inputManager.SaveToDisc();
        }
    }

    public void DestroyEntries()
    {
        foreach(UIKeyBind keybind in generatedObjects)
        {
            Destroy(keybind.gameObject);
        }        
    }    
    
    GameObject GenerateEntry(int col, int row)
    {
        float x = columnPositions[col];
        float y = rowYStart + row * rowSpacing;
        
        GameObject uiEntry = Instantiate(prefabUI, transform);
        
        uiEntry.transform.localPosition = new Vector3(x, y, 0);
        
        return uiEntry;
    }
	
}
