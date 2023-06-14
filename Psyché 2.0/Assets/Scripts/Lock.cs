using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public int lockID; // a unique ID for each lock
    private bool canInteract = false;
    private bool contact;


    private void Update()
    {
        if (contact && PlayerControllerV2.sleeping) canInteract = false;
        if (contact && !PlayerControllerV2.sleeping && !canInteract) canInteract = true;

        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            // check if the player has all three keys
            if (Inventory.instance.HasKey(1) && Inventory.instance.HasKey(2) && Inventory.instance.HasKey(3))
            {
                // remove the lock from the scene
                Debug.Log("Lock Opened");
                Destroy(gameObject);

                // check if all locks have been removed
                bool allLocksRemoved = true;
                foreach (Lock l in FindObjectsOfType<Lock>())
                {
                    if (l.gameObject.activeSelf)
                    {
                        allLocksRemoved = false;
                        break;
                    }
                }

                // open the door if all locks have been removed
                if (allLocksRemoved)
                {
                    OpenDoor();  // does nothing oder so
                }
            }
            else
            {
                // remove the lock if the player has the specific key for it
                if (Inventory.instance.HasKey(lockID))
                {
                    Inventory.instance.RemoveKey(lockID);
                    Debug.Log("Lock Opened");
                    
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
            contact = true;
            Debug.Log("Maybe open Lock");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            contact = false;
            Debug.Log("open Lock no more");
        }
    }

    private void OpenDoor()
    {
        // ?!? put your code here to open the door
    }
}
