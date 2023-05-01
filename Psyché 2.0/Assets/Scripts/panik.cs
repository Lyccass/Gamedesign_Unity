using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panik : MonoBehaviour
{


    public SpriteRenderer spriteRenderer;
    public float Insanity = 0f;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {

        Insanity = GameManager.Instance.Insanity;

        ChangeSprite();
    }

    void ChangeSprite()
    {
        transform.localScale = new Vector3(26f + (Insanity / 25), 1f + (Insanity / 25), 1f + (Insanity / 25));
    }
}



  
