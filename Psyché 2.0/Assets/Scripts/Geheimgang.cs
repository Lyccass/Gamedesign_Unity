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
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            handle = true;
            canInteract = false;
            secretDoor.SetActive(true);
            ClosedDoorColider.SetActive(false);
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
