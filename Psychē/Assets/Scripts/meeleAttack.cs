using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meeleAttack: MonoBehaviour
{

    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnim.Play("shlash");
        }
        
    }

}
