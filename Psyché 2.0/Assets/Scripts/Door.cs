using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int[] connectedLockIDs; // an array of the IDs of the locks connected to this door

    private void Start()
    {
        // disable the door if any connected locks are still in the scene
        foreach (int lockID in connectedLockIDs)
        {
            // Searches all locks in the scene by name
            if (GameObject.Find("Lock" + lockID.ToString()) != null)
            {
                
               // gameObject.SetActive(false);
                return;
            }
        }

        // all connected locks have been removed, so enable the door
        //gameObject.SetActive(true);
    }

    private void Update()
    {
        // check if all connected locks have been removed
        foreach (int lockID in connectedLockIDs)
        {
            if (GameObject.Find("Lock" + lockID.ToString()) != null)
            {
              //  Debug.Log("Lock" + lockID.ToString() + " exists");
                // this lock still exists, so the door should be disabled
                //gameObject.SetActive(false);
                return;
            }
        
        }

        // all connected locks have been removed, so enable the door
        //gameObject.SetActive(true);
        Destroy(gameObject);
    }

}
