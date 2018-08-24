using System.Collections;
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
        int entries = inputManager.getInputActions().NumberOfActions();
        int entryPerCol = entries / columnPositions.Length;
        if(entries % 2 == 1)
        {
            entryPerCol += 1;
        }
        
        //Loop over all actions
        for(int index = 0; index < entries; index++)
        {
            int col = index / entryPerCol;
            int row = index % entryPerCol;
            GameObject entry = GenerateEntry(col, row);
            
            UIKeyBind keyBind = entry.GetComponent<UIKeyBind>();
            generatedObjects.Add(keyBind);
            
            keyBind.AssignActionInput(index);
        }
    } 
    
    public void ApplyChanges()
    {
        Debug.Log("UIKeyBindTable: Checking for keybind changes...");
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
        else
        {
            Debug.Log("UIKeyBindTable: No changes to apply");
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
