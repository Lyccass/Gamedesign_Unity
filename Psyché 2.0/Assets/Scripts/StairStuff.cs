using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairStuff : MonoBehaviour
{

   // public GameObject stairCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player falling/ inair / not grounded, activate staircollider(referenced maybe)! 
        if (!PlayerControllerV2.isGrounded)
        {
            GameObject stairtrigger = collision.gameObject;
            if(stairtrigger != null)
            {
                Debug.Log("staitrigger object da!");
                //TODO: Fix access , cant access scrpit
                
               // stairtrigger.GetComponent<StairTrigger>().toggleStairCollider(true);
            }
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
