using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    //public Transform top; // transform of the top of the ladder
    //public Transform bottom; // transform of the bottom of the ladder

    public float up;
    public float down;

    //public bool onGround;

    private bool contact;
    private bool canTeleport; // flag to indicate if the player can teleport
    private GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("1111 ladder maybe");
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
            canTeleport = true;
            contact = true;
            Debug.Log("2222 ladder maybe");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = false;
            contact = false;
        }
    }

    private void Update()
    {
        if (contact && PlayerControllerV2.sleeping) canTeleport = false;
        if (contact && !PlayerControllerV2.sleeping && !canTeleport) canTeleport = true;


        if (canTeleport && Input.GetKeyDown(KeyCode.F) && PlayerControllerV2.isGrounded)  //&& PlayerControllerV2.isGrounded
        {
            GameManager.Instance.isFading = true;
            fadeCanvas.fadeSpeed = 0.001f;
        }

        if (canTeleport && fadeCanvas.alpha >= 1)  //&& PlayerControllerV2.isGrounded
        {
            ladderMove();


        }


    }

    private void ladderMove()
    {
        // teleport the player to the opposite end of the ladder
        Vector3 playerPos = Player.transform.position;
        if (playerPos.y > transform.position.y) // player is at the top of the ladder
        {
            //Player.transform.position = bottom.position;
            Player.transform.position += new Vector3(0, -down, 0);
            Debug.Log("ladder down");
            Time.timeScale = 0.001f;

        }
        else // player is at the bottom of the ladder
        {
            //Player.transform.position = top.position;
            Player.transform.position += new Vector3(0, up, 0);
            Debug.Log("ladder up");
            Time.timeScale = 0.001f;
        }
    }
}
