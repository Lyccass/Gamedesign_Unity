using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //   Debug.Log("OOUTER TRIGGEr");
            if (!PlayerControllerV2.hidden)
            {
                PlayerControllerV2.warning = true;
            }
            else
            {
                PlayerControllerV2.warning = false;
            }
            
            // if hidden aber trtz warning, lass so! weil das wurde dann von detect gesetzt!
            // sonst if hidden false

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Debug.Log("OOUTER TRIGGEr off");
            PlayerControllerV2.warning = false;
        }
    }
}
