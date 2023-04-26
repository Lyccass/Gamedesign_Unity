using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    private static GameManager instance;
    public static GameManager Instance
    {

        get
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }

    }

    public bool IsGameOver = false;

   

    public void setGameOver()
    {
        IsGameOver = true;
        Debug.Log("GameOver");
    }


    public void addInsanity(float value)
    {


    
      

        panik.Insanity += 0.5f;


        Debug.Log("Insanity:" + panik.Insanity);
        if (panik.Insanity >= 100)
        {
            setGameOver();
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

}
