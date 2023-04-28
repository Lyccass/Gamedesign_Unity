using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance = null;
    private bool isGameOver = false;


    public static GameManager Instance
    {
       
        get
        {
            if (GameManager.instance == null)
            {
            
              GameManager.instance = new GameManager();
            }
            return GameManager.instance;
        }
    }

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

}
