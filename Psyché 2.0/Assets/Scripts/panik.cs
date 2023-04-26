using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panik : MonoBehaviour
{


    public SpriteRenderer spriteRenderer;
    static public float Insanity = 0f;

    private void Awake()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()

    {
       
        ChangeSprite();
    }

    void ChangeSprite()
    {
        transform.localScale = new Vector3(26f + (Insanity/10 ), 1f + (Insanity/10 ), 1f + (Insanity/10 ));
    }
}

  
