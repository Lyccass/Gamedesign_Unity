using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public static bool InRange = false;
    public GameObject Dialoge;
   

    void Update()
    {
        
    }

    public void turnOnDialoge(bool InRange)
    {
        Dialoge.SetActive(InRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InRange = true;
            
        }

      
    }
}
