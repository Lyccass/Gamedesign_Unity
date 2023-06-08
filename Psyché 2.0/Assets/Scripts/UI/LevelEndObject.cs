using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEndObject : MonoBehaviour
{
    // Start is called before the first frame update
    bool triggerActive = false;
    private bool contact;
    public string nextSceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contact && PlayerControllerV2.sleeping) triggerActive = false;
        if (contact && !PlayerControllerV2.sleeping && !triggerActive) triggerActive = true;

        if (triggerActive && Input.GetKeyDown(KeyCode.F) && PlayerControllerV2.isGrounded)
        {
            GameManager.Instance.isFading = true;

        }

        if (triggerActive && fadeCanvas.alpha >= 1)  //&& PlayerControllerV2.isGrounded
        {
            Debug.Log("Entering next level!");
            // Skip to next levek
            SceneManager.LoadScene(nextSceneName);
            // Set insanity to 20
            GameManager.Instance.Insanity = 20f;

        }

    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            triggerActive = true;
            contact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerActive = false;
            contact = false;
        }
    }
}
