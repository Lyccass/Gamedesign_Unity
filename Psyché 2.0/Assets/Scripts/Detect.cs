using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool triggerActive = false;
    bool hidden = false;




    public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger active!");
            triggerActive = true;

            if (!PlayerController.hidden)
            {
                GameManager.Instance.setGameOver();
            }


        }


    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //   Debug.Log("Trigger active!");
            triggerActive = true;

            if (!PlayerController.hidden)
            {
                GameManager.Instance.setGameOver();
            }


        }

    }
}
