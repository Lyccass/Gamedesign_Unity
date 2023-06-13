using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyID; // a unique ID for each key
    private bool canInteract = false;
    private bool contact;
    private Sprite sprite;


    private void Start()
    {
        SpriteRenderer sr =  gameObject.GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
    }
    private void Update()
    {
        if (contact && PlayerControllerV2.sleeping) canInteract = false;
        if (contact && !PlayerControllerV2.sleeping && !canInteract) canInteract = true;

        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            // add the key to the player's inventory
            Inventory.instance.AddKey(keyID,sprite);

            Debug.Log("Key Collected");
            // Remove the key from the scene
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
            contact = true;
            
        Debug.Log("Maybe Key");
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            contact = false;
            Debug.Log("Key no more");
        }
    }
}
