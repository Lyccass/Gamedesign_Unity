using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detect script for Guards
public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
    bool hidden = false;




    public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.CompareTag("Player"))
        {

            if (!PlayerControllerV2.hidden)
            {
                GameManager.Instance.gameOver();
            }


        }


    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (!PlayerControllerV2.hidden)
            {
                GameManager.Instance.gameOver();
            }


        }

    }
}
