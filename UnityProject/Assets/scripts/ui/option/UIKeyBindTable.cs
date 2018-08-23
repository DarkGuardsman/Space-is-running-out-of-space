using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyBindTable : MonoBehaviour 
{
    public float[] columnPositions;
    public float rowSpacing = 50f;
    public float rowYStart = 50f;
    
    public GameObject prefabUI;
    
    private List<GameObject> generatedObjects = new List<GameObject>();
    
    public void GenerateEntries()
    {
        PlayerInputManager inputManager = FindObjectOfType<PlayerInputManager>();
        
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
            keyBind.AssignActionInput(action);
            
            index += 1;
        }
    } 

    public void DestroyEntries()
    {
        foreach(GameObject entry in generatedObjects)
        {
            Destroy(entry);
        }        
    }    
    
    GameObject GenerateEntry(int col, int row)
    {
        float x = columnPositions[col];
        float y = rowYStart + row * rowSpacing;
        
        GameObject uiEntry = Instantiate(prefabUI, transform);
        generatedObjects.Add(uiEntry);
        
        uiEntry.transform.localPosition = new Vector3(x, y, 0);
        
        return uiEntry;
    }
	
}
