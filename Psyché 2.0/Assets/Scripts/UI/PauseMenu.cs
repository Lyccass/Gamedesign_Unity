using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    static public bool isPaused = true;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && isPaused)
        {
            isPaused = false;
        }else if (Input.GetButtonDown("Cancel") && !isPaused)
        {
            isPaused = true;
        }


        if (isPaused)
        {
            resumeGame();

        }else
        {
            PauseGame();
        }     
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        
    }

    public void resumeGame() 
    
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
       
    }
}
