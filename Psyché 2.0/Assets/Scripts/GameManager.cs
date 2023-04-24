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

    public float Insanity = 0f;

    public void setGameOver()
    {
        IsGameOver = true;
        Debug.Log("GameOver");
    }


    public void addInsanity(float value)
    {

        Insanity += value;
        Debug.Log("Insanity:" + Insanity);
        if (Insanity >= 100)
        {
            setGameOver();
        }
    }

    public void decrementInsanity(float value)
    {

        this.Insanity -= value;

        if (Insanity < 0)
        {
            Insanity = 0;
        }
        Debug.Log("Insanity:" + Insanity);

    }

}
