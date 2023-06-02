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
            Debug.Log("OOUTER TRIGGEr");
            PlayerControllerV2.warning = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("OOUTER TRIGGEr off");
            PlayerControllerV2.warning = false;
        }
    }
}
