using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F; // the key code for interaction
    public int keyID; // a unique ID for each key
    private bool canInteract = false;

    private void Update()
    {
        if ( canInteract && Input.GetKeyDown(interactKey))
        {
            // add the key to the player's inventory
            Inventory.instance.AddKey(keyID);

            Debug.Log("Key Collected");

            // remove the key from the scene
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
        canInteract = true;
        Debug.Log("Maybe Key");
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Key no more");
        }
    }
}
