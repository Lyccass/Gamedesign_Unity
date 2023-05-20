using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activation : MonoBehaviour
{
    static public bool trigger = false;
    public GameObject sprite;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (trigger == true)
        {
            sprite.GetComponent<SpriteRenderer>().sortingLayerName = "Player";

        }
    }
}

 
