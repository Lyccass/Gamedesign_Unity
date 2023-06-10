using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
   public GameObject DeathPanel;
    private bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver && !check)
        {
            GameManager.Instance.isFading = true;
            fadeCanvas.fadeSpeed = 0.001f;
        }
        if (GameManager.Instance.IsGameOver && fadeCanvas.alpha >= 1)  //&& PlayerControllerV2.isGrounded
        {
            turnOnDeathScreen(true);
            check = true;
        }
        if(!GameManager.Instance.IsGameOver)
        {
            turnOnDeathScreen(false);
            check = false;
        }
    }

   public void turnOnDeathScreen(bool state)
    {
        DeathPanel.SetActive(state);
    }
}

