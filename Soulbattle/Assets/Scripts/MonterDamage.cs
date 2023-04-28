using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterDamage : MonoBehaviour
{

    public int damage;
    public Playerhealth playerHealth;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
