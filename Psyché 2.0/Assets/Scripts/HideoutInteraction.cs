using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutInteraction : MonoBehaviour
{

    bool triggerActive = false;

    public GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

            if (triggerActive && Input.GetKeyDown(KeyCode.F))
            {
            // Switch hidden
            PlayerControllerV2.hidden = !PlayerControllerV2.hidden;

            }

    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //  Debug.Log("Trigger active!");

            triggerActive = true;
        
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Trigger deactive!");

            triggerActive = false;
                              
        }
    }
}
