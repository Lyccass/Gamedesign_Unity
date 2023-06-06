using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // singleton instance
    //  public GameObject UIInventory;
    // Get this from the UI
    public List<GameObject> inventoryImages;
    private List<int> keyIDs = new List<int>(); // list of key IDs the player has collected
    private int count = 0;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey(int keyID, Sprite sprite)
    {
        if (count >= inventoryImages.Capacity) return;
        // add the key ID to the list if it's not already there
        if (!keyIDs.Contains(keyID))
        {
            keyIDs.Add(keyID);
            // -1, da die nummerierung der keys anders ist
            inventoryImages[keyID-1].SetActive(true);
            inventoryImages[keyID-1].GetComponent<Image>().sprite = sprite;

            // TODO: Enable removing! (in lock oder so) : set active false und sprite = 0, zum recyceln 
            
            // TODO: Add Image to Inventory
            // Use given sprite;
             count++;
        }
    }

    public void RemoveKey(int keyID) {
        keyIDs.Remove(keyID);
        inventoryImages[keyID - 1].SetActive(false);
        inventoryImages[keyID - 1].GetComponent<Image>().sprite = null;

    }

    public bool HasKey(int keyID)
    {
        return keyIDs.Contains(keyID);
    }
 

}


