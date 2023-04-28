using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // singleton instance

    private List<int> keyIDs = new List<int>(); // list of key IDs the player has collected

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

    public void AddKey(int keyID)
    {
        // add the key ID to the list if it's not already there
        if (!keyIDs.Contains(keyID))
        {
            keyIDs.Add(keyID);
        }
    }

    public bool HasKey(int keyID)
    {
        return keyIDs.Contains(keyID);
    }

}
