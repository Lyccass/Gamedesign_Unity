using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
   public GameObject DeathPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            turnOnDeathScreen(true);
        }
        else
        {
            turnOnDeathScreen(false);
        }
    }

   public void turnOnDeathScreen(bool state)
    {
        DeathPanel.SetActive(state);
    }
}

