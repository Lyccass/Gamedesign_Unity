using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    private static GameManager instance;
    public Vector2 checkpoint;
    public static GameManager Instance
    {

        get
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }

    }

   public bool IsGameOver = false;

   

    public void gameOver()
    {
        IsGameOver = true;
        Debug.Log("GameOver");
     
        panik.Insanity = 0f;
        Time.timeScale = 0;
        

    }


    public void restartGame()
    {
        IsGameOver = false;
        // TODO: set player to current checkpoint!
        Time.timeScale = 1;

    }


    public void setCurrentCheckpoint(Vector2 position)
    {
        checkpoint = position;
    }
    public void addInsanity(float value)
    {


    
      

        panik.Insanity += 10f;


        Debug.Log("Insanity:" + panik.Insanity);
        if (panik.Insanity >= 100)
        {
            gameOver();
        }
    }

    public void decrementInsanity(float value)
    {

        panik.Insanity -= value;
        if (panik.Insanity < 0)
        {
            panik.Insanity = 0;
        }

    }

    public void pause()
    {

    }

}
