using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutInteraction : MonoBehaviour
{

    bool triggerActive = false;
    private bool contact;

    public GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contact && PlayerControllerV2.sleeping) triggerActive = false;
        if (contact && !PlayerControllerV2.sleeping && !triggerActive) triggerActive = true;

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
            contact = true;
        
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Trigger deactive!");

            triggerActive = false;
            contact = false;
                              
        }
    }
}
