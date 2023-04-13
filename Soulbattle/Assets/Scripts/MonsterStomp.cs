using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weakpoint")

        {
            Debug.Log("Destroyed the object" + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

}
