using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour
{

    public GameObject stairCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (stairCollider.activeSelf && Input.GetKeyDown(KeyCode.S)){
            stairCollider.SetActive(false);
            PlayerControllerV2.isStaired = false;
            // TODO: checken ob das dauerhaft geht oder obder trigger den collider dann direkt wieder anmacht

            // Evtl erst wieder trigger prüfen wenn spieler danach wieder grounded ist
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When players stairtrigger (wiht special tag) enters this trigger
        if (collision.gameObject.CompareTag("StairCheck"))
        {

            Debug.Log("staitrigger object da!");
            // activate referenced collider! 
            stairCollider.SetActive(true);
            PlayerControllerV2.isStaired = true;

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StairCheck"))
        {

            Debug.Log("staitrigger object weg!");
         //   activate referenced collider! 
          stairCollider.SetActive(false);
            PlayerControllerV2.isStaired = false;

        }
    }

}
