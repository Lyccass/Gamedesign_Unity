using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEndObject : MonoBehaviour
{
    // Start is called before the first frame update
    bool triggerActive = false;
    public string nextSceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.F))
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }
}
