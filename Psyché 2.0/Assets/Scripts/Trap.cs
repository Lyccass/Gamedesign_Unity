using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trigger;
       public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.CompareTag("Player"))
        {

            GameManager.Instance.Insanity = GameManager.Instance.Insanity + trigger;
            activation.trigger = true;


        }
    }
        }