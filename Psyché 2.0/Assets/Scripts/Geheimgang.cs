using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geheimgang : MonoBehaviour
{
    private bool canInteract = false;
    private Sprite sprite;
    private bool handle = false;
    public GameObject secretDoor;
    public GameObject ClosedDoorColider;
    public AudioSource door;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F) && handle == false)
        {
            door.Play();
            handle = true;
            canInteract = false;
            secretDoor.SetActive(true);
            ClosedDoorColider.SetActive(false);
        }

        if (canInteract && Input.GetKeyDown(KeyCode.F) && handle == true)
        {
            door.Play();
            handle = false;
            canInteract = true;
            secretDoor.SetActive(false);
            ClosedDoorColider.SetActive(true);
        }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("handle is close");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Kein handle in der nahe");
        }
    }
}
