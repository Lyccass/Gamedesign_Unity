using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{

    bool hasBeenActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasBeenActivated)
        {
             GameManager.Instance.checkpoint = gameObject.transform.position;
            hasBeenActivated = true;        
        }
       
    }
}
