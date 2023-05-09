using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    private static GameManager instance;
    public Vector2 checkpoint;
    public bool restart = false;
    public bool cheats = false;
    public bool isFading = false;
    public static GameManager Instance
    {

        get
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }

    }

    public float Insanity = 0f;
   public bool IsGameOver = false;

   

    public void gameOver()
    {
	if(cheats){
	return;
	}

        IsGameOver = true;
        Debug.Log("GameOver");
        Time.timeScale = 0;
       // restartGame();         ///// BITTE WIEDER LÖSCHEN, SONST INSTANT RESTART



    }


    public void restartGame()
    {
        IsGameOver = false;
        Time.timeScale = 1;
        Insanity = 0f;
        restart = true;


    }

    public void addInsanity(float value)
    {
		if(cheats){
		return;}

        Insanity += value;
       // Debug.Log("Insanity:" + Insanity);
        if (Insanity >= 100)
        {
            gameOver();
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

    public void pause()
    {

    }

}
